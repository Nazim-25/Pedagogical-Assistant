using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Interfaces; // Importing interfaces
using Beans;  // Importing beans/entities

namespace AssistantPedagogique.Classes
{
     // This class implements the 'AuthEns' interface
    // It manages authentication and interaction with teaching personnel
    class AuthEnsImpl : MarshalByRefObject, AuthEns
    {

        // Creating instances of remote objects
        AccesAuxDonnees SGBD = (AccesAuxDonnees)Activator.GetObject(typeof(AccesAuxDonnees), "tcp://localhost:8085/SGBDobj");
        AuthEtu AuthEtudiant = (AuthEtu)Activator.GetObject(typeof(AuthEtu), "tcp://localhost:8086/AuthEtu");

        // Authentication of teaching personnel
        public Boolean AuthetificationEnseignant(string email, string password)
        {
            Boolean b = SGBD.AuthentificationEnseignant(email, password);
            return b;
        }

        // Retrieving a teacher's details based on email and password
        public Beans.Enseignant getTeacher(string email, string password)
        {
            Beans.Enseignant e = SGBD.getEnseignant(email, password);

            return e;

        }
        
        public Beans.Enseignant getTeacherById(int id_enseignant)
        {
            Beans.Enseignant e = SGBD.getTeacherById(id_enseignant);

            return e;

        }

        List<Beans.Enseignant> OnlineTeachers = new List<Beans.Enseignant>();
        List<Beans.DateTimes> TimesTable = new List<Beans.DateTimes>();
        // Records a teacher's connection along with port and IP information
        public void ConnectTracher(Beans.Enseignant e, int port, IPAddress localAddr)
        {
            DateTime now = new DateTime();
            OnlineTeachers.Add(e);
            TimesTable.Add(new DateTimes(e.id_enseignant, now, port, localAddr));

        }
         // Fetches a table of date and time information for a specific teacher
        public List<Beans.DateTimes> getDateTimeTable(int id_enseignant)
        {
            List<Beans.DateTimes> DatesTable = new List<Beans.DateTimes>();
            foreach (DateTimes Dt in TimesTable)
            {
                if (Dt.id_enseignant == id_enseignant)
                {
                    DatesTable.Add(Dt);

                }
            }
            return DatesTable;
        }
        // Retrieves the list of connected teachers
        public List<Beans.Enseignant> getConnectTrachers()
        {
            return OnlineTeachers;

        }

