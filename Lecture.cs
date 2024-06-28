using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Lecture
    {
        private string Name;
        private string TPNumber;
        private string email;
        private string contactNumber;
        private string Address;
        private string Level;
        private string Module;
        private DateTime StartMonth;
        private DateTime EndMonth;
        private string Role;
        private string BioData;
        private string Date;
        public string ModuleID;
        private string Trainer_TPNum;
        private string Charges;
        private string PaymentStatus;
        private string RequestID;

        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public string Email { get => email; set => email = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public string Name1 { get => Name; set => Name = value; }
        public string TPNumber1 { get => TPNumber; set => TPNumber = value; }
        public string BioData1 { get => BioData; set => BioData = value; }
        public string Role1 { get => Role; set => Role = value; }
        public string Address1 { get => Address; set => Address = value; }
        public string Date1 { get => Date; set => Date = value; }
        public string Level1 { get => Level; set => Level = value; }
        public string Module1 { get => Module; set => Module = value; }
        public DateTime StartMonth1 { get => StartMonth; set => StartMonth = value; }
        public string PaymentStatus1 { get => PaymentStatus; set => PaymentStatus = value; }
        public string Charges1 { get => Charges; set => Charges = value; }
        public string Trainer_TPNum1 { get => Trainer_TPNum; set => Trainer_TPNum = value; }

        public Lecture(string tp,string n, string e, string num,string a)
        {
            TPNumber = tp;
            Name = n;
            Email = e;
            ContactNumber = num;
            Address = a;
        }

        public Lecture(string tp)
        {
            TPNumber = tp;
        }

        public Lecture(string tp,string lvl,string m, DateTime sm,DateTime em)
        {
            TPNumber=tp;
            Level = lvl;
            Module = m;
            StartMonth = sm;
            EndMonth = em;
        }

        public Lecture(string m, string lvl)
        {
            Module = m;
            Level = lvl;
        }

        public Lecture(string id, string lvl,string mn)
        {
            RequestID = id;
            Level = lvl;
            Module = mn;
        }


        public Lecture(string e,string num,string a,string bio)
        {
            email = e;
            contactNumber = num;
            Address=a;
            BioData = bio;
        }

        public Lecture(string tp, string n, string l,string mid,string mn,string t_tp,string c,string ps, DateTime sm,DateTime em )
        {
            TPNumber = tp;
            Name = n;
            Level = l;
            ModuleID = mid;
            Module = mn;
            Trainer_TPNum = t_tp;
            Charges = c;
            PaymentStatus = ps;
            StartMonth = sm;
            EndMonth = em;
        }

        public string studentRegistration()
        {
            string status;
            con.Open();
            //prevent duplicate data(based on tp num) created
            SqlCommand cmdadd = new SqlCommand("SELECT COUNT(*) FROM Students WHERE TPNumber = @tp", con);
            cmdadd.Parameters.AddWithValue("@tp", TPNumber);

            int count = Convert.ToInt32(cmdadd.ExecuteScalar().ToString());

            if (count > 0) //tpnumber already exists
            {
                status = "Sorry. TP Number have been used. Please use another TP Number.";
            }
            else //tpnumber havent been used before 
            {
                SqlCommand cmd = new SqlCommand("insert into Students (TPNumber,Name,Email,ContactNumber,Address) values(@tp,@n,@e,@num,@a)", con);

                SqlCommand cmd2 = new SqlCommand("insert into Users(TPNumber,Password,role) values(@tp,@tp,'Student')", con);
                cmd.Parameters.AddWithValue("@tp", TPNumber);
                cmd2.Parameters.AddWithValue("@tp", TPNumber);
                cmd.Parameters.AddWithValue("@n", Name);
                cmd.Parameters.AddWithValue("@e", Email);
                cmd.Parameters.AddWithValue("@num", ContactNumber);
                cmd.Parameters.AddWithValue("@a", Address);

                cmd2.ExecuteNonQuery();
                int i = cmd.ExecuteNonQuery();
                if (i != 0)

                    status = "Registration Successful";

                else
                    status = "Registration Failed"; // if (i = 0) which mean no rows in the table is affected thus registration failed

                

                
            }
            con.Close();
            return status;
        }

        public static ArrayList ViewStudentTPNum()
        {
            //arraylist to create dynamic array
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("select TPNumber from Students", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetString(0));//add element into arraylist
            }
            con.Close() ;
            return nm;
        }

        public static void ViewStudentDetails(Lecture o1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Students where TPNumber = '" + o1.TPNumber + "'", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while(rd.Read())
            {
                o1.Name = rd.GetString(1);
                o1.Email = rd.GetString(2);
                o1.contactNumber = rd.GetString(3);
            }
            con.Close();
        }

        public string StudentEnrollment()
        {
            string status;
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into studentList (TPNumber,Name,Level,ModuleID,ModuleName,StartMonth,EndMonth,Trainer_TPNum,Charges,PaymentStatus) values(@tp,@n,@lvl,@mid,@m,@sm,@em,@t_tp,@c,@ps)", con);
            cmd.Parameters.AddWithValue("@tp", TPNumber);
            cmd.Parameters.AddWithValue("n", Name);
            cmd.Parameters.AddWithValue("@lvl", Level);
            cmd.Parameters.AddWithValue("@mid", ModuleID);
            cmd.Parameters.AddWithValue("@m", Module);
            cmd.Parameters.AddWithValue("@sm", StartMonth);
            cmd.Parameters.AddWithValue("@em", EndMonth);
            cmd.Parameters.AddWithValue("@t_tp",Trainer_TPNum);
            cmd.Parameters.AddWithValue("@c",Charges);
            cmd.Parameters.AddWithValue("@ps",PaymentStatus);

            int i = cmd.ExecuteNonQuery();
            if (i != 0)

                status = "Enrollment Successful";

            else
                status = "Enrollment Failed"; // if (i = 0) which mean no rows in the table is affected thus enrollment failed

            con.Close();

            return status;
        }
        public static ArrayList StudentCourseUpdation(Lecture o1)
        {
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("select TPNumber from studentList where ModuleID = '" + o1.ModuleID + "' AND Level = '" + o1.Level + "'AND ModuleName = '" + o1.Module + "'", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetString(0));//add element into arraylist
            }
            con.Close();
            return nm;
        }

        public string ApproveStudentRequest()
        {
            string status;
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into studentList(TPNumber, Name, Level, ModuleID, ModuleName, StartMonth, EndMonth, Trainer_TPNum, Charges, PaymentStatus) values(@tp, @n, @lvl, @mid, @m, @sm, @em, @t_tp, @c, @ps)", con);
            cmd.Parameters.AddWithValue("@tp", TPNumber);
            cmd.Parameters.AddWithValue("n", Name);
            cmd.Parameters.AddWithValue("@lvl", Level);
            cmd.Parameters.AddWithValue("@mid", ModuleID);
            cmd.Parameters.AddWithValue("@m", Module);
            cmd.Parameters.AddWithValue("@sm", StartMonth);
            cmd.Parameters.AddWithValue("@em", EndMonth);
            cmd.Parameters.AddWithValue("@t_tp", Trainer_TPNum);
            cmd.Parameters.AddWithValue("@c", Charges);
            cmd.Parameters.AddWithValue("@ps", PaymentStatus);

            int i = cmd.ExecuteNonQuery();
            if (i != 0)

                status = "Request Approved";
            else
                status = "Failed To Request";

            con.Close();
              
            return status;
        }
        public string updateProfile(string e, string num, string a, string bio)
        {
            string status;
            con.Open();

            email = e;
            contactNumber = num;
            Address = a;
            BioData = bio;

            SqlCommand cmd = new SqlCommand("UPDATE Lecturers SET Email = @e, ContactNumber = @c, Address = @a, Biodata = @bio WHERE TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@c", contactNumber);
            cmd.Parameters.AddWithValue("@TP", TPNumber);
            cmd.Parameters.AddWithValue("@bio", BioData);
            cmd.Parameters.AddWithValue("@a", Address);

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

        public static void ViewProfile(Lecture o1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Lecturers WHERE TPNumber = @tp", con);
            cmd.Parameters.AddWithValue("@tp", o1.TPNumber);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                o1.Name = rd["Name"].ToString();
                o1.Email = rd["Email"].ToString();
                o1.ContactNumber = rd["ContactNumber"].ToString();
                o1.Address = rd["Address"].ToString();
                o1.BioData = rd["Biodata"].ToString();
            }
            rd.Close();
            con.Close();
        }

        public static ArrayList StudentRequestID()
        {
            //arraylist to create dynamic array
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("select Id from Requests where RequestStatus = 'Pending'", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetString(0));//add element into arraylist
            }
            con.Close();
            return nm;
        }

        public string RequestRejected()
        {
            string status;
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Requests set RequestStatus = 'Rejected' where Id =@id and AdditionalModule =@m", con);
            cmd.Parameters.AddWithValue("@id", RequestID);
            cmd.Parameters.AddWithValue("@m", Module);

            int i = cmd.ExecuteNonQuery();
            if (i != 0)

                status = "Rejected";

            else
                status = "Failed to deny";

            con.Close();

            return status;
        }

    }

}
