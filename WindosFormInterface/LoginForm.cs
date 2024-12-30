using BankBussniesLayer1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        //=============start====================
        private bool _CheckInfoLogin()
        {

            if (txtusername.Text == null)
            {
                MessageBox.Show("Should Enter Your Info", "Error", MessageBoxButtons.OK);
                return false;
            }

            clsGlobal.CurrentUser = clsUser.FindUserLogin(txtusername.Text, txtPassword.Text);


            if (clsGlobal.CurrentUser != null)
            {

                return (clsGlobal.CurrentUser.UserName == txtusername.Text &&
                     (clsGlobal.CurrentUser.Password == txtPassword.Text));

            }


            return false;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            DateTime today = DateTime.Now.Date;
            lblDate.Text = today.ToString("yyyy-MM-dd");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            lblTime.Text = currentTime.ToString("hh:mm:ss tt");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (_CheckInfoLogin())
            {
                clsUser.AddUserLoginLog(clsGlobal.CurrentUser.UserID,DateTime.Now);

                if (lbErrorLogin.Visible == true)
                {
                    lbErrorLogin.Visible = false;
                }

                txtusername.Text = "";
                txtPassword.Text = "";
                checkBox1.Checked = false;
                Form1 home = new Form1();
                home.ShowDialog();

            }
            else
            {
                lbErrorLogin.Visible = true;
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.PasswordChar = '\0'; 
            }
            else
            {
                txtPassword.PasswordChar = '●';
            }
        }

        //=============End=======================

    }
}
