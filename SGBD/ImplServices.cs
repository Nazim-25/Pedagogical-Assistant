using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Interfaces;
using System.Data.Entity;
//using AssistantPedagogique.Classes;
using Beans;

namespace SGBD
{
     // Class implementing 'AccesAuxDonnees' interface
    // This class deals with database operations
    class ImplServices : MarshalByRefObject, AccesAuxDonnees
    {
        public final connetionString = "Data Source=DESKTOP-203420U\\NAZIM;Initial Catalog=AssistantDB;Integrated Security=True";
        public ImplServices()
        {

        }
        //Beans.AssistantDB db = new Beans.AssistantDB();

       

        // Method to authenticate an instructor using email and password
        public Boolean AuthentificationEnseignant(string email, string password)
        {
            // SQL connection establishment
            string connetionString = null;
            SqlConnection con;
            con = new SqlConnection(connetionString);

             // SQL command to check instructor credentials
            SqlCommand cmd = new SqlCommand("Select * from Enseignant where email= @email and  password= @password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Boolean b = false;
            // Checking and setting a boolean based on the read data
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    b = true;
                    break;
                }

            }
            con.Close();
            return b;

        }

        // Method to retrieve module by ID
        public Beans.module2 getModule(int id_module)
        {

            // SQL connection establishment
            string connetionString = null;
            SqlConnection con;
            con = new SqlConnection(connetionString);

            // SQL command to retrieve module information
            SqlCommand cmd = new SqlCommand("Select * from module2 where id_module= @Id", con);
            cmd.Parameters.AddWithValue("@Id", id_module);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Beans.module2 module = new Beans.module2();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    module.id_module = int.Parse(dr[0].ToString());
                    module.nom_module = dr[1].ToString();
                    module.abrv_module = dr[2].ToString();
                    module.coefficient = int.Parse(dr[3].ToString());
                }
            }
            con.Close();
            return module;

        }

        // Method to retrieve instructor by email and password
        public Beans.Enseignant getEnseignant(string email, string password)
        {

            string connetionString = null;
            SqlConnection con;
            con = new SqlConnection(connetionString);

            SqlCommand cmd = new SqlCommand("Select * from Enseignant where email= @email and  password= @password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();


            Beans.Enseignant e = new Beans.Enseignant();
            int id_module = 0;
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    // Populating the Enseignant object with retrieved data
                    e.id_enseignant = int.Parse(dr[0].ToString());
                    e.nom = dr[1].ToString();
                    e.prenom = dr[2].ToString();
                    e.email = dr[3].ToString();
                    e.password = dr[4].ToString();
                    e.id_module = int.Parse(dr[5].ToString());
                    id_module = int.Parse(dr[5].ToString());

                }

            }
            // Fetches module details and sets it in the Enseignant object
            e.module2 = getModule(id_module);
            con.Close();
            return e;
        }
        // Retrieves instructor information using Entity Framework and related module information
        public Beans.Enseignant getEnseignant2(string email, string password)
        {
            try
            {
                using (var db1 = new Beans.AssistantDB())
                {
                    // Fetching instructor and related module using Entity Framework
                    List<Beans.module2> modules = db1.module2.ToList();
                    Beans.Enseignant e = db1.Enseignants.FirstOrDefault(a => a.email == email && a.password == password);
                    foreach (Beans.module2 m2 in modules)
                    {
                        if (m2.id_module == e.id_module)
                        {
                            e.module2 = m2;

                        }
                    }
                    return e;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        List<Beans.Enseignant> allTeachers;

        // Retrieves a list of all instructors
        public List<Beans.Enseignant> getEnseignants()
        {
            using (var db1 = new Beans.AssistantDB())
            {
                  // Retrieves all instructors using Entity Framework
                allTeachers = db1.Enseignants.ToList();
            }
            return allTeachers;
        }


        // Authenticates an instructor using Entity Framework
        public Boolean AuthentificationEnseignant2(string email, string password)
        {
            using (var db1 = new Beans.AssistantDB())
            {
                 // Checks if the instructor exists based on the provided email and password
                if (db1.Enseignants.Local.Any(a => a.email == email && a.password == password) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        // Initiates a session for an instructor with a specific module using Entity Framework
        public void startSeance(int id_enseignant, int id_module)
        {
            DateTime now = DateTime.Now;
            using (var db1 = new Beans.AssistantDB())
            {
                 // Creates a new session with associated instructor and module
                Beans.Seance2 seance = new Beans.Seance2();
                {
                    seance.id_enseignant = id_enseignant;
                    seance.id_module = id_module;
                    seance.date = now;
                }
                db1.Seance2.Add(seance);
                db1.SaveChanges();
            }

        }
            // Authenticates student based on email and password 
        public Boolean AuthentificationEtudiant(string email, string password)
        {
            string connetionString = null;
            SqlConnection con;
            con = new SqlConnection(connetionString);

            SqlCommand cmd = new SqlCommand("Select * from etudiant where email= @email and  password= @password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Boolean b = false;
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    b = true;
                    break;
                }

            }
            con.Close();
            return b;
        }
       
        // Retrieves a student entity using Entity Framework based on email and password
        public Beans.Etudiant getEtudiant(string email, string password)
        {
            Beans.Etudiant etudiant = new Beans.Etudiant();
        
            
            using (var db1 = new Beans.AssistantDB())
            {
               // Fetches a student and associated modules using Entity Framework
               etudiant = db1.Etudiants.Include(e => e.ModuleEtudies).FirstOrDefault(a => a.email == email && a.password == password);
               return etudiant;

            }
        }
        // Retrieves a student entity based on ID using Entity Framework
        public Beans.Etudiant getStudent(int id)
        {
            Beans.Etudiant etudiant = new Beans.Etudiant();
            using (var db1 = new Beans.AssistantDB())
            {
                etudiant = db1.Etudiants.Include(e => e.ModuleEtudies).FirstOrDefault(a => a.id_etudiant == id );
                return etudiant;

            }
        }
        // Retrieves the ID of the current session for a given instructor using Entity Framework
         public int getCurrentSession(int id_enseignant) 
        {
            
            using (var db1 = new Beans.AssistantDB())
            {
                // Fetches the latest session ID for a specific instructor using Entity Framework
                DateTime now = DateTime.Now;
                List<Beans.Seance2> seances = db1.Seance2.Where(a => a.id_enseignant == id_enseignant).OrderByDescending(s=>s.date).ToList();
                Beans.Seance2 seance = seances.First();

                return seance.id_seance;

            }
        }
        // Registers a student to a specific session 
         public void RegisterToSession(int id_seance, int id_etudiant)
         {
             using (var db1 = new Beans.AssistantDB())
             {

                 // Creates a session attendance record for a student in a specific session
                 Beans.SeanceAssiste seanceAssiste = new Beans.SeanceAssiste();
                 {
                     seanceAssiste.id_etudiant = id_etudiant;
                     seanceAssiste.id_seance = id_seance;
                     
                 }
                 db1.SeanceAssistes.Add(seanceAssiste);
                 db1.SaveChanges();
             }
         }
        // Retrieves a list of students for a specific module 
         public List<Beans.Etudiant> getStudentsByModule(int id_module) 
         {
             List<Beans.Etudiant> StudentsByModule=new List<Beans.Etudiant>();
             using (var db1 = new Beans.AssistantDB())
             {
                // Fetches students enrolled in a particular module 
                  StudentsByModule = db1.ModuleEtudies.Where(o => o.id_module == id_module).Select(o => o.Etudiant) .Distinct().ToList();
             }
             return StudentsByModule;
         }
        // Retrieves instructor information based on the instructor's ID
        public Beans.Enseignant getTeacherById(int id_enseignant) 
         {
             Beans.Enseignant enseignant = new Beans.Enseignant();
             using (var db1 = new Beans.AssistantDB())
             {
                 // Fetches an instructor and associated session information
                 enseignant = db1.Enseignants.Include(e => e.Seance2).FirstOrDefault(a => a.id_enseignant == id_enseignant);
                 return enseignant;
             }
         
         }

        // Retrieves the ModuleEtudie entity for a specific student and module
        public Beans.ModuleEtudie getModuleEtudie(int id_etudiant, int id_module)
        {
            Beans.ModuleEtudie moduleEtudie = new Beans.ModuleEtudie();
            using (var db1 = new Beans.AssistantDB())
            {
                // Fetches the ModuleEtudie entity for a given student and module
                moduleEtudie = db1.ModuleEtudies.FirstOrDefault(a => a.id_module != null && a.id_module == id_module && a.id_etudiant == id_etudiant);
                return moduleEtudie;
            }

        }


        // Retrieves instructor information based on the instructor's ID
        public void AddAbsence(List<Beans.Etudiant> absStudents,int id_module)
        {
           

            using (var db1 = new Beans.AssistantDB())
            {
                foreach (Beans.Etudiant e in absStudents)
                {
                    // Updates the absence count for each student in a specific module 
                    Beans.ModuleEtudie module = db1.ModuleEtudies.FirstOrDefault(a => a.id_module != null && a.id_module == id_module && a.id_etudiant == e.id_etudiant);
                    module.nbr_absence = module.nbr_absence + 1;
                    db1.SaveChanges();
                }
                
            }
        }
        // Retrieves a list of excluded students for a specific module based on their absence count 
        public List<Beans.Etudiant> getExcludedStudents(int id_module)
        {

            List<Beans.Etudiant> ExcludedStudents = new List<Beans.Etudiant>();
            using (var db1 = new Beans.AssistantDB())
            {
                 // Fetches students excluded from a module due to excessive absences
                List<Beans.ModuleEtudie> ExcludedStudentsIds = db1.ModuleEtudies.Where(a => a.id_module != null && a.id_module == id_module && a.nbr_absence >= 3).ToList();
                foreach (Beans.ModuleEtudie md in ExcludedStudentsIds)
                {
                    Beans.Etudiant e = getStudent((int)md.id_etudiant);
                    ExcludedStudents.Add(e);
                }
            }
            return ExcludedStudents;
        }
       // Saves file information for a specific session
        public void SaveFile(int id_seance,Byte[] buffer,string extn,string name)
        {
            string connetionString = null;
            SqlConnection con;
            con = new SqlConnection(connetionString);

            SqlCommand cmd = new SqlCommand("UPDATE SEANCE2 SET Data=@data ,Extension=@extn ,FileName=@name where id_seance=@id_seance ", con);
            // Parameterized values for the SQL command
            cmd.Parameters.AddWithValue("@id_seance", id_seance);
            cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = name;
            cmd.Parameters.AddWithValue("@data", SqlDbType.VarBinary).Value=buffer;
            cmd.Parameters.AddWithValue("@extn", SqlDbType.Char).Value = extn;
           
            con.Open();
            cmd.ExecuteNonQuery();

        
        }
        // Retrieves session details based on the session ID
       public Beans.Seance2 getSeance(int id_seance)
        {
          
          string connetionString = null;
          SqlConnection con;

          con = new SqlConnection(connetionString);
        // SQL command to select a session by ID
          SqlCommand cmd = new SqlCommand("Select * from Seance2 where id_seance= @Id", con);
          cmd.Parameters.AddWithValue("@Id", id_seance);

          con.Open();
          SqlDataReader dr = cmd.ExecuteReader();
          Beans.Seance2 seance = new Beans.Seance2();
          while (dr.Read())
          {
              if (dr.HasRows == true)
              {
                 // Retrieving session details
                  seance.id_seance = int.Parse(dr[0].ToString());
                  seance.date = Convert.ToDateTime(dr[1].ToString());
                  seance.id_enseignant = int.Parse(dr[2].ToString());
                  seance.id_module = int.Parse(dr[3].ToString());
                  seance.Data = (byte[])dr[4];
                  seance.Extension = dr[5].ToString();
                  seance.FileName = dr[6].ToString();
              }
          }
          con.Close();

          
           return seance;
        }

        // Saves a file for a specific student and session
       public void SaveFileEtudiant(int id_etudiant, int id_seance, Byte[] buffer, string extn, string name) 
       {
           string connetionString = null;
           SqlConnection con;
           con = new SqlConnection(connetionString);
            // SQL command to insert a file record for a student and session
           SqlCommand cmd = new SqlCommand("Insert into RarFiles(id_etudiant,[File],FileName,Extension,id_seance) values (@id_etudiant,@data,@name,@extn,@id_seance)", con);
           
           cmd.Parameters.AddWithValue("@id_etudiant", id_etudiant);
           cmd.Parameters.AddWithValue("@data", SqlDbType.VarBinary).Value = buffer;
           cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = name;
           cmd.Parameters.AddWithValue("@extn", SqlDbType.Char).Value = extn;
           cmd.Parameters.AddWithValue("@id_seance", id_seance);

           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       
       }
        // Retrieves a file record for a specific session and student 
      public Beans.RarFile getFileZip(int id_seance, int id_etudiant) 
       {

           string connetionString = null;
           SqlConnection con;
           con = new SqlConnection(connetionString);
             // SQL command to select a file record by session and student ID
           SqlCommand cmd = new SqlCommand("Select * from RarFiles where id_seance= @Id and id_etudiant= @Id_etudiant", con);
           cmd.Parameters.AddWithValue("@Id", id_seance);
           cmd.Parameters.AddWithValue("@Id_etudiant", id_etudiant);

           con.Open();
           SqlDataReader dr = cmd.ExecuteReader();
           Beans.RarFile RarFile = new Beans.RarFile();
           while (dr.Read())
           {
               if (dr.HasRows == true)
               {
                    // Retrieving file record details
                   RarFile.id_file = int.Parse(dr[0].ToString());
                   RarFile.id_etudiant = int.Parse(dr[1].ToString());
                   RarFile.File = (byte[])dr[2];
                   RarFile.FileName = dr[3].ToString();
                   RarFile.Extension = dr[4].ToString();
                   RarFile.id_seance = int.Parse(dr[5].ToString());
               }
           }
           con.Close();
           return RarFile;
       }
 
    }  
    }

