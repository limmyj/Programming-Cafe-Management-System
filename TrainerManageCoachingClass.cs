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
    public partial class TrainerManageCoachingClass : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public TrainerManageCoachingClass()
        {
            InitializeComponent();
        }
        public TrainerManageCoachingClass(string n)
        {
            InitializeComponent();
            TPNum = n;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cboModule.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select the Module ID");
            }
            else
            {
                string selectedModule = cboModule.SelectedItem.ToString();
                con.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE studentList SET Charges = @charges WHERE ModuleID = @ID AND Trainer_TPNum = @TP", con);
                cmd2.Parameters.AddWithValue("@TP", TPNum);
                cmd2.Parameters.AddWithValue("@ID", selectedModule);
                cmd2.Parameters.AddWithValue("@charges", txtCharges.Text);
                cmd2.ExecuteNonQuery();
                con.Close();

                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE classSchedule SET Charges = @charges, Day = @day, Time = @time, Location = @location WHERE ModuleID = @ID AND TPNumber = @TP", con);
                cmd.Parameters.AddWithValue("@ID", selectedModule);
                cmd.Parameters.AddWithValue("@TP", TPNum);
                cmd.Parameters.AddWithValue("@charges", txtCharges.Text);
                cmd.Parameters.AddWithValue("@day", txtDay.Text);
                cmd.Parameters.AddWithValue("@time", txtTime.Text);
                cmd.Parameters.AddWithValue("@location", txtLocation.Text);

                int i = cmd.ExecuteNonQuery();

                if (i != 0)
                {
                    MessageBox.Show("Record updated successfully.");
                    txtCharges.Text = string.Empty;
                    txtDay.Text = string.Empty;
                    txtTime.Text = string.Empty;
                    txtLocation.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Failed.");
                    txtCharges.Text = string.Empty;
                    txtDay.Text = string.Empty;
                    txtTime.Text = string.Empty;
                    txtLocation.Text = string.Empty;
                }
                con.Close();
            }

            displayData();
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

        private void TrainerManageCoachingClass_Load(object sender, EventArgs e)
        {
            RefreshComboBoxValues();
            displayData();
            lblTPNum.Text = TPNum;
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM classSchedule WHERE TPNumber = @TP AND ModuleID = @selectedModule", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);;
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
                txtCharges.Text = dr["Charges"].ToString();
                txtDay.Text = dr["Day"].ToString();
                txtTime.Text = dr["Time"].ToString();
                txtLocation.Text = dr["Location"].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            TrainerSetting f6 = new TrainerSetting(TPNumber);
            f6.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            TrainerUpdateCoachingClass f1 = new TrainerUpdateCoachingClass();
            f1.Show();
            this.Hide();
        }
    }
}
