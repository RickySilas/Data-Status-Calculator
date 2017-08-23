using System;
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
    class FormTypeDAL
    {
        private static tblFormTypeTableAdapter daFormTypeR;
        private static DAL.DataObjects.dbDSCMainDataSet.tblFormTypeDataTable dtFormTypeR;
        private static DAL.DataObjects.dbDSCMainDataSet.tblFormTypeRow drwFormTypeR;
        private static int n = 0;
        
        internal static int AddFormType()
        {
            try
            {
                daFormTypeR = new tblFormTypeTableAdapter();
                dtFormTypeR = new DAL.DataObjects.dbDSCMainDataSet.tblFormTypeDataTable();

                drwFormTypeR = dtFormTypeR.NewtblFormTypeRow();

                drwFormTypeR.FormTypeCode = FormTypeBL.FormTypeCode;
                drwFormTypeR.FormTypeDescription = FormTypeBL.FormTypeDescription;
                drwFormTypeR.Active = FormTypeBL.Active;
                drwFormTypeR.DateCreated = FormTypeBL.DateCreated;
                drwFormTypeR.CreatedBy = FormTypeBL.CreatedBy;

                dtFormTypeR.Rows.Add(drwFormTypeR);
                n = daFormTypeR.Update(dtFormTypeR);
                return n;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString()  ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return n;
            }
        }

        internal static int UpdateFormType()
        {
            try
            {
                daFormTypeR = new tblFormTypeTableAdapter();
                dtFormTypeR = new dbDSCMainDataSet.tblFormTypeDataTable();

                daFormTypeR.FillByCode(dtFormTypeR, FormTypeBL.FormTypeCode);
                drwFormTypeR = dtFormTypeR[0];

                drwFormTypeR.FormTypeCode = FormTypeBL.FormTypeCode;
                drwFormTypeR.FormTypeDescription = FormTypeBL.FormTypeDescription;
                drwFormTypeR.Active = FormTypeBL.Active;
                drwFormTypeR.DateModified = FormTypeBL.DateModified;
                drwFormTypeR.ModifiedBy = FormTypeBL.ModifiedBy;

                n = daFormTypeR.Update(dtFormTypeR);
                return n;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return n;
            }
        }

    }
}
