using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Etudiant.Classes;
using Interfaces;
using Beans;
namespace Etudiant
{
    public partial class EtudiantMain : Form
    {
        private  string jump = "\r\n";
        private string LOCALHOST="127.0.0.1"; // Localhost IP
        private  int DEFAULT_PORT ; // Default port for connections
        int id_seance;

        private IPAddress _serverIpAddress; // Server IP address
        private int _port;
        private TcpClient _client; // TCP client for communication
        string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\"; // Default file path
        Boolean selectedPath = false; // Flag for selected path
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

        AuthEtu AuthEtudiant = (AuthEtu)Activator.GetObject(typeof(AuthEtu), "tcp://localhost:8086/AuthEtu");
        Beans.Etudiant etudiant = new Beans.Etudiant();
        DateTimes dt = new DateTimes(); // Session timing information
        public EtudiantMain()
        {
            InitializeComponent();
        }

        // Constructor to initialize the main form for a student session
        public EtudiantMain(Beans.Etudiant e, DateTimes dt, int id_seance)
        {
            
            InitializeComponent();
            etudiant = e;
            this.id_seance = id_seance;
            this.dt = dt;
            _port = dt.port;
            _serverIpAddress = dt.localAddr;
            ConnectToServer();
            SendFile_btn.Enabled = false;
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();  
           
           
        }

        private void EtudiantMain_Load(object sender, EventArgs e)
        {
           
        }

      

        // Establish a connection to the server 
        private void ConnectToServer()
        {
            try
            {


                _client = new TcpClient(_serverIpAddress.ToString(), _port); // Connect to the server
                Thread t = new Thread(ProcessClientTransactions); // Initialize a thread to manage client-server communication
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start(_client);



            }
            catch (Exception ex)
            {
                ConversationBox.Text += jump + "Problem connecting to server."; // Display connection issues in the conversation box
                ConversationBox.Text += jump + ex.ToString();
            }
        }
        
        // Method to send a message to everyone
        private void Send_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_client.Connected)
                {
                    // Send the message to the server
                    StreamWriter writer = new StreamWriter(_client.GetStream());
                    writer.WriteLine("From " + etudiant.nom + " " + etudiant.prenom + " >" + MessageBox_inp.Text + ":Chat");
                    writer.Flush();
                    ConversationBox.Text += jump + "Me > " + MessageBox_inp.Text; // Display sent message in the conversation box
                    MessageBox_inp.Text = string.Empty; // Clear the input message box
                }

            }
            catch (Exception ex)
            {
                ConversationBox.Text += jump + "Problem sending message to Everyone..."; // Display send message issues in the conversation box
                ConversationBox.Text += jump + ex.ToString();
            }
        }

        // Handles the client's communication transactions with the server.
        private void ProcessClientTransactions(object tcpClient)
        {
            TcpClient client = (TcpClient)tcpClient;
            string input=string.Empty ;
            StreamReader reader = null;
            StreamWriter writer = null;


            try
            {
                // Initialize the reader and writer for the client's communication
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());

               // Send connection acknowledgment to the server
                writer.WriteLine(etudiant.nom+" "+etudiant.prenom+" Connected !. :Chat");
                writer.Flush();

                while (client.Connected)
                {
                    string[] data=null;
                    string pdf = "Pdf", chat = "Chat";
                    input = reader.ReadLine(); 
                    data = input.Split(':');
                   
                    if (input == null)
                    {
                       // DisconnectFromServer();
                    }
                    else if (data[1].Equals(chat)) 
                    {
                        // Append chat message to the conversation box
                        ConversationBox.Append(stb => stb.Text += jump + "" + data[0]);

                    }
                    else if (data[1].Equals(pdf))
                     {
                         if (!selectedPath)
                         {

                            // Check for the file path
                             folderBrowserDialog1.ShowNewFolderButton = true;
                             if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                             {

                                 Path = folderBrowserDialog1.SelectedPath + "\\";
                                 selectedPath = true;
                             }

                         }
                         // Retrieve session data and download the file        
                         Beans.Seance2 seance = AuthEtudiant.getSeance(id_seance);
                         DownloadFile(seance);
                         ConversationBox.Append(stb => stb.Text += jump + "" + data[0]);
                     }
                 } 
             


                
            }
            catch (Exception ex)
            {
                ConversationBox.Append(stb => stb.Text += jump + "Problem communicating with the server. Connection may have been intentionally disconnected.");
               
            }

           
          

        }
        // Method to download a file
        public void DownloadFile(Seance2 seance)
        {
            // Get the file data and write it to a file
            byte[] myFile = seance.Data;
            using (StreamWriter sWriter = new StreamWriter(Path + seance.FileName))
            {

                BinaryWriter writer = new BinaryWriter(sWriter.BaseStream);

                writer.Write(myFile);

                // writer.Close();

            }
           
        }
        // Method to disconnect from the server
        private void DisconnectFromServer()
        {

            try
            {
                // Close the client connection
                _client.Close();
                ConversationBox.Append(stb => stb.Text += jump + "Disconnected from the server!");
                ConversationBox.Append(stb => stb.Text = string.Empty);
            }
            catch (Exception ex)
            {
                ConversationBox.Append(stb => stb.Text += jump + "Problem disconnecting from the server.");
                ConversationBox.Append(stb => stb.Text += jump + ex.ToString());
            }

            ConversationBox.Append(stb => stb.Text = string.Empty);

        }
        string txtFilePath = "";

        /// Initiates file selection when the "Browser" button is clicked.
        private void Browser_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "All Files (*.*)|*.*";
            Dlg.CheckFileExists = true;
            Dlg.Title = "Choose a File";
            Dlg.InitialDirectory = @"C:\";
            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                txtFilePath = Dlg.FileName;

            }
            SendFile_btn.Enabled = true;
        }

        // Handles the action upon clicking the "Send File" button.
        private void SendFile_btn_Click(object sender, EventArgs e)
        {
            SaveFile(txtFilePath);
        }

        // Saves the selected file before sending it to the server.
        private void SaveFile(string filePath)
        {
          
            using (Stream stream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                var fi = new FileInfo(filePath);
                string extn = fi.Extension;
                string name = fi.Name;
                // Save the file to the server
                AuthEtudiant.SaveFileEtudiant(etudiant.id_etudiant,id_seance, buffer, extn, name);
                try
                {
                    if (_client.Connected)
                    {
                         // Sends a notification to the server about the received file
                        StreamWriter writer = new StreamWriter(_client.GetStream());
                        writer.WriteLine( "a file have been arrived:Zip:"+etudiant.nom+":"+etudiant.prenom+":"+etudiant.id_etudiant);
                        writer.Flush();
                        ConversationBox.Append(stb => stb.Text += jump + "Me > The file is sent to the teacher");
                       
                    }

                }
                catch (Exception ex)
                {
                    ConversationBox.Text += jump + "Problem sending File to teacher";
                    ConversationBox.Text += jump + ex.ToString();
                }
            }
        }

     
    }
}
