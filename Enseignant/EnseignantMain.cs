using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Enseignant.Classes;
using Interfaces;
using Beans;
namespace Enseignant
{

    // Class representing the EnseignantMain form
    public partial class EnseignantMain : Form
    {

       // Constant string for line breaks
        private const string jump = "\r\n";

         // List to store connected TcpClients
        private List<TcpClient> _client_list;

        // TcpListener to handle connections
        private TcpListener _listener;

        // Counter for connected clients
        private int _client_count;

        // Flag to control the server loop
        private bool _keep_going;

         // Session ID
        int id_seance =0;

         // Path to the desktop folder
        string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\";

        // Boolean flag for the selected path
        Boolean selectedPath = false;

         // FolderBrowserDialog for selecting file paths
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

        // List of students' Bean objects for environment variables
        List<Beans.Etudiant> EtudiantEnv = new List<Beans.Etudiant>();
        
        // Method to handle the presence check in a separate thread
        public void ThreadPresence() 
        {
            Thread.Sleep(30000); // Wait for 30 seconds
            AuthEns AuthE = (AuthEns)Activator.GetObject(typeof(AuthEns), "tcp://localhost:8086/AuthEns");
            AuthE.MarquePresence(enseignant.id_enseignant); // Mark the presence of the teacher
            MessageBox.Show(" : the presence check is done !."); // Show completion message

        }
        Beans.Enseignant enseignant = new Beans.Enseignant();
        int _port;
        IPAddress localAddr;
        public EnseignantMain()
        {
           InitializeComponent();
          
                
        }
        public EnseignantMain(Beans.Enseignant e, int port, IPAddress localAddr)
        {
            
            InitializeComponent(); 
            enseignant = e; // Set teacher information
            this._port = port; // Set port information
            this.localAddr = localAddr; // Set Local Ip adress
            _client_list = new List<TcpClient>(); // Initialize client list
            _client_count = 0;
            id_seance = AuthE.getCurrentSession(enseignant.id_enseignant); // Get the current session
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();   // Initialize folder browser dialog
            StartServer(); // Start the server
            Thread t = new Thread(ThreadPresence);
            t.Start();  // Create and start a thread for presence check
            MessageBox.Show(localAddr + " ; " + _port); // Show local address and port
            SendFile_btn.Enabled =false; // Disable send file button
           
        }

         // Instance of AuthEns to communicate with the server
        AuthEns AuthE = (AuthEns)Activator.GetObject(typeof(AuthEns), "tcp://localhost:8086/AuthEns");
        
        

        private void EnseignantMain_Load(object sender, EventArgs e)
        {
           
        
        }

        // Event handler for displaying all connected students
        private void AllStudents_btn_Click(object sender, EventArgs e)
        {
            // Clear the display and get and show the list of present students
            ConnectList.DataSource = null;
            ConnectList.Items.Clear();
             ListType.Text="Present Students list ";
            List<string> ConnectedStudents = new List<string>();
            ConnectedStudents = AuthE.getStudentsNames(enseignant.id_enseignant);
            ConnectList.DataSource = ConnectedStudents;
                
        }
        // Event handler for displaying all students
        private void button1_Click(object sender, EventArgs e)
        {
            // Clear the display and get and show the list of all students
            ConnectList.DataSource = null;
            ConnectList.Items.Clear();
            ListType.Text="All Students list ";
            List<string> AllStudents = new List<string>();
            AllStudents = AuthE.getStudentsNamesByModule((int)enseignant.id_module);
            ConnectList.DataSource = AllStudents;

           
        }
        // Event handler for displaying absent students
        private void AbsStudents_btn_Click(object sender, EventArgs e)
        {
            // Clear the display and get and show the list of absent students
            ConnectList.DataSource = null;
            ConnectList.DataSource = null;
            ConnectList.Items.Clear();
            ListType.Text="Absent Students list ";
            List<string> AbsStudents = new List<string>();
            AbsStudents = AuthE.getAbsenceStudentsNames((int)enseignant.id_enseignant);
            ConnectList.DataSource = AbsStudents;

            
        }
        // Event handler for displaying excluded students
        private void ExcludedStudents_btn_Click(object sender, EventArgs e)
        {

            // Clear the display and get and show the list of excluded students
            ConnectList.DataSource = null;
            ConnectList.Items.Clear();
            ListType.Text="Excluded Students list ";
            List<string> ExcludedStudents = new List<string>();
            ExcludedStudents = AuthE.getExcludedStudentsNames((int)enseignant.id_module);

            ConnectList.DataSource = ExcludedStudents;
           
        }

       


