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
    public partial class LectureRegistrationForm : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public static string TPNum;
        public LectureRegistrationForm()
        {
            InitializeComponent();
        }
        public LectureRegistrationForm(string tp)
        {
            InitializeComponent();
            TPNum = tp;
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            if (txtTPNum.Text == "" || txtName.Text == "" || txtEmail.Text == "" || txtContact.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Please enter a valid input");
            }
            else
            {
                Lecture obj1 = new Lecture(txtTPNum.Text, txtName.Text, txtEmail.Text, txtContact.Text, txtAddress.Text);
                MessageBox.Show(obj1.studentRegistration());

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students where TPNumber = @tp", con);
                cmd.Parameters.AddWithValue("@tp", txtTPNum.Text);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblTPNum.Text = dr["TPNumber"].ToString();
                    lblName.Text = dr["Name"].ToString();
                    lblEmail.Text = dr["Email"].ToString();
                    lblContactNum.Text = dr["ContactNumber"].ToString();
                }
                con.Close();
            }
            txtTPNum.Text = string.Empty;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            LectureSetting f1 = new LectureSetting(TPNumber);
            f1.Show();//allows to move to Profile Page
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            LectureMain f2 = new LectureMain();
            f2.Show(); //allows to move to Login Page
            this.Hide();
        }
    }
}