        public void startSeance(int id_enseignant, int id_module)
        {
            SGBD.startSeance(id_enseignant, id_module);
        }
        // Retrieves the list of students associated with a specific teacher
        public List<Beans.Etudiant> getStudents(int id_enseignant)
        {
            List<Beans.Etudiant> connectEtudiants = AuthEtudiant.getStudents(id_enseignant);
            return connectEtudiants;
        }
        // Fetches the list of students associated with a particular module
        public List<Beans.Etudiant> getStudentsByModule(int id_module)
        {
            List<Beans.Etudiant> StudentsByModule = SGBD.getStudentsByModule(id_module);
            return StudentsByModule;

        }
        // Retrieves the list of students absent during a session for a specific teacher
        public List<Beans.Etudiant> getAbsenceStudents(int id_enseignant)
        {
            Beans.Enseignant enseignant = getTeacherById(id_enseignant);

            List<Beans.Etudiant> StudentsByModule = getStudentsByModule((int)enseignant.id_module);
            List<Beans.Etudiant> ConnectStudents = getStudents(id_enseignant);
            List<Beans.Etudiant> AbsenceStudents = new List<Beans.Etudiant>();

            foreach (Beans.Etudiant e in StudentsByModule)
            {
                Boolean b = false;
                foreach (Beans.Etudiant e1 in ConnectStudents)
                {
                    if (e.id_etudiant == e1.id_etudiant)
                    {
                        b = true;
                    }
                }
                if (!b)
                {
                    AbsenceStudents.Add(e);
                }
            }
            return AbsenceStudents;
        }   
        // Marks the absence of students for a specific teacher based on session data
        public void MarquePresence(int id_enseignant)
        {
            Beans.Enseignant enseignant = getTeacherById(id_enseignant);
            List<Beans.Etudiant> AbsenceStudents = getAbsenceStudents(id_enseignant);

            SGBD.AddAbsence(AbsenceStudents, (int)enseignant.id_module);
        }
        // Retrieves the list of students excluded from a module
        public List<Beans.Etudiant> getExcludedStudents(int id_module)
        {
            List<Beans.Etudiant> ExcludedStudents = SGBD.getExcludedStudents(id_module);
            return ExcludedStudents;
        }
        // Retrieves the names of students absent during a session for a specific teacher
        public List<string> getAbsenceStudentsNames(int id_enseignant)
        {

            List<Beans.Etudiant> AbsStudents = getAbsenceStudents(id_enseignant);
            List<string> studentsNames = new List<string>();
            studentsNames.Add("nom  " + "prénom ");
            foreach (Beans.Etudiant etudiant in AbsStudents)
            {
                string fullName = etudiant.nom + "  " + etudiant.prenom;
                studentsNames.Add(fullName);

            }
            return studentsNames;
        }
        // Retrieves the names of students excluded from a module
        public List<string> getExcludedStudentsNames(int id_module)
        {
            List<string> studentsNames = new List<string>();
            studentsNames.Add("nom  " + "prénom ");
            List<Beans.Etudiant> ExcludedStudents = new List<Beans.Etudiant>();
            ExcludedStudents = getExcludedStudents(id_module);

            foreach (Beans.Etudiant etudiant in ExcludedStudents)
            {
                string fullName = etudiant.nom + "  " + etudiant.prenom;
                studentsNames.Add(fullName);

            }
            return studentsNames;
        }
        // Retrieves the names of all students for a specific teacher
        public List<string> getStudentsNames(int id_enseignant)
        {
            List<string> studentsNames = new List<string>();
            studentsNames.Add("nom  " + "prénom ");
            List<Beans.Etudiant> AllStudents = getStudents(id_enseignant);

            foreach (Beans.Etudiant etudiant in AllStudents)
            {
                string fullName = etudiant.nom + "  " + etudiant.prenom;
                studentsNames.Add(fullName);

            }
            return studentsNames;
        }
        // Retrieves the names of students associated with a specific module
        public List<string> getStudentsNamesByModule(int id_module)
        {
            List<string> studentsNames = new List<string>();
            studentsNames.Add("nom  " + "prénom ");
            List<Beans.Etudiant> AllStudents = getStudentsByModule(id_module);
            foreach (Beans.Etudiant etudiant in AllStudents)
            {
                string fullName = etudiant.nom + "  " + etudiant.prenom;
                studentsNames.Add(fullName);
            }
            return studentsNames;
        }
        // Retrieves the current session ID for a teacher
        public int getCurrentSession(int id_enseignant) 
        {
            int id_seance = AuthEtudiant.getCurrentSession(id_enseignant);
            return id_seance;
        }
        // Saves a file related to a teaching session
       public void SaveFile(int id_seance, Byte[] buffer, string extn,string name){
            SGBD.SaveFile(id_seance, buffer, extn,name);
        }

        // Retrieves a compressed file (RarFile) related to a session and a specific student
       public Beans.RarFile getFileZip(int id_seance, int id_etudiant)
       {
           Beans.RarFile rarFile = SGBD.getFileZip(id_seance, id_etudiant);
           return rarFile;
       }

        // Retrieving a student based on their ID 
       public Beans.Etudiant getEtudiant(int id_etudiant)
        {
            Beans.Etudiant etudiant = new Beans.Etudiant();
             etudiant = SGBD.getStudent(id_etudiant);
            return etudiant;
        }
        /*
       public void updateTcpClientList(int id_enseignant, List<TcpClient> _client_list)
        {
            foreach (DateTimes Dt in TimesTable)
            {
                if (Dt.id_enseignant == id_enseignant)
                {
                    Dt._client_list = _client_list;
                }
            }
        }

        public List<TcpClient> getTcpClientList(int id_enseignant)
        {
            List<TcpClient> TcpClients = new List<TcpClient>();
            foreach (DateTimes Dt in TimesTable)
            {
                if (Dt.id_enseignant == id_enseignant)
                {
                    TcpClients = Dt._client_list;
                }
             
            }
            return TcpClients;
        }
         * */
    }
}
