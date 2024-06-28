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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Assignment
{
    public partial class Admin_TL_AssignTrainer : Form
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public static string adminTP; //global

        public static string trainerTP; //global
        public static string trainerName; //global
        public static string trainerEmail; //global
        public static string trainerLevel; //to add trainer level
        public static string trainerModuleName; //to add trainer module name
        public static string trainerModuleID; //to add trainer module id

        public static string tmdlid; //global

        public Admin_TL_AssignTrainer()
        {
            InitializeComponent();
        }
        public Admin_TL_AssignTrainer(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }

        //when redirected to this page, showing trainer tp list
        private void Admin_TL_AssignTrainer_Load(object sender, EventArgs e)
        {
            cbxAAssign_Level.SelectedIndex = -1;
            btnAAssign_LevelDone.Enabled = false;
            cbxAAssign_Level.Enabled = false;

            //show the whole trainer assignment table from DB
            con.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM TrainerAssignment", con);
            DataTable dtbl = new DataTable();
            sqlda.Fill(dtbl);

            dataGridView1.DataSource = dtbl;

            con.Close();

            //show all trainer tp
            ArrayList tTP = new ArrayList();

            tTP = Admin_Trainer.allTrainer();
            foreach (var item in tTP)
            {
                libAAssign_SelectTrainerTP.Items.Add(item);
            }

            //can't assign trainer for now
            grpboxAAssign_Module.Enabled = false;
        }

        private void btnAAssign_LevelDone_Click(object sender, EventArgs e)
        {
            // to assign trainer level
            Admin_Trainer obj = new Admin_Trainer(trainerTP, trainerLevel);
            MessageBox.Show(obj.assignTrainerLevel()); //return status

            cbxAAssign_Level.SelectedIndex = -1;
        }

        private void cbxAAssign_Level_SelectedIndexChanged(object sender, EventArgs e)
        {
            // level not selected, show not set 
            if (cbxAAssign_Level.SelectedIndex == -1)
            {
                trainerLevel = "Not Set";
                btnAAssign_LevelDone.Enabled = false;
            }
            else
            {
                trainerLevel = cbxAAssign_Level.SelectedItem.ToString();
                btnAAssign_LevelDone.Enabled = true;
            }
        }

        private void btnAAssign_ModuleDone_Click(object sender, EventArgs e)
        {
            trainerName = lblAAssign_Name.Text;
            trainerEmail = lblAAssign_Email.Text;

            trainerModuleName = txtAAssign_ModuleName.Text;
            trainerModuleID = txtAAssign_ModuleID.Text;
            trainerLevel = lblAAssign_Level.Text;

            if (trainerModuleName == "" || trainerModuleID == "")
            {
                MessageBox.Show("Please enter a valid input");
            }
            else
            {
                Admin_Trainer am = new Admin_Trainer(trainerTP, trainerName, trainerEmail);
                MessageBox.Show(am.assignModule(trainerTP, trainerLevel, trainerModuleName, trainerModuleID));
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_TL_AssignTrainer astreshow = new Admin_TL_AssignTrainer();
            astreshow.Show();
        }

        private void pboAReturn_Click(object sender, EventArgs e)
        {
            Admin_TrainerList atl = new Admin_TrainerList();
            atl.Show();

            this.Hide();
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

        private void libAAssign_SelectTrainerTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when admin selected specific trainer
            trainerTP = libAAssign_SelectTrainerTP.SelectedItem.ToString();
            //only show the details of the specific trainer in the table 
            con.Open(); //connection open
            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM TrainerAssignment WHERE TPNumber = '" + trainerTP + "' ", con);
            DataTable dtbl = new DataTable();
            sqlda.Fill(dtbl);

            dataGridView1.DataSource = dtbl;

            con.Close();

            //show trainer details
            Admin_Trainer obj1 = new Admin_Trainer(trainerTP);
            Admin_Trainer.trainerDetails(obj1);

            lblAAssign_TP.Text = obj1.tTP;
            lblAAssign_Name.Text = obj1.tName;
            lblAAssign_Email.Text = obj1.tEmail;
            lblAAssign_Level.Text = obj1.tLevel;

            //level not assgined
            //able to assign level
            if (lblAAssign_Level.Text == "not set")
            {
                cbxAAssign_Level.Enabled = true;
                btnAAssign_LevelDone.Enabled = false;
                MessageBox.Show("Trainer ( " + trainerTP + " ) has not been assigned to any Level yet.");

                //if selected items not beginner / intermediate / advance 
                //unable to click button
                if (cbxAAssign_Level.SelectedIndex == -1 || cbxAAssign_Level.SelectedItem.ToString() == "")
                {
                    //not selecting beginner / intermediate / advance 
                    //--> not showing button
                    btnAAssign_LevelDone.Enabled = false;
                }
                //if selected beginner / intermediate / advance
                //can click button
                else if (cbxAAssign_Level.SelectedItem.ToString() == "Beginner")
                {
                    trainerLevel = "Beginner";
                    btnAAssign_LevelDone.Enabled = true;
                }
                else if (cbxAAssign_Level.SelectedItem.ToString() == "Intermediate")
                {
                    trainerLevel = "Intermediate";
                    btnAAssign_LevelDone.Enabled = true;
                }
                else
                {
                    trainerLevel = "Advance";
                    btnAAssign_LevelDone.Enabled = true;
                }
            }
            else //level ady assigned //can't assign level again
            {
                cbxAAssign_Level.SelectedIndex = -1;
                cbxAAssign_Level.Enabled = false;
                btnAAssign_LevelDone.Enabled = false;
            }

            if (lblAAssign_Level.Text == "Beginner" || lblAAssign_Level.Text == "Intermediate" || lblAAssign_Level.Text == "Advance")
            {
                grpboxAAssign_Module.Enabled = true;
            }
            else
            {
                grpboxAAssign_Module.Enabled = false;
            }
        }
    }
}