        #region Event Handlers

        // Function to start the server for listening to incoming connections
        private void StartServer()
        {
            try
            {
               
                _client_count = 0;
                _client_list.Clear();
                Thread t = new Thread(ListenForIncomingConnections);
                t.Name = "Server Listener Thread";
                t.IsBackground = true;
                t.Start();
               
            }
            catch (Exception ex)
            {
                // Handle any exceptions when starting the server
                _statusTextBox.Text += jump + "Problem starting server.";
                _statusTextBox.Text += jump + ex.ToString();
            }
        }

        #endregion Event Handlers

        // Function to listen for incoming client connections
        private void ListenForIncomingConnections()
        {
            try
            {
                _keep_going = true;
                _listener = new TcpListener(localAddr, _port);
                _listener.Start();

                _statusTextBox.Append(stb => stb.Text += jump + "Server started. Listening on port: " + _port);

                while (_keep_going)
                {
                    _statusTextBox.Append(stb => stb.Text += jump + "Waiting for incoming student connection...");
                    TcpClient client = _listener.AcceptTcpClient();  
                   // AuthE.updateTcpClientList(enseignant.id_enseignant,_client_list);
                    _statusTextBox.Append(stb => stb.Text += jump + "Incoming student connection accepted...");
                    Thread t = new Thread(ProcessClientRequests);
                    t.IsBackground = true;
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start(client);
                }
            }
            catch (SocketException se)
            {
                // Handle any socket-related exceptions
                _statusTextBox.Append(stb => stb.Text += jump + "Problem starting the server.");
                _statusTextBox.Append(stb => stb.Text += jump + se.ToString());
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                _statusTextBox.Append(stb => stb.Text += jump + "Problem starting the server.");
                _statusTextBox.Append(stb => stb.Text += jump + ex.ToString());
            }

            _statusTextBox.Append(stb => stb.Text += jump + "Exiting listener thread...");
            _statusTextBox.Append(stb => stb.Text = String.Empty);

        }
      
       
        // Function to process client requests
        private void ProcessClientRequests(object o)
        {
            TcpClient client = (TcpClient)o;
            _client_list.Add(client);
            _client_count += 1;
          


            string input = string.Empty;


            try
            {
                // Handle communication with the client
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                // Continuously listen for incoming data from the client
                while (client.Connected)
                {
                    string[] data;
                    string zip = "Zip", chat = "Chat";
                    input = reader.ReadLine(); 
                    data = input.Split(':');
                   
                    
                    if(input!=null){
                            if (data[1].Equals(chat)) 
                            {
                                // Handle chat-related data from the client
                                // Broadcast received chat message to other client
                                _statusTextBox.Append(stb => stb.Text += jump + "" + data[0]);
                             
                                foreach (TcpClient client1 in _client_list)
                                {
                                    if (client.GetHashCode() != client1.GetHashCode())
                                    {
                                    StreamWriter writer1 = new StreamWriter(client1.GetStream());
                                    writer1.WriteLine(input);
                                    writer1.Flush();
                                    }
                                }
                            }
                            else if (data[1].Equals(zip))
                            {
                                // Handle file transfer request and save the file
                                // Select path if not already chosen and download the file
                               if (!selectedPath)
                               {
         
                              
                                folderBrowserDialog1.ShowNewFolderButton = true;  
                                  if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                                     {
                                   
                                       Path = folderBrowserDialog1.SelectedPath+"\\";
                                       selectedPath = true;
                                   }
                                          
                               }
                                 
                               int id_etudiant = int.Parse(data[4]);
                               Beans.Etudiant etudiant = AuthE.getEtudiant(id_etudiant);
                               EtudiantEnv.Add(etudiant);
                               DownloadFileZip(id_seance, id_etudiant, Path);
                               _statusTextBox.Append(stb => stb.Text += jump + "" + data[0] + " from " + data[2] + " " + data[3]);
                               
                            }
                    }
                }

            }
            catch (SocketException se)
            {
              

            }
            catch (Exception ex)
            {
                _statusTextBox.Append(stb => stb.Text += jump + "Problem processing student requests. ");
                
            }

            _client_list.Remove(client);
            _client_count -= 1;

            _statusTextBox.Append(stb => stb.Text += jump + "Finished processing student requests for student: " + client.GetHashCode());

            if (_client_count == 0)
            {
                _statusTextBox.Append(stb => stb.Text = string.Empty);
            }
        }
        // Function to download and save a file
        public void DownloadFileZip(int id_seance, int id_etudiant, string Path)
        {
            // Retrieve file information
            Beans.RarFile rarFile = AuthE.getFileZip(id_seance, id_etudiant);
           
           // Write the file content to the specified path
            using (StreamWriter sWriter = new StreamWriter(Path + rarFile.FileName))
            {

                BinaryWriter writer = new BinaryWriter(sWriter.BaseStream);

                writer.Write(rarFile.File);

                // writer.Close();

            }

        }

