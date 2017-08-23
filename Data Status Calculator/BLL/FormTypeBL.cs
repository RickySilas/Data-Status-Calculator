using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Data_Status_Calculator.DAL;
 
namespace Data_Status_Calculator.BLL
{
    class FormTypeBL:FormTypeDAL
    {
        #region Declarations
        private static String _FormTypeCode = string.Empty;
        private static String _FormTypeDescription = string.Empty;
        private static bool _Active = true;
        
        private static DateTime _DateCreated = DateTime.Now;  
        private static String _CreatedBy = string.Empty;
        private static DateTime _DateModified = DateTime.Now;  
        private static String _ModifiedBy = string.Empty;

        #endregion

        public static String  FormTypeCode
        {
            get
            {
                return _FormTypeCode;
            }
            set
            {
                _FormTypeCode = value;
            }
        }
        public static String FormTypeDescription
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
        public static bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
            }
        }

        public static DateTime  DateCreated
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
        public static String  CreatedBy
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
        
        public static DateTime  DateModified
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
        public static string  ModifiedBy
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

        public bool NewFormType()
        {
            bool ok = false;
            string strSQL = "SELECT FormTypeCode FROM tblFormType WHERE FormTypeCode LIKE '" + FormTypeBL.FormTypeCode + "'";
            Int16 retVal = DataAccess.RecordExists(strSQL);
            
            if (retVal == 1)
            {
                MessageBox.Show("Form Type Already Exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (retVal == 2)
            {
                if (AddFormType() > 0)
                {
                    ok = true;
                    MessageBox.Show("Save Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return ok;
        }

        public bool EditFormType()
        {
            bool ok = false;
            string strSQL = "SELECT FormTypeCode FROM tblFormType WHERE FormTypeCode LIKE '" + FormTypeBL.FormTypeCode + "'";
            Int16 retVal = DataAccess.RecordExists(strSQL);
            
            if (retVal == 1)
            {
                if (UpdateFormType() > 0)
                {
                    ok = true;
                    MessageBox.Show("Update Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (retVal == 2)
            {
                    MessageBox.Show("Record Does Not Exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return ok;
        }

        public bool DeleteFormType()
        {
            return true;
        }


    }
}
