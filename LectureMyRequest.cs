using Group_8_IOOP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Assignment
{
    public partial class LectureMyRequest : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public static int RequestID;
        public static string TPNum;
        public static string ModuleID;
        public static string Trainer_TPNum;
        public static string Charges;
        public static string PaymentStatus;
        public LectureMyRequest()
        {
            InitializeComponent();
        }

        private void RefreshComboBoxValues()
        {
            cboRequestID.Items.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ID FROM Requests WHERE RequestStatus = 'Pending'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
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

        private void LectureMyRequest_Load(object sender, EventArgs e)
        {
            RefreshComboBoxValues();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            string request_id;
            if (cboRequestID.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid Please Select the request ID");
            }
            else
            {
                request_id = cboRequestID.SelectedItem.ToString();
                Name = lblName.Text;
                ModuleID = lblModuleID.Text;
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM classSchedule WHERE ModuleID =@ModuleID", con);
                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                cmd.ExecuteNonQuery();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Trainer_TPNum = dr["TPNumber"].ToString();
                    Charges = dr["Charges"].ToString();
                }
                con.Close();

                ModuleID = lblModuleID.Text;
                DateTime date;
                date = DateTime.Parse(lblMonth.Text);
                DateTime Emonth = date.AddMonths(3);
                int.Parse(Charges);
                PaymentStatus = "Unpaid";
                Lecture obj1 = new Lecture(TPNum, Name, lblLevel.Text, ModuleID, lblAdditionalClass.Text, Trainer_TPNum, Charges, PaymentStatus, date, Emonth);
                MessageBox.Show(obj1.ApproveStudentRequest());
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Update Requests set RequestStatus = 'Approved' where Id =@id", con);
                cmd2.Parameters.AddWithValue("@id", request_id);
                cmd2.ExecuteNonQuery();
                con.Close();
                lblRequestDate.Text = string.Empty;
                lblStudentName.Text = string.Empty;
                lblLevel.Text = string.Empty;
                lblModuleID.Text = string.Empty;
                lblAdditionalClass.Text = string.Empty;
                lblMonth.Text = string.Empty;
                cboRequestID.Text = string.Empty;
                RefreshComboBoxValues();
            }
        }

        private void btnDeny_Click_1(object sender, EventArgs e)
        {
            string RequestID;
            string module;
            string Level;
            if (cboRequestID.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid Please Select the request ID");
            }
            else
            {
                RequestID = cboRequestID.SelectedItem.ToString();
                module = lblAdditionalClass.Text;
                Level = lblLevel.Text;
                Lecture obj1 = new Lecture(RequestID, Level, module);
                MessageBox.Show(obj1.RequestRejected());
                lblRequestDate.Text = string.Empty;
                lblStudentName.Text = string.Empty;
                lblLevel.Text = string.Empty;
                lblModuleID.Text = string.Empty;
                lblAdditionalClass.Text = string.Empty;
                lblMonth.Text = string.Empty;
                cboRequestID.Text = string.Empty;
                RefreshComboBoxValues();
            }
        }

        private void cboRequestID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string request_id = cboRequestID.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Requests WHERE ID = @id AND RequestStatus = 'Pending'", con);
            cmd.Parameters.AddWithValue("@id", request_id);
            cmd.ExecuteNonQuery();


            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblRequestDate.Text = dr["Date"].ToString();
                TPNum = dr["TPNumber"].ToString();
                lblName.Text = dr["Name"].ToString();
                lblLevel.Text = dr["Level"].ToString();
                lblModuleID.Text = dr["ModuleID"].ToString();
                lblAdditionalClass.Text = dr["AdditionalModule"].ToString();
                lblMonth.Text = dr["MonthOfEnrollment"].ToString();
            }
            con.Close();
            con.Open();
            //SqlCommand cmd2 = new SqlCommand("SELECT * FROM Requests where TPNumber = @tp", con);
            SqlCommand cmd2 = new SqlCommand ("SELECT * from Students INNER JOIN Requests ON Students.TPNumber = Requests.TPNumber WHERE Requests.TPNumber = @tp", con);
            cmd2.Parameters.AddWithValue("@tp", TPNum);
            cmd2.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader rd = cmd2.ExecuteReader();
            while (rd.Read())
            {
                lblName.Text = rd["Name"].ToString();
                lblEmail.Text = rd["Email"].ToString();
                lblContactNum.Text = rd["ContactNumber"].ToString();
            }
            con.Close();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            LectureSetting f3 = new LectureSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            LectureMain f1 = new LectureMain();
            f1.Show();// allows to move to LectureMain Page
            this.Hide();
        }
    }
}
