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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

        // =====================What Sysytem Product============================
        private void btnManPerson_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.CurrentUser.CheckPermissions(clsUser.enPermissions.pManagePeople))
            {
                MessageBox.Show("Access Dendied,Contact Your Admin", "Falied Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            ManagePerson managePerson = new ManagePerson(); 
            managePerson.ShowDialog();
        }

        private void btnManClient_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.CurrentUser.CheckPermissions(clsUser.enPermissions.pManageClient))
            {
                MessageBox.Show("Access Dendied,Contact Your Admin", "Falied Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            ManageClient manageClient = new ManageClient();
            manageClient.ShowDialog();
        }

        private void bbtnManUser_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.CurrentUser.CheckPermissions(clsUser.enPermissions.pManageUser))
            {
                MessageBox.Show("Access Dendied,Contact Your Admin", "Falied Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            ManageUser manageUser = new ManageUser();
            manageUser.ShowDialog();    
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.CurrentUser.CheckPermissions(clsUser.enPermissions.pManageTransaction))
            {
                MessageBox.Show("Access Dendied,Contact Your Admin", "Falied Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            ManageTransaction manageTransaction = new ManageTransaction();
            manageTransaction.ShowDialog();
        }

        private void btnShowLogin_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.CurrentUser.CheckPermissions(clsUser.enPermissions.pManageLoginRegister))
            {
                MessageBox.Show("Access Dendied,Contact Your Admin", "Falied Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            ShowUserLogin login = new ShowUserLogin();  
            login.ShowDialog();
        }

        


    }
}
