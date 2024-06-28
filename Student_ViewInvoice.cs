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
    public partial class frmViewInvoice : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        public string TPNum; //global
        public frmViewInvoice()
        {
            InitializeComponent();
        }

        public frmViewInvoice(string tp)
        {
            InitializeComponent();
            TPNum = tp;
        }

        private void frmViewInvoice_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Invoices WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT SUM(Charges) FROM Invoices WHERE TPNumber = @tp", con);
            cmd2.Parameters.AddWithValue("@tp", TPNum);

            string total_charges = cmd2.ExecuteScalar().ToString();
            lblTotal.Text = "RM" + total_charges;
            con.Close();

            lbl1.Text = TPNum;
        }

        private void pboLogOut_Click(object sender, EventArgs e)
        {
            frmStudentPayment f2 = new frmStudentPayment();
            f2.Show();
            this.Hide();
        }

        private void pboSettings_Click(object sender, EventArgs e)
        {
            string TPNumber = TPNum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }
    }
}