        // Method to send a message to connected clients
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                // Send a message to all connected clients
                foreach (TcpClient client in _client_list)
                {
                    StreamWriter writer = new StreamWriter(client.GetStream());
                    writer.WriteLine("From Teacher "+ ">"+_MessageTextBox.Text+":Chat" );
                    writer.Flush();

                }
                _statusTextBox.Append(stb => stb.Text += jump + "Teacher > " + _MessageTextBox.Text);
                _MessageTextBox.Text = string.Empty; // Clear the message box after sending

            }
            catch (Exception ex)
            {
                // Handle exceptions when sending a message to clients
                _statusTextBox.Text += jump + "Problem sending command to clients";
                _statusTextBox.Text += jump + ex.ToString();
            }
        }

        
        string txtFilePath="";

        // Method to browse and select a file
        private void Browser_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "All Files (*.*)|*.*";
            Dlg.CheckFileExists = true;
            Dlg.Title = "Choose a File";
            Dlg.InitialDirectory = @"C:\";

            // Open a file dialog to choose a file to send
            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                txtFilePath = Dlg.FileName;
                
            }
            SendFile_btn.Enabled = true; // Enable the button to send the file
        }

        // Method to send a selected file to connected clients
        private void SendFile_btn_Click(object sender, EventArgs e)
        {
           SaveFile(txtFilePath); // Call the method to save and send the selected file

        }

        // Method to save and send a file to clients
        private void SaveFile(string filePath) 
        {
            // Get the current session ID
            int id_seance = AuthE.getCurrentSession(enseignant.id_enseignant);
            using(Stream stream =File.OpenRead(filePath))
                {
                // Read the file into a byte array
                byte[] buffer= new byte[stream. Length];
                stream.Read(buffer, 0, buffer. Length) ;
                var fi = new FileInfo(filePath);
                string extn = fi.Extension;
                string name = fi.Name;
               // string extn = new FileInfo (filePath).Extension;

                // Save the file on the server side
                AuthE.SaveFile(id_seance, buffer, extn, name);
                try
                {
                    // Notify clients about the sent file
                    foreach (TcpClient client in _client_list)
                    {
                        StreamWriter writer = new StreamWriter(client.GetStream());
                        writer.WriteLine("From Teacher " + "> a file have been arrived :Pdf");
                        writer.Flush();

                    }
                    _statusTextBox.Append(stb => stb.Text += jump + "Teacher > The file is sent to all students");
               

                }
                catch (Exception ex)
                {
                     // Handle exceptions when sending the file to clients
                    _statusTextBox.Text += jump + "Problem sending command to clients";
                    _statusTextBox.Text += jump + ex.ToString();
                }

     
                }
        }
       // Method to display a list of students who have reported
        private void CompteRendusRecu_Click(object sender, EventArgs e)
        {
            // Clear and prepare the UI to display the received reports
            ConnectList.DataSource = null;
            ConnectList.Items.Clear();
            ListType.Text = " Students reported list ";

            // Initialize a list to store the details of students who have submitted reports
            List<string> RecivedReportsList = new List<string>();
            RecivedReportsList.Add("Nom    Prenom    Nom2    Prenom2");

            // Loop through the stored student data to populate the displayed list
            foreach (Beans.Etudiant e1 in EtudiantEnv) 
            {
                if (e1.id_binome != null)
                {
                    // If a paired student exists, retrieve their details and add to the list
                    Beans.Etudiant e2 = AuthE.getEtudiant((int)e1.id_binome);
                    RecivedReportsList.Add(e1.nom + "    " + e1.prenom + "    " + e2.nom + "    " + e2.prenom);
                }
                else
                {
                    // If no paired student, add the individual student's details to the list
                    RecivedReportsList.Add(e1.nom + "    " + e1.prenom);
                }
            }

            ConnectList.DataSource = RecivedReportsList;
        }

        // Method to display a list of students who have not reported
        private void CompteRendusManquants_Click(object sender, EventArgs e)
        {
             // Prepare the UI to display students who haven't submitted reports
            ConnectList.DataSource = null;
            ConnectList.Items.Clear();
            ListType.Text = " Students missing reports list ";

            / Initialize a list to store details of students who have not submitted reports
            List<string> MissingReports = new List<string>();
            MissingReports.Add("Nom      Prenom    ");

            // Retrieve the list of students and compare with submitted reports
            List<Beans.Etudiant> PresentStudents = new List<Beans.Etudiant>();
            PresentStudents = AuthE.getStudents(enseignant.id_enseignant);
            foreach (Beans.Etudiant e1 in PresentStudents)
            {
                // Check if the student or their paired student's report is missing
                Boolean c = false;
                foreach (Beans.Etudiant e2 in EtudiantEnv)
                {
                    if (e1.id_etudiant == e2.id_etudiant || e1.id_etudiant== e2.id_binome) 
                    {
                        c = true;
                    }
                }
                // If the report is missing, add the student's details to the list
                if(!c)
                {
                    MissingReports.Add(e1.nom + "    " + e1.prenom);
                }
              
            }
            // Bind the list of students missing reports to the UI for display
            ConnectList.DataSource = MissingReports;
        }

        }
    }




