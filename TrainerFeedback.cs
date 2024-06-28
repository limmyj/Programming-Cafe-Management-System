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
    public partial class TrainerFeedback : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public static string TPNum;
        public TrainerFeedback()
        {
            InitializeComponent();
        }

        public TrainerFeedback(string n)
        {
            InitializeComponent();
            TPNum = n;
        }

        private void TrainerFeedback_Load(object sender, EventArgs e)
        {
            lblTPNum.Text = TPNum;
            Trainer obj1 = new Trainer(TPNum);

            //calling static method require className.Method(..)
            //pass object obj1 to method sendFeedback
            Trainer.sendFeedback(obj1);

            lblName.Text = obj1.name;
            lblRole.Text = "Trainer";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbFeedback.Text))
            {
                MessageBox.Show("Failed.");
            }
            else if (rtbFeedback.Text.Length > 250)
            {
                MessageBox.Show("You are exceeding the limit word count (250).");
            }
            else if (rtbFeedback.Text.Length <= 250)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Feedback (TPNumber, Name, Date, Feedback) VALUES (@TPNum, @Name, @Date, @Feedback)", con);
                cmd.Parameters.AddWithValue("@TPNum", TPNum);
                cmd.Parameters.AddWithValue("@Name", lblName.Text);
                cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@Feedback", rtbFeedback.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //displayData();
                MessageBox.Show("Feedback is sendt.");
                rtbFeedback.Text = string.Empty;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                dateTimePicker1.CustomFormat = "";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            lblWordCount.Text = rtbFeedback.Text.Length.ToString() + "/250";
        }

        private void pboExit_Click(object sender, EventArgs e)
        {
            TrainerMain f1 = new TrainerMain();
            f1.Show();
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
