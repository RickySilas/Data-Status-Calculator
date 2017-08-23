namespace Data_Status_Calculator.GUI
{
    partial class rptTransactionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dbDSCMainDataSet = new Data_Status_Calculator.DAL.DataObjects.dbDSCMainDataSet();
            this.tblTransactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblTransactionsTableAdapter1 = new Data_Status_Calculator.DAL.DataObjects.dbDSCMainDataSetTableAdapters.tblTransactionsTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dbDSCMainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblTransactionsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dbDSCMainDataSet
            // 
            this.dbDSCMainDataSet.DataSetName = "dbDSCMainDataSet";
            this.dbDSCMainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblTransactionsBindingSource
            // 
            this.tblTransactionsBindingSource.DataMember = "tblTransactions";
            this.tblTransactionsBindingSource.DataSource = this.dbDSCMainDataSet;
            // 
            // tblTransactionsTableAdapter1
            // 
            this.tblTransactionsTableAdapter1.ClearBeforeFill = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "TransactionsReport";
            reportDataSource3.Value = this.tblTransactionsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Data_Status_Calculator.Reports.rptTransactionsGroupedByDataClerk.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(886, 347);
            this.reportViewer1.TabIndex = 1;
            // 
            // rptTransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 347);
            this.Controls.Add(this.reportViewer1);
            this.Name = "rptTransactionsForm";
            this.Text = "Transactions Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rptTransactionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbDSCMainDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblTransactionsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DAL.DataObjects.dbDSCMainDataSet dbDSCMainDataSet;
        private System.Windows.Forms.BindingSource tblTransactionsBindingSource;
        private DAL.DataObjects.dbDSCMainDataSetTableAdapters.tblTransactionsTableAdapter tblTransactionsTableAdapter1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

    }
}