using Group_8_IOOP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class User
    {
        private string TPNum;
        private string password;

        public User(string u, string p)
        {
            TPNum = u;
            password = p;
        }

        public string login(string un)
        {
            string status = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();

            //SQLCommand objectName = new Constructor(SQLQuery, connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE TPNumber = @TP AND Password = @p", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);
            cmd.Parameters.AddWithValue("@p", password);
            
            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            if (count > 0) //if login successful
            {
                SqlCommand cmd2 = new SqlCommand("SELECT Role FROM Users WHERE TPNumber = @TP AND Password = @p", con);
                cmd2.Parameters.AddWithValue("@TP", TPNum);
                cmd2.Parameters.AddWithValue("@p", password); 
                string userRole = cmd2.ExecuteScalar().ToString();

                if (userRole == "Trainer")
                {
                    TrainerMain t = new TrainerMain(un);
                    t.Show();

                    LoginPage log = new LoginPage();
                    log.Hide();
                }
                else if (userRole == "Student")
                {
                    frmStudentMainpage s = new frmStudentMainpage(un);
                    s.Show();

                    LoginPage log = new LoginPage();
                    log.Hide();
                }
                else if (userRole == "Admin")
                {
                    Admin_MainPage a = new Admin_MainPage(un);
                    a.Show();

                    LoginPage log = new LoginPage();
                    log.Hide();
                }
                else if (userRole == "Lecture")
                {
                    LectureMain l = new LectureMain(un);
                    l.Show();
                }
            }
            else
            {
                status = "Incorrect TP Number/password";
            }
            con.Close();

            return status;
        }
    }
}
