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
    public partial class Admin_S_UpdateProfile : Form
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        private string role;

        public static string adminTP; //global

        public Admin_S_UpdateProfile()
        {
            InitializeComponent();
        }

        public Admin_S_UpdateProfile(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }

        private void Admin_S_UpdateProfile_Load(object sender, EventArgs e)
        {
            lblARole.Text = "Admin";

            Admin obj1 = new Admin(adminTP);
            Admin.adminProfile(obj1);

            lblAProfile_TP.Text = adminTP;
            txtASetting_NewEmail.Text = obj1.AdminEmail;
            txtASetting_NewContact.Text = obj1.AdminContact;
            txtASetting_NewAddress.Text = obj1.AdminAddress;
            txtASetting_NewBio.Text = obj1.AdminBio;
        }

        private void btnASetting_UpdateProfile_Click(object sender, EventArgs e)
        {
            //call method
            Admin obj1 = new Admin(adminTP);
            MessageBox.Show(obj1.updateAdminProfile(txtASetting_NewEmail.Text, txtASetting_NewContact.Text, txtASetting_NewAddress.Text, txtASetting_NewBio.Text));
            txtASetting_NewEmail.Text = string.Empty;
            txtASetting_NewContact.Text = string.Empty;
            txtASetting_NewAddress.Text = string.Empty;
            txtASetting_NewBio.Text = string.Empty;

            if (txtASetting_NewBio.Text.Length > 150)
            {
                MessageBox.Show("Your biodata are exceeding the limit word count.");
            }
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            {
                Admin_MainPage amainpage = new Admin_MainPage(adminTP);
                amainpage.Show();

                this.Hide();
            }
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

        private void txtASetting_NewBio_TextChanged(object sender, EventArgs e)
        {
            lblAProfile_WordCount.Text = txtASetting_NewBio.Text.Length.ToString() + "/150";
        }
    }
}