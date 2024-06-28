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
    public partial class frmStudentMainpage : Form
    {
        public static string tpnum;//global
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public frmStudentMainpage()
        {
            InitializeComponent();
        }

        public frmStudentMainpage(string tp)
        {
            InitializeComponent();
            tpnum = tp;
        }
        private void btnSchedule_Click(object sender, EventArgs e)
        {
            string tpNum = tpnum;
            frmStudentSchedule f1 = new frmStudentSchedule(tpNum);
            f1.Show();
            this.Hide();        
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            string tpNum = tpnum;
            frmStudentRequest f2 = new frmStudentRequest(tpNum);
            f2.Show();
            this.Hide();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            string tpNum = tpnum;
            frmStudentPayment f3 = new frmStudentPayment(tpNum);
            f3.Show();
            this.Hide();
        }

        private void frmStudentMainpage_Load(object sender, EventArgs e)
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
            lblStudent.Text = "Hello, " + tpnum + " !";
        }

        private void pboProfile_Click(object sender, EventArgs e)
        {
            string tpNum = tpnum;
            frmStudentProfile f4 = new frmStudentProfile(tpNum);
            f4.Show();
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
            string TPNumber = tpnum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
