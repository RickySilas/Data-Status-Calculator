using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Data_Status_Calculator.BLL;
using Data_Status_Calculator.GUI;
using Data_Status_Calculator.DAL.DataObjects;
using Data_Status_Calculator.DAL.DataObjects.dbDSCMainDataSetTableAdapters;


namespace Data_Status_Calculator
{
    public partial class DSCMain : Form
    {
        public DSCMain()
        {
            InitializeComponent();
        }

        UserInfo CurrentUser;

        private static StringBuilder sb;
        private static bool Proceed = true;
        private static string err;

        private enum CurrentAction { Add = 1, Edit = 2, Delete = 3, Save = 5, Cancel = 4 };
        private static Int16 XCurrentAction;

        private Int16 DaysBeforeTransactionsLock;


        private static dbDSCMainDataSet MainDs = new dbDSCMainDataSet();
        BindingSource MainBs = new BindingSource(MainDs, "tblTransactions");
        tblTransactionsTableAdapter MainDa = new tblTransactionsTableAdapter();
        //DAL.DataObjects.dbDSCMainDataSet.tblTransactionsDataTable MainDt = new DAL.DataObjects.dbDSCMainDataSet.tblTransactionsDataTable();



        private void ResetControls()
        {
            XCurrentAction = (Int16)CurrentAction.Cancel;
            this.MainTableLayoutPanel.Enabled = false;
            this.EditToolStripButton.Enabled = true;
            this.AddNewToolStripButton.Enabled = true;
            this.SaveToolStripButton.Enabled = false;
            this.EditToolStripButton.Enabled = true;
            this.TransactionsDataGridView.Enabled = true;
            this.SearchTableLayoutPanel.Enabled = true;

            ePX.Clear();

            this.SearchStartDateDateTimePicker.Value = SearchLastDateDateTimePicker.Value.AddDays(-7);
            this.SearchStartDateDateTimePicker.Checked = false;
            this.SearchLastDateDateTimePicker.Checked = false;
            //disable the addnew button if transactions for today already exist
            //if (TransactionsBL.TransactionExists(DateTime.Now ))
            //{
            //    this.AddNewToolStripButton.Enabled = false;
            //}
        }

        private void AddRecord()
        {
            XCurrentAction = (Int16)CurrentAction.Add;
            this.MainTableLayoutPanel.Enabled = true;
            this.EditToolStripButton.Enabled = false;
            this.AddNewToolStripButton.Enabled = false;
            this.EditToolStripButton.Enabled = false;
            this.SaveToolStripButton.Enabled = true;
            this.TransactionsDataGridView.Enabled = false;
            this.SearchTableLayoutPanel.Enabled = false;


        }

        private void EditRecord()
        {
            XCurrentAction = (Int16)CurrentAction.Edit;
            this.MainTableLayoutPanel.Enabled = true;
            this.EditToolStripButton.Enabled = false;
            this.AddNewToolStripButton.Enabled = false;
            this.SaveToolStripButton.Enabled = true;
            this.TransactionsDataGridView.Enabled = false;
            this.SearchTableLayoutPanel.Enabled = false;

        }

        private void ResetBindings(Control pCtrl)
        {
            foreach (Control ctl in pCtrl.Controls)
            {
                if (ctl.HasChildren)
                {
                    ResetBindings(ctl);
                }
                if (ctl is TextBox)
                {
                    ctl.ResetText();
                }
            }
        }

