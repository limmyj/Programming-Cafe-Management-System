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

namespace Assignment
{
    public partial class Admin_TL_DeleteTrainer : Form
    {
        public static string trainerTP; //global

        public static string adminTP; //global

        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public Admin_TL_DeleteTrainer()
        {
            InitializeComponent();
        }
        public Admin_TL_DeleteTrainer(string tp)
        {
            InitializeComponent();
            adminTP = tp;
        }

        private void Admin_TL_DeleteTrainer_Load(object sender, EventArgs e)
        {
            ArrayList tTP = new ArrayList();

            tTP = Admin_Trainer.allTrainer();
            foreach (var item in tTP)
            {
                libADelete_SelectTrainerTP.Items.Add(item);
            }
        }

        private void btnADelete_Done_Click(object sender, EventArgs e)
        {
            Admin_Trainer obj1 = new Admin_Trainer(trainerTP);
            MessageBox.Show(obj1.deleteTrainer());
        }

        private void pboAHomePage_Click(object sender, EventArgs e)
        {
            Admin_MainPage amainpage = new Admin_MainPage();
            amainpage.Show();

            this.Hide();
        }

        private void pboAReturn_Click(object sender, EventArgs e)
        {
            Admin_TrainerList atl = new Admin_TrainerList(adminTP);
            atl.Show();

            this.Hide();
        }

        private void pboSetting_Click(object sender, EventArgs e)
        {
            Admin_Setting asetting = new Admin_Setting(adminTP);
            asetting.Show();

            this.Hide();
        }

        private void libADelete_SelectTrainerTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            trainerTP = libADelete_SelectTrainerTP.SelectedItem.ToString();

            con.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM TrainerAssignment WHERE TPNumber = '" + trainerTP + "' ", con);
            DataTable dtbl = new DataTable();
            sqlda.Fill(dtbl);

            dataGridView1.DataSource = dtbl;

            con.Close();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM TrainerAssignment WHERE TPNumber = '" + trainerTP + "' ", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAAssign_TP.Text = dr["TPNumber"].ToString();
                lblAAssign_Name.Text = dr["Name"].ToString();
                lblAAssign_Email.Text = dr["Email"].ToString();
            }

            con.Close();
        }
    }
}
