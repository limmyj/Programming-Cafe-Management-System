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
using System.Xml;

namespace Assignment
{
    public partial class TrainerProfile : Form
    {
        public static string TPNum;

        public TrainerProfile()
        {
            InitializeComponent();
        }
        public TrainerProfile(string n)
        {
            InitializeComponent();
            TPNum = n;
        }
        private void TrainerProfile_Load(object sender, EventArgs e)
        {
            lblTPNum.Text = TPNum;
            Trainer obj1 = new Trainer(TPNum);

            //calling static method require className.Method(..)
            //pass object obj1 to method viewProfile
            Trainer.viewProfile(obj1);

            lblName.Text = obj1.name;
            lblEmail.Text = obj1.Email;
            lblContactNum.Text = obj1.ContactNumber;
            lblAddress.Text = obj1.address;
            lblBiodata.Text = obj1.biodata;
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
            TrainerMain f2 = new TrainerMain();
            f2.Show();
            this.Hide();
        }
    }
}
