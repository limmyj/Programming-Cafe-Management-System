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
    public partial class TrainerDeleteCoachingClass : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public TrainerDeleteCoachingClass()
        {
            InitializeComponent();
        }
        public TrainerDeleteCoachingClass(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cboModule.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select the Module ID");
            }
            else
            {
                string selectedModule = cboModule.SelectedItem.ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM classSchedule WHERE ModuleID = @ID AND TPNumber = @TP", con);
                cmd.Parameters.AddWithValue("@TP", TPNum);
                cmd.Parameters.AddWithValue("@ID", selectedModule);

                int i = cmd.ExecuteNonQuery();

                if (i != 0)
                {
                    MessageBox.Show("Record deleted successfully.");
                    lblModule.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Failed");
                    lblModule.Text = string.Empty;
                }

                con.Close();
            }

            displayData();
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

        private void DeleteCoachingClass_Load(object sender, EventArgs e)
        {
            RefreshComboBoxValues(); 
            displayData();

            lblTPNum.Text = TPNum;
        }

        private void cboModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedModule = cboModule.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM classSchedule WHERE TPNumber = @TP AND ModuleID = @selectedModule", con);
            cmd.Parameters.AddWithValue("@TP", TPNum); ;
            cmd.Parameters.AddWithValue("@selectedModule", selectedModule);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string moduleName = dr["ModuleName"].ToString();
                lblModule.Text = moduleName;
            }
            dr.Close();
            con.Close();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            TrainerUpdateCoachingClass f1 = new TrainerUpdateCoachingClass();
            f1.Show();
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
