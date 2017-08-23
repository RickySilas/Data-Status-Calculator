using System;

namespace Data_Status_Calculator.BLL
{
    class TransactionsBL:TransactionsDAL 
    {
        #region Declarations
        private static long _ID = 0;
        private static DateTime  _TransactionDate = DateTime.Now;
        private static string _FormTypeCode = string.Empty;
        private static string _FormTypeDescription = string.Empty;
        private static long _FormsBfwd = 0;
        private static long _FormsReceived = 0;
        private static long _FormsSorted = 0;
        private static long _FormsScanned = 0;
        private static long _FormsVerified = 0;
        private static long _BackLog = 0;
        private static string _DataClerk = string.Empty;
        private static string _CouncellorCode = string.Empty;
        private static DateTime _DateCreated = DateTime.Now;
        private static string _CreatedBy = string.Empty ;
        private static DateTime _DateModified = DateTime.Now;
        private static string _ModifiedBy = string.Empty;
         private static bool _Deleted = false;
         private static string _DeletedBy = string.Empty;
         private static DateTime _DateDeleted = DateTime.Now ;
        #endregion

        #region methods
        public static long ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public static DateTime TransactionDate
        {
            get
            {
                return _TransactionDate ;
            }
            set
            {
                _TransactionDate = value;
            }
        }

        public static string FormTypeCode
        {
            get
            {
                return _FormTypeCode ;
            }
            set
            {
                _FormTypeCode = value;
            }
        }

        public static string FormTypeDescription
        {
            get
            {
                return _FormTypeDescription;
            }
            set
            {
                _FormTypeDescription = value;
            }
        }

        public static long FormsBfwd
        {
            get
            {
                return _FormsBfwd;
            }
            set
            {
                _FormsBfwd = value;
            }
        }

        public static long FormsReceived
        {
            get
            {
                return _FormsReceived;
            }
            set
            {
                _FormsReceived = value;
            }
        }

        public static long FormsSorted
        {
            get
            {
                return _FormsSorted;
            }
            set
            {
                _FormsSorted = value;
            }
        }

        public static long FormsScanned
        {
            get
            {
                return _FormsScanned;
            }
            set
            {
                _FormsScanned = value;
            }
        }

        public static long FormsVerified
        {
            get
            {
                return _FormsVerified;
            }
            set
            {
                _FormsVerified = value;
            }
        }

        public static long BackLog
        {
            get
            {
                return _BackLog;
            }
            set
            {
                _BackLog = value;
            }
        }

        public static string DataClerk
        {
            get
            {
                return _DataClerk;
            }
            set
            {
                _DataClerk = value;
            }
        }

        public static DateTime DateCreated
        {
            get
            {
                return _DateCreated;
            }
            set
            {
                _DateCreated = value;
            }
        }

        public static string CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                _CreatedBy = value;
            }
        }
        public static DateTime DateModified
        {
            get
            {
                return _DateModified;
            }
            set
            {
                _DateModified = value;
            }
        }

        public static bool Deleted
        {
            get
            {
                return _Deleted;
            }
            set
            {
                _Deleted = value;
            }
        }


        public static string DeletedBy
        {
            get
            {
                return _DeletedBy;
            }
            set
            {
                _DeletedBy = value;
            }
        }

        public static DateTime DateDeleted
        {
            get
            {
                return _DateDeleted;
            }
            set
            {
                _DateDeleted = value;
            }
        }

        public static string ModifiedBy
        {
            get
            {
                return _ModifiedBy;
            }
            set
            {
                _ModifiedBy = value;
            }
        }

        internal static Int64 getBacklogBfwd(DateTime CurrentTransactiondate, string CurrentUserName,string FormType)
        {
            string strSQL = "SELECT top (1) Backlog FROM tblTransactions WHERE dbo.StripTimeFromDate(TransactionDate)<'" + CurrentTransactiondate.ToString("yyyy/MM/dd") + "' AND CreatedBy LIKE '" + CurrentUserName + "' AND FormTypeCode LIKE '"+FormType+"' ORDER BY TransactionDate DESC";
            return (Int64)DataAccess.ExecuteQueryRetScalar(strSQL);
            
        }

        internal static bool TransactionExists(DateTime CurrentTransactiondate,string FormType,string CurrentUserName)
        {
            string strSQL = "SELECT ID FROM tblTransactions WHERE dbo.StripTimeFromDate(TransactionDate)='" + CurrentTransactiondate.ToString("yyyy/MM/dd") + "' AND FormTypeCode LIKE '"+FormType+"' AND CreatedBy='" +CurrentUserName + "'";
            if (DataAccess.RecordExists(strSQL) == (Int16)DataAccess.RecordCheck.Exist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}
