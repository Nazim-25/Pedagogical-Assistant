using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Interfaces;
using System.Data.Entity;
using Beans;
namespace SGBD
{
    // Implementation of StudentsManagement interface using Entity Framework
    class StudentsManagementImpl : MarshalByRefObject, StudentsManagement
    {
        // Constructor
        public StudentsManagementImpl() 
        {
        
        }
        // Add student to database
        public void AddStudent(string nom, string prenom, string email, string password, int groupe)
        {
            using (var db1 = new Beans.AssistantDB())
            {
                // Map to entity
                Beans.Etudiant Etudiant = new Beans.Etudiant();
                {
                    Etudiant.nom = nom;
                    Etudiant.prenom = prenom;
                    Etudiant.email = email;
                    Etudiant.password = password;
                    Etudiant.groupe = groupe;

                }
                // Add and save entity
                db1.Etudiants.Add(Etudiant);
                db1.SaveChanges();
            }
        }

        // Delete student by ID
        public void DeleteStudent(int id_etudiant) 
        {

            using (var db1 = new Beans.AssistantDB()) 
            {
                // Get entity to delete
                var itemToRemove = db1.Etudiants.SingleOrDefault(x => x.id_etudiant == id_etudiant); 

                if (itemToRemove != null)
                {
                     // Delete and save
                    db1.Etudiants.Remove(itemToRemove);
                    db1.SaveChanges();
                }
            }
        
        }

        List<Beans.Etudiant> allStudients;
        // Get all students
        public List<Beans.Etudiant> getAllStudients()
        {
            using (var db1 = new Beans.AssistantDB())
            {
                // Include related data for efficiency
                allStudients = db1.Etudiants.Include(e => e.ModuleEtudies).ToList();
            }
            return allStudients;
        }

        List<Beans.module2> allModules;

         // Get all modules
        public List<Beans.module2> getModules() 
        {
            using (var db1 = new Beans.AssistantDB())
            {
                allModules = db1.module2.ToList();
            }
            return allModules;
        
        }
        // Register student to a subject
        public void RegisterToSubject(int id_etudiant,int id_module)
        {
            using (var db1 = new Beans.AssistantDB())
            {
                Beans.ModuleEtudie moduleEtudie = new Beans.ModuleEtudie();
                {
                    moduleEtudie.id_etudiant = id_etudiant;
                    moduleEtudie.id_module = id_module;
                    moduleEtudie.nbr_absence = 0;
                }
                db1.ModuleEtudies.Add(moduleEtudie);
                db1.SaveChanges();
            }
        }
        // Get module name by ID
        public Beans.module2 getModuleName(int id_module)
        {
            Beans.module2 module = new Beans.module2();
            using (var db1 = new Beans.AssistantDB())
            {
                module = db1.module2.FirstOrDefault(a => a.id_module == id_module);
            }
            return module;

        }

         // Get students by name
        public List<Beans.Etudiant> getStudentsByName(string nom) 
        {
            List<Beans.Etudiant> studentsByName = new List<Beans.Etudiant>();
            using (var db1 = new Beans.AssistantDB())
            {
                studentsByName = db1.Etudiants.Include(e => e.ModuleEtudies).Where(a => a.nom == nom || a.prenom == nom || a.email == nom).ToList();

            }
            return studentsByName;
        }
    }
}
