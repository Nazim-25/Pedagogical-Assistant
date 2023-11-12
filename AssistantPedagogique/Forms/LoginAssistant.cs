using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssistantPedagogique.Forms
{
    public partial class LoginAssistant : Form
    {
        public LoginAssistant()
        {
            InitializeComponent();
        }

        private void LoginAssistant_Load(object sender, EventArgs e)
        {
            
        }

        private void click_exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void Key(TextBox tx)
        {
            tx.SelectAll();
            tx.Focus();
        }
        public void Save() {

            if (EmailBox.Text == "" || PasswordBox.Text == "")
            {
                if (EmailBox.Text == "")
                {
                    Key(EmailBox);
                }
                else
                {
                    Key(PasswordBox);
                }
            }
            else
            {
                if (EmailBox.Text == "Admin@gmail.com" && PasswordBox.Text == "admin")
                {
                this.Hide();
                Forms.AssistantMain main = new Forms.AssistantMain();
                main.ShowDialog();
                }
                else if(EmailBox.Text != "Admin@gmail.com" )
                {
                    EmailBox.BackColor=Color.Red;
                    Key(EmailBox);
                }else
                {
                    PasswordBox.BackColor=Color.Red;
                    Key(PasswordBox);
                }
            }
        }

        private void EmailBox_TextChanged(object sender, EventArgs e)
        {
            EmailBox.BackColor = Color.White;
        }

        private void LoginAssistant_Load_1(object sender, EventArgs e)
        {

        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            PasswordBox.BackColor = Color.White;
        }
    }
}
