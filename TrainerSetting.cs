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
    public partial class TrainerSetting : Form
    {
        public static string TPNum;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public TrainerSetting()
        {
            InitializeComponent();
        }
        public TrainerSetting(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerChangePassword f1 = new TrainerChangePassword(TPNumber);
            f1.Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;

            TrainerUpdateProfile f1 = new TrainerUpdateProfile(TPNumber);
            f1.Show();
            this.Hide();
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            TrainerMain f2 = new TrainerMain();
            f2.Show();
            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            TrainerSetting f6 = new TrainerSetting(TPNumber);
            f6.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void TrainerSetting_Load(object sender, EventArgs e)
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

            lblRole.Text = "Trainer";
        }
    }
}
