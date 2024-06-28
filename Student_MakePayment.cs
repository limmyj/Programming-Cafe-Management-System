using Assignment;
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

namespace Group_8_IOOP
{
    public partial class frmStudent_MakePayment : Form
    {
        public string tpnum; //GLOBAL
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public frmStudent_MakePayment()
        {
            InitializeComponent();
        }
        public frmStudent_MakePayment(string tp)
        {
            InitializeComponent();
            tpnum = tp;
        }

        private void frmStudent_MakePayment_Load(object sender, EventArgs e)
        {
            Student s1 = new Student(tpnum);
            lblDate.Text = DateTime.Now.ToLongDateString(); //get current date
            Student.viewDetails(s1);
            lblTPNumber.Text = s1.Tpnum;
            lblName.Text = s1.Name;
            RefreshComboBoxValues();

            lbl1.Text = s1.Tpnum;
        }

        //method to refresh combo box values
        private void RefreshComboBoxValues()
        {
            cboModuleID.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ModuleID FROM studentList WHERE TPNumber = @TP AND PaymentStatus = 'Unpaid'", con);
            cmd.Parameters.AddWithValue("@TP", tpnum);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                string moduleID = dr["ModuleID"].ToString();
                if (!cboModuleID.Items.Contains(moduleID))
                {
                    cboModuleID.Items.Add(moduleID);
                }
                
            }
            con.Close();
        }

        private void btnMakePayment_Click(object sender, EventArgs e)
        {
            if (cboModuleID.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select the Module ID");
            }
            else
            {
                string selectedModule = cboModuleID.SelectedItem.ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Charges FROM studentList where ModuleID = @id AND TPNumber = @tp", con);
                cmd.Parameters.AddWithValue("id", selectedModule);
                cmd.Parameters.AddWithValue("tp", tpnum);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string charge = dr["Charges"].ToString();
                    lblPaymentMessage.Text = "You have successfully paid: ";
                    lblcharge.Text = charge;
                }
                con.Close();

                con.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE studentList SET PaymentStatus = 'Paid' WHERE ModuleID = @id AND TPNumber = @tp", con);
                cmd2.Parameters.AddWithValue("@id", selectedModule);
                cmd2.Parameters.AddWithValue("@tp", tpnum);
                cmd2.ExecuteNonQuery();
                con.Close();

                con.Open();
                SqlCommand cmd3 = new SqlCommand("INSERT INTO Invoices (TPNumber, Name, Date, ModuleID, Charges) VALUES (@tp, @n, @d, @mid,@c)", con);
                cmd3.Parameters.AddWithValue("@tp", tpnum);
                cmd3.Parameters.AddWithValue("@n", Name);
                cmd3.Parameters.AddWithValue("@d", lblDate.Text);
                cmd3.Parameters.AddWithValue("mid", selectedModule);
                cmd3.Parameters.AddWithValue("@c", lblcharge.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd3);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dt.Columns.Add("InvoiceNumber", Type.GetType("System.Int32"));
                dt.Columns.Add("TPNumber", Type.GetType("System.String"));
                dt.Columns.Add("Name", Type.GetType("System.String"));
                dt.Columns.Add("Date", Type.GetType("System.String"));
                dt.Columns.Add("ModuleID", Type.GetType("System.String"));
                dt.Columns.Add("Charges", Type.GetType("System.String"));
                //allow invoice number to increase automatically by 1
                dt.Columns["InvoiceNumber"].AutoIncrement = true;
                dt.Columns["InvoiceNumber"].AutoIncrementSeed = 1;
                dt.Columns["InvoiceNumber"].AutoIncrementStep = 1;
                dt.Rows.Add(null, tpnum, Name, lblDate.Text, selectedModule, lblcharge.Text);
                con.Close();
            }

            RefreshComboBoxValues();
        }

        private void pboLogOut_Click(object sender, EventArgs e)
        {
            frmStudentPayment f2 = new frmStudentPayment();
            f2.Show();
            this.Hide();
        }

        private void pboSettings_Click(object sender, EventArgs e)
        {
            string TPNumber = tpnum;
            StudentSetting f3 = new StudentSetting(TPNumber);
            f3.Show(); //allows to move to Setting Page
            this.Hide();
        }

        private void cboModuleID_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedModule = cboModuleID.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM studentList WHERE TPNumber = @TP AND ModuleID = @selectedModule", con);
            cmd.Parameters.AddWithValue("@TP", tpnum);
            cmd.Parameters.AddWithValue("@selectedModule", selectedModule);


            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string charge = dr["Charges"].ToString();
                lblTotalAmount.Text = charge;
            }
            dr.Close();
            con.Close();
        }
    }
}
