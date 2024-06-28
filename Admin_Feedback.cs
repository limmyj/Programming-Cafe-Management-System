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
    public partial class Admin_Feedback : Form
    {
        public static string adminTP; //global

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public Admin_Feedback()
        {
            InitializeComponent();
        }
        public Admin_Feedback(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }

        private void Admin_Feedback_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Feedback", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            Admin_MainPage amainpage = new Admin_MainPage();
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
