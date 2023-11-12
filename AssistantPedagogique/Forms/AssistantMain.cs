using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Beans;
using Interfaces;
namespace AssistantPedagogique.Forms
{
    public partial class AssistantMain : Form
    {
        StudentsManagement StudentsMng = (StudentsManagement)Activator.GetObject(typeof(StudentsManagement), "tcp://localhost:8085/StudentsManagementObj");
        DataTable table = new DataTable("table");

        public AssistantMain()
        {
            InitializeComponent();
            fm_RefreshDgv();
        }
        List<Beans.Etudiant> AllStudents = new List<Beans.Etudiant>();
        void fm_RefreshDgv()
        {
            dataGridView1.Rows.Clear();
            AllStudents = StudentsMng.getAllStudients();
            // var bindingList = new BindingList<Beans.Etudiant>(AllStudents);
            // var source = new BindingSource(bindingList, null);
            /*
             DataGridViewCheckBoxColumn nom = new DataGridViewCheckBoxColumn();
             nom.Name = "nom";
             dataGridView1.Columns.Add(nom);
             DataGridViewCheckBoxColumn prenom = new DataGridViewCheckBoxColumn();
             nom.Name = "prenom";
             dataGridView1.Columns.Add(prenom);
             DataGridViewCheckBoxColumn email = new DataGridViewCheckBoxColumn();
             nom.Name = "email";
             dataGridView1.Columns.Add(email);
             DataGridViewCheckBoxColumn password = new DataGridViewCheckBoxColumn();
             nom.Name = "password";
             dataGridView1.Columns.Add(password);
             DataGridViewComboBoxColumn combo1 = new DataGridViewComboBoxColumn();
             combo1.Name = "ModuleEtudie";
             dataGridView1.Columns.Add(combo1);
             * */
            foreach (Beans.Etudiant etudiant in AllStudents)
            {
                List<Beans.ModuleEtudie> moduleEtudie = etudiant.ModuleEtudies.ToList();
                List<string> moduleNames = new List<string>();
                foreach (Beans.ModuleEtudie mdEtudie in moduleEtudie)
                {
                    Beans.module2 md = StudentsMng.getModuleName((int)mdEtudie.id_module);
                    moduleNames.Add(md.abrv_module);
                }
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = etudiant.id_etudiant;
                row.Cells[1].Value = etudiant.nom;
                row.Cells[2].Value = etudiant.prenom;
                row.Cells[3].Value = etudiant.email;
                row.Cells[4].Value = etudiant.password;
                ((DataGridViewComboBoxCell)row.Cells[5]).DataSource = moduleNames;
                //this.dataGridView1.Columns[0].Visible = false;
                dataGridView1.Rows.Add(row);

            }

            //  dataGridView1.DataSource = AllStudents;
        }
        private void AssistantMain_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'assistantDBDataSet.Etudiant'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.etudiantTableAdapter.Fill(this.assistantDBDataSet.Etudiant);
            /*
            table.Columns.Add("Id_etudiant", Type.GetType("System.Int32"));
            table.Columns.Add("nom", Type.GetType("System.String"));
            table.Columns.Add("prenom", Type.GetType("System.String"));
            table.Columns.Add("email", Type.GetType("System.String"));
            table.Columns.Add("password", Type.GetType("System.String"));
            table.Columns.Add("groupe", Type.GetType("System.Int32"));
           // table.Columns.Add("nomBinome", Type.GetType("System.String"));
            //table.Columns.Add("prenomBinome", Type.GetType("System.String"));
            dataGridView1.DataSource = table;
            */


        }

        private void AddStudent_Click(object sender, EventArgs e)
        {
            AddUserForm main = new AddUserForm(this);
            main.RefreshDgv += new AddUserForm.DoEvent(fm_RefreshDgv);
            main.ShowDialog();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string selected_eid = dataGridView1.CurrentRow.Cells["id_etudiant"].Value.ToString();
            StudentsMng.DeleteStudent(Int32.Parse(selected_eid));
            /*
            dataGridView1.Update();
            dataGridView1.Refresh();
            */
            fm_RefreshDgv();



        }

        private void Search_btn_Click(object sender, EventArgs e)
        {
            List<Beans.Etudiant> StudentsByName = new List<Beans.Etudiant>();
            if (String.IsNullOrEmpty(SearchTextBox.Text))
            {
                MessageBox.Show(" :" + SearchTextBox.Text);
                fm_RefreshDgv();
            }
            else
            {
                MessageBox.Show(" :" + SearchTextBox.Text);
                StudentsByName = StudentsMng.getStudentsByName(SearchTextBox.Text);
                
                dataGridView1.Rows.Clear();
                
                foreach (Beans.Etudiant etudiant in StudentsByName)
                {
                    MessageBox.Show(" :" + etudiant.email);
                    
                    List<Beans.ModuleEtudie> moduleEtudie = etudiant.ModuleEtudies.ToList();
                    List<string> moduleNames = new List<string>();
                    foreach (Beans.ModuleEtudie mdEtudie in moduleEtudie)
                    {
                        Beans.module2 md = StudentsMng.getModuleName((int)mdEtudie.id_module);
                        moduleNames.Add(md.abrv_module);
                    }
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = etudiant.id_etudiant;
                    row.Cells[1].Value = etudiant.nom;
                    row.Cells[2].Value = etudiant.prenom;
                    row.Cells[3].Value = etudiant.email;
                    row.Cells[4].Value = etudiant.password;
                    ((DataGridViewComboBoxCell)row.Cells[5]).DataSource = moduleNames;
                    //this.dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Rows.Add(row);
                     
                }
                 
            }
            
        }
    }
}
