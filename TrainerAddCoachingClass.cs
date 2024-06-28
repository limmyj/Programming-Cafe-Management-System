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
    public partial class TrainerAddCoachingClass : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public TrainerAddCoachingClass()
        {
            InitializeComponent();
        }
        public TrainerAddCoachingClass(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            TrainerUpdateCoachingClass f1 = new TrainerUpdateCoachingClass();
            f1.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
             if (string.IsNullOrWhiteSpace(txtModuleID.Text))
             {
                 if (string.IsNullOrWhiteSpace(txtModule.Text))
                 {
                     MessageBox.Show("Please insert module id / module name.");
                 }
             }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM classSchedule WHERE ModuleID = @ID AND TPNumber = @TP", con);
                cmd.Parameters.AddWithValue("@TP", TPNum);
                cmd.Parameters.AddWithValue("@ID", txtModuleID.Text);

                int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (count > 0)
                {
                    MessageBox.Show("The Module ID has been added.");
                    txtModuleID.Text = string.Empty;
                    txtModule.Text = string.Empty;
                    txtCharges.Text = string.Empty;
                    txtDay.Text = string.Empty;
                    txtTime.Text = string.Empty;
                    txtLocation.Text = string.Empty;
                }
                else
                {
                    string selectedLevel = cboLevel.SelectedItem.ToString();
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO classSchedule (Level, ModuleID, ModuleName, TPNumber, Charges, Day, Time, Location) VALUES (@selectedLevel, @ID, @name, @TP, @charges, @day, @time, @location)", con);
                    cmd2.Parameters.AddWithValue("@selectedLevel", selectedLevel);
                    cmd2.Parameters.AddWithValue("@ID", txtModuleID.Text);
                    cmd2.Parameters.AddWithValue("@name", txtModule.Text);
                    cmd2.Parameters.AddWithValue("@TP", TPNum);
                    cmd2.Parameters.AddWithValue("@charges", txtCharges.Text);
                    cmd2.Parameters.AddWithValue("@day", txtDay.Text);
                    cmd2.Parameters.AddWithValue("@time", txtTime.Text);
                    cmd2.Parameters.AddWithValue("@location", txtLocation.Text);

                    int i = cmd2.ExecuteNonQuery();

                    if (i != 0)
                    {
                        MessageBox.Show("Record inserted successfully.");
                        txtModuleID.Text = string.Empty;
                        txtModule.Text = string.Empty;
                        txtCharges.Text = string.Empty;
                        txtDay.Text = string.Empty;
                        txtTime.Text = string.Empty;
                        txtLocation.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                        txtModuleID.Text = string.Empty;
                        txtModule.Text = string.Empty;
                        txtCharges.Text = string.Empty;
                        txtDay.Text = string.Empty;
                        txtTime.Text = string.Empty;
                        txtLocation.Text = string.Empty;
                    }
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

        private void AddCoachingClass_Load(object sender, EventArgs e)
        {
            displayData();
        }
    }
}
