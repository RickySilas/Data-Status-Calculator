using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Data_Status_Calculator.DAL;
using Data_Status_Calculator.DAL.DataObjects;
using Data_Status_Calculator.DAL.DataObjects.dbDSCMainDataSetTableAdapters;
using Data_Status_Calculator.BLL;

namespace Data_Status_Calculator.GUI
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        private static UserInfo myUSER;
        private static StringBuilder sb;
        private static bool Proceed = true;
        private static string err;
        private enum CurrentAction {Add = 1, Edit = 2, Delete = 3,Save=5, Cancel = 4 };
        private static Int16 XCurrentAction;

        dbDSCMainDataSet UserDs = new dbDSCMainDataSet();
        BindingSource UserBs = new BindingSource();
        tblLOGINTableAdapter UserDa = new tblLOGINTableAdapter();
        dbDSCMainDataSet.tblLOGINDataTable UserDt = new dbDSCMainDataSet.tblLOGINDataTable();
        

        private void UsersForm_Load(object sender, EventArgs e)
        {
            UserDa.Fill(UserDt);
            UserBs.DataSource = UserDt;
            
            UsersDataGridView.DataSource = UserBs;
            this.UsersDataGridView.Columns["Password"].Visible = false;
            this.UsersDataGridView.Columns["ID"].Visible = false;

            DataInterface.BindObjects(pnUsers, UserBs);
        }



        private void btnNew_Click(object sender, EventArgs e)
        {
            
            AddRecord();
            UserBs.AddNew();            
            //UserBs.ResetCurrentItem();  
            //UserBs.MoveFirst();
  
            ResetBindings(this);             
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

        private void btnSave_Click(object sender, EventArgs e)
        {            
            myUSER = new UserInfo(); 
            if (FormIsValid())
            {
                if (string.IsNullOrEmpty(this.iDTextBox.Text.Trim().ToString()))
                {
                    UserInfo.ID = 0; 
                }
                else
                {
                    UserInfo.ID = Convert.ToInt64(this.iDTextBox.Text.ToString());
                }
                UserInfo.Password = this.passwordTextBox.Text;
                UserInfo.UserName = this.userNameTextBox.Text;
                UserInfo.OfficialName = this.officialNameTextBox.Text;
                UserInfo.DateCreated = Convert.ToDateTime((DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                UserInfo.CreatedBy = LoginBLL.UserName;
                UserInfo.DateModified = Convert.ToDateTime((DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                UserInfo.ModifiedBy = LoginBLL.UserName;
                UserInfo.AllowAccess = this.allowAccessCheckBox.Checked;
                UserInfo.AddEntry  = this.addEntryCheckBox.Checked;
                UserInfo.DeleteEntry = this.deleteEntryCheckBox.Checked;
                UserInfo.EditEntry = this.editEntryCheckBox.Checked;
                UserInfo.ViewEntry = this.viewEntrycheckBox.Checked;
                UserInfo.ManageUsers = this.manageUsersCheckBox.Checked;
                UserInfo.ChangePassword = this.ChangePasswordCheckBox.Checked;
                UserInfo.ManageFormTypes = this.ManageFormTypesCheckBox.Checked;
                UserInfo.DaysBeforeTransactionsLock = Convert.ToInt16 (this.DaysBeforeTransactionsLockTextBox.Text.ToString ());  

                if (XCurrentAction ==(Int16) CurrentAction.Add)
                {
                    if (myUSER.NewUserInfo())
                    {
                        this.ResetControls();
                        UserDa.Fill(UserDt);
                    }                
                }
                else if (XCurrentAction ==(Int16)CurrentAction.Edit )
                {
                    if (myUSER.EditUserInfo())
                    {
                        this.ResetControls();
                        UserDa.Fill(UserDt);
                    }  
                }
                
            }
            else
            {
                MessageBox.Show(err,"Validation",MessageBoxButtons.OK ,MessageBoxIcon.Exclamation   ); 
            }
        }

        private bool FormIsValid()
        {
            Proceed = true;
            sb = new StringBuilder("Please resolve the following to proceed:\n");

            foreach (Control a in pnUsers.Controls)
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

            if (this.passwordTextBox.Text != this.ConfirmPasswordTextBox.Text)
            {
                ePX.SetError(ConfirmPasswordTextBox, "The Password And The Confirmed Password Do Not Match..");
                sb.Append("* " + ePX.GetError(ConfirmPasswordTextBox) + " \n");
                Proceed = false;
            }
            else
            {
                ePX.SetError(ConfirmPasswordTextBox,""); 
            }

            err = sb.ToString();

            return Proceed;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.iDTextBox.Text.Trim()))
            {
                MessageBox.Show("Select the record to edit","Validation",MessageBoxButtons.OK,MessageBoxIcon.Exclamation    );
                return;
            }
            EditRecord();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
            UserDa.Fill(UserDt);
        }

        private void ResetControls()
        {
            XCurrentAction = (Int16)CurrentAction.Cancel;
            this.pnUsers.Enabled = false;
            this.btnEdit.Enabled = true;
            this.btnNew.Enabled = true ;
            this.btnSave.Enabled = false;
            this.btnEdit.Enabled = true;
            this.UsersDataGridView.Enabled = true;
        }

        private void AddRecord()
        {
            XCurrentAction = (Int16)CurrentAction.Add;  
            this.pnUsers.Enabled = true;
            this.btnEdit.Enabled = false;
            this.btnNew.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnSave.Enabled = true;
            this.ChangePasswordCheckBox.Enabled = false;
            this.ChangePasswordCheckBox.Checked = false; 
            this.UsersDataGridView.Enabled = false; 
        }

        private void EditRecord()
        {
            XCurrentAction = (Int16)CurrentAction.Edit;
            this.pnUsers.Enabled = true ;
            this.btnEdit.Enabled = false;
            this.btnNew.Enabled = false ;
            this.btnSave.Enabled = true;
            this.passwordTextBox.Enabled = false;
            this.ConfirmPasswordTextBox.Enabled = false;
            this.ChangePasswordCheckBox.Enabled = true;
            this.ChangePasswordCheckBox.Checked = false; 

        }

        private void ChangePasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ChangePasswordCheckBox.Checked == true)
            {
                this.passwordTextBox.Text = string.Empty;
                this.ConfirmPasswordTextBox.Text = string.Empty;
                this.passwordTextBox.Enabled = true;
                this.ConfirmPasswordTextBox.Enabled = true;
            }
            else
            {
                UserBs.ResetCurrentItem(); 
            }
        }
    }
}