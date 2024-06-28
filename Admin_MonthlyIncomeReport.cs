using Group_8_IOOP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class Admin_MonthlyIncomeReport : Form
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public static string adminTP; //global

        public Admin_MonthlyIncomeReport()
        {
            InitializeComponent();
            
        }
        public Admin_MonthlyIncomeReport(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }

        private void Admin_MonthlyIncomeReport_Load(object sender, EventArgs e)
        {
            //show trainer tp list
            ArrayList tTP = new ArrayList();

            tTP = Admin_Trainer.allTrainer();
            foreach (var item in tTP)
            {
                libAMIR_SelectTrainer.Items.Add(item);
            }

            libAMIR_Month.SelectedIndex = -1;
            libAMIR_SelectTrainer.SelectedIndex = -1;
            libAMIR_SelectLevel.SelectedIndex = -1;
            libAMIR_SelectModule.SelectedIndex = -1;

            con.Open();
            SqlDataAdapter showall = new SqlDataAdapter("SELECT * FROM TrainerMIR", con);
            DataTable dtbl = new DataTable();
            showall.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
            con.Close();

            showAllModule();
        }

        private void showAllModule()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ModuleName FROM classSchedule", con);
            //cmd.Parameters.AddWithValue("@TP", TPNum);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                string moduleName = dr["ModuleName"].ToString();
                if (!libAMIR_SelectModule.Items.Contains(moduleName))
                {
                    libAMIR_SelectModule.Items.Add(moduleName);
                }
            }
            con.Close();
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            Admin_MainPage amainpage = new Admin_MainPage();
            amainpage.Show();

            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            Admin_Setting asetting = new Admin_Setting(adminTP);
            asetting.Show();

            this.Hide();
        }

        private void btnAMIR_Reset_Click(object sender, EventArgs e)
        {
            libAMIR_Month.ClearSelected();
            libAMIR_SelectTrainer.ClearSelected();
            libAMIR_SelectLevel.ClearSelected();
            libAMIR_SelectModule.ClearSelected();

            libAMIR_Month.Enabled = true;
            libAMIR_SelectTrainer.Enabled = true;
            libAMIR_SelectLevel.Enabled = true;
            libAMIR_SelectModule.Enabled = true;

            con.Open();
            SqlDataAdapter showall = new SqlDataAdapter("SELECT * FROM TrainerMIR", con);
            DataTable dtbl = new DataTable();
            showall.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
            con.Close();
        }

        private void libAMIR_SelectTrainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            libAMIR_SelectModule.Enabled = false;
            libAMIR_SelectLevel.Enabled = false;
        }

        private void libAMIR_SelectLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            libAMIR_SelectTrainer.Enabled = false;
            libAMIR_SelectModule.Enabled = false;
        }

        private void libAMIR_SelectModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            libAMIR_SelectLevel.Enabled = false;
            libAMIR_SelectModule.Enabled = false;
        }

        private void btnAMIR_Search_Click(object sender, EventArgs e)
        {
            string month;
            string trainerTP;
            string trainerLevel;
            string trainerModule;

            if (libAMIR_Month.SelectedIndex == -1) //month not selected
            {
                MessageBox.Show("Please select specific month.");
                libAMIR_Month.Focus();
            }
            else //month selected
            {
                month = libAMIR_Month.SelectedItem.ToString();
                if (libAMIR_SelectTrainer.SelectedIndex == -1) //tp not selected
                {
                    trainerTP = "Not Selected";
                    if (libAMIR_SelectLevel.SelectedIndex == -1) //level not selected
                    {
                        trainerLevel = "Not Selected";
                        if (libAMIR_SelectModule.SelectedIndex == -1) //module not selected
                        {
                            trainerModule = "Not Selected";

                            //show whole table
                            con.Open();
                            SqlDataAdapter showmonth = new SqlDataAdapter("SELECT * FROM TrainerMIR WHERE Month = '" + month + "'", con);
                            DataTable dtbl = new DataTable();
                            showmonth.Fill(dtbl);
                            dataGridView1.DataSource = dtbl;
                            con.Close();
                        }
                        else //module selected
                        {
                            trainerModule = libAMIR_SelectModule.SelectedItem.ToString();

                            //show table based on module & month
                            con.Open();
                            SqlDataAdapter showm = new SqlDataAdapter("SELECT * FROM TrainerMIR WHERE Module = '" + trainerModule + "' AND Month = '" + month + "'", con);
                            DataTable dtbl = new DataTable();
                            showm.Fill(dtbl);
                            dataGridView1.DataSource = dtbl;
                            con.Close();
                        }
                    }
                    else //level selected
                    {
                        trainerLevel = libAMIR_SelectLevel.SelectedItem.ToString();

                        //show table based on level & month
                        con.Open();
                        SqlDataAdapter showl = new SqlDataAdapter("SELECT * FROM TrainerMIR WHERE Level = '" + trainerLevel + "' AND Month = '" + month + "'", con);
                        DataTable dtbl = new DataTable();
                        showl.Fill(dtbl);
                        dataGridView1.DataSource = dtbl;
                        con.Close();
                    }
                }
                else //tp selected
                {
                    trainerTP = libAMIR_SelectTrainer.SelectedItem.ToString();

                    //show table based on trainer tp & month
                    con.Open();
                    SqlDataAdapter showtp = new SqlDataAdapter("SELECT * FROM TrainerMIR WHERE TPNumber = '" + trainerTP + "' AND Month = '" + month + "'", con);
                    DataTable dtbl = new DataTable();
                    showtp.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;
                    con.Close();
                }
            }
        }
    }
}
