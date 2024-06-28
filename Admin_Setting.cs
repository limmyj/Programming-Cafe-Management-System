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
    public partial class Admin_Setting : Form
    {
        public static string adminTP; //global
        public static string adminName; //global
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public Admin_Setting()
        {
            InitializeComponent();
        }

        public Admin_Setting(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }


        private void btn_TL_ChangePW_Click(object sender, EventArgs e)
        {
            Admin_S_ChangePW acpw = new Admin_S_ChangePW(adminTP);
            acpw.Show();

            this.Hide();
        }

        private void btn_TL_UpdateProfile_Click(object sender, EventArgs e)
        {
            Admin_S_UpdateProfile aup = new Admin_S_UpdateProfile(adminTP);
            aup.Show();

            this.Hide();
        }

        private void pboALogout_Click(object sender, EventArgs e)
        {
            {
                LoginPage logout = new LoginPage();
                logout.Show();

                this.Hide();
            }
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            Admin_MainPage ap = new Admin_MainPage(adminTP);
            ap.Show();

            this.Hide();
        }

        private void Admin_Setting_Load(object sender, EventArgs e)
        {
            lblARole.Text = "Admin";

            lblTPNum.Text = adminTP;

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Admins WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", adminTP);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblName.Text = dr["Name"].ToString();
            }
            con.Close();
        }
    }
}