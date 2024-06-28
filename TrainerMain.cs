using Group_8_IOOP;
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
    public partial class TrainerMain : Form
    {
        public static string TPNum;
        public static string name;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public TrainerMain()
        {
            InitializeComponent();
        }

        public TrainerMain(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void btnCoach_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerCoachingClass f1 = new TrainerCoachingClass(TPNumber);
            f1.Show();
            this.Hide();
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerFeedback f2 = new TrainerFeedback(TPNumber);
            f2.Show();
            this.Hide();
        }

        private void TrainerMain_Load_1(object sender, EventArgs e)
        {
            con.Open();
            lblRole.Text = "Trainer";
            SqlCommand cmd = new SqlCommand("SELECT * FROM Trainers WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblTPNum.Text = dr["TPNumber"].ToString();
                lblName.Text = dr["Name"].ToString();
            }
            con.Close();

            Trainer obj = new Trainer(TPNum, name);
            lblWelcome.Text = "Welcome, " + obj.trainerWelcome();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerStudentList f5 = new TrainerStudentList(TPNumber);
            f5.Show();
            this.Hide();

        }

        private void pboProfile_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            TrainerProfile f3 = new TrainerProfile(TPNumber);
            f3.Show(); //allows to move to Profile Page
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            LoginPage f4 = new LoginPage();
            f4.Show(); //allows to move to Login Page
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            TrainerSetting f6 = new TrainerSetting(TPNumber);
            f6.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
