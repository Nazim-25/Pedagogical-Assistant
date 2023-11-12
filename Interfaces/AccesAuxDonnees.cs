using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beans;
namespace Interfaces
{
    // Interface for data access methods
   public interface AccesAuxDonnees
    {
       // Authenticate instructor by email and password
        Boolean AuthentificationEnseignant(string email, string password);

        // Get instructor by email and password
        Enseignant getEnseignant(string email, string password);

        // Get instructor by ID
        Enseignant getTeacherById(int id_enseignant);

        // Start session for instructor's module
        void startSeance(int id_enseignant, int id_module);


        // Authenticate student by email and password
        Boolean AuthentificationEtudiant(string email, string password);
        // Get student by email and password
        Etudiant getEtudiant(string email, string password);
        // Get student by ID
        Etudiant getStudent(int id_etudiant);

        // Get current session ID
        int getCurrentSession(int id_enseignant);
        // Register student to session
        void RegisterToSession(int id_seance, int id_etudiant);
        List<Beans.Etudiant> getStudentsByModule(int id_module);
        void AddAbsence(List<Beans.Etudiant> absStudents,  int id_module);
        List<Beans.Etudiant> getExcludedStudents(int id_module);
        void SaveFile(int id_seance,Byte[] buffer,string extn,string name);
        Beans.Seance2 getSeance(int id_seance);

        // Save student file to session
        void SaveFileEtudiant(int id_etudiant, int id_seance, Byte[] buffer, string extn, string name);
        // Get student zip file
        Beans.RarFile getFileZip(int id_seance,int id_etudiant);

       /*
        Boolean AuthentificationEtudinat(string email, string password);
        List<Etudiant> getEtudiants();
        Seance2 getLastSeanse();
        List<Etudiant> getSeanseAssister();
        List<Etudiant> getEtudiantExclu();
        */
    }
}
