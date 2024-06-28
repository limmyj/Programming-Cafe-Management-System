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
    public partial class Admin_Profile : Form
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public static string adminTP; //global
        public Admin_Profile()
        {
            InitializeComponent();
        }
        public Admin_Profile(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }

        private void Admin_Profile_Load(object sender, EventArgs e)
        {
            lblARole.Text = "Admin";

            lblAProfile_TP.Text = adminTP;
            Admin obj1 = new Admin(adminTP);

            Admin.adminProfile(obj1);

            lblAProfile_Name.Text = obj1.adminName;
            lblAProfile_Email.Text = obj1.AdminEmail;
            lblAProfile_ContactNum.Text = obj1.AdminContact;
            lblAProfile_Address.Text = obj1.AdminAddress;
            lblAProfile_Bio.Text = obj1.AdminBio;
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            Admin_MainPage amainpage = new Admin_MainPage(adminTP);
            amainpage.Show();

            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            Admin_Setting asetting = new Admin_Setting(adminTP);
            asetting.Show();

            this.Hide();
        }
    }
}
