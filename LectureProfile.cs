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
    public partial class LectureProfile : Form
    {
        public static string TPNumber;
        public LectureProfile()
        {
            InitializeComponent();
        }
        public LectureProfile(string tp)
        {
            InitializeComponent();
            TPNumber = tp;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string TPNum = TPNumber;
            LectureUpdateProfile f1 = new LectureUpdateProfile(TPNumber);
            f1.Show();//allows to move to Update Profile
        }

        private void LectureProfile_Load(object sender, EventArgs e)
        {
            lblTPNum.Text = TPNumber;
            Lecture obj1 = new Lecture(TPNumber);
            Lecture.ViewProfile(obj1);
            lblName.Text = obj1.Name1;
            lblEmail.Text = obj1.Email;
            lblContact.Text = obj1.ContactNumber;
            lblAddress.Text = obj1.Address1;
            lblBio.Text = obj1.BioData1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LectureMain f1 = new LectureMain();
            f1.Show();//allows to move to LectureMainPage
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            LectureMain f1 = new LectureMain();
            f1.Show();//allows to move to LectureMainPage
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNum = TPNumber;
            LectureSetting f1 = new LectureSetting(TPNum);
            f1.Show();//allows to move to Update Profile
            this.Hide();
        }
    }
}
