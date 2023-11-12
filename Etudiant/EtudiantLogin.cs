using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfaces;
using Beans;
namespace Etudiant
{
    public partial class EtudiantLogin : Form
    {
        public Beans.Etudiant student { get; set; }
        private string email1;
        public string email { get { return email1; } set { email1 = value; } }
        /*
        public delegate void UpdateDelegate (object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }
           
        */
        public EtudiantLogin()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Instances for accessing Authentication interfaces
        AuthEtu AuthEtudiant = (AuthEtu)Activator.GetObject(typeof(AuthEtu), "tcp://localhost:8086/AuthEtu");
        AuthEns AuthEn = (AuthEns)Activator.GetObject(typeof(AuthEns), "tcp://localhost:8086/AuthEns");
        // List to hold online teachers
        List<Beans.Enseignant> onlineTeachers =new List<Beans.Enseignant>();
        

        public void Key(TextBox tx)
        {
            tx.SelectAll();
            tx.Focus();
        }

        // Method to handle user login and session registration
        public void Save()
        {

            if (EmailBox.Text == "" || PasswordBox.Text == "" || comboBox.SelectedIndex == -1)
            {
                if (EmailBox.Text == "")
                {
                    Key(EmailBox);
                }
                else if (PasswordBox.Text == "")
                {
                    Key(PasswordBox);
                }
                else if (comboBox.SelectedIndex == -1) 
                {
                    MessageBox.Show("Please select a seance value");
                }
            }
            else
            {
                // Authenticate student credentials
                Boolean b = AuthEtudiant.AuthentificationEtudiant(EmailBox.Text, PasswordBox.Text);


                if (b)
                {
                    // Retrieve student and related module information
                    Beans.Etudiant etudiant = AuthEtudiant.getStudent(EmailBox.Text, PasswordBox.Text);
                    ICollection<Beans.ModuleEtudie> EtudiantModules = etudiant.ModuleEtudies;
                    List<Beans.DateTimes> DatesTable = new List<Beans.DateTimes>();
                    
                    // Extract selected teacher's information from the ComboBox
                    string Name = ((Enseignant)comboBox.SelectedItem).nom.ToString();
                    string prenom = ((Enseignant)comboBox.SelectedItem).prenom.ToString();
                    string moduleAbv = ((Enseignant)comboBox.SelectedItem).module2.abrv_module.ToString();
                    int id_enseignant = Int16.Parse(((Enseignant)comboBox.SelectedItem).id_enseignant.ToString());
                    string ID_mdl = comboBox.SelectedValue.ToString();
                    int id_module= Int16.Parse(ID_mdl);
                    int nbr_abs=0;
                    Boolean exist = false;
                    Boolean allowed = false;
                    DateTime now =new DateTime();
                    DatesTable = AuthEn.getDateTimeTable(id_enseignant);

                    // Iterate through the student's enrolled modules
                    foreach (ModuleEtudie md in EtudiantModules) 
                        {
                            
                            if (md.id_module == id_module) 
                            {
                                exist = true;
                                 nbr_abs = md.nbr_absence;
                            }
                        }
                    DateTimes dt =new DateTimes();

                    // Iterate through the session timings
                    foreach (DateTimes dts in DatesTable) 
                    {
                         TimeSpan ts = now - dts.Date;
                         if (ts.TotalMinutes < 10) 
                         {
                             dt = dts;
                             allowed = true;
                         }
                    }

                    // Evaluate access conditions and handle accordingly
                    if (allowed && exist && nbr_abs < 3) 
                    {

                        // Set student information, connect to the session, and display the main form
                        student = new Beans.Etudiant();
                        student = etudiant;
                        email = etudiant.email;
                        AuthEtudiant.connectStudent(etudiant.id_etudiant,id_enseignant);
                        int id_seance =AuthEtudiant.getCurrentSession(id_enseignant);
                        AuthEtudiant.RegisterToSession(id_seance,etudiant.id_etudiant);
                         this.Hide();
                         EtudiantMain main = new EtudiantMain(etudiant, dt, id_seance);
                         main.ShowDialog();
                    }else if(!exist)
                    {
                        MessageBox.Show("You can't entre to this TP");
                    }
                    else if (exist && nbr_abs >= 3) 
                    {
                        MessageBox.Show("you are excluded from this module");
                    }
                    else if (!allowed && exist && nbr_abs >= 3)
                    {
                        MessageBox.Show("you're late mate");
                    }  
                }
                else
                {
                    MessageBox.Show("Email or password are incorrect");
                }
            }
        }
        
        // Method triggered upon form load
        private void EtudiantLogin_Load(object sender, EventArgs e)
        {
            
            onlineTeachers=AuthEn.getConnectTrachers();
            comboBox.DataSource = onlineTeachers;
            comboBox.ValueMember = "id_module";           
            comboBox.DisplayMember ="module";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
         // Method for formatting displayed text in the ComboBox
        private void comboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            int id_enseignant=((Beans.Enseignant)e.ListItem).id_enseignant;
            string lastname = ((Beans.Enseignant)e.ListItem).nom;
            string firstname = ((Beans.Enseignant)e.ListItem).prenom;
            string module = ((Beans.Enseignant)e.ListItem).module2.abrv_module;
            e.Value = "" + module + "( " + lastname + " " + firstname + " )";
        }
        // Event handler for the login button click
        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }

        

    }
}
