using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using Data_Status_Calculator.BLL;

namespace Data_Status_Calculator.GUI
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }       

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to change your password?", "Password Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { 
                string OldPassword=UserInfo.CurrentUserPassword  ;
                string CurrentPassword=this.CurrentPasswordTextBox.Text ;
                string NewPassword=this.NewPasswordTextBox.Text ;
                string ConfirmedPassword=this.ConfirmPasswordTextBox.Text ;

                CurrentPassword = LoginBLL.EncryptPassword(CurrentPassword);
                NewPassword = LoginBLL.EncryptPassword(NewPassword);
                ConfirmedPassword = LoginBLL.EncryptPassword(ConfirmedPassword);

                if (string.IsNullOrEmpty (CurrentPassword ))
                {
                    MessageBox.Show("Enter the Current Password", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     this.CurrentPasswordTextBox.Focus(); 
                    return;
                }

                if (string.IsNullOrEmpty (NewPassword))
                {
                    MessageBox.Show("Enter the New Password", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.NewPasswordTextBox.Focus(); 
                    return;
                     
                }

                if (string.IsNullOrEmpty (ConfirmedPassword))
                {
                    MessageBox.Show("Confirm the New Password", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.ConfirmPasswordTextBox.Focus(); 
                    return;
                }

                if (CurrentPassword != OldPassword)
                {
                    MessageBox.Show("The current password is wrong. Please Check","Password Change",MessageBoxButtons.OK,MessageBoxIcon.Exclamation );
                    return;
                }

                if (CurrentPassword == NewPassword)
                {
                    MessageBox.Show("The old Password and the New Password MUST NOT be the same. Please Check", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (ConfirmedPassword != NewPassword)
                {
                    MessageBox.Show("The New Password and the Confirmed Password MUST be the same . Plesae Check", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

               
                if (ChangePassword(this.NewPasswordTextBox.Text  ))
                {
                    MessageBox.Show("Password Changed Successfully.", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UserInfo.CurrentUserPassword = NewPassword;
                    this.Close();
                }
            }
        }

        private static bool ChangePassword(string NewPassword)
        {
            UserInfo.UserName = UserInfo.CurrentUserName;  
            UserInfo.Password = NewPassword;
            if (UserInfo.SavePassword() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CurrentPasswordTextBox_Enter(object sender, EventArgs e)
        {
            this.CurrentPasswordTextBox.SelectAll();
        }

        private void NewPasswordTextBox_Enter(object sender, EventArgs e)
        {
            this.NewPasswordTextBox.SelectAll();
        }

        private void ConfirmPasswordTextBox_Enter(object sender, EventArgs e)
        {
            this.ConfirmPasswordTextBox.SelectAll();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}