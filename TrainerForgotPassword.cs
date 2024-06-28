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
    public partial class TrainerForgotPassword : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public TrainerForgotPassword()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", txtTPNum.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Your password is " + dr.GetValue(2).ToString());
            }
            else
            {
                MessageBox.Show("Incorrect TPNumber");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginPage f1 = new LoginPage();
            f1.Show();
            this.Hide();
        }
    }
}
