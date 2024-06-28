using Group_8_IOOP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class Admin_TL_AddTrainer : Form
    {
        public static string adminTP; //global
        private string trainerTP, trainerName, trainerEmail;
        public Admin_TL_AddTrainer()
        {
            InitializeComponent();
        }
        public Admin_TL_AddTrainer(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }

        private void pboAReturn_Click(object sender, EventArgs e)
        {
            //go back to the trainer list page
            Admin_TrainerList atl = new Admin_TrainerList(adminTP);
            atl.Show();

            this.Hide();
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            //back to admin main page
            Admin_MainPage amainpage = new Admin_MainPage();
            amainpage.Show();

            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            //go to settings page
            Admin_Setting asetting = new Admin_Setting(adminTP);
            asetting.Show();

            this.Hide();
        }

        private void btnAAdd_Done_Click(object sender, EventArgs e)
        {
            //to auto set the input from user to uppercase letter to be stored in DB
            trainerTP = txtAAdd_TPNumber.Text.ToUpper();
            trainerName = txtAAdd_Name.Text;
            trainerEmail = txtAAdd_Email.Text;

            //if any of the details for adding trainer user doesnot input correctly
            //failed to add trainer
            if (trainerTP == "" || trainerName == "" || trainerEmail == "")
            {
                MessageBox.Show("Please enter a valid input");
            }
            else //all done input
            {
                Admin_Trainer obj1 = new Admin_Trainer(trainerTP, trainerName, trainerEmail, trainerTP);
                MessageBox.Show(obj1.addTrainer());
                //from the class if tp ady valid -- show failed to add
                //               if tp doesnot exist before --added successfully

                txtAAdd_TPNumber.Text = string.Empty;
                txtAAdd_Name.Text = string.Empty;
                txtAAdd_Email.Text = string.Empty;
            }
        }

        private void txtAAdd_TPNumber_TextChanged(object sender, EventArgs e)
        {
            //auto adding the student email created by apu
            txtAAdd_Email.Text = txtAAdd_TPNumber.Text.ToLower() + "@mail.apu.edu.my";
        }
    }
}
