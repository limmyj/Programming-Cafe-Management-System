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
    public partial class TrainerUpdateCoachingClass : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public TrainerUpdateCoachingClass()
        {
            InitializeComponent();
        }
        public TrainerUpdateCoachingClass(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerDeleteCoachingClass f2 = new TrainerDeleteCoachingClass(TPNumber);
            f2.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            TrainerCoachingClass f3 = new TrainerCoachingClass();
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerManageCoachingClass f4 = new TrainerManageCoachingClass(TPNumber);
            f4.Show();
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            TrainerMain f4 = new TrainerMain(TPNumber);
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

        private void TrainerUpdateCoachingClass_Load(object sender, EventArgs e)
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
        }
    }
}
