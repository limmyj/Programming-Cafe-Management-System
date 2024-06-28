using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Group_8_IOOP;

namespace Assignment
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void forgotPW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrainerForgotPassword f1 = new TrainerForgotPassword();
            f1.Show();
            this.Hide();
        }

        private void currentHide_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            currentShow.Show();
            currentHide.Hide();
            txtPassword.PasswordChar = '*';
        }

        private void currentShow_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            currentHide.Show();
            currentShow.Hide();
            txtPassword.PasswordChar = '\0';
        }

        private void pboClear_Click(object sender, EventArgs e)
        {
            txtTPNum.Clear();
            txtPassword.Clear();
        }

        private void pboLogout_Click(object sender, EventArgs e)
        {
            Welcome f2 = new Welcome();
            f2.Show();
            this.Hide();
        }

        private void pboLogin_Click(object sender, EventArgs e)
        {
            string stat; //hold the login status --> pass/fail
            User obj1 = new User(txtTPNum.Text, txtPassword.Text);
            stat = obj1.login(txtTPNum.Text);
            if (stat != null)
            {
                MessageBox.Show(stat);
            }
            txtTPNum.Text = String.Empty;
            txtPassword.Text = String.Empty;

            //this.Hide();
        }
    }
}
