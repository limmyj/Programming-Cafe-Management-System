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
    public partial class LectureSetting : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public LectureSetting()
        {
            InitializeComponent();
        }
        public LectureSetting(string tp)
        {
            InitializeComponent();
            TPNum = tp;
        }

        private void LectureSetting_Load(object sender, EventArgs e)
        {
            con.Open();
            lblRole.Text = "Lecture";
            SqlCommand cmd = new SqlCommand("SELECT * FROM Lecturers where TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@tp", TPNum);
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

        private void pboExit_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            LectureMain f3 = new LectureMain(TPNumber);
            f3.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            LectureSetting f3 = new LectureSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void btnPassword_Click_1(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            LectureChangePassword f2 = new LectureChangePassword(TPNumber);
            f2.Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            LectureUpdateProfile f1 = new LectureUpdateProfile(TPNumber);
            f1.Show();
            this.Hide();
        }
    }
}
