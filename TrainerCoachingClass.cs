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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Assignment
{
    public partial class TrainerCoachingClass : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public TrainerCoachingClass()
        {
            InitializeComponent();
        }

        public TrainerCoachingClass(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerUpdateCoachingClass f3 = new TrainerUpdateCoachingClass(TPNumber);
            f3.Show();
            this.Hide();
        }

        private void TrainerCoachingClass_Load(object sender, EventArgs e)
        {         
            RefreshComboBoxValues();
            displayData();
            lblTPNum.Text = TPNum;

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM classSchedule WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string level = dr["Level"].ToString();
                lblLevel.Text = level;
            }
            dr.Close();
            con.Close();
        }

        private void RefreshComboBoxValues()
        {
            cboModule.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ModuleID FROM classSchedule WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);
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
            }
            con.Close();
        }

        private void cboModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedModule = cboModule.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM classSchedule WHERE TPNumber = @TP AND ModuleID = @selectedModule", con);
            cmd2.Parameters.AddWithValue("@TP", TPNum);
            cmd2.Parameters.AddWithValue("@selectedModule", selectedModule);

            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            SqlDataReader dr = cmd2.ExecuteReader();
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
            SqlCommand cmd3 = new SqlCommand("SELECT * FROM classSchedule WHERE TPNumber = @TP AND Day = @selectedDay", con);
            cmd3.Parameters.AddWithValue("@TP", TPNum);
            cmd3.Parameters.AddWithValue("@selectedDay", selectedDay);

            SqlDataAdapter da = new SqlDataAdapter(cmd3);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }
        public void displayData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM classSchedule WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            TrainerMain f4 = new TrainerMain();
            f4.Show(); //allows to move to Login Page
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            TrainerSetting f6 = new TrainerSetting(TPNumber);
            f6.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
