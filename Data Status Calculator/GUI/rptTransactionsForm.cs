using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Reporting.WinForms;
 
namespace Data_Status_Calculator.GUI
{
    public partial class rptTransactionsForm : Form
    {
        public rptTransactionsForm()
        {
            InitializeComponent();
        }

        public rptTransactionsForm(Int16 rGroup,DateTime StartDate,DateTime LastDate,bool showSummaryOnly,string dataClerk)
        {
            InitializeComponent();
            xReportingGroup = rGroup;
            xStartDate = StartDate;
            xLastDate = LastDate;
            xShowSummaryOnly =showSummaryOnly;
            xDataClerk = dataClerk;
        }

        public enum ReportGrouping {GroupByDataClerk=1,GroupByTransactionDate=2};
        private static Int16 _xReportingGroup;
        private static DateTime _xLastDate;
        private static DateTime _xStartDate;
        private static bool _xShowSummaryOnly;
        private static string _xDataClerk;

        public Int16 xReportingGroup
        {
            get {
                return _xReportingGroup;
            }
            set {
                _xReportingGroup = value;
            }
        }

        public DateTime xStartDate
        {
            get            {                return _xStartDate;            }
            set            {                _xStartDate = value;            }
        }

        public DateTime xLastDate
        {
            get            {                return _xLastDate;            }
            set            {                _xLastDate = value;            }
        }


        public bool xShowSummaryOnly
        { 
            get{                return _xShowSummaryOnly;            }
            set{                _xShowSummaryOnly=value;            }
        }

        public string xDataClerk
        {
            get { return _xDataClerk; }
            set { _xDataClerk = value; }
        }


        private void rptTransactionsForm_Load(object sender, EventArgs e)
        {
            this.tblTransactionsTableAdapter1.FillByTransactionDate(this.dbDSCMainDataSet.tblTransactions,this.xStartDate.Date  ,this.xLastDate.Date,'%'+this.xDataClerk+'%'  );
          

            ReportParameter rpStartDate = new ReportParameter("StartDate", this.xStartDate.Date.ToString("yyyy/MM/dd") );
            ReportParameter rpLastDate = new ReportParameter("LastDate", this.xLastDate.Date.ToString("yyyy/MM/dd") );
            ReportParameter rpShowSummaryOnly = new ReportParameter("ShowSummaryOnly", this.xShowSummaryOnly.ToString());

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rpStartDate, rpLastDate, rpShowSummaryOnly });
            
            if (xReportingGroup == (Int16)ReportGrouping.GroupByDataClerk)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Data_Status_Calculator.Reports.rptTransactionsGroupedByDataClerk.rdlc";
            }
            else if (xReportingGroup == (Int16)ReportGrouping.GroupByTransactionDate)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Data_Status_Calculator.Reports.rptTransactionsGroupedByTransactionDate.rdlc";
            }
            
            this.reportViewer1.RefreshReport();
        }


    }
}
