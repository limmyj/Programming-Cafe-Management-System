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
    public partial class LectureUpdateProfile : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public string TPNumber;
        public LectureUpdateProfile()
        {
            InitializeComponent();
        }
        public LectureUpdateProfile(string tp)
        {
            InitializeComponent();
            TPNumber = tp;
        }

        private void LectureUpdateProfile_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Lecturers where TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@tp", TPNumber);
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

            Lecture obj1 = new Lecture(TPNumber);
            Lecture.ViewProfile(obj1);
            txtEmail.Text = obj1.Email;
            txtContactNum.Text = obj1.ContactNumber;
            txtAddress.Text = obj1.Address1;
            txtBioData.Text = obj1.BioData1;
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            LectureMain f2 = new LectureMain();
            f2.Show(); //allows to move to Login Page
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNum = TPNumber;
            LectureSetting f3 = new LectureSetting(TPNum);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            Lecture obj1 = new Lecture(TPNumber);
            MessageBox.Show(obj1.updateProfile(txtEmail.Text, txtContactNum.Text, txtAddress.Text, txtBioData.Text));
            txtEmail.Text = string.Empty;
            txtContactNum.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtBioData.Text = string.Empty;
        }
    }
}
