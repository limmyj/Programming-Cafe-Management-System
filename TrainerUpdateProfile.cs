using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class TrainerUpdateProfile : Form
    {
        public static string TPNum;
        public TrainerUpdateProfile()
        {
            InitializeComponent();
        }
        public TrainerUpdateProfile(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void TrainerUpdateProfile_Load(object sender, EventArgs e)
        {
            Trainer obj1 = new Trainer(TPNum);
            Trainer.viewProfile(obj1);
            txtEmail.Text = obj1.Email;
            txtContact.Text = obj1.ContactNumber;
            txtAddress.Text = obj1.address;
            rtbBiodata.Text = obj1.biodata;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Trainer obj1 = new Trainer(TPNum);
            MessageBox.Show(obj1.updateProfile(txtEmail.Text, txtContact.Text, txtAddress.Text, rtbBiodata.Text));
            txtEmail.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtAddress.Text = string.Empty;
            rtbBiodata.Text = string.Empty;

            if (rtbBiodata.Text.Length > 100)
            {
                MessageBox.Show("Your biodata are exceeding the limit word count.");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            lblCount.Text = rtbBiodata.Text.Length.ToString() + "/150";
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerMain f2 = new TrainerMain(TPNumber);
            f2.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerSetting f1 = new TrainerSetting(TPNumber);
            f1.Show();
            this.Hide();
        }
    }
}
