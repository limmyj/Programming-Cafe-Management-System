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
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class LectureCourseUpdate : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public LectureCourseUpdate()
        {
            InitializeComponent();
        }
        public LectureCourseUpdate(string tp)
        {
            InitializeComponent();
            TPNum = tp;
        }
        private void RefreshComboBoxValues()
        {
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

        public void displayData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM studentList", con);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            DataTable dt1 = dt;
            dataGridView1.DataSource = dt1;

            con.Close();
        }

        private void LectureCourseUpdation_Load(object sender, EventArgs e)
        {
            RefreshComboBoxValues();
            displayData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cboModuleID.SelectedIndex == -1 || cboTPNum.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid Please Select the ModuleID and TP Number");
            }
            else
            {
                string TPNumber;
                TPNumber = cboTPNum.SelectedItem.ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from studentList where TPNumber = @tp and Level = @l and ModuleName = @mn", con);
                cmd.Parameters.AddWithValue("@tp", TPNumber);
                cmd.Parameters.AddWithValue("@l", lblLevel.Text);
                cmd.Parameters.AddWithValue("@mn", lblModule.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted !");
                con.Close();
                lblTPNum.Text = string.Empty;
                lblName.Text = string.Empty;
                cboTPNum.Text = string.Empty;
                lblModule.Text = string.Empty;
                displayData();
            }
        }

        private void cboModuleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTPNum.Items.Clear();
            cboTPNum.Text = string.Empty;
            string Module = cboModuleID.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentList WHERE ModuleID =@mid", con);
            cmd.Parameters.AddWithValue("@mid", Module);
            cmd.ExecuteNonQuery();


            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblModule.Text = dr["ModuleName"].ToString();
                lblLevel.Text = dr["Level"].ToString();
            }
            con.Close();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT TPNumber FROM studentList where ModuleID =@mid", con);
            cmd2.Parameters.AddWithValue("@mid", Module);
            cmd2.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow rd in dt.Rows)
            {
                string TPNumber = rd["TPNumber"].ToString();
                if (!cboTPNum.Items.Contains(TPNumber))
                {
                    cboTPNum.Items.Add(TPNumber);
                }
            }
            con.Close();
        }

        private void cboTPNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTPNum.Text = cboTPNum.SelectedItem.ToString();
            con.Open();
            lblRole.Text = "Student";
            SqlCommand cmd = new SqlCommand("SELECT * FROM studentList where TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@tp", lblTPNum.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblName.Text = dr["Name"].ToString();
            }
            con.Close();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            LectureEnrollment f1 = new LectureEnrollment();
            f1.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            LectureSetting f3 = new LectureSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
