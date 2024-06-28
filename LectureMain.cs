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
    public partial class LectureMain : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public LectureMain()
        {
            InitializeComponent();
        }

        public LectureMain(string tp)
        {
            InitializeComponent();
            TPNum = tp;
        }

        private void LectureMain_Load_1(object sender, EventArgs e)
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
            lblWelcome.Text = "Hello, " + TPNum;
        }

        private void pboProfile_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            LectureProfile f1 = new LectureProfile(TPNumber);
            f1.Show(); //allows to move to Profile Page
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            LoginPage f2 = new LoginPage();
            f2.Show(); //allows to move to Login Page
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            LectureSetting f3 = new LectureSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void btnRegistration_Click_1(object sender, EventArgs e)
        {
            //className ObjectName = new className();
            string TPNumber = TPNum;
            LectureRegistrationForm f1 = new LectureRegistrationForm(TPNumber);
            f1.Show();//allows to move to Registration Page
            this.Hide();
        }

        private void btnEnrolment_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            LectureEnrollment f1 = new LectureEnrollment(TPNumber);
            f1.Show();// allows to move to Enrolment Page
            this.Hide();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            LectureMyRequest f1 = new LectureMyRequest();
            f1.Show();//allows to move to My Request Page
            this.Hide();
        }
    }
}
