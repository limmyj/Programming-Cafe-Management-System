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
    public partial class TrainerStudentList : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public TrainerStudentList()
        {
            InitializeComponent();
        }
        public TrainerStudentList(string n)
        {
            InitializeComponent();
            TPNum = n;
        }
        private void TrainerStudentList_Load(object sender, EventArgs e)
        {
            RefreshComboBoxValues();
            displayData();

            lblTPNum.Text = TPNum;

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM studentList WHERE Trainer_TPNum = @TP", con);
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
            SqlCommand cmd = new SqlCommand("SELECT ModuleID FROM studentList WHERE Trainer_TPNum = @TP", con);
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM studentList WHERE Trainer_TPNum = @TP AND ModuleID = @selectedModule AND PaymentStatus = 'Paid'", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);
            cmd.Parameters.AddWithValue("@selectedModule", selectedModule);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string charges = dr["Charges"].ToString();
                lblCharges.Text = charges + "/student";

                string moduleName = dr["ModuleName"].ToString();
                lblModule.Text = moduleName;
            }
            dr.Close();
            con.Close();
        }
        public void displayData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM studentList WHERE Trainer_TPNum = @TP", con);
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
            TrainerMain f1 = new TrainerMain();
            f1.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            TrainerUpdateCoachingClass f2 = new TrainerUpdateCoachingClass(TPNumber);
            f2.Show();
            this.Hide();
        }
    }
}
