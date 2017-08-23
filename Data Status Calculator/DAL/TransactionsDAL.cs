using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using Microsoft.SqlServer;

using Data_Status_Calculator.BLL; 
using Data_Status_Calculator.DAL.DataObjects.dbDSCMainDataSetTableAdapters;


using System.Data.SqlClient;
using System.Windows.Forms;


namespace Data_Status_Calculator
{
    class TransactionsDAL
    {      
        private static tblTransactionsTableAdapter daTransactions;
        private static DAL.DataObjects.dbDSCMainDataSet.tblTransactionsDataTable  dtTransactions;
        private static DAL.DataObjects.dbDSCMainDataSet.tblTransactionsRow drwTransactions;
        private static int n = 0;
        
        internal static int AddTransaction()
        {
            try
            {
                
                daTransactions = new tblTransactionsTableAdapter();
                dtTransactions = new DAL.DataObjects.dbDSCMainDataSet.tblTransactionsDataTable();

                drwTransactions = dtTransactions.NewtblTransactionsRow();

                drwTransactions.ID = TransactionsBL.ID;
                drwTransactions.TransactionDate = TransactionsBL.TransactionDate ;
                drwTransactions.FormTypeCode = TransactionsBL.FormTypeCode;
                drwTransactions.FormTypeDescription  = TransactionsBL.FormTypeDescription;
                drwTransactions.FormsBfwd = TransactionsBL.FormsBfwd;
                drwTransactions.FormsReceived = TransactionsBL.FormsReceived;
                drwTransactions.FormsSorted = TransactionsBL.FormsSorted;
                drwTransactions.FormsScanned = TransactionsBL.FormsScanned;
                drwTransactions.FormsVerified = TransactionsBL.FormsVerified;
                drwTransactions.BackLog = TransactionsBL.BackLog;
                drwTransactions.DataClerk = TransactionsBL.DataClerk;
                drwTransactions.Deleted = TransactionsBL.Deleted;

                drwTransactions.DateCreated = TransactionsBL.DateCreated;
                drwTransactions.CreatedBy = TransactionsBL.CreatedBy;

                dtTransactions.Rows.Add(drwTransactions);
                n = daTransactions.Update(dtTransactions);
                return n;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString()  ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return n;
            }
        }

        internal static int UpdateTransaction()
        {
            try
            {
                daTransactions = new tblTransactionsTableAdapter();
                dtTransactions = new DAL.DataObjects.dbDSCMainDataSet.tblTransactionsDataTable();

                daTransactions.FillById(dtTransactions, TransactionsBL.ID);
                drwTransactions = dtTransactions[0];

                //drwTransactions.TransactionDate = TransactionsBL.TransactionDate;
                drwTransactions.FormTypeCode = TransactionsBL.FormTypeCode;
                drwTransactions.FormTypeDescription= TransactionsBL.FormTypeDescription;
                drwTransactions.FormsBfwd = TransactionsBL.FormsBfwd;
                drwTransactions.FormsReceived = TransactionsBL.FormsReceived;
                drwTransactions.FormsSorted  = TransactionsBL.FormsSorted;
                drwTransactions.FormsScanned = TransactionsBL.FormsScanned;
                drwTransactions.FormsVerified = TransactionsBL.FormsVerified;
                drwTransactions.BackLog  = TransactionsBL.BackLog;
                drwTransactions.DataClerk = TransactionsBL.DataClerk ;
                drwTransactions.DateCreated = TransactionsBL.DateCreated;
                drwTransactions.CreatedBy = TransactionsBL.CreatedBy ;
                drwTransactions.DateModified = TransactionsBL.DateModified;
                drwTransactions.ModifiedBy = TransactionsBL.ModifiedBy;

                n = daTransactions.Update(dtTransactions);
                return n;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return n;
            }
        }


        

        internal static bool GetTransactionsDetails(string TransactionsName,string AccessRight)
        {
            bool retVal = false ;
            string strSQL = "SELECT * FROM tblLOGIN WHERE TransactionsName LIKE '" + TransactionsName + "'";
            DataTable TransactionsDt = DataAccess.GetDataTable(strSQL);
            if (TransactionsDt != null) 
            { 
                if (TransactionsDt.Rows.Count>0 )
                    {
                        DataRow TransactionsDr ;
                        TransactionsDr = TransactionsDt.Rows[0];
                        if (TransactionsDr[AccessRight].ToString() == "True")
                        {
                            retVal = true;
                        }                        
                }
            }
            return retVal ;
        }


        internal static int DeleteTransaction()
        {
            n = 0;
            try
            {
                daTransactions = new tblTransactionsTableAdapter();
                dtTransactions = new DAL.DataObjects.dbDSCMainDataSet.tblTransactionsDataTable();
                daTransactions.FillById(dtTransactions, TransactionsBL.ID);

                drwTransactions = dtTransactions[0];

                drwTransactions.BeginEdit();

                drwTransactions.Deleted = TransactionsBL.Deleted;
                drwTransactions.DeletedBy = TransactionsBL.DeletedBy;
                drwTransactions.DateDeleted = TransactionsBL.DateDeleted;

                drwTransactions.EndEdit();

                n = daTransactions.Update(dtTransactions);

                return n;
            }
            catch
            {
                return n;
            }
        }


    internal static ComboBox getFormTypes(ComboBox cmb)
    {
        String strSQL = "SELECT FormTypeCode,FormTypeDescription FROM tblFormType";
        DataTable dt = DataAccess.GetDataTable(strSQL);
        cmb.DataSource = dt;
        cmb.ValueMember = "FormTypeCode";
        cmb.DisplayMember = "FormTypeDescription";
        return cmb;
    }

    internal static ComboBox getDataClerks(ComboBox cmb)
    {
        String strSQL = "SELECT UserName,OfficialName FROM tblLogin";
        DataTable dt = DataAccess.GetDataTable(strSQL);
        cmb.DataSource = dt;
        cmb.ValueMember = "UserName";
        cmb.DisplayMember = "OfficialName";
        return cmb;        
    }



    }
}
