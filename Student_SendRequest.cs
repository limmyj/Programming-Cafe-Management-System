using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Group_8_IOOP;

namespace Assignment
{
    public partial class frmSendRequest : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public string tpnum;
        public string RequestStatus;
        public string Module_ID;
        public static string Trainer_TPNum;
        public static string Charges;
        public static string PaymentStatus;


        public frmSendRequest()
        {
            InitializeComponent();
        }

        public frmSendRequest(string tp)
        {
            InitializeComponent();
            tpnum = tp;
        }

        private void frmSendRequest_Load(object sender, EventArgs e)
        {
            Student s1 = new Student(tpnum);
            lblDate.Text = DateTime.Now.ToLongDateString();
            Student.SendRequest(s1);
            lblTPNumber.Text = s1.Tpnum;
            lblName.Text = s1.Name;

            cboModuleID.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ModuleID FROM classSchedule", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                string moduleID = dr["ModuleID"].ToString();
                if (!cboModuleID.Items.Contains(moduleID))
                {
                    cboModuleID.Items.Add(moduleID);
                }
            }
            con.Close();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cboModuleID.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select the Module ID");
            }
            else if (cboMonth.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select the Month");
            }
            else if (cboModuleID.SelectedIndex == -1 && cboMonth.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select the Module ID and Month");
            }
            else
            {
                Student s1 = new Student(tpnum);
                Student.SendRequest(s1);
                string yr_level = lblLevel.Text;
                string module_id = cboModuleID.SelectedItem.ToString();
                string add_class = lblModule.Text;
                string month_of_enrollment = cboMonth.SelectedItem.ToString();
                DateTime date;
                date = DateTime.Parse(month_of_enrollment);
                string request_status = "Pending";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Requests WHERE TPNumber = @tp AND Level = @l AND AdditionalModule = @am", con);
                cmd.Parameters.AddWithValue("@tp", tpnum);
                cmd.Parameters.AddWithValue("@l", yr_level);
                cmd.Parameters.AddWithValue("@am", add_class);
                int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (count > 0)
                {
                    MessageBox.Show("You cannot enroll for the same additional module twice!");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO Requests (TPNumber, Name, Date, Level, ModuleID, AdditionalModule, MonthOfEnrollment, RequestStatus) VALUES (@tp, @n, @d, @l,@mid, @am, @m, @r)", con);
                    cmd2.Parameters.AddWithValue("@tp", s1.Tpnum);
                    cmd2.Parameters.AddWithValue("@n", s1.Name);
                    cmd2.Parameters.AddWithValue("@d", lblDate.Text);
                    cmd2.Parameters.AddWithValue("@l", yr_level);
                    cmd2.Parameters.AddWithValue("mid", module_id);
                    cmd2.Parameters.AddWithValue("@am", add_class);
                    cmd2.Parameters.AddWithValue("@m", month_of_enrollment);
                    cmd2.Parameters.AddWithValue("@r", request_status);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Request is sent.");
                    this.Hide();
                    frmStudentRequest F1 = new frmStudentRequest();
                    F1.ShowDialog();
                }
                con.Close();
            }
        }
        private void cboModuleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblModule.Text = string.Empty;
            Module_ID = cboModuleID.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM classSchedule WHERE ModuleID =@ModuleID", con);
            cmd.Parameters.AddWithValue("@ModuleID", Module_ID);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblLevel.Text = dr["Level"].ToString();
                lblModule.Text = dr["ModuleName"].ToString();
            }
            con.Close();

            con.Open();
            SqlCommand cmd3 = new SqlCommand("SELECT * from classSchedule WHERE ModuleID = @mid", con);
            cmd3.Parameters.AddWithValue("@mid", Module_ID);
            cmd3.ExecuteNonQuery();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd3);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            SqlDataReader rd2 = cmd3.ExecuteReader();
            while (rd2.Read())
            {
                lblTrainerTP.Text = rd2["TPNumber"].ToString();
                lblCharges.Text = rd2["Charges"].ToString();
            }
            con.Close();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * from classSchedule INNER JOIN Trainers ON Trainers.TPNumber = classSchedule.TPNumber WHERE classSchedule.ModuleID = @mid", con);
            cmd2.Parameters.AddWithValue("@mid", Module_ID);
            cmd2.ExecuteNonQuery();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            SqlDataReader rd3 = cmd2.ExecuteReader();
            while (rd3.Read())
            {
                lblTName.Text = rd3["Name"].ToString();
            }
            con.Close();
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
