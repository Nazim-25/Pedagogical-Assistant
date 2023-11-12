using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Beans;
namespace Interfaces
{
    public interface AuthEns
    {
        // Authenticate instructor by email and password
       Boolean AuthetificationEnseignant(string email, string password);

       // Get instructor object by email and password
       Enseignant getTeacher(string email, string password);

       // Connect instructor to server
       void ConnectTracher(Beans.Enseignant e, int port, IPAddress localAddr);
       // Get list of connected instructors
       List<Beans.Enseignant> getConnectTrachers();

       // Start session for instructor's module
       void startSeance(int id_enseignant, int id_module);

       // Get current session ID
       int getCurrentSession(int id_enseignant);

       // Get instructor's date/time table
       List<Beans.DateTimes> getDateTimeTable(int id_enseignant);
       

       // Mark student attendance
       void MarquePresence(int id_enseignant);

        // Get list of absent students
       List<Beans.Etudiant> getAbsenceStudents(int id_enseignant);

        // Get list of excluded students
       List<Beans.Etudiant> getExcludedStudents(int id_module);
        // Get list of students by module
        List<Beans.Etudiant> getStudentsByModule(int id_module);

        // Get list of absent students
         List<string> getAbsenceStudentsNames(int id_enseignant);

         // Get list of excluded students
         List<string> getExcludedStudentsNames(int id_module);
         List<string> getStudentsNames(int id_enseignant);

         // Get list of students by module
         List<string> getStudentsNamesByModule(int id_module);
        // void updateTcpClientList(int id_enseignant, List<TcpClient> _client_list);
         //List<TcpClient> getTcpClientList(int id_enseignant);


        // Save file to session
         void SaveFile(int id_seance, Byte[] buffer, string extn, string name);

         // Get zip file for student
        Beans.RarFile getFileZip(int id_seance,int id_etudiant);

        // Get list of students
       List<Beans.Etudiant> getStudents(int id_enseignant);

       // Get student by ID
       Beans.Etudiant getEtudiant(int id_etudaint);
    }
}
