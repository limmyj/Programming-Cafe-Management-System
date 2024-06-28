using Group_8_IOOP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class frmStudentSchedule : Form
    {
        //global
        public static string tpnum;
        public static string level;
        public static string module;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public frmStudentSchedule()
        {
            InitializeComponent();
        }

        public frmStudentSchedule(string tp)
        {
            InitializeComponent();
            tpnum = tp;
        }
        private void frmStudentSchedule_Load(object sender, EventArgs e)
        {
            displayData();
            RefreshComboBoxValues();

            lblTPNum.Text = tpnum;
        }
        public void displayData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT classSchedule.Level, classSchedule.ModuleID, classSchedule.ModuleName, classSchedule.TPNumber, classSchedule.Charges, classSchedule.Day, classSchedule.Time, classSchedule.Location FROM classSchedule INNER JOIN studentList ON studentList.ModuleID = classSchedule.ModuleID WHERE studentList.TPNumber = @tp", con); // obtain the module enrolled by the student, need to get schedule table name
            cmd.Parameters.AddWithValue("@tp", tpnum);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void RefreshComboBoxValues()
        {
            cboModule.Items.Clear();
            cboDay.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from classSchedule INNER JOIN studentList ON studentList.ModuleID = classSchedule.ModuleID WHERE studentList.TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@TP", tpnum);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                string moduleID = dr["ModuleID"].ToString();
                if (!cboModule.Items.Contains(moduleID))
                {
                    cboModule.Items.Add(moduleID);
                }
                string day = dr["Day"].ToString();
                if (!cboDay.Items.Contains(day))
                {
                    cboDay.Items.Add(day);
                }
                string level = dr["Level"].ToString();
                if (!cboLevel.Items.Contains(level))
                {
                    cboLevel.Items.Add(level);
                }
            }
            con.Close();
        }

        private void cboModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedModule = cboModule.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ModuleID, ModuleName, Day, Time FROM classSchedule WHERE ModuleID = @selectedModule", con);
            cmd.Parameters.AddWithValue("@selectedModule", selectedModule);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string moduleName = dr["ModuleName"].ToString();
                lblModule.Text = moduleName;
            }
            dr.Close();
            con.Close();
        }

        private void cboDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDay = cboDay.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ModuleID, ModuleName, Day, Time FROM classSchedule WHERE Day = @selectedDay", con);
            cmd.Parameters.AddWithValue("@selectedDay", selectedDay);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void cboLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLevel = cboLevel.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from classSchedule INNER JOIN studentList ON studentList.ModuleID = classSchedule.ModuleID WHERE studentList.TPNumber = @tp AND studentList.Level = @l", con);
            cmd.Parameters.AddWithValue("@TP", tpnum);
            cmd.Parameters.AddWithValue("@l", selectedLevel);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmStudentMainpage f4 = new frmStudentMainpage();
            f4.Show(); //allows to move to Login Page
            this.Hide();
        }
    }
}
