using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Assignment
{
    internal class Admin
    {
        //attributes for admin
        public string password;
        public string adminName;
        public string tpnum;
        public string adminEmail;
        public string adminContact;
        public string adminAddress;
        public string role;
        public string adminBio;

        //db connection
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());


        //mainly for update profile 
        public string Password { get => password; set => password = value; }
        //public string AdminName { get => adminName; set => adminName = value; }
        //public string Tpnum { get => tpnum; set => tpnum = value; }
        public string AdminEmail { get => adminEmail; set => adminEmail = value; }
        public string AdminContact { get => adminContact; set => adminContact = value; }
        public string AdminAddress { get => adminAddress; set => adminAddress = value; }
        public string Role { get => role; set => role = value; }
        public string AdminBio { get => adminBio; set => adminBio = value; }



        //constructors
        public Admin (string tp)
        {
            tpnum =tp;
        }

        public Admin(string tp, string n)
        {
            tpnum = tp;
            adminName = n;
        }

        public Admin(string tp, string n, string r, string e, string c, string a, string b)
        {
            tpnum = tp;
            adminName = n;
            //password = pw;
            adminEmail = e;
            adminContact = c;
            adminAddress = a;
            role = r;
            adminBio = b;
        }


        //member method
        

        //to show Welcome, name on ADMIN main page
        //DONE
        public string adminWelcome()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();

            //show admin name for "Welcome admin"
            SqlCommand cmdw = new SqlCommand("SELECT Name FROM Admins WHERE TPNumber = '" + tpnum +"'" , con);
            //cmdw.Parameters.AddWithValue("@tpnum", tpnum);
            //cmdw.Parameters.AddWithValue("Name", adminName);

            adminName = cmdw.ExecuteScalar().ToString();
            
            con.Close();
            return adminName; //method adminWelcome return value back to admin tpnum in DB
        }


        //to show all ADMIN details on the profile
        public static void adminProfile(Admin o1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Admins WHERE TPNumber = '" +o1.tpnum + "'", con);
            //cmd.Parameters.AddWithValue("@TP", o1.TPNum);
            
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                o1.adminName = rd["Name"].ToString();
                o1.AdminEmail = rd["Email"].ToString();
                o1.AdminContact = rd["ContactNumber"].ToString();
                o1.AdminAddress = rd["Address"].ToString();
                o1.AdminBio = rd["Biodata"].ToString();
            }
            rd.Close();
            con.Close();
        }

        //update admin profile
        public string updateAdminProfile(string em, string num, string ad, string bio)
        {
            string status;
            con.Open();

            adminEmail = em;
            adminContact = num;
            adminAddress = ad;
            adminBio = bio;

            SqlCommand cmd = new SqlCommand("UPDATE Admins SET Email = '" + adminEmail + "', ContactNumber = '" + adminContact + "', Address = '" + adminAddress + "', Biodata = '" + adminBio + "' WHERE TPNumber = '" + tpnum + "'", con);

            int i = cmd.ExecuteNonQuery();
            if (i != 0)
            {
                status = "Update Successfully";
            }
            else
            {
                status = "Unable to update";
            }
            con.Close();

            return status;
        }
    }
}
