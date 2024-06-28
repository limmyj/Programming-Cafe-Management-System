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
    public partial class StudentViewRequest : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public string TPNumber;
        public StudentViewRequest()
        {
            InitializeComponent();
        }
        public StudentViewRequest(string tp)
        {
            InitializeComponent();
            TPNumber = tp;
        }

        public void displayData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Requests where TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@tp", TPNumber);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            DataTable dt1 = dt;
            dataGridView1.DataSource = dt1;

            con.Close();
        }

        private void StudentViewRequest_Load(object sender, EventArgs e)
        {
            displayData();

            con.Open();
            lblRole.Text = "Student";
            SqlCommand cmd = new SqlCommand("SELECT * FROM Students where TPNumber = @tp", con);
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
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            frmStudentRequest f4 = new frmStudentRequest();
            f4.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNum = TPNumber;
            StudentSetting f3 = new StudentSetting(TPNum);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
