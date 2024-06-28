using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Student
    {

        //attributes of Student class
        private string tpnum;
        private string name;
        private string email;
        private string contactNum;
        private string address;
        private string biodata;
        private string role;
        private string level;
        private string date;
        private string additional_module;
        private string month_of_enrollment;
        private string request_status;


        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
        // get set method for each attributes
        public string Email { get => email; set => email = value; }
        public string ContactNum { get => contactNum; set => contactNum = value; }
        public string Address { get => address; set => address = value; }
        public string Biodata { get => biodata; set => biodata = value; }
        public string Name { get => name; set => name = value; }
        public string Tpnum { get => tpnum; set => tpnum = value; }
        public string Role { get => role; set => role = value; }
        public string Level { get => level; set => level = value; }
        public string Date { get => date; set => date = value; }
        public string Additional_module { get => additional_module; set => additional_module = value; }
        public string Month_of_enrollment { get => month_of_enrollment; set => month_of_enrollment = value; }
        public string Request_status { get => request_status; set => request_status = value; }

        public Student(string t) // Student constructor
        {
            this.tpnum = t;
        }

        public Student(string tp, string n, string r, string e, string c, string a, string b) // Student constructor for View Profile
        {
            tpnum = tp;
            name = n;
            role = r;
            email = e;
            contactNum = c;
            address = a;
            biodata = b;
        }

        public Student(string tp, string n)
        {
            tpnum = tp;
            name = n;
        }


        public string updateProfile(string em, string cn, string a, string b) // class method
        {
            string status;
            con.Open();

            email = em;
            contactNum = cn;
            address = a;
            biodata = b;
            // update the new email, contact, address 
            SqlCommand cmd = new SqlCommand("update Students set Email = @e, ContactNumber = @c, Address = @a, Biodata = @b where TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@tp", tpnum);
            cmd.Parameters.AddWithValue("@e", em);
            cmd.Parameters.AddWithValue("@c", cn);
            cmd.Parameters.AddWithValue("@a", a);
            cmd.Parameters.AddWithValue("@b", b);
            

            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                status = "Update successfully!";
            else
                status = "Unable to update";

            con.Close();
            
            return status;
        }
        // class method to show the tpnumber and name when making payment
        public static void viewDetails (Student s1) 
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Students where TPNumber = @tp", con); // get information of student from Student class
            cmd.Parameters.AddWithValue("@tp", s1.tpnum);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                s1.name = rd["Name"].ToString();
            }
            con.Close();
        }

        //class method to view Student Profile
        public static void viewProfile (Student s1) 
        { 
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Students where TPNumber = @tp", con); // get information of student from Student class
            cmd.Parameters.AddWithValue("@tp", s1.tpnum);
            SqlDataReader rd = cmd.ExecuteReader();

            

            if (rd.Read())
            {
                s1.name = rd["Name"].ToString();
                s1.email = rd["Email"].ToString();
                s1.contactNum = rd["ContactNumber"].ToString();
                s1.address = rd["Address"].ToString();
                s1.biodata = rd["Biodata"].ToString();

            }
            rd.Close();
            SqlCommand cmd2 = new SqlCommand("select * from Users where TPNumber = @tp", con); // get the role from User class
            cmd2.Parameters.AddWithValue("@tp", s1.tpnum);
            SqlDataReader rd2 = cmd2.ExecuteReader();

            if (rd2.Read())
            {
                s1.role = rd2.GetString(3);
            }
            
            rd2.Close();
            con.Close();
        }

        //class method to send request
        public static void SendRequest(Student s1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Students where TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@tp", s1.tpnum);
            SqlDataReader rd = cmd.ExecuteReader();
            
            while(rd.Read())
            {
                s1.name = rd["Name"].ToString();
            }
            con.Close();

        }  
    }
}
