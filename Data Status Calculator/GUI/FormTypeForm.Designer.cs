namespace Data_Status_Calculator.GUI
{
    partial class FormTypeForm
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
            System.Windows.Forms.Label formTypeCodeLabel;
            System.Windows.Forms.Label formTypeDescriptionLabel;
            System.Windows.Forms.Label activeLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypeForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FormTypesBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.AddNewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.EditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.activeCheckBox = new System.Windows.Forms.CheckBox();
            this.formTypeCodeTextBox = new System.Windows.Forms.TextBox();
            this.formTypeDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.FormTypesDataGridView = new System.Windows.Forms.DataGridView();
            this.ePX = new System.Windows.Forms.ErrorProvider(this.components);
            formTypeCodeLabel = new System.Windows.Forms.Label();
            formTypeDescriptionLabel = new System.Windows.Forms.Label();
            activeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormTypesBindingNavigator)).BeginInit();
            this.FormTypesBindingNavigator.SuspendLayout();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormTypesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePX)).BeginInit();
            this.SuspendLayout();
            // 
            // formTypeCodeLabel
            // 
            formTypeCodeLabel.AutoSize = true;
            formTypeCodeLabel.Location = new System.Drawing.Point(3, 20);
            formTypeCodeLabel.Name = "formTypeCodeLabel";
            formTypeCodeLabel.Size = new System.Drawing.Size(88, 13);
            formTypeCodeLabel.TabIndex = 0;
            formTypeCodeLabel.Text = "Form Type Code:";
            // 
            // formTypeDescriptionLabel
            // 
            formTypeDescriptionLabel.AutoSize = true;
            formTypeDescriptionLabel.Location = new System.Drawing.Point(3, 60);
            formTypeDescriptionLabel.Name = "formTypeDescriptionLabel";
            formTypeDescriptionLabel.Size = new System.Drawing.Size(116, 13);
            formTypeDescriptionLabel.TabIndex = 2;
            formTypeDescriptionLabel.Text = "Form Type Description:";
            // 
            // activeLabel
            // 
            activeLabel.AutoSize = true;
            activeLabel.Location = new System.Drawing.Point(3, 100);
            activeLabel.Name = "activeLabel";
            activeLabel.Size = new System.Drawing.Size(40, 13);
            activeLabel.TabIndex = 4;
            activeLabel.Text = "Active:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FormTypesBindingNavigator);
            this.splitContainer1.Panel1.Controls.Add(this.MainTableLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FormTypesDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(833, 262);
            this.splitContainer1.SplitterDistance = 324;
            this.splitContainer1.TabIndex = 0;
            // 
            // FormTypesBindingNavigator
            // 
            this.FormTypesBindingNavigator.AddNewItem = null;
            this.FormTypesBindingNavigator.CountItem = null;
            this.FormTypesBindingNavigator.DeleteItem = null;
            this.FormTypesBindingNavigator.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.FormTypesBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorSeparator,
            this.AddNewToolStripButton,
            this.toolStripSeparator3,
            this.SaveToolStripButton,
            this.toolStripSeparator1,
            this.EditToolStripButton,
            this.toolStripSeparator2,
            this.CancelToolStripButton,
            this.toolStripSeparator4});
            this.FormTypesBindingNavigator.Location = new System.Drawing.Point(0, 157);
            this.FormTypesBindingNavigator.MoveFirstItem = null;
            this.FormTypesBindingNavigator.MoveLastItem = null;
            this.FormTypesBindingNavigator.MoveNextItem = null;
            this.FormTypesBindingNavigator.MovePreviousItem = null;
            this.FormTypesBindingNavigator.Name = "FormTypesBindingNavigator";
            this.FormTypesBindingNavigator.PositionItem = null;
            this.FormTypesBindingNavigator.Size = new System.Drawing.Size(324, 25);
            this.FormTypesBindingNavigator.TabIndex = 2;
            this.FormTypesBindingNavigator.Text = "TransactionsBindingNavigator";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // AddNewToolStripButton
            // 
            this.AddNewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddNewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("AddNewToolStripButton.Image")));
            this.AddNewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddNewToolStripButton.Name = "AddNewToolStripButton";
            this.AddNewToolStripButton.Size = new System.Drawing.Size(60, 22);
            this.AddNewToolStripButton.Text = "Add New";
            this.AddNewToolStripButton.Click += new System.EventHandler(this.AddNewToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveToolStripButton.Image")));
            this.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.SaveToolStripButton.Text = "Save";
            this.SaveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // EditToolStripButton
            // 
            this.EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EditToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("EditToolStripButton.Image")));
            this.EditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditToolStripButton.Name = "EditToolStripButton";
            this.EditToolStripButton.Size = new System.Drawing.Size(31, 22);
            this.EditToolStripButton.Text = "Edit";
            this.EditToolStripButton.Click += new System.EventHandler(this.EditToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // CancelToolStripButton
            // 
            this.CancelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CancelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CancelToolStripButton.Image")));
            this.CancelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelToolStripButton.Name = "CancelToolStripButton";
            this.CancelToolStripButton.Size = new System.Drawing.Size(47, 22);
            this.CancelToolStripButton.Text = "Cancel";
            this.CancelToolStripButton.Click += new System.EventHandler(this.CancelToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.AutoScroll = true;
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(activeLabel, 0, 5);
            this.MainTableLayoutPanel.Controls.Add(this.activeCheckBox, 1, 5);
            this.MainTableLayoutPanel.Controls.Add(formTypeCodeLabel, 0, 1);
            this.MainTableLayoutPanel.Controls.Add(this.formTypeCodeTextBox, 1, 1);
            this.MainTableLayoutPanel.Controls.Add(formTypeDescriptionLabel, 0, 3);
            this.MainTableLayoutPanel.Controls.Add(this.formTypeDescriptionTextBox, 1, 3);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 7;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(324, 157);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // activeCheckBox
            // 
            this.activeCheckBox.Location = new System.Drawing.Point(128, 103);
            this.activeCheckBox.Name = "activeCheckBox";
            this.activeCheckBox.Size = new System.Drawing.Size(104, 14);
            this.activeCheckBox.TabIndex = 5;
            this.activeCheckBox.Tag = "Active";
            this.activeCheckBox.UseVisualStyleBackColor = true;
            // 
            // formTypeCodeTextBox
            // 
            this.formTypeCodeTextBox.AccessibleDescription = "Enter Form Code";
            this.formTypeCodeTextBox.Location = new System.Drawing.Point(128, 23);
            this.formTypeCodeTextBox.Name = "formTypeCodeTextBox";
            this.formTypeCodeTextBox.Size = new System.Drawing.Size(97, 20);
            this.formTypeCodeTextBox.TabIndex = 1;
            this.formTypeCodeTextBox.Tag = "FormTypeCode";
            // 
            // formTypeDescriptionTextBox
            // 
            this.formTypeDescriptionTextBox.AccessibleDescription = "Enter Form Description";
            this.formTypeDescriptionTextBox.Location = new System.Drawing.Point(128, 63);
            this.formTypeDescriptionTextBox.Name = "formTypeDescriptionTextBox";
            this.formTypeDescriptionTextBox.Size = new System.Drawing.Size(193, 20);
            this.formTypeDescriptionTextBox.TabIndex = 3;
            this.formTypeDescriptionTextBox.Tag = "FormTypeDescription";
            // 
            // FormTypesDataGridView
            // 
            this.FormTypesDataGridView.AllowUserToAddRows = false;
            this.FormTypesDataGridView.AllowUserToDeleteRows = false;
            this.FormTypesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FormTypesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormTypesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.FormTypesDataGridView.MultiSelect = false;
            this.FormTypesDataGridView.Name = "FormTypesDataGridView";
            this.FormTypesDataGridView.ReadOnly = true;
            this.FormTypesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FormTypesDataGridView.Size = new System.Drawing.Size(505, 262);
            this.FormTypesDataGridView.TabIndex = 0;
            // 
            // ePX
            // 
            this.ePX.ContainerControl = this;
            // 
            // FormTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 262);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form Types";
            this.Load += new System.EventHandler(this.FormTypeForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FormTypesBindingNavigator)).EndInit();
            this.FormTypesBindingNavigator.ResumeLayout(false);
            this.FormTypesBindingNavigator.PerformLayout();
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormTypesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.DataGridView FormTypesDataGridView;
        private System.Windows.Forms.TextBox formTypeCodeTextBox;
        private System.Windows.Forms.TextBox formTypeDescriptionTextBox;
        private System.Windows.Forms.CheckBox activeCheckBox;
        private System.Windows.Forms.BindingNavigator FormTypesBindingNavigator;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripButton AddNewToolStripButton;
        private System.Windows.Forms.ToolStripButton SaveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton EditToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton CancelToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ErrorProvider ePX;
    }
}