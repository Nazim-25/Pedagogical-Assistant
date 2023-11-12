using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Interfaces;

namespace Enseignant
{
     // Class representing the EnseignantLogin form
    public partial class EnseignantLogin : Form
    {
        
        

        public EnseignantLogin()
        {
            InitializeComponent();
            
        }
        
        public EnseignantLogin(int i)
        {
 
        }
        
         public Beans.Enseignant enseignant { get; set;}
         private string email1;
         public string email { get { return email1; } set { email1 = value; } }
        
        // Instance of AuthEns to communicate with the server
        AuthEns AuthE = (AuthEns)Activator.GetObject(typeof(AuthEns), "tcp://localhost:8086/AuthEns");

        // Click event handler for exiting the application
        private void Click_Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // Method to select all text in a TextBox and focus on it
           public void Key(TextBox tx)
        {
            tx.SelectAll();
            tx.Focus();
        }

        // Method to handle the login process
        public void Save() {
             // Check for empty email or password fields
            if (EnsEmailBox.Text == "" || EnsPasswordBox.Text == "")
            {
                // If either field is empty, highlight the respective field
                if (EnsEmailBox.Text == "")
                {
                    Key(EnsEmailBox);
                }
                else
                {
                    Key(EnsPasswordBox);
                }
            }
            else
            {
                // Authenticate with the provided email and password
                Boolean b = AuthE.AuthetificationEnseignant(EnsEmailBox.Text, EnsPasswordBox.Text);
                

                if (b)
                {
                    // If authentication is successful, retrieve Enseignant object and perform additional actions
                    Beans.Enseignant e = AuthE.getTeacher(EnsEmailBox.Text, EnsPasswordBox.Text);
                    enseignant = new Beans.Enseignant();
                    enseignant = e;
                    email = e.email;
                    // Generate a random port number and get the local IP address
                    int _min =2000;
                    int _max =6000;
                    Random _rdm = new Random();
                    int port =_rdm.Next(_min, _max);
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    IPAddress localAddr = IPAddress.Parse(myIP);

                    // Connect the teacher and start a session
                    AuthE.ConnectTracher(e, port,localAddr);
                    AuthE.startSeance(e.id_enseignant,e.module2.id_module);
                    // Hide the current form and open the EnseignantMain form
                    this.Hide();
                    EnseignantMain main = new EnseignantMain(e, port, localAddr);
                main.ShowDialog();
                }
                else 
                {
                     // If authentication fails, show an error message
                    MessageBox.Show("Email or password are incorrect");
                }
            }
        }

        private void EnsEmailBox_TextChanged(object sender, EventArgs e)
        {
            EnsEmailBox.BackColor = Color.White;
        }

        

        private void EnsPasswordBox_TextChanged(object sender, EventArgs e)
        {
            EnsPasswordBox.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void EnseignantLogin_Load(object sender, EventArgs e)
        {

        }

       


    }
    }

