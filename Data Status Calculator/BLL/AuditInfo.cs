using System;
using System.Collections.Generic;
using System.Text;
using Data_Status_Calculator.DAL; 

namespace Data_Status_Calculator.BLL
{
    class AuditInfo
    {
        private static string _UserName = string.Empty;
        private static DateTime _ActionDate = DateTime.Now;  
        private static string _ActionType = string.Empty;
        private static string _ActionDescription = string.Empty;

        public static string UserName
        {
            get {
                return _UserName;
            }
            set {
                _UserName = value;
            }
        }

        public static DateTime ActionDate
        {
            get
            {
                return _ActionDate;
            }
            set
            {
                _ActionDate = value;
            }
        }

        public static string ActionType
        {
            get
            {
                return _ActionType;
            }
            set
            {
                _ActionType = value;
            }
        }

        public static string ActionDescription
        {
            get
            {
                return _ActionDescription;
            }
            set
            {
                _ActionDescription = value;
            }
        }
        
        public static bool SaveAudit()
        {
            string strSQL = "INSERT INTO tblAudit (UserName,ActionDate,ActionType,ActionDescription) " +
                " VALUES ('" + UserName + "','" + ActionDate.ToString("yyyy/MM/dd HH:mm:ss")  + "','" + ActionType + "','" + ActionDescription + "')";

            if (DataAccess.ExecuteQueryStatement(strSQL) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
