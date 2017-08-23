using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Data_Status_Calculator.Properties;
using System.Configuration;

using System.IO;
  
namespace Data_Status_Calculator
{
    class DataAccess
    {
        public  static SqlConnection cnConnection = new SqlConnection();
        public enum RecordCheck { Error=0, Exist=1, NotExist=2};
        private static string strConnectionString=string.Empty  ;

        private static string server = ".\\sqlexpress2008R2";
        private static string db = "dbDSC";
        private static string uid = "sa";
        private static string pwd = "maun";
        private static string dsn = "N/A";
        private static string trusted ="False";  
 
        public static void setConnectionString()
        {
            //strConnectionString = "Data Source=" + server + ";Initial Catalog=" + db + "; User ID=" + uid + ";Password=" + pwd + ";Trusted_Connection=" + trusted  + ";";
            strConnectionString = Properties.Settings.Default.dbDSCConnectionString.ToString();    
            //try
            //{
            //    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(
            //                                                  "dbLIBConnectionString", strConnectionString));
            //    config.Save(ConfigurationSaveMode.Modified, true);
            //    ConfigurationManager.RefreshSection("connectionStrings");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(),"Configuration Error",MessageBoxButtons.OK,MessageBoxIcon.Error   );  
            //}
        }

        public static void getConnectionDetails()
        {
            string strPath = Application.StartupPath;
            string strConfigFile = "config.DAT";
            string FileURL=strPath +"\\" + strConfigFile;
            if (File.Exists(FileURL))
            {
                    using (StreamReader sr=File.OpenText(FileURL))
                    {
                       string input;
                                               
                       while ((input = sr.ReadLine()) != null)
                        {
                            
                            string s = input.ToString() ;
                            string[] varX = s.Split(' ');
                            switch (varX[0].ToString())
                            {
                                case "server":
                                    server = varX[1].ToString();
                                    break;
                                case "db":
                                    db = varX[1].ToString();
                                    break;
                                case "uid":
                                    uid = varX[1].ToString();
                                    break;
                                case "pwd":
                                    pwd = varX[1].ToString();
                                    break;
                                case "dsn":
                                    dsn = varX[1].ToString();
                                    break;
                                case "trusted":
                                    trusted = varX[1].ToString();
                                    break ;
                            }
                        }                       
                    }
  

            }
        }

        public static bool OpenConnection()
        {
            if (cnConnection.State != ConnectionState.Open)
            {
                try
                {
                    if (strConnectionString == string.Empty)
                    {
                        getConnectionDetails();
                        setConnectionString();
                    }
                        //string cnString = System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString;
                    //cnString =Settings.Default.dbLIBConnectionString;

                    cnConnection.ConnectionString = strConnectionString  ;
                    cnConnection.Open();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Unable To Open Database Connection: Reason " + ex.Message.ToString () , "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        
        public static Int16 RecordExists(string strSQL)
        {
            try
            {                
                DataTable dt = new DataTable();
                dt = GetDataTable(strSQL);
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt16 (RecordCheck.Exist);
                }
                else
                {
                    return Convert.ToInt16(RecordCheck.NotExist);
                }
            }
            catch
            {
                MessageBox.Show("Error: ");
                return Convert.ToInt16(RecordCheck.Error);            
            }

        }

        public static DataTable GetDataTable(string strSQL)
        {
            try
            {
                using (cnConnection)
                {
                    if (OpenConnection() == true)
                    {
                        SqlCommand cmd = new SqlCommand(strSQL, cnConnection);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt=new DataTable ();
                        da.Fill(dt);
                        return dt;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static SqlDataReader GetDataReader(string strSQL)
        {
            try
            {
                cnConnection = new SqlConnection();
                if (OpenConnection() == true)
                {
                    SqlCommand cmd = new SqlCommand(strSQL, cnConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        return dr;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public static long ExecuteQueryStatement(string strSQL)
        {
            long n = 0;
            using (cnConnection)
            {
                if (OpenConnection() == true)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(strSQL, cnConnection);                        
                        n = cmd.ExecuteNonQuery();
                        return n;
                    }
                    catch 
                    {
                        return n;
                    }
                }
                else
                {
                    return n;
                }
            }            
        }

        public static long ExecuteQueryRetScalar(string strSQL)
        {
            long n = 0;
            using (cnConnection)
            {
                if (OpenConnection() == true)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(strSQL, cnConnection);
                        string retVal = cmd.ExecuteScalar().ToString();
                        n = Convert.ToInt64(retVal) ;
                        //Console.WriteLine(cmd.ExecuteScalar().ToString() );  
                        return n;
                    }
                    catch
                    {
                        return n;
                    }
                }
                else
                {
                    return n;
                }
            }
        }

        public  static void BackUpDatabase()
        {
            string _BakUpDir = "C:\\";
            SqlConnection Temp = new SqlConnection(); ;

            SqlCommand cmd;
            try
            {
                string x = DateTime.Now.ToString("dd-MMM-yyy-HHmmss").ToUpper();
                string bak = _BakUpDir + x;
                string sqlQuery = "BACKUP DATABASE dbDSC TO Disk='" + bak + "_dbDSC'";
                
                Temp.ConnectionString = Settings.Default.dbDSCConnectionString;
                if (Temp.State == ConnectionState.Closed)
                {
                    Temp.Open();
                }
                cmd = new SqlCommand(sqlQuery, Temp);
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                {
                    MessageBox.Show("Query executed successfully.\nCreated: " + DateTime.Now.Date, "BackUp Progress | Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Back up data\n" + ex.Message, "BackUp Progress | Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Temp.Dispose();
            }
        }


    }
}
