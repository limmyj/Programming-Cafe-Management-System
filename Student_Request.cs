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
    public partial class frmStudentRequest : Form
    {
        public static string tpnum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public frmStudentRequest()
        {
            InitializeComponent();
        }

        public frmStudentRequest(string tp)
        {
            InitializeComponent();
            tpnum = tp;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            frmSendRequest f1 = new frmSendRequest(tpnum);
            f1.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string TPNum = tpnum;
            frmDeleteRequest f2 = new frmDeleteRequest(TPNum);
            f2.Show();
            this.Hide();
        }

        private void frmStudentRequest_Load(object sender, EventArgs e)
        {
            con.Open();
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
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Requests WHERE TPNumber = @TP", con);
            cmd2.Parameters.AddWithValue("@TP", tpnum);
            int count = Convert.ToInt32(cmd2.ExecuteScalar());
            lblNumberRequest.Text = count.ToString();
            con.Close();
        }

        private void btnViewRequest_Click(object sender, EventArgs e)
        {
            StudentViewRequest F1 = new StudentViewRequest(tpnum);
            F1.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            frmStudentMainpage f2 = new frmStudentMainpage();
            f2.Show();
            this.Hide();
        }
    }
}
