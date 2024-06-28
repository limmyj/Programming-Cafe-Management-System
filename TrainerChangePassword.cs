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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Assignment
{
    public partial class TrainerChangePassword : Form
    {
        public static string TPNum;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public TrainerChangePassword()
        {
            InitializeComponent();
        }
        public TrainerChangePassword(string n)
        {
            InitializeComponent();
            TPNum = n;
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
                if (txtNew.Text == txtConfirm.Text) //if new password with the confirm new password same then update the new password to users table
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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrent.Text))
            {
                MessageBox.Show("Please insert current password.");
            }
            else
            {
                //to check the password that user enter
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
        }

        private void TrainerChangePassword_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            lblTPNum.Text = TPNum;
        }

        private void currentShow_Click(object sender, EventArgs e)
        {
            txtCurrent.UseSystemPasswordChar = false;
            currentHide.Show();
            currentShow.Hide();
            txtCurrent.PasswordChar = '\0';
        }

        private void currentHide_Click(object sender, EventArgs e)
        {
            txtCurrent.UseSystemPasswordChar = false;
            currentShow.Show();
            currentHide.Hide();
            txtCurrent.PasswordChar = '*';
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            TrainerSetting f6 = new TrainerSetting(TPNumber);
            f6.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            TrainerMain f4 = new TrainerMain();
            f4.Show(); //allows to move to Login Page
            this.Hide();
        }
    }
}
