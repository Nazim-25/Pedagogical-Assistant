using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beans;
namespace Interfaces
{   
    // Interface for student management methods
   public interface StudentsManagement
    {
        // Add a new student
        void AddStudent(string nom, string prenom, string email, string password, int groupe);

        // Delete a student by ID
        void DeleteStudent(int id_etudiant);
        List<Beans.Etudiant> getAllStudients();

        // Get all modules/subjects
        List<Beans.module2> getModules();

        // Register student to subject
        void RegisterToSubject(int id_etudiant, int id_module);

        
        // Get module name by ID
        Beans.module2 getModuleName(int id_module);

        // Get students by last name
        List<Beans.Etudiant> getStudentsByName(string nom);
    }
}
