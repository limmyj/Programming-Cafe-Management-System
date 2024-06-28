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
    public partial class Admin_S_ChangePW : Form
    {
        public static string adminTP; //global
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public Admin_S_ChangePW()
        {
            InitializeComponent();
        }

        public Admin_S_ChangePW(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }


        private void Admin_S_ChangePW_Load(object sender, EventArgs e)
        {
            panel1.Visible = false; //do not show panel

            lblARole.Text = "Admin";
            lblAProfile_TP.Text = adminTP;
        }

        private void btnASetting_CheckCurrentPW_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtASetting_CurrentPW.Text))
            {
                MessageBox.Show("Please insert current password.");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE TPNumber = '" + adminTP + "' AND Password = '"+ txtASetting_CurrentPW.Text + "'", con);
                SqlDataReader cpw = cmd.ExecuteReader();
                if (cpw.Read())
                {
                    MessageBox.Show("Currect password correct" + adminTP + txtASetting_CurrentPW.Text);
                    panel1.Visible = true;
                }
                else
                {
                    MessageBox.Show("Currect password incorrect");
                }
                cpw.Close();
                con.Close();
                txtASetting_CurrentPW.Text = string.Empty;
            }
        }

        private void btnASetting_SavePW_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtASetting_NewPW.Text))
            {
                MessageBox.Show("Please insert new password.");
            }
            if (string.IsNullOrWhiteSpace(txtASetting_ConfirmPW.Text))
            {
                MessageBox.Show("Please reinsert new password.");
            }
            else
            {
                if (txtASetting_NewPW.Text == txtASetting_ConfirmPW.Text)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Password = '"+ txtASetting_ConfirmPW .Text+ "' WHERE TPNumber = '"+ adminTP +"'", con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Password had been changed successfully.");
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Password does not match.");
                }
                txtASetting_NewPW.Text = string.Empty;
                txtASetting_ConfirmPW.Text = string.Empty;
            }
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            Admin_MainPage amainpage = new Admin_MainPage(adminTP);
            amainpage.Show();

            this.Hide();
        }

        private void pboAReturn_Click(object sender, EventArgs e)
        {
            Admin_Setting asetting = new Admin_Setting();
            asetting.Show();

            this.Hide();
        }

        private void pboProfile_Click(object sender, EventArgs e)
        {
            Admin_Profile ap = new Admin_Profile(adminTP);
            ap.Show();

            this.Hide();
        }
    }
}
