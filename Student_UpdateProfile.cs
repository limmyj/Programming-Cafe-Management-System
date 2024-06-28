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
    public partial class frmStudentUpdateProfile : Form
    {
        public static string tpnum;
        public frmStudentUpdateProfile()
        {
            InitializeComponent();
        }
        public frmStudentUpdateProfile(string tp)
        {
            InitializeComponent();
            tpnum = tp;
        }

        private void frmStudentUpdateProfile_Load(object sender, EventArgs e)
        {
            Student obj1 = new Student(tpnum);
            Student.viewProfile(obj1);
            txtNewEmail.Text = obj1.Email;
            txtNewContact.Text = obj1.ContactNum;
            txtNewAddress.Text = obj1.Address;
            richTextBox1.Text = obj1.Biodata;
            lblStudentTPNumber.Text = tpnum;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student std1 = new Student(tpnum);
            std1.updateProfile(txtNewEmail.Text, txtNewContact.Text, txtNewAddress.Text, richTextBox1.Text);
            MessageBox.Show("Successfully Update!");

            if (richTextBox1.Text.Length > 100)
            {
                MessageBox.Show("Your biodata are exceeding the limit word count.");
            }
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            frmStudentMainpage f2 = new frmStudentMainpage();
            f2.Show();
            this.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = richTextBox1.Text.Length.ToString() + "/150";
        }
    }
}
