using System;
using System.Collections;  
using System.Collections.Generic;
using System.Text;
using System.Data ;
using Microsoft.SqlServer;
using Data_Status_Calculator.DAL;
using Data_Status_Calculator.DAL.DataObjects;
using Data_Status_Calculator.DAL.DataObjects.dbDSCMainDataSetTableAdapters;
using Data_Status_Calculator.BLL;

using System.Data.SqlClient;
using System.Windows.Forms;
namespace Data_Status_Calculator.DAL
{
    class UserDAL
    {
        private static tblLOGINTableAdapter daUSER;
        private static DAL.DataObjects.dbDSCMainDataSet.tblLOGINDataTable dtUSER;
        private static DAL.DataObjects.dbDSCMainDataSet.tblLOGINRow drwUSER;
        private static int n = 0;

        internal static int AddUserDetails()
        {
            try
            {
                daUSER = new tblLOGINTableAdapter();
                dtUSER = new DAL.DataObjects.dbDSCMainDataSet.tblLOGINDataTable();

                drwUSER = dtUSER.NewtblLOGINRow();

                drwUSER.UserName = UserInfo.UserName;
                drwUSER.Password = LoginBLL.EncryptPassword(UserInfo.Password);
                drwUSER.OfficialName = UserInfo.OfficialName;
                drwUSER.AddEntry = UserInfo.AddEntry;
                drwUSER.EditEntry = UserInfo.EditEntry;
                drwUSER.DeleteEntry = UserInfo.DeleteEntry;
                drwUSER.AllowAccess = UserInfo.AllowAccess;
                drwUSER.ViewEntry = UserInfo.ViewEntry;
                drwUSER.ManageUsers = UserInfo.ManageUsers;
                drwUSER.DateCreated = UserInfo.DateCreated;
                drwUSER.CreatedBy = UserInfo.CreatedBy;

                drwUSER.ManageFormTypes = UserInfo.ManageFormTypes;
                drwUSER.DaysBeforeTransactionsLock = UserInfo.DaysBeforeTransactionsLock;


                dtUSER.Rows.Add(drwUSER);
                n = daUSER.Update(dtUSER);
                return n;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return n;
            }
        }

        internal static int UpdateUserDetails()
        {
            try
            {
                daUSER = new tblLOGINTableAdapter();
                dtUSER = new dbDSCMainDataSet.tblLOGINDataTable();

                daUSER.FillById(dtUSER, UserInfo.ID);
                drwUSER = dtUSER[0];

                drwUSER.UserName = UserInfo.UserName;
                if (UserInfo.ChangePassword == true)
                {
                    drwUSER.Password = LoginBLL.EncryptPassword(UserInfo.Password);
                }
                drwUSER.OfficialName = UserInfo.OfficialName;
                drwUSER.AddEntry = UserInfo.AddEntry;
                drwUSER.EditEntry = UserInfo.EditEntry;
                drwUSER.DeleteEntry = UserInfo.DeleteEntry;
                drwUSER.AllowAccess = UserInfo.AllowAccess;
                drwUSER.ViewEntry = UserInfo.ViewEntry;
                drwUSER.ManageUsers = UserInfo.ManageUsers;
                drwUSER.DateModified = UserInfo.DateModified;
                drwUSER.ModifiedBy = UserInfo.ModifiedBy;

                drwUSER.ManageFormTypes = UserInfo.ManageFormTypes;
                drwUSER.DaysBeforeTransactionsLock = UserInfo.DaysBeforeTransactionsLock;

                n = daUSER.Update(dtUSER);
                return n;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return n;
            }
        }


        internal static int SavePassword()
        {
            try
            {
                daUSER = new tblLOGINTableAdapter();
                dtUSER = new dbDSCMainDataSet.tblLOGINDataTable();

                daUSER.FillByUserName(dtUSER, UserInfo.UserName);
                drwUSER = dtUSER[0];

                drwUSER.UserName = UserInfo.UserName;
                drwUSER.Password = LoginBLL.EncryptPassword(UserInfo.Password);

                n = daUSER.Update(dtUSER);
                return n;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return n;
            }
        }

        internal static bool ValidatePassword(string CorrectPassword, string PasswordToValidate)
        {
            PasswordToValidate = LoginBLL.EncryptPassword(PasswordToValidate);
            if (CorrectPassword == PasswordToValidate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        internal static bool GetUserDetailsOLD(string UserName, string AccessRight)
        {
            bool retVal = false;
            string strSQL = "SELECT * FROM tblLOGIN WHERE UserName LIKE '" + UserName + "'";
            DataTable UserDt = DataAccess.GetDataTable(strSQL);
            if (UserDt != null)
            {
                if (UserDt.Rows.Count > 0)
                {
                    DataRow UserDr;
                    UserDr = UserDt.Rows[0];
                    if (UserDr[AccessRight].ToString() == "True")
                    {
                        retVal = true;
                    }
                }
            }
            return retVal;
        }

        internal static Hashtable GetUserDetails(string UserName)
        {
            string strSQL = "SELECT * FROM tblLOGIN WHERE UserName LIKE '" + UserName + "'";
            SqlDataReader UserDataRow = DataAccess.GetDataReader(strSQL);

            Hashtable ht=new Hashtable(); 
            while (UserDataRow.Read())
            {
                for (int i = 0; i < UserDataRow.FieldCount; i++)
                {
                    ht.Add(UserDataRow.GetName(i).ToString(), UserDataRow[i].ToString());
                }
            }
            UserDataRow.Dispose();          
            return ht ;
        }

    }
}
