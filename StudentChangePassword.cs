using Assignment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group_8_IOOP
{
    public partial class StudentChangePassword : Form
    {
        public static string TPNum;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public StudentChangePassword()
        {
            InitializeComponent();
        }
        public StudentChangePassword(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE TPNumber = @TP AND Password = @p", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);
            cmd.Parameters.AddWithValue("@p", txtCurrent.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Currect password correct");
                panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("Currect password incorrect");
            }
            dr.Close();
            con.Close();
            txtCurrent.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNew.Text))
            {
                MessageBox.Show("Please insert new password.");
            }
            if (string.IsNullOrWhiteSpace(txtConfirm.Text))
            {
                MessageBox.Show("Please reinsert new password.");
            }
            else
            {
                if (txtNew.Text == txtConfirm.Text)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Password = @new WHERE TPNumber = @TP", con);
                    cmd.Parameters.AddWithValue("@TP", TPNum);
                    cmd.Parameters.AddWithValue("@new", txtConfirm.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Password had been changed successfully.");
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Password does not match.");
                }
                txtNew.Text = string.Empty;
                txtConfirm.Text = string.Empty;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LectureSetting f1 = new LectureSetting();
            f1.Show();
            this.Hide();
        }

        private void currentShow_Click_1(object sender, EventArgs e)
        {
            txtCurrent.UseSystemPasswordChar = false;
            currentHide.Show();
            currentShow.Hide();
            txtCurrent.PasswordChar = '\0';
        }

        private void currentHide_Click_1(object sender, EventArgs e)
        {
            txtCurrent.UseSystemPasswordChar = false;
            currentShow.Show();
            currentHide.Hide();
            txtCurrent.PasswordChar = '*';
        }

        private void StudentChangePassword_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            lblStudentTPNumber.Text = TPNum;
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            frmStudentMainpage f2 = new frmStudentMainpage();
            f2.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