/*
        public  void  ThreadProc() {
            AuthEns AuthE = (AuthEns)Activator.GetObject(typeof(AuthEns), "tcp://localhost:8086/AuthEns");

            int i = 0;
            int j = -1;
            while (true)
            {
                List<Beans.Etudiant> AllStudents = new List<Beans.Etudiant>();
                AllStudents = AuthE.getStudents();
                List<string> studentsNames = new List<string>();
                i = AllStudents.Count;
                if (i != j)
                {
                    foreach (Beans.Etudiant etudiant in AllStudents)
                    {
                        string fullName = etudiant.nom + "  " + etudiant.prenom;
                        studentsNames.Add(fullName);

                    }
                    RefrechList(studentsNames);
                    j = i;
                    
                }
            }
            }

        void RefrechList(List<string> Students) 
        {
            //ConnectList.Items.Clear();
            foreach (string student in Students)
            {
                ConnectList.Items.Add(student);
            }
            //this.ConnectList.Refresh();
        }
            
        private void messageBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData (DataFormats.FileDrop);
                if (files != null && files.Length != 0){
                    sendfile(files[0]);
                }
        } 

        private void sendfile(string fn){
            IPAddress ipAddress =IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEnd = new IPEndPoint(ipAddress, 3004);
            Socket clientsocket = new Socket(AddressFamily. InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string fileName = fn;// 'c:\filetosend. txt";
            byte[] fileNamebyte = Encoding.ASCII.GetBytes(fileName);
            byte[] fileNameLen= BitConverter.GetBytes(fileNamebyte.Length);
            byte[] fileData = File.ReadAllBytes(fileName);
            byte[] clientData = new byte[4 + fileNamebyte.Length + fileData. Length];

            fileNameLen.CopyTo(clientData, 0);
            fileNamebyte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNamebyte.Length);
            clientsocket.Connect(ipEnd);
            clientsocket.Send(clientData);
            clientsocket.Close();

        }

        private void messageBox_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                sendfile(files[0]);
            }
        }
        string fileName="";
        private void Browser_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "All Files (*.*)|*.*";
            Dlg.CheckFileExists = true;
            Dlg.Title = "Choose a File";
            Dlg.InitialDirectory = @"C:\";
            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                 fileName = Dlg.FileName;
                
            }
        }

        private void SendFile_btn_Click(object sender, EventArgs e)
        {
            sendfile(fileName);
        }

 
         * */