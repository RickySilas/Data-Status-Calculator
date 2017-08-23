using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Data_Status_Calculator.BLL  ;

namespace Data_Status_Calculator
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty ( this.txtUserName.ToString().Trim()))
            {
                this.txtUserName.Focus(); 
                MessageBox.Show ( "Enter User Name","Failed Login",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                
            }
            if (string.IsNullOrEmpty(this.txtPassword.ToString().Trim()))
            {
                this.txtPassword.Focus();
                MessageBox.Show("Enter Password", "Failed Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            UserInfo.UserName = this.txtUserName.Text;
            UserInfo.Password = this.txtPassword.Text;
            if (UserInfo.LoginUser())
            {
                Form MainFrm = new DSCMain();
                MainFrm.WindowState=FormWindowState.Normal;
                MainFrm.Show();
                this.Hide();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit(); 
        }


        private void LoginForm_Activated(object sender, EventArgs e)
        {
            this.txtUserName.Text = Environment.UserName;  
            this.txtUserName.Focus();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            this.txtPassword.SelectAll();
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            this.txtUserName.SelectAll(); 
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUserName.ToString().Trim()))
            {
                this.txtUserName.Focus();
                MessageBox.Show("Enter User Name", "Failed Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            if (string.IsNullOrEmpty(this.txtPassword.ToString().Trim()))
            {
                this.txtPassword.Focus();
                MessageBox.Show("Enter Password", "Failed Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            UserInfo.UserName = this.txtUserName.Text;
            UserInfo.Password = this.txtPassword.Text;
            if (UserInfo.LoginUser())
            {
                Form MainFrm = new DSCMain();
                MainFrm.WindowState = FormWindowState.Normal;
                MainFrm.Show();
                this.Hide();
            }
        }

    }
}