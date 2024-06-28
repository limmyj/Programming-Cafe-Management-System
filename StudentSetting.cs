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
    public partial class StudentSetting : Form
    {
        public static string tpnum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public StudentSetting()
        {
            InitializeComponent();
        }
        public StudentSetting (string tp)
        {
            InitializeComponent();
            tpnum = tp;
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;

            StudentChangePassword f2 = new StudentChangePassword(TPNumber);
            f2.Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;

            frmStudentUpdateProfile f1 = new frmStudentUpdateProfile(TPNumber);
            f1.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;

            LectureMain f3 = new LectureMain(TPNumber);
            f3.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmStudentMainpage f2 = new frmStudentMainpage();
            f2.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void StudentSetting_Load(object sender, EventArgs e)
        {
            con.Open();
            lblRole.Text = "Student";
            SqlCommand cmd = new SqlCommand("SELECT * FROM Students where TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@tp", tpnum);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblTPNum.Text = dr["TPNumber"].ToString();
                lblName.Text = dr["Name"].ToString();
            }
            con.Close();
        }
    }
}
