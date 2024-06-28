using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Data;

namespace Assignment
{
    public class Trainer
    {
        public string TPNum;
        public string name;
        public string email;
        public string contactNum;
        public string Address;
        public string Biodata;
        public string moduleName;
        public string charges;
        public string day;
        public string time;
        public string location;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public Trainer(string n)
        {
            TPNum = n;
        }

        public Trainer(string n, string t)
        {
            TPNum = n;
            name = t;
        }

        public string Email { get => email; set => email = value; }

        public string ContactNumber { get => contactNum; set => contactNum = value; }

        public string address { get => Address; set => Address = value; }

        public string biodata { get => Biodata; set => Biodata = value; }

        public string trainerWelcome()
        { 
            con.Open();

            //show trainer name for "Welcome trainer"
            SqlCommand cmd = new SqlCommand("SELECT Name FROM Trainers WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", TPNum);

            name = cmd.ExecuteScalar().ToString();

            con.Close();
            return name; //method trainerWelcome return value back to admin tpnum in DB
        }

        public static void viewProfile(Trainer o1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Trainers WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@TP", o1.TPNum);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                o1.name = rd["Name"].ToString();
                o1.email = rd["Email"].ToString();
                o1.contactNum = rd["ContactNumber"].ToString();
                o1.Address = rd["Address"].ToString();
                o1.Biodata = rd["Biodata"].ToString();
            }
            rd.Close();
            con.Close();
        }

        public string updateProfile(string em, string num, string ad, string bio)
        {
            string status;
            con.Open();

            email = em;
            contactNum = num;
            Address = ad;
            Biodata = bio;

            SqlCommand cmd = new SqlCommand("UPDATE Trainers SET Email = @e, ContactNumber = @c, Address = @a, Biodata = @bio WHERE TPNumber = @TP", con);
            cmd.Parameters.AddWithValue("@e", em);
            cmd.Parameters.AddWithValue("@c", num);
            cmd.Parameters.AddWithValue("@TP", TPNum);
            cmd.Parameters.AddWithValue("@bio", bio);
            cmd.Parameters.AddWithValue("@a", ad);

            int i = cmd.ExecuteNonQuery();
            if (i != 0)
            {
                status = "Profile updated successfully";
            }
            else
            {
                status = "Unable to update";
            }
            con.Close();

            return status;
        }
        public static void sendFeedback(Trainer o1)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Trainers WHERE TPNumber = @TP", con);
            cmd2.Parameters.AddWithValue("@TP", o1.TPNum);
            SqlDataReader rd2 = cmd2.ExecuteReader();

            while (rd2.Read())
            {
                o1.name = rd2["Name"].ToString();
            }
            con.Close();
        }
    }
}