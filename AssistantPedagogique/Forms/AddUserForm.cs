using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfaces;
using AssistantPedagogique.Classes;
namespace AssistantPedagogique.Forms
{
    // Partial class for the 'AddUserForm' form in the application
    public partial class AddUserForm : Form
    {
        private readonly AssistantMain frm1;
        // Delegates and events for refreshing the DataGridView
        public delegate void DoEvent();
        public event DoEvent RefreshDgv;
        // Instances of the AuthEtuImpl class and StudentsManagement class
        AuthEtuImpl AuthEtu = new AuthEtuImpl(); 
        public AddUserForm(AssistantMain frm)
        {
            InitializeComponent();
            this.frm1 = frm;
        }

        StudentsManagement StudentsMng = (StudentsManagement)Activator.GetObject(typeof(StudentsManagement), "tcp://localhost:8085/StudentsManagementObj");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
             // Retrieves selected group and adds a student
            int selectedIndex = comboBox.SelectedIndex;
            comboBox.SelectedItem.ToString();
            int groupe = (int)comboBox.Items[selectedIndex];
            StudentsMng.AddStudent(NametextBox.Text, PrenomtextBox.Text, EmailtextBox.Text, PasswordtextBox.Text,groupe);
             // Registers the student to selected modules
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) 
            {
                if (!row.IsNewRow) 
                {
                    string id = row.Cells["id_module"].Value.ToString();
                    int id_module = Int32.Parse(id);
                    Beans.Etudiant etudiant =new Beans.Etudiant();
                    etudiant =AuthEtu.getStudent(EmailtextBox.Text,PasswordtextBox.Text);
                    StudentsMng.RegisterToSubject(etudiant.id_etudiant,id_module);
                }
            }
            //frm1.dataGridView1.Update();
            //frm1.dataGridView1.Refresh();

            // Refreshes the DataGridView and closes the form
            this.RefreshDgv();
            this.Close();
        }
         // Event handler for the form load
        private void AddUserForm_Load(object sender, EventArgs e)
        {
            // Adds items to the ComboBox
            comboBox.Items.Add(1);
            comboBox.Items.Add(2);
            comboBox.Items.Add(3);
            comboBox.ValueMember = "groupe";
            comboBox.DisplayMember = "groupe";

            // Retrieves modules and populates the DataGridView
            List<Beans.module2> allModules=StudentsMng.getModules();
            var bindingList = new BindingList<Beans.module2>(allModules);
            var source = new BindingSource(bindingList, null);
           
            dataGridView1.DataSource = allModules;
            this.dataGridView1.Columns[0].Visible = false;
        }
    }
}
