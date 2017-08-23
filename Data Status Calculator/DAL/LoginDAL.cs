using System;
using System.Collections.Generic;
using System.Text;
using Data_Status_Calculator.DAL;
using System.Data;
using Microsoft.SqlServer;
using System.Data.SqlClient ;


namespace Data_Status_Calculator
{
    class LoginBLL:DataAccess 
    {
        private static long _LoginId = 0;
        private static string _UserName = string.Empty;
        private static string _OfficialName = string.Empty;        
        private static String  _LoginTime = "" ;
        private static DateTime _LogoutTime = DateTime.Now;
        private enum LoginResult {Error = -1, NotFound = 0, Found = 1, Disabled = 2 };

        public static long LoginId
        {
            get
            {
                return _LoginId;
            }
            set
            {
                _LoginId = value;
            }
        }

        public static string UserName
        { 
            get
            {
            return _UserName ;
            }
            set
            {
                _UserName= value;
            }        
        }

        public static string OfficialName
        {
            get
            {
                return _OfficialName;
            }
            set
            {
                _OfficialName = value;
            }
        }


        public static String  LoginTime
        {
            get
            {
                return _LoginTime;
            }
            set
            {
                _LoginTime= value;
            }
        }

        public static DateTime  logouttime
        {
            get
            {
                return _LogoutTime;
            }
            set
            {
                _LogoutTime= value;
            }
        }

        private static bool saveLogoutTime()
        {
            string strSQL = "Update tblLoginTrail SET LogoutTime=getDate() WHERE LoginId LIKE '" + LoginId + "'";
            if (DataAccess.ExecuteQueryStatement(strSQL ) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool saveLoginTime(string UserName)
        { 
            string strSQL = "INSERT INTO tblLoginTrail(UserName,LoginTime)VALUES('" + UserName  + "',getDate());"+
                "Select Scope_Identity()";
            LoginId=DataAccess.ExecuteQueryRetScalar(strSQL);
            if (LoginId>0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public static Int16 LoginUser(string UserName, string Password)
        {
            
            Password = LoginBLL.EncryptPassword(Password);
            Password = SqlSafe(Password, 30, true);
            string strSQL = "SELECT * FROM tblLOGIN WHERE UserName LIKE '" + UserName + "' AND Password LIKE '" + Password  + "'";


            DataTable LoginDt=DataAccess.GetDataTable(strSQL);
            if (LoginDt == null)
            {
                return Convert.ToInt16 (LoginResult.Error);
            }
            else
            {
                if (LoginDt.Rows.Count <= 0)
                {
                    return (Int16)LoginResult.NotFound;
                }
                else
                {
                    DataRow LoginDr = LoginDt.Rows[0];
                    if ((Boolean)LoginDr["AllowAccess"] == false)
                    {
                        return (Int16)LoginResult.Disabled;
                    }
                    else
                    {
                        saveLoginTime(UserName);
                        LoginTime =DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        OfficialName = LoginDr["OfficialName"].ToString ();

                        return (Int16)LoginResult.Found;
                    }
                }
            }
        }

        public static bool LogoutUser()
        { 
            return saveLogoutTime();
        }

        public static String EncryptPassword(string pwd)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pwd);
            data = x.ComputeHash(data);
            String md5Hash = System.Text.Encoding.ASCII.GetString(data);
            return md5Hash;
        }

        /// <summary>
        /// Returns a string with single quotes escaped to protect against SQL injection attacks
        /// 
        /// This method will throw an exception if the supplied string's length is greater than maxlen
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxlen"></param>
        /// <returns></returns>
        public static string SqlSafe(string s, int maxlen)
        {
            return SqlSafe(s, maxlen, false);
        }
        /// <summary>
        /// Returns a string with single quotes escaped to protect against SQL injection attacks
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxlen"></param>
        /// <param name="ThrowExceptionOnTruncate"></param>
        /// <returns></returns>
        public static string SqlSafe(string s, int maxlen, bool ThrowExceptionOnTruncate)
        {
            if (s == null)
            {
                return "";
            }
            if (ThrowExceptionOnTruncate && s.Length > maxlen)
            {
                throw new Exception("StringTools.SqlSafe string exceeds maximum length");
            }
            //replace apostrophies AFTER truncation 
            //(the doubles don't count for field length)
            
            return  Truncate(s, maxlen).Replace("'", "''");
        }

        public static string Truncate(string str, int maxLength) 
    {
    if (str == null) return null;
    return str.Substring(0, Math.Min(maxLength, str.Length));
}


    }
}
