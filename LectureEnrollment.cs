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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Assignment
{
    public partial class LectureEnrollment : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public static string TPNum;
        public static string Trainer_TPNum;
        public static string Charges;
        public static string PaymentStatus;
        public LectureEnrollment()
        {
            InitializeComponent();
        }

        public LectureEnrollment(string tp)
        {
            InitializeComponent();
            TPNum = tp;
        }

        private void LectureEnrollment_Load(object sender, EventArgs e)
        {
            //Create a list to store TPNumber
            ArrayList TPNumber = new ArrayList();

            //call static method > classname.method(..)
            TPNumber = Lecture.ViewStudentTPNum();
            foreach (var item in TPNumber)
            {
                lstStudentName.Items.Add(item);
            }
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
            LectureMain f2 = new LectureMain();
            f2.Show();// allows to move to LectureMain Page
            this.Hide();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            LectureCourseUpdate f1 = new LectureCourseUpdate(TPNumber);
            f1.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void btnEnrol_Click_1(object sender, EventArgs e)
        {
            string TPNumber;
            string moduleID;
            string level;
            string Name;
            string module;
            string Smonth;

            if (lstStudentName.SelectedIndex == -1 || cboModuleID.SelectedIndex == -1 || cboMonth.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid, Please Select the TPNumber,ModuleID and Month of Enrollment");
            }
            else
            {
                TPNumber = lstStudentName.SelectedItem.ToString();
                module = lblModule.Text;
                Name = lblStudentName2.Text;
                level = lblLevel.Text;
                Smonth = cboMonth.SelectedItem.ToString();
                moduleID = cboModuleID.SelectedItem.ToString();
                DateTime date;
                date = DateTime.Parse(Smonth);
                DateTime Emonth = date.AddMonths(3);
                string Module_ID = cboModuleID.SelectedItem.ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM studentList WHERE TPNumber = @tp AND ModuleName = @am", con);
                cmd.Parameters.AddWithValue("@tp", TPNumber); ;
                cmd.Parameters.AddWithValue("@am", module);
                int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();

                if (count > 0)
                {
                    MessageBox.Show("You cannot enroll for the same additional module twice!");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM classSchedule WHERE ModuleID = @ModuleID", con);
                    cmd2.Parameters.AddWithValue("@ModuleID", Module_ID);
                    cmd2.ExecuteNonQuery();

                    SqlDataReader dr = cmd2.ExecuteReader();
                    while (dr.Read())
                    {
                        Trainer_TPNum = dr["TPNumber"].ToString();
                        Charges = dr["Charges"].ToString();
                    }
                    con.Close();
                    PaymentStatus = "Unpaid";
                    int.Parse(Charges);
                    Lecture obj1 = new Lecture(TPNumber, Name, level, moduleID, module, Trainer_TPNum, Charges, PaymentStatus, date, Emonth);
                    MessageBox.Show(obj1.StudentEnrollment());
                }
            }
        }

        private void lstStudentName_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string TPNumber;
            if (lstStudentName.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid, Please select a TPNumber");
            }
            else
            {
                TPNumber = lstStudentName.SelectedItem.ToString();
                Lecture obj1 = new Lecture(TPNumber);
                Lecture.ViewStudentDetails(obj1);
                lblStudentName2.Text = obj1.Name1;
                lblEmail2.Text = obj1.Email;
                lblContact2.Text = obj1.ContactNumber;

                cboModuleID.Items.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ModuleID FROM classSchedule", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow rd in dt.Rows)
                {
                    string moduleID = rd["ModuleID"].ToString();
                    if (!cboModuleID.Items.Contains(moduleID))
                    {
                        cboModuleID.Items.Add(moduleID);
                    }
                }
                con.Close();
            }
        }

        private void cboModuleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Module_ID = cboModuleID.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM classSchedule where ModuleID = @mid", con);
            cmd.Parameters.AddWithValue("mid", Module_ID);
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
        }
    }
}
