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
using System.Configuration;
using Group_8_IOOP;

namespace Assignment
{
    
    public partial class Admin_TrainerList : Form
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public static string adminTP; //global

        public Admin_TrainerList()
        {
            InitializeComponent();
        }
        public Admin_TrainerList(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }

        private void Admin_TrainerList_Load(object sender, EventArgs e)
        {
            //show whole trainer table from db
            con.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Trainers", con);
            DataTable dtbl = new DataTable();
            sqlda.Fill(dtbl);

            dataGridView1.DataSource = dtbl;

            con.Close();
        }

        private void btnAAddTrainer_Click(object sender, EventArgs e)
        {
            //link to the add trainer page
            Admin_TL_AddTrainer aaddt = new Admin_TL_AddTrainer(adminTP);
            aaddt.Show();

            this.Hide();
        }

        private void btnADeleteTrainer_Click(object sender, EventArgs e)
        {
            //go to the delete trainer page
            Admin_TL_DeleteTrainer adltt = new Admin_TL_DeleteTrainer(adminTP);
            adltt.Show();

            this.Hide();
        }

        private void btnAAssignTrainer_Click(object sender, EventArgs e)
        {
            //got to the assign trainer page
            Admin_TL_AssignTrainer aassignt = new Admin_TL_AssignTrainer(adminTP);
            aassignt.Show();

            this.Hide();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //to show the whole trainer table again
            con.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Trainers", con);
            DataTable dtbl = new DataTable();
            sqlda.Fill(dtbl);

            dataGridView1.DataSource = dtbl;

            con.Close();
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            //back to admin main page
            Admin_MainPage amainpage = new Admin_MainPage();
            amainpage.Show();

            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            // go to the settings page

            Admin_Setting asetting = new Admin_Setting(adminTP);
            asetting.Show();

            this.Hide();
        }
    }
}
