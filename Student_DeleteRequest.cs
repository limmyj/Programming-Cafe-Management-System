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
    public partial class frmDeleteRequest : Form
    {
        //global
        public static string tpnum;
        public static string Module_ID;
        public frmDeleteRequest()
        {
            InitializeComponent();
        }
        public frmDeleteRequest(string tp)
        {
            InitializeComponent();
            tpnum = tp; //pass in tp value
        }

        private void frmDeleteRequest_Load(object sender, EventArgs e)
        {
            RefreshComboBoxValues();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cboRequestID.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select the Module ID");
            }
            else
            {
                String Request_ID = cboRequestID.SelectedItem.ToString();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
                con.Open();
                //delete request
                SqlCommand cmd = new SqlCommand("delete from Requests where TPNumber = @tp AND Id = @id", con);
                cmd.Parameters.AddWithValue("@tp", tpnum);
                cmd.Parameters.AddWithValue("@id", Request_ID);
                if (lblRequestStatus.Text == "Pending")
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Request has been deleted successfully!");
                    }
                    else
                        MessageBox.Show("Error in deleting request!");
                }
                else
                    MessageBox.Show("The request has been approved. Cannot be deleted.");

                this.Hide();
                frmStudentRequest F1 = new frmStudentRequest();
                F1.ShowDialog();
            }
        }

        private void RefreshComboBoxValues()
        {
            cboRequestID.Items.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();
            //select ID
            SqlCommand cmd = new SqlCommand("SELECT ID FROM Requests WHERE TPNumber = @tp AND RequestStatus = 'Pending'", con);
            cmd.Parameters.AddWithValue("@tp", tpnum);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //read ID
            foreach (DataRow dr in dt.Rows)
            {
                string requestID = dr["ID"].ToString();
                if (!cboRequestID.Items.Contains(requestID))
                {
                    cboRequestID.Items.Add(requestID);
                }
            }
            con.Close();

        }
        private void cboRequestID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string request_id = cboRequestID.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();
            //select requests
            SqlCommand cmd = new SqlCommand("SELECT * FROM Requests WHERE ID = @id AND TPNumber = @tp AND RequestStatus = 'Pending'", con);
            cmd.Parameters.AddWithValue("@id", request_id);
            cmd.Parameters.AddWithValue("@tp", tpnum);
            cmd.ExecuteNonQuery();

            //read request data
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblDate.Text = dr["Date"].ToString();
                lblName.Text = dr["Name"].ToString();
                lblYearLevel.Text = dr["Level"].ToString();
                lblMonth.Text = dr["MonthOfEnrollment"].ToString();
                lblAdditionalModule.Text = dr["AdditionalModule"].ToString();
                lblRequestStatus.Text = dr["RequestStatus"].ToString();
                Module_ID = dr["ModuleID"].ToString();
            }
            con.Close();

            con.Open();
            //select class schedule
            SqlCommand cmd3 = new SqlCommand("SELECT * from classSchedule WHERE ModuleID = @mid", con);
            cmd3.Parameters.AddWithValue("@mid", Module_ID);
            cmd3.ExecuteNonQuery();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd3);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            //read charges and tp number from class schedule
            SqlDataReader rd2 = cmd3.ExecuteReader();
            while (rd2.Read())
            {
                lblTrainerTP.Text = rd2["TPNumber"].ToString();
                lblCharges.Text = rd2["Charges"].ToString();
            }
            con.Close();

            lblRole.Text = "Trainer";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmStudentRequest f4 = new frmStudentRequest();
            f4.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
