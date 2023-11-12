using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Interfaces;
using Beans;
namespace AssistantPedagogique.Classes
{
     // Class AuthEtuImpl implementing the AuthEtu interface
    class AuthEtuImpl : MarshalByRefObject,AuthEtu
    {
        // Access to the data layer and teacher authentication instance
        AccesAuxDonnees SGBD = (AccesAuxDonnees)Activator.GetObject(typeof(AccesAuxDonnees), "tcp://localhost:8085/SGBDobj");
        AuthEns AuthE = (AuthEns)Activator.GetObject(typeof(AuthEns), "tcp://localhost:8086/AuthEns");
        // Table to store connected students
        List<Beans.ConnectTable> connectTable = new List<Beans.ConnectTable>();
       
        // Authenticates a student given an email and password
        public Boolean AuthentificationEtudiant(string email, string password)
        {
            Boolean b = SGBD.AuthentificationEtudiant(email, password);
            return b;
            
        }
         // Retrieves a list of students associated with a specific teacher
        public List<Beans.Etudiant> getStudents(int id_enseignant)
        {
            List<Beans.Etudiant> connectEtudiants = new List<Beans.Etudiant>();
            

            foreach (ConnectTable Ct in connectTable) 
            {
                if (Ct.id_enseignant == id_enseignant)
                {
                Beans.Etudiant e=SGBD.getStudent(Ct.id_etudiant);
                connectEtudiants.Add(e);
                }
            }
            return connectEtudiants;
        }
        // Retrieves a student by their email and password
        public Beans.Etudiant getStudent(string email, string password)
        {
            Beans.Etudiant etudiant = SGBD.getEtudiant(email,password);
            return etudiant;
        }
        // Connects a student to a specific teacher
        public void connectStudent(int id_etudiant , int id_enseignant ) 
        {
            Beans.ConnectTable cnt = new ConnectTable(id_etudiant, id_enseignant);
            connectTable.Add(cnt);

        }  
        // Retrieves students connected to a particular teacher's session
        public List<Beans.Etudiant> connectSessionStudent(int id_enseignant)
        {
            List<Beans.Etudiant> EtudiantGroup = new List<Beans.Etudiant>();
            foreach (Beans.ConnectTable ct in connectTable) 
            {
                if (ct.id_enseignant == id_enseignant) 
                {
                    Beans.Etudiant e = SGBD.getStudent(ct.id_etudiant);
                    EtudiantGroup.Add(e);
                }
            }

            return EtudiantGroup;
        }
        // Retrieves the current session for a specific teacher
        public int getCurrentSession(int id_enseignant)
        {
            int id_seance =SGBD.getCurrentSession(id_enseignant);

                return id_seance;
        }
         // Registers a student to a session
        public void RegisterToSession(int id_seance, int id_etudiant)
        {
            SGBD.RegisterToSession(id_seance, id_etudiant);
        }
         // Retrieves the datetime table for a specific teacher
       public List<Beans.DateTimes> getDateTimeTable(int id_enseignant) 
       {
           List<Beans.DateTimes> DatesTable = new List<Beans.DateTimes>();
           DatesTable=AuthE.getDateTimeTable(id_enseignant);
           return DatesTable;
       }
        // Retrieves session information by session ID
      public Beans.Seance2 getSeance(int id_seance)
       {
           Beans.Seance2 seance = SGBD.getSeance(id_seance);
           return seance;
       }

       // Saves a file for a specific student and session
      public void SaveFileEtudiant(int id_etudiant, int id_seance, Byte[] buffer, string extn, string name)
      {
          SGBD.SaveFileEtudiant(id_etudiant, id_seance, buffer, extn, name);
      }
     
        /*
       public List<TcpClient> getTcpClientList(int id_enseignant) 
       {
           List<TcpClient> _client_list = AuthE.getTcpClientList(id_enseignant);
           return _client_list;
       }
         */
    }
}