        private void RemoveBindings(Control pCtrl)
        {
            try
            {
                foreach (Control ctl in pCtrl.Controls)
                {
                    if (ctl.HasChildren)
                    {
                        RemoveBindings(ctl);
                    }
                    if (ctl is DateTimePicker)
                    {
                        foreach (Binding bnd in ctl.DataBindings)
                        {
                            ctl.DataBindings.Remove(bnd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                //throw new Exception("xxxx") ;
            }
        }


        private void NewRecordBindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            AddRecord();
            //MainBs.AddNew();
            MainBs.ResetCurrentItem();
            //MainBs.MoveFirst();

            ResetBindings(this);
        }

        private bool FormIsValid()
        {
            Proceed = true;
            sb = new StringBuilder("Please resolve the following to proceed:\n");

            foreach (Control a in MainTableLayoutPanel.Controls)
            {
                #region ComboBox
                if ((a is ComboBox) && (a.CausesValidation))
                {
                    ComboBox cntrl = (ComboBox)a;
                    if (cntrl.Text.Length.Equals(0))
                    {
                        ePX.SetError(cntrl, cntrl.AccessibleDescription);
                        sb.Append("* " + ePX.GetError(cntrl) + " \n");
                        Proceed = false;
                    }
                    else
                    {
                        ePX.SetError(cntrl, "");
                    }
                }
                #endregion
                #region Dates
                if ((a is DateTimePicker) && (a.CausesValidation))
                {
                    DateTimePicker cntrl = (DateTimePicker)a;
                    if (!cntrl.Checked)
                    {
                        ePX.SetError(cntrl, cntrl.AccessibleDescription);
                        sb.Append("* " + ePX.GetError(cntrl) + " \n");
                        Proceed = false;
                    }
                    else
                    {
                        ePX.SetError(cntrl, "");
                    }
                }
                #endregion
                #region TextBox
                if ((a is TextBox) && (a.CausesValidation))
                {
                    TextBox cntrl = (TextBox)a;
                    if (cntrl.Text.Length.Equals(0))
                    {
                        ePX.SetError(cntrl, cntrl.AccessibleDescription);
                        sb.Append("* " + ePX.GetError(cntrl) + " \n");
                        Proceed = false;
                    }
                    else
                    {
                        ePX.SetError(cntrl, "");
                    }
                }
                #endregion
                #region Numericupdown
                if ((a is NumericUpDown) && (a.CausesValidation))
                {
                    NumericUpDown cntrl = (NumericUpDown)a;
                    if (Convert.ToDouble(cntrl.Value) == 0)
                    {
                        ePX.SetError(cntrl, cntrl.AccessibleDescription);
                        sb.Append("* " + ePX.GetError(cntrl) + " \n");
                        Proceed = false;
                    }
                    else
                    {
                        ePX.SetError(cntrl, "");
                    }
                }
                #endregion
                #region Panel.RadioButtons
                if ((a.HasChildren) && (a.CausesValidation))
                {
                    bool radioOK = false;
                    bool rbExists = false;
                    foreach (Control d in a.Controls)
                    {
                        #region Radiobuttons
                        if ((d is RadioButton) && (d.CausesValidation))
                        {
                            rbExists = true;
                            RadioButton cntrl = (RadioButton)d;
                            if (cntrl.Checked)
                            {
                                radioOK = true;
                            }
                        }
                        #endregion
                        #region TextBox
                        if ((d is TextBox) && (d.CausesValidation))
                        {
                            TextBox cntrl = (TextBox)d;
                            if (cntrl.Text.Length.Equals(0))
                            {
                                ePX.SetError(cntrl, cntrl.AccessibleDescription);
                                sb.Append("* " + ePX.GetError(cntrl) + " \n");
                                Proceed = false;
                                Console.Write(d.Name);

                            }
                            else
                            {
                                ePX.SetError(cntrl, "");
                            }
                        }
                        #region Dates
                        if ((d is DateTimePicker) && (d.CausesValidation))
                        {
                            DateTimePicker cntrl = (DateTimePicker)d;
                            if (!cntrl.Checked)
                            {
                                ePX.SetError(cntrl, cntrl.AccessibleDescription);
                                sb.Append("* " + ePX.GetError(cntrl) + " \n");
                                Proceed = false;
                            }
                            else
                            {
                                ePX.SetError(cntrl, "");
                            }
                        }
                        #endregion
                        #endregion
                    }
                    #region Checkradiobuttons
                    if ((!radioOK) && (rbExists))
                    {
                        ePX.SetError(a, a.AccessibleDescription);
                        sb.Append("* " + ePX.GetError(a) + " \n");
                        Proceed = false;
                    }
                    else
                    {
                        ePX.SetError(a, "");
                    }
                    #endregion
                }
                #endregion

            }

            #region CustomValidation
            if (Convert.ToInt32(this.formsSortedTextBox.Text) > (Convert.ToInt32(formsReceivedTextBox.Text) + Convert.ToInt32(formsBfwdTextBox.Text)))
            {
                ePX.SetError(formsSortedTextBox, "Forms Sorted Cannot be greater than the forms received");
                sb.Append("* " + "Forms Sorted Cannot be greater than the forms received" + " \n");
                Proceed = false;

            }
            else
            {
                ePX.SetError(this.formsSortedTextBox, "");
            }


            if (Convert.ToInt32(this.formsScannedTextBox.Text) > (Convert.ToInt32(formsReceivedTextBox.Text) + Convert.ToInt32(formsBfwdTextBox.Text)))
            {
                ePX.SetError(formsScannedTextBox, "Forms Scanned Cannot be greater than the forms received");
                sb.Append("* " + "Forms Scanned Cannot be greater than the forms received" + " \n");
                Proceed = false;

            }
            else
            {
                ePX.SetError(this.formsScannedTextBox, "");
            }

            if (Convert.ToInt32(this.formsVerifiedTextBox.Text) > (Convert.ToInt32(formsReceivedTextBox.Text) + Convert.ToInt32(formsBfwdTextBox.Text)))
            {
                ePX.SetError(formsVerifiedTextBox, "Forms Verified Cannot be greater than the forms received");
                sb.Append("* " + "Forms Verified Cannot be greater than the forms received" + " \n");
                Proceed = false;

            }
            else
            {
                ePX.SetError(this.formsVerifiedTextBox, "");
            }

            if (XCurrentAction == (Int16)CurrentAction.Add)
            {
                if (TransactionsBL.TransactionExists(this.transactionDateDateTimePicker.Value, this.formTypeCodeComboBox.SelectedValue.ToString(), UserInfo.CurrentUserName))
                {
                    ePX.SetError(transactionDateDateTimePicker, "This transaction Already Exists. You cannot Proceed On");
                    sb.Append("* " + "This transaction Already Exists. You cannot Proceed On" + " \n");
                    Proceed = false;
                }
                else
                {
                    ePX.SetError(this.transactionDateDateTimePicker, "");
                }

            }

            #endregion

            err = sb.ToString();

            return Proceed;
        }

        private void AddNewToolStripButton_Click(object sender, EventArgs e)
        {
            AddRecord();
            MainBs.AddNew();
            //MainBs.ResetCurrentItem();
            //MainBs.MoveFirst();

            ResetBindings(this);
        }

        private void AddNewToolStripButton_Click_1(object sender, EventArgs e)
        {
            AddRecord();
            MainBs.AddNew();
            //bindingSource1.AddNew();

            this.transactionDateDateTimePicker.Checked = true;
            this.transactionDateDateTimePicker.MaxDate = DateTime.Now.Date;
            this.transactionDateDateTimePicker.MinDate = DateTime.Now.AddDays(DaysBeforeTransactionsLock).Date;

            this.transactionDateDateTimePicker.Value = DateTime.Now.Date;
            this.iDTextBox.ReadOnly = true;
            this.formTypeCodeComboBox.SelectedIndex = -1;
            this.dataClerkComboBox.SelectedValue = UserInfo.UserName;
            this.dataClerkComboBox.Enabled = false;
            //this.backLogTextBox.Enabled = false;
            computeBacklogBfwd();
        }

        private void FormTypeButton_Click(object sender, EventArgs e)
        {
            Form FormType = new FormTypeForm();
            if (FormType.ShowDialog() == DialogResult.OK)
            {
                LoadCombos();
            }
        }

        private void EditToolStripButton_Click(object sender, EventArgs e)
        {
            if (TransactionsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select the record to edit", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (transactionDateDateTimePicker.Value.Date < DateTime.Now.AddDays(DaysBeforeTransactionsLock).Date || this.SearchDataClerkComboBox.SelectedValue.ToString().Trim().ToLower() != UserInfo.CurrentUserName.Trim().ToLower())
            {
                MessageBox.Show("You cannot edit this record. \nContact the Application Administrator for Advice", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.iDTextBox.Text = TransactionsDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            this.transactionDateDateTimePicker.Checked = true;
            this.dataClerkComboBox.Enabled = false;
            EditRecord();

            //this.backLogTextBox.Enabled = false;
            computeBacklogBfwd();
            //this.formsBfwdTextBox.Enabled = true;
        }

        private void CancelToolStripButton_Click(object sender, EventArgs e)
        {
            ResetControls();
            loadInitialData();
            LoadCombos();
            MainBs.CancelEdit();

            //MainBs.ResetBindings(true);
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            TransactionsBL TransactionsMain = new TransactionsBL();
            if (FormIsValid())
            {
                if (string.IsNullOrEmpty(this.iDTextBox.Text.Trim().ToString()))
                {
                    TransactionsBL.ID = 0;
                }
                else
                {
                    TransactionsBL.ID = Convert.ToInt64(this.iDTextBox.Text.ToString());
                }
                if (XCurrentAction == (Int16)CurrentAction.Add)
                {
                    TransactionsBL.TransactionDate = this.transactionDateDateTimePicker.Value.Date;
                }
                TransactionsBL.FormTypeCode = this.formTypeCodeComboBox.SelectedValue.ToString();
                TransactionsBL.FormTypeDescription = this.formTypeCodeComboBox.Text;
                TransactionsBL.FormsBfwd = Convert.ToInt64(this.formsBfwdTextBox.Text);
                TransactionsBL.FormsReceived = Convert.ToInt64(this.formsReceivedTextBox.Text);
                TransactionsBL.FormsSorted = Convert.ToInt64(this.formsSortedTextBox.Text);
                TransactionsBL.FormsScanned = Convert.ToInt64(this.formsScannedTextBox.Text);
                TransactionsBL.FormsVerified = Convert.ToInt64(this.formsVerifiedTextBox.Text);
                TransactionsBL.BackLog = TransactionsBL.FormsBfwd + TransactionsBL.FormsReceived - TransactionsBL.FormsVerified;
                TransactionsBL.DataClerk = this.dataClerkComboBox.Text;

                TransactionsBL.DateCreated = Convert.ToDateTime((DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                TransactionsBL.CreatedBy = UserInfo.CurrentUserName;
                TransactionsBL.DateModified = Convert.ToDateTime((DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                TransactionsBL.ModifiedBy = UserInfo.CurrentUserName;

                //this.backLogTextBox.Text = TransactionsBL.BackLog.ToString (); 

                if (XCurrentAction == (Int16)CurrentAction.Add)
                {
                    if (TransactionsBL.AddTransaction() > 0)
                    {
                        this.ResetControls();
                        loadInitialData();
                    }
                }
                else if (XCurrentAction == (Int16)CurrentAction.Edit)
                {
                    if (TransactionsBL.UpdateTransaction() > 0)
                    {
                        this.ResetControls();
                        loadInitialData();
                    }
                }

            }
            else
            {
                MessageBox.Show(err, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void DSCMain_Activated(object sender, EventArgs e)
        {
            //CurrentUser = new UserInfo();
            //CurrentUser.GetUserInfo(UserInfo.CurrentUserName);
            //AuditInfo.UserName = UserInfo.CurrentUserName;

            this.ActiveUsertoolStripStatusLabel.Text = "Logged In As " + UserInfo.CurrentUserName + ": " + UserInfo.CurrentUserOfficialName;
        }

        private void DSCMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserInfo.LogoutUser();
            Application.Exit();
        }

        private void DSCMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            DialogResult rs = MessageBox.Show("Do you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs.ToString().Equals(DialogResult.Yes.ToString()))
            {
                e.Cancel = false;
            }
        }

        private void TransactiondateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TransactionDatePanel.Enabled = this.TransactiondateCheckBox.Checked;
        }

        private void DataClertCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.SearchDataClerkComboBox.Enabled = this.DataClerkCheckBox.Checked;
            this.SearchDataClerkComboBox.SelectedIndex = -1;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveBindings(MainTableLayoutPanel);
                MainBs.ResetBindings(true);

                if (this.SelectedDataClerkRadioButton.Checked == true)
                {
                    MainDa.FillByUser(MainDs.tblTransactions, SearchDataClerkComboBox.SelectedValue.ToString());
                }
                else
                {
                    MainDa.Fill(MainDs.tblTransactions);
                }
                DataInterface.BindObjects(MainTableLayoutPanel, MainBs);

                StringBuilder strSearch = new StringBuilder("");
                if (DataClerkCheckBox.Checked == true)
                {
                    strSearch.Append("DataClerk LIKE '%" + this.SearchDataClerkComboBox.Text + "%' AND ");
                }

                if (TransactiondateCheckBox.Checked == true)
                {
                    strSearch.Append("TransactionDate>= '" + this.SearchStartDateDateTimePicker.Value.ToString("yyyy/MM/dd") + "' AND TransactionDate<='" + this.SearchLastDateDateTimePicker.Value.ToString("yyyy/MM/dd") + "' AND ");
                }


                strSearch.Append(strSearch.Length > 0 ? ")" : "");
                strSearch.Replace("AND )", "");

                MainBs.Filter = strSearch.ToString();
                //this.TransactionsDataGridView.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void DSCMain_Load(object sender, EventArgs e)
        {
            MainDs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            MainDa.ClearBeforeFill = true;

            LoadCurrentUser();
            SetAccessibleAreas();

            ResetControls();
            loadInitialData();
            LoadCombos();

            DataInterface.BindObjects(MainTableLayoutPanel, MainBs);

            this.TransactionsDataGridView.Columns["FormTypeCode"].Visible = false;
            this.TransactionsDataGridView.Columns["ID"].Visible = false;

            this.DaysBeforeTransactionsLock = (Int16)(-1 * UserInfo.DaysBeforeTransactionsLock);

        }

        private void PrintPreviewButton_Click(object sender, EventArgs e)
        {
            string SelectedDataClerk = this.SearchDataClerkComboBox.SelectedIndex == -1 ? string.Empty : this.SearchDataClerkComboBox.SelectedValue.ToString();
            if (DataClerkCheckBox.Checked == true)
            {
                if (SelectedDataClerk == string.Empty)
                {
                    MessageBox.Show("Select a Data Clerk on the Search Filter pane or uncheck the [Data Clerk] Checkbox");
                    return;
                }
            }
            using (Form myReport = new GUI.rptTransactionsForm(ReportDataClerksRadioButton.Checked == true ? (Int16)1 : (Int16)2, SearchStartDateDateTimePicker.Value, SearchLastDateDateTimePicker.Value, SummaryOnlyCheckBox.Checked, SelectedDataClerk))
            {
                myReport.ShowDialog();
            }
        }

        private void loadInitialData()
        {
            RemoveBindings(MainTableLayoutPanel);
            MainDa.FillByUser(MainDs.tblTransactions, UserInfo.CurrentUserName);

            MainBs.DataSource = MainDs;
            MainBs.DataMember = "tblTransactions";

            TransactionsDataGridView.DataSource = MainBs;
            TransactionsBindingNavigator.BindingSource = MainBs;
            DataInterface.BindObjects(MainTableLayoutPanel, MainBs);

        }

        private void formTypesSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormType = new FormTypeForm();
            if (FormType.ShowDialog() == DialogResult.OK)
            {
                LoadCombos();
            }
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form UserManagement = new UsersForm();
            if (UserManagement.ShowDialog() == DialogResult.OK)
            {
                LoadCombos();
            }
        }

        private void LoadCurrentUser()
        {
            CurrentUser = new UserInfo();
            CurrentUser.GetUserInfo(UserInfo.CurrentUserName);
            AuditInfo.UserName = UserInfo.CurrentUserName;

            CurrentUser.GetUserInfo(UserInfo.CurrentUserName);
        }

        private void SetAccessibleAreas()
        {
            this.formTypesSetupToolStripMenuItem.Visible = CurrentUser.CanManageFormTypes;
            this.userManagementToolStripMenuItem.Visible = CurrentUser.CanManageUsers;

        }

        private void LoadCombos()
        {
            TransactionsBL.getFormTypes(formTypeCodeComboBox);
            TransactionsBL.getDataClerks(dataClerkComboBox);
            TransactionsBL.getDataClerks(SearchDataClerkComboBox);
            //this.SearchDataClerkComboBox.SelectedIndex = -1;
            this.SearchDataClerkComboBox.SelectedValue = UserInfo.CurrentUserName;
        }

        private void restartApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Restart the application?\n\nAll unsaved work will be lost", "Application Restart Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ChangePassword = new ChangePasswordForm();
            ChangePassword.ShowDialog();
        }

        private void formsVerifiedTextBox_TextChanged(object sender, EventArgs e)
        {
            computeBacklog();
        }

        private void computeBacklog()
        {
            if (this.formsBfwdTextBox.Text.Length > 0 && this.formsReceivedTextBox.Text.Length > 0 && this.formsVerifiedTextBox.Text.Length > 0)
            {
                //this.backLogTextBox.Text = (Convert.ToInt32(this.formsBfwdTextBox.Text) + Convert.ToInt32(this.formsReceivedTextBox.Text)- Convert.ToInt32(this.formsVerifiedTextBox.Text)).ToString();
            }
        }

        private void formsBfwdTextBox_TextChanged(object sender, EventArgs e)
        {
            computeBacklog();
        }

        private void TransactionsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            Console.WriteLine(e.Exception.ToString());
        }

        private void AllDataClerksRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.DataClerkCheckBox.Enabled = this.AllDataClerksRadioButton.Checked == true ? false : true;
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataAccess.BackUpDatabase();
        }

        private void formsBfwdTextBox_Enter(object sender, EventArgs e)
        {
            this.formsBfwdTextBox.SelectAll();
        }

        private void formsReceivedTextBox_Enter(object sender, EventArgs e)
        {
            this.formsReceivedTextBox.SelectAll();
        }

        private void formsSortedTextBox_Enter(object sender, EventArgs e)
        {
            this.formsSortedTextBox.SelectAll();
        }

        private void formsScannedTextBox_Enter(object sender, EventArgs e)
        {
            this.formsScannedTextBox.SelectAll();
        }

        private void formsVerifiedTextBox_Enter(object sender, EventArgs e)
        {
            this.formsVerifiedTextBox.SelectAll();
        }

        private void computeBacklogBfwd()
        {
            if (this.formTypeCodeComboBox.SelectedValue != null)
            {

                this.formsBfwdTextBox.Text = TransactionsBL.getBacklogBfwd(this.transactionDateDateTimePicker.Value, UserInfo.CurrentUserName, this.formTypeCodeComboBox.SelectedValue.ToString()).ToString();
            }
        }

        private void formTypeCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (XCurrentAction == (Int16)CurrentAction.Add)
            {
                computeBacklogBfwd();
            }
        }
    }
}
