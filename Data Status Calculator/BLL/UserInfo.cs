using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Data_Status_Calculator.DAL;

namespace Data_Status_Calculator.BLL
{
    class UserInfo : UserDAL
    {
        #region Declarations
        private static long _ID = 0;
        private static String _CurrentUserName = string.Empty;
        private static String _CurrentUserOfficialName = string.Empty;

        private static String _UserName = string.Empty;
        private static String _Password = string.Empty;
        private static String _OfficialName = string.Empty;
        private static Boolean _AllowAccess = Convert.ToBoolean(1);
        private static Boolean _AddEntry = Convert.ToBoolean(1);
        private static Boolean _DeleteEntry = Convert.ToBoolean(0);
        private static Boolean _EditEntry = Convert.ToBoolean(0);
        private static Boolean _ViewEntry = Convert.ToBoolean(0);
        private static Boolean _ChangePassword = Convert.ToBoolean(0);
        private static Boolean _ManageUsers = Convert.ToBoolean(0);
        private static Boolean _ManageFormTypes = Convert.ToBoolean(0);
        private static string _CurrentUserPassword = string.Empty;
        private static short _DaysBeforeTransactionsLock = 0;

        private static DateTime _DateCreated = DateTime.Now;
        private static String _CreatedBy = string.Empty;
        private static DateTime _DateModified = DateTime.Now;
        private static String _ModifiedBy = string.Empty;

        private static Int64 _LoginId = 0;
        #endregion

        public static long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public static String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public static String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public static String OfficialName
        {
            get { return _OfficialName; }
            set { _OfficialName = value; }
        }

        public static bool AllowAccess
        {
            get { return _AllowAccess; }
            set { _AllowAccess = value; }
        }

        public static bool AddEntry
        {
            get { return _AddEntry; }
            set { _AddEntry = value; }
        }
        public static bool DeleteEntry
        {
            get { return _DeleteEntry; }
            set { _DeleteEntry = value; }
        }
        public static bool EditEntry
        {
            get { return _EditEntry; }
            set { _EditEntry = value; }
        }
        public static bool ViewEntry
        {
            get { return _ViewEntry; }
            set { _ViewEntry = value; }
        }

        public static bool ManageUsers
        {
            get { return _ManageUsers; }
            set { _ManageUsers = value; }
        }

        public static bool ManageFormTypes
        {
            get { return _ManageFormTypes; }
            set { _ManageFormTypes = value; }
        }
        public static bool ChangePassword
        {
            get { return _ChangePassword; }
            set
            {
                _ChangePassword = value;
            }
        }
        public static DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        public static String CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public static DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }
        public static string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        public static String CurrentUserName
        {
            get { return _CurrentUserName; }
            set { _CurrentUserName = value; }
        }

        public static String CurrentUserPassword
        {
            get { return _CurrentUserPassword; }
            set { _CurrentUserPassword = value; }
        }


        public static String CurrentUserOfficialName
        {
            get { return _CurrentUserOfficialName; }
            set { _CurrentUserOfficialName = value; }
        }

        public static Int64 LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }

        public static short DaysBeforeTransactionsLock
        {
            get { return _DaysBeforeTransactionsLock; }
            set { _DaysBeforeTransactionsLock = value; }
        }

        public bool CanAddEntry;
        public bool CanEditEntry;
        public bool CanDeleteEntry;
        public bool CanViewEntry;
        public bool CanManageUsers;
        public bool CanManageFormTypes;

        public bool NewUserInfo()
        {
            bool ok = false;
            string strSQL = "SELECT UserName FROM tblLOGIN WHERE UserName LIKE '" + UserInfo.UserName + "'";
            Int16 retVal = DataAccess.RecordExists(strSQL);

            if (retVal == 1)
            {
                MessageBox.Show("User Name Already Exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (retVal == 2)
            {
                if (AddUserDetails() > 0)
                {
                    ok = true;
                    MessageBox.Show("Save Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return ok;
        }

        public bool EditUserInfo()
        {
            bool ok = false;
            string strSQL = "SELECT ID FROM tblLOGIN WHERE ID LIKE '" + UserInfo.ID + "'";
            Int16 retVal = DataAccess.RecordExists(strSQL);

            if (retVal == 1)
            {
                if (UpdateUserDetails() > 0)
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

        public bool DeleteUserInfo()
        {
            return true;
        }

        public bool GetUserInfo(string UserName)
        {
            try
            {

                Hashtable userDetailsList = new Hashtable();
                userDetailsList = GetUserDetails(UserName);

                foreach (DictionaryEntry UserDetailsEntry in userDetailsList)
                {
                    switch (UserDetailsEntry.Key.ToString())
                    {
                        case "AddEntry":
                            CanAddEntry = UserDetailsEntry.Value.ToString() == "True" ? true : false;
                            break;
                        case "DeleteEntry":
                            CanDeleteEntry = UserDetailsEntry.Value.ToString() == "True" ? true : false;
                            break;
                        case "EditEntry":
                            CanEditEntry = UserDetailsEntry.Value.ToString() == "True" ? true : false;
                            break;
                        case "ViewEntry":
                            CanViewEntry = UserDetailsEntry.Value.ToString() == "True" ? true : false;
                            break;
                        case "ManageUsers":
                            CanManageUsers = UserDetailsEntry.Value.ToString() == "True" ? true : false;
                            break;
                        case "ManageFormTypes":
                            CanManageFormTypes = UserDetailsEntry.Value.ToString()=="True"?true:false ;
                            break;
                        case "DaysBeforeTransactionsLock":
                            DaysBeforeTransactionsLock = Convert.ToInt16(  UserDetailsEntry.Value);
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(UserDetailsEntry.Key +" : "+UserDetailsEntry.Value);  
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation );  
                return false;
            }

        }

        public static bool LoginUser()
        {
            bool logged = false;

            Int16 retVal = LoginBLL.LoginUser(UserName, Password);
            if (retVal == -1)
            {
                MessageBox.Show("An error occured while trying to access data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (retVal == 0)
            {
                MessageBox.Show("Username and Password Do not match. Try Again..", "User Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (retVal == 1)
            {
                UserInfo.CurrentUserName = UserName;
                UserInfo.CurrentUserOfficialName = LoginBLL.OfficialName;
                UserInfo.CurrentUserPassword = LoginBLL.EncryptPassword(Password);
                UserInfo.LoginId = LoginBLL.LoginId;
                logged = true;
            }
            else if (retVal == 2)
            {
                MessageBox.Show("Your Account has been Denied Access. Contact the System Administrator..", "User Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return logged;
        }

        public static bool LogoutUser()
        {
            bool logged = false;
            LoginBLL.LoginId = UserInfo.LoginId;
            logged = LoginBLL.LogoutUser();

            return logged;
        }

    }
}
