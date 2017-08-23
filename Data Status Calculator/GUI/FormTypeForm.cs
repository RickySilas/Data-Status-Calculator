using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Data_Status_Calculator.BLL;
using Data_Status_Calculator.DAL.DataObjects;
using Data_Status_Calculator.DAL.DataObjects.dbDSCMainDataSetTableAdapters;

namespace Data_Status_Calculator.GUI
{
    public partial class FormTypeForm : Form
    {
        public FormTypeForm()
        {
            InitializeComponent();
        }

        private static FormTypeBL myFormType;
        private static StringBuilder sb;
        private static bool Proceed = true;
        private static string err;
        private enum CurrentAction { Add = 1, Edit = 2, Delete = 3, Save = 5, Cancel = 4 };
        private static Int16 XCurrentAction;

        dbDSCMainDataSet FormTypeDs = new dbDSCMainDataSet();
        //dbDSCMainDataSet.tblFormTypeDataTable FormTypeDt = new dbDSCMainDataSet.tblFormTypeDataTable();
        BindingSource FormTypeBs = new BindingSource();
        tblFormTypeTableAdapter FormTypeDa = new tblFormTypeTableAdapter();

        private void tblFormTypeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.FormTypeBs.EndEdit();  
            //this.tblFormTypeBindingSource.EndEdit();
            this.FormTypeDa.Update(FormTypeDs);
            //this.tableAdapterManager.UpdateAll(this.dbDSCMainDataSet);
        }

        private void FormTypeForm_Load(object sender, EventArgs e)
        {
            ResetControls();

            //FormTypeDa.Fill(FormTypeDt);
            FormTypeDa.Fill(FormTypeDs.tblFormType);

            loadInitialData();

            DataInterface.BindObjects(MainTableLayoutPanel, FormTypeBs);
        }

        private void AddNewToolStripButton_Click(object sender, EventArgs e)
        {

            AddRecord();
            FormTypeBs.AddNew();
//            this.activeCheckBox.Checked = true;
            ResetBindings(this); 
        }

        private void EditToolStripButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.formTypeCodeTextBox.Text.Trim()))
            {
                MessageBox.Show("Select the record to edit", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            EditRecord();
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            myFormType = new FormTypeBL();
            if (FormIsValid())
            {

                FormTypeBL.FormTypeCode = this.formTypeCodeTextBox.Text;
                FormTypeBL.FormTypeDescription = this.formTypeDescriptionTextBox.Text;
                FormTypeBL.Active = this.activeCheckBox.Checked;
                FormTypeBL.CreatedBy = UserInfo.CurrentUserName;
                FormTypeBL.DateCreated = DateTime.Now ;

                //*************Alternative Code**********************To be tried out later
                //DataRowView drv = (DataRowView)FormTypeBs.Current ;
                //drv["DateCreated"] = DateTime.Now;
                //drv["CreatedBy"] = UserInfo.CurrentUserName;

                //this.Validate();
                //this.FormTypeBs.EndEdit();
                //this.FormTypeDa.Update(FormTypeDs);
                //return;
                //*************Alternative Code**********************

                if (XCurrentAction == (Int16)CurrentAction.Add)
                {
                    if (myFormType.NewFormType())
                    {
                        this.ResetControls();
                        FormTypeDa.Fill(FormTypeDs.tblFormType);
                    }
                }
                else if (XCurrentAction == (Int16)CurrentAction.Edit)
                {
                    if (myFormType.EditFormType())
                    {
                        this.ResetControls();
                        FormTypeDa.Fill(FormTypeDs.tblFormType );
                    }
                }
            }
            else
            {
                MessageBox.Show(err, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CancelToolStripButton_Click(object sender, EventArgs e)
        {
            ResetControls();
            FormTypeDa.Fill(FormTypeDs.tblFormType);
        }

        private void ResetControls()
        {
            XCurrentAction = (Int16)CurrentAction.Cancel;
            this.MainTableLayoutPanel.Enabled = false;
            this.EditToolStripButton.Enabled = true;
            this.AddNewToolStripButton.Enabled = true;
            this.SaveToolStripButton.Enabled = false;
            this.EditToolStripButton.Enabled = true;
            this.FormTypesDataGridView.Enabled = true;
        }

        private void AddRecord()
        {
            XCurrentAction = (Int16)CurrentAction.Add;
            this.MainTableLayoutPanel.Enabled = true;
            this.EditToolStripButton.Enabled = false;
            this.AddNewToolStripButton.Enabled = false;
            this.EditToolStripButton.Enabled = false;
            this.SaveToolStripButton.Enabled = true;
            this.FormTypesDataGridView.Enabled = false;
        }

        private void EditRecord()
        {
            XCurrentAction = (Int16)CurrentAction.Edit;
            this.MainTableLayoutPanel.Enabled = true;
            this.EditToolStripButton.Enabled = false;
            this.AddNewToolStripButton.Enabled = false;
            this.SaveToolStripButton.Enabled = true;
            this.FormTypesDataGridView.Enabled = false;

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


            err = sb.ToString();

            return Proceed;
        }

        private void loadInitialData()
        {
            FormTypeBs.DataSource = FormTypeDs.tblFormType;

            FormTypesDataGridView.DataSource = FormTypeBs;
            FormTypesBindingNavigator.BindingSource = FormTypeBs;


        }
    }
}
