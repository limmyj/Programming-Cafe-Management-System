using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    internal class Admin_Trainer
    {
        // 应该放在add trainer的form
        //attributes for adminAddTrainer part
        public string tTP; //trainer tp number
        public string tName; //trainer name
        public string tEmail; //trainer email
        private string tPW; //trainer pw

        public string tLevel;
        public string tModuleName;
        public string tModuleID;


        //db connection
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());

        public Admin_Trainer(string ttp)
        {
            tTP = ttp;
        }

        
        public Admin_Trainer(string ttp, string tlvl)
        {
            tTP = ttp;
            tLevel = tlvl;
        }

        /*
        public Admin_Trainer(string ttp, string tlvl, string tmdln, string tmdlid)
        {
            tTP = ttp;
            tModuleName = tmdln;
            tModuleID = tmdlid;
        }
        */

        public Admin_Trainer(string ttp, string tn, string te)
        {
            tTP = ttp;
            tName = tn;
            tEmail = te;

        }



        public Admin_Trainer(string ttp, string tn, string te, string tpw)
        {
            tTP = ttp;
            tName = tn;
            tEmail = te;
            tPW = tpw;
        }

        //member method
        //for all select trainer listbox from db
        public static ArrayList allTrainer()
        {
            ArrayList ast = new ArrayList();
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT TPNumber FROM Trainers", con);
            SqlDataReader readTrainer = cmd.ExecuteReader();

            while (readTrainer.Read())
            {
                ast.Add(readTrainer.GetString(0));
            }

            con.Close();
            return ast;
        }

        //showing all module including the module that trainer created
        /*
        public string trainerModule()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ModuleName FROM classSchedule WHERE TPNumber = '" + tTP + "'", con);
            //cmd.Parameters.AddWithValue("@TP", o1.TPNum);

            tModuleName = cmd.ExecuteScalar().ToString();
            con.Close();
            return tModuleName;
        }
        */
        /*
        public static ArrayList trainerModule()
        {
            ArrayList am = new ArrayList();
            con.Open();

            //module are based on the classSchedule table 
            //SELECT ModuleName FROM classSchedule WHERE Level = tlvl
            SqlCommand cmd = new SqlCommand("SELECT ModuleName FROM classSchedule WHERE TPNumber = '" + tTP + "'", con);
            SqlDataReader readModule = cmd.ExecuteReader();

            while (readModule.Read())
            {
                m1.tModuleName = readModule["Name"].ToString();
                am.Add(readModule.GetString(0));
            }

            con.Close();
            return am;
        }
        */

  
        //show trainer details for delete page
        public static void trainerDetails(Admin_Trainer o1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["APUCafe"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Name, Email, Level, Module, ModuleID FROM TrainerAssignment WHERE TPNumber = '" + o1.tTP + "'", con);
            //cmd.Parameters.AddWithValue("@TP", o1.TPNum);

            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                o1.tName = rd["Name"].ToString();
                o1.tEmail = rd["Email"].ToString();
                o1.tLevel = rd["Level"].ToString();
                o1.tModuleName = rd["Module"].ToString();
                o1.tModuleID = rd["ModuleID"].ToString();
            }
            rd.Close();
            con.Close();
        }

        /*
        //show trainerAssignemnt table in Assign Trainer Page
        public string showTrainerDetails()
        {

        }
        */

        //add trainer
        public string addTrainer()
        {
            string status;
            con.Open();

            //prevent duplicate data(based on tp num) created
            SqlCommand cmdadd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE TPNumber = '" + tTP + "'", con);

            int count = Convert.ToInt32(cmdadd.ExecuteScalar().ToString());
            //int count = int.Parse(cmdadd.ExecuteScalar().ToString());

            if (count > 0) //tpnumber already exists
            {
                status = "Sorry. TP Number have been used. Please use another TP Number.";
            }
            else //tpnumber havent been used before 
            {
                //table Users add acc --tpnum, pw, role
                SqlCommand cmdadd1 = new SqlCommand("INSERT INTO Users(TPNumber, Password, Role) VALUES('" + tTP + "', '" + tPW + "', 'Trainer')", con);

                //table Trainers add acc -- tpnum, name, email
                SqlCommand cmdadd2 = new SqlCommand("INSERT INTO Trainers(TPNumber, Name, Email) VALUES('" + tTP + "', '" + tName + "', '" + tEmail + "')", con);

                //table TrainerAssignment add acc -- tpnum, name, emil, level, module
                SqlCommand cmdadd3 = new SqlCommand("INSERT INTO TrainerAssignment(TPNumber, Name, Email, Level, Module, ModuleID) VALUES('" + tTP + "', '" + tName + "', '" + tEmail + "', 'not set', 'not set', 'not set')", con);

                //table TrainerMIR add acc -- tpnum, name, level, module
                SqlCommand cmdadd4 = new SqlCommand("INSERT INTO TrainerMIR(TPNumber, Name, Level, Module, ModuleID, Month, Income) VALUES('" + tTP + "', '" + tName + "', 'not set', 'not set', 'not set', 'not set', 'not set')", con);

                //cmdadd.ExecuteNonQuery();
                cmdadd2.ExecuteNonQuery();
                cmdadd3.ExecuteNonQuery();
                cmdadd4.ExecuteNonQuery();

                int i = cmdadd1.ExecuteNonQuery();

                if (i != 0)
                {
                    status = "Registration successful.";
                }
                else
                {
                    status = "Registration failed.";
                }
            }
            con.Close();
            return status;
        }

        //delete trainer
        public string deleteTrainer()
        {
            string status;
            con.Open();

            //table Users delete acc
            SqlCommand cmddlt1 = new SqlCommand("DELETE FROM Users WHERE TPNumber = '" + tTP + "'", con);

            //table Trainers delete acc
            SqlCommand cmddlt2 = new SqlCommand("DELETE FROM Trainers WHERE TPNumber = '" + tTP + "'", con);

            //table TrainerAssignment delete acc
            SqlCommand cmddlt3 = new SqlCommand("DELETE FROM TrainerAssignment WHERE TPNumber = '" + tTP + "'", con);

            //table TrainerMIR delete acc
            SqlCommand cmddlt4 = new SqlCommand("DELETE FROM TrainerMIR WHERE TPNumber = '" + tTP + "'", con);

            //table classSchedule delete acc
            SqlCommand cmddlt5 = new SqlCommand("DELETE FROM classSchedule WHERE TPNumber = '" + tTP + "'", con);

            //cmdadd.ExecuteNonQuery();
            cmddlt2.ExecuteNonQuery();
            cmddlt3.ExecuteNonQuery();
            cmddlt4.ExecuteNonQuery();
            cmddlt5.ExecuteNonQuery();

            int i = cmddlt1.ExecuteNonQuery();

            if (i != 0)
            {
                status = "Successfully deleted.";
            }
            else
            {
                status = "Failed to delete.";
            }
            con.Close();
            return status;
        }


        //assign trainer Level
        public string assignTrainerLevel()
        {
            string status;
            con.Open();

            //update TrainerAssignment table column Level and Module
            SqlCommand cmdass1 = new SqlCommand("UPDATE TrainerAssignment SET Level = '" + tLevel + "' WHERE TPNumber = '" + tTP + "'", con);

            //update TrainerMIR table column Level and Module
            SqlCommand cmdass2 = new SqlCommand("UPDATE TrainerMIR SET Level = '" + tLevel + "' WHERE TPNumber = '" + tTP + "'", con);

            //update classSchedule table column Level and Module
            //SqlCommand cmdass3 = new SqlCommand("UPDATE classSchedule SET Level = '" + tLevel + "' WHERE TPNumber = '" + tTP + "'", con);

            //level updated successfully = cmdass1, cmdass3
            int a = cmdass1.ExecuteNonQuery();
            int b = cmdass2.ExecuteNonQuery();
            //int c = cmdass3.ExecuteNonQuery();

            if (a != 0 && b != 0)
            {
                status = "Level (" + tLevel + ") assigned to Trainer (" + tTP +") successfully.";
            }
            else
            {
                status = "Failed to update trainer's level";
            }

            con.Close();
            return status;
        }
        /*
        //assign trainer Module
        public string assignTrainerModule(string m)
        {
            m = tModule;

            string status;
            con.Open();

            //update TrainerAssignment table column Level and Module
            SqlCommand cmdass3 = new SqlCommand("UPDATE TrainerAssignment SET Module = '" + tModule + "' WHERE TPNumber = '" + tTP + "'", con);

            //update TrainerMIR table column Level and Module
            SqlCommand cmdass4 = new SqlCommand("UPDATE TrainerMIR SET Module = '" + tModule + "' WHERE TPNumber = '" + tTP + "'", con);

            //module updated successfully = cmdass2, cmdass4
            int c = cmdass3.ExecuteNonQuery();
            int d = cmdass4.ExecuteNonQuery();

            if (c != 0 && d != 0)
            {
                status = "Module updated successfully.";
            }
            else
            {
                status = "Failed to update trainer's module";
            }

            con.Close();
            return status;
        }
        */

        public string assignModule(string ttp, string tlvl, string tmdln, string tmdlid)
        {
            string status;
            con.Open();

            //prevent duplicate data Module ID (based on Module ID) created
            SqlCommand cmdadd = new SqlCommand("SELECT COUNT(*) FROM classSchedule WHERE ModuleID = '" + tmdlid + "'", con);

            int count = Convert.ToInt32(cmdadd.ExecuteScalar().ToString());
            //int count = int.Parse(cmdadd.ExecuteScalar().ToString());

            if (count > 0) //module id already exists
            {
                status = "Sorry. Module ID have been used. Please use another Module ID.";
            }
            else //module id havent been used before 
            {

                //table trainerAssignment update module, module id
                //SqlCommand cmdaddm1 = new SqlCommand("UPDATE TrainerAssignment SET Module = '"+ tmdln +"', ModuleID = '"+ tmdlid +"' WHERE TPNumber = '"+ tTP +"'", con);


                //TrainerMIR TABLE 
                //table trainerMIR update module, module id
                //SqlCommand cmdaddm2 = new SqlCommand("UPDATE TrainerMIR SET Module = '" + tmdln + "', ModuleID = '" + tmdlid + "' WHERE TPNumber = '" + tTP + "'", con);

                //TrainerAssignemnt TABLE 
                //for adding trainer data that has more than 1 module assigned
                SqlCommand check1 = new SqlCommand("SELECT COUNT(*) FROM TrainerAssignment WHERE TPNumber = '" + tTP + "'AND Module = 'not set'AND ModuleID = 'not set'", con);
                //for add into TrainerAssignemnt table  
                int count1 = Convert.ToInt32(check1.ExecuteScalar().ToString());
                
                if (count1 == 0) //if first data no 'not set' value, means ady set the first module, can add 2nd data = create another row of data
                {
                    SqlCommand cmdaddm1 = new SqlCommand("INSERT INTO TrainerAssignment(TPNumber, Name, Email, Level, Module, ModuleID) VALUES('" + ttp + "', '" + tName + "', '" + tEmail + "', '" + tlvl + "', '" + tmdln + "', '" + tmdlid + "')", con);
                    cmdaddm1.ExecuteNonQuery();
                }
                else // COUNT <0 or >0 //first data still has 'not set' value, update 
                {
                    //table TrainerAssignemnt update module name, module id
                    SqlCommand cmdaddm2 = new SqlCommand("UPDATE TrainerAssignment SET Module = '" + tmdln + "', ModuleID = '" + tmdlid + "' WHERE TPNumber = '" + tTP + "'", con);
                    cmdaddm2.ExecuteNonQuery();
                }

                //TrainerMIR TABLE 
                //for adding trainer data that has more than 1 module assigned
                SqlCommand check2 = new SqlCommand("SELECT COUNT(*) FROM TrainerMIR WHERE TPNumber = '" + tTP + "'AND Module = 'not set' AND ModuleID = 'not set'", con);
                //for add into TrainerMIR table  
                int count2 = Convert.ToInt32(check2.ExecuteScalar().ToString());

                if (count2 == 0) //if first data no 'not set' value, means ady set the first module, can add 2nd data = create another row of data
                {
                    SqlCommand cmdaddm3 = new SqlCommand("INSERT INTO TrainerMIR(TPNumber, Name, Level, Module, ModuleID, Month, Income) VALUES('" + ttp + "', '" + tName + "', '" + tlvl + "', '" + tmdln + "', '" + tmdlid + "', 'not set', 'not set')", con);
                    cmdaddm3.ExecuteNonQuery();
                }
                else // COUNT <0 or >0 //first data still has 'not set' value, update 
                {
                    //table TrainerMIR update module name, module id
                    SqlCommand cmdaddm4 = new SqlCommand("UPDATE TrainerMIR SET Module = '" + tmdln + "', ModuleID = '" + tmdlid + "' WHERE TPNumber = '" + tTP + "'", con);
                    cmdaddm4.ExecuteNonQuery();
                }

                //add classSchecule data
                //table classSchedule add data
                SqlCommand cmdaddm5 = new SqlCommand("INSERT INTO classSchedule(TPNumber, Level, ModuleName, ModuleID, Charges, Day, Time, Location) VALUES('" + ttp + "', '" + tlvl + "', '" + tmdln + "', '" + tmdlid + "', 0, 'not set', 'not set', 'not set')", con);



                //cmdadd.ExecuteNonQuery();
                //cmdaddm2.ExecuteNonQuery();
                //cmdaddm5.ExecuteNonQuery();

                int i = cmdaddm5.ExecuteNonQuery();

                if (i != 0)
                {
                    status = "Module is assigned successfully.";
                }
                else
                {
                    status = "Failed to assign module.";
                }
            }
            con.Close();
            return status;
        }


        //trainer MIR
        //trainer feedbacks
    }
}
