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
    public partial class AddUpdateUsers : Form
    {
        private enum enPermissions
        {
            eAll = -1, pManagePeople = 1, pManageClient = 2, pManageUser = 4,
            pManageTransaction = 8, pManageLoginRegister = 16
        };

        private void _SetPermission()
        {
            _Permission = 0;

            if (chkManageClient.Checked)
            {
                _Permission |= (int)enPermissions.pManageClient;
            }
            if (chkManageUser.Checked)
            {
                _Permission |= (int)enPermissions.pManageUser;
            }
            if (chkManageTransaction.Checked)
            {
                _Permission |= (int)enPermissions.pManageTransaction;
            }
            if (chkLoginReg.Checked)
            {
                _Permission |= (int)enPermissions.pManageLoginRegister;
            }
            if (chkManagePeople.Checked)
            {
                _Permission |= (int)enPermissions.pManagePeople;
            }
            if (chkAll.Checked)
            {
                _Permission = (int)enPermissions.eAll;
            }
       
        }

        private void _AssignPermissionWhenUpdate(int perm)
        {
            if ((perm & (int)enPermissions.eAll) == (int)enPermissions.eAll)
            {
                chkAll.Checked = true;
                return;
            }

            chkManageClient.Checked = (perm & (int)enPermissions.pManageClient) != 0;
            chkManageUser.Checked = (perm & (int)enPermissions.pManageUser) != 0;
            chkManageTransaction.Checked = (perm & (int)enPermissions.pManageTransaction) != 0;
            chkLoginReg.Checked = (perm & (int)enPermissions.pManageLoginRegister) != 0;
            chkManagePeople.Checked = (perm & (int)enPermissions.pManagePeople) != 0;

        }

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        public clsPersons _Person;
        public int _PersonID;

        public int _UserID;
        public clsUser _User;
        int _Permission = 0;

        public AddUpdateUsers(int id)
        {
            InitializeComponent();

            _UserID = id;

            if (_UserID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;


        }


        //=======================start=================================

        private void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                lbAddUpdate.Text = "Add New User";
                _User = new clsUser();
                return;
            }

            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show("This form will be closed because No _User with ID = " + _PersonID);
                this.Close();

                return;
            }
            lbAddUpdate.Text = "Update User";

            lbUserID.Text = _UserID.ToString();
            lbPersonUserID2.Text = _User.PersonId.ToString();
            txtUsername.Text = _User.UserName;
            txtPassword.Text = _User.Password.ToString();
            _Permission = _User.Permissions;
            _AssignPermissionWhenUpdate(_Permission);
            txtFindPerson.Text = _User.PersonId.ToString();
        }


        private void AddUpdateUsers_Load(object sender, EventArgs e)
        {
            _LoadData();
            _AssignPermissionWhenUpdate(_Permission);

            if (_Mode == enMode.Update)
            {
                _ShowPersinInfo();

                if (txtFindPerson.Enabled)
                {

                    txtFindPerson.Enabled = false;
                }
            }
        }

        private void btnNextInfo_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage2;
        }

        private void _ShowPersinInfo()
        {

            if ((txtFindPerson.Text) == "")
            {
                MessageBox.Show("Should Enter Person ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsPersons person = clsPersons.Find(Convert.ToInt32(txtFindPerson.Text));

            if (person != null)
            {
                _PersonID = person.PersonID;

                lbPersonIDUser.Text = person.PersonID.ToString();
                lbName.Text = person.FirstName + " " + person.LastName;
                lbEmail.Text = person.Email;
                lbPhone.Text = person.Phone;
                lbPersonUserID2.Text = person.PersonID.ToString();
            }
            else
            {
                MessageBox.Show("Person Not Exist To Assign !", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnSelectPerson_Click(object sender, EventArgs e)
        {
            if ((txtFindPerson.Text) == "")
            {
                MessageBox.Show("Should Enter Person ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsPersons.IsPersonIdExist(Convert.ToInt32(txtFindPerson.Text)))
            {
                MessageBox.Show("Person ID Assigned For Another Person ", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFindPerson.Text = "";
                return;
            }

            _ShowPersinInfo();
        }

        private bool IsValid()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Username Code cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtUsername, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Password cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtPassword, string.Empty);
            }
          
            return isValid;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lbPersonIDUser.Text == "??")
            {
                MessageBox.Show("You Should Enter All Field !");
                return;
            }

            if (!IsValid()) return;

            _User.PersonId = Convert.ToInt32(lbPersonIDUser.Text);
            _User.UserName = txtUsername.Text;
            _User.Password = txtPassword.Text ;
            _SetPermission();
            _User.Permissions = _Permission;

            if (_User.Save())
            {
                _UserID = _User.UserID;
                lbUserID.Text = _UserID.ToString();

                if (MessageBox.Show("Data Saved Successfully.", "Information", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
                MessageBox.Show("Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK);

            _Mode = enMode.Update;
        }

        private void UpdatePersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lbPersonIDUser.Text == "??")
            {
                MessageBox.Show("Should Enter Person ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                AddUpdatePerson person = new AddUpdatePerson(Convert.ToInt32(lbPersonIDUser.Text));
                person.ShowDialog();
                _ShowPersinInfo();
            }
            catch (Exception)
            {
                MessageBox.Show("You Should First Select Person", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUsername.Text))
                {

                    errorProvider1.SetError(txtUsername, "This Username  already exists.");
                }
                else
                {
                    errorProvider1.SetError(txtUsername, "");
                }
            }

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //loop for each chewckbox in group box
            foreach (Control control in guna2GroupBox1.Controls)
            {
                if (control is CheckBox checkBox && checkBox != chkAll)
                {
                    checkBox.Checked = chkAll.Checked; //check all checkbox if the user selectAll
                }
            }
        }


        //==================End===========================
    }
}
