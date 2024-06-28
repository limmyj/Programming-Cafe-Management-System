using Group_8_IOOP;
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

namespace Assignment
{
    public partial class Admin_MainPage : Form
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        //global variable 
        public static string adminName;
        public static string adminTP;
        private string role;

        public Admin_MainPage()
        {
            InitializeComponent();
        }

        public Admin_MainPage(string tp)
        {
            InitializeComponent();
            //adminName = a;
            adminTP =tp;
        }
        

        private void btnATrainerList_Click(object sender, EventArgs e)
        {
            //redirect to trainer list page
            Admin_TrainerList atl = new Admin_TrainerList(adminTP);
            atl.Show();

            this.Hide();
        }

        private void btnAMonthlyIncomeReport_Click(object sender, EventArgs e)
        {
            //redirect to trainer MIR page
            Admin_MonthlyIncomeReport amir = new Admin_MonthlyIncomeReport(adminTP);
            amir.Show();

            this.Hide();
        }

        private void btnAFeedback_Click(object sender, EventArgs e)
        {
            //redirect to trainer feedback page
            Admin_Feedback afb = new Admin_Feedback(adminTP);
            afb.Show();

            this.Hide();
        }


        private void Admin_MainPage_Load(object sender, EventArgs e)
        {
            //showing welcome, adminName when form load
            Admin obj = new Admin(adminTP, adminName);
            lblAWelcome.Text = "Welcome, " + obj.adminWelcome();
            lblAName.Text = obj.adminWelcome();

            //showing the admin role
            lblARole.Text = "Admin";

            lblATPNum.Text = adminTP;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //go to settings 
            Admin_Setting asetting = new Admin_Setting(adminTP);
            asetting.Show();

            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //logout from this admin acc
            LoginPage logout = new LoginPage();
            logout.Show();

            this.Close();
        }

        private void pboAProfile_Click(object sender, EventArgs e)
        {
            ////redirect to admin own profile
            Admin_Profile ap = new Admin_Profile(adminTP);
            ap.Show();

            this.Hide();
        }
    }
}
