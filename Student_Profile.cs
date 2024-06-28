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
    public partial class frmStudentProfile : Form
    {
        private static string tpnum; // global
        
        public frmStudentProfile()
        {
            InitializeComponent();
        }
        public frmStudentProfile(string tp)
        {
            InitializeComponent();
            tpnum = tp;
        }

        private void frmStudentProfile_Load(object sender, EventArgs e)
        {
            lblStudentTPNumber.Text = tpnum;
            Student s1 = new Student(tpnum);
            Student.viewProfile(s1); // call the class method

            lblStudentName.Text = s1.Name;
            lblStudentContact.Text = s1.ContactNum;
            lblStudentAddress.Text = s1.Address;
            lblStudentRole.Text = s1.Role;
            lblStudentEmail.Text = s1.Email;
            lblStudentBiodata.Text = s1.Biodata;
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            frmStudentMainpage f2 = new frmStudentMainpage();
            f2.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
