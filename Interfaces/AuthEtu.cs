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
  // Interface for authentication and student management methods
   public interface AuthEtu
    {
        // Authenticate student by email and password
        Boolean AuthentificationEtudiant(string email, string password);
        // Get student object by email and password
        Beans.Etudiant getStudent(string email, string password);
        //  Beans.Etudiant getEtduiant(int id_etudaint);
        // Get list of students for instructor
        List<Beans.Etudiant> getStudents(int id_enseignant);
        // Get instructor's date/time table
        List<Beans.DateTimes> getDateTimeTable(int id_enseignant);

        // Connect student to instructor
         void connectStudent(int id_etudiant, int id_enseignant);
         // Get current session ID for instructor
        int getCurrentSession(int id_enseignant);
        // Register student to session
        void RegisterToSession(int id_seance, int id_etudiant);
        Beans.Seance2 getSeance(int id_seance);
        // Save student file to session
        void SaveFileEtudiant(int id_etudiant, int id_seance, Byte[] buffer, string extn, string name);
       // List<TcpClient> getTcpClientList(int id_enseignant);
    }
}
