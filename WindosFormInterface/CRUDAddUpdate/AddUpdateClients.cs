using BankBussniesLayer1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;

namespace BankManagementSystem
{
    public partial class AddUpdateClients : Form
    {



        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        public clsPersons _Person;
        public int _PersonID;

        public int _ClientID;
        public clsClients _Client;

        public AddUpdateClients(int id)
        {
            InitializeComponent();

            _ClientID = id;

            if (_ClientID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;


        }

        private void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                lbAddUpdate.Text = "Add New Client";
                _Client = new clsClients();
                return;
            }

            _Client = clsClients.Find(_ClientID);

            if (_Client == null)
            {
                MessageBox.Show("This form will be closed because No Client with ID = " + _PersonID);
                this.Close();

                return;
            }
            lbAddUpdate.Text = "Update Client";
            lbClientID.Text = _ClientID.ToString();
            lbPersonClientID2.Text = _Client.PersonId.ToString();
            txtAccoutnNum.Text = _Client.AccountNumber;
            txtBalance.Text = _Client.Balance.ToString();
            txtPinCode.Text = _Client.PinCode;
            txtFindPerson.Text = _Client.PersonId.ToString();   
        }

        private void AddUpdateClients_Load(object sender, EventArgs e)
        {
            _LoadData();

            if (_Mode == enMode.Update)
            {                
                _ShowPersinInfo();

                if (txtFindPerson.Enabled) {

                    txtFindPerson.Enabled = false;
                }
            }
        }

        //Person Info Assign
        private void btnNextInfo_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage2;
        }

        private void _ShowPersinInfo()
        {

            if ((txtFindPerson.Text)=="")
            {
                MessageBox.Show("Should Enter Person ID","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            clsPersons person = clsPersons.Find(Convert.ToInt32(txtFindPerson.Text));

            if (person != null)
            {
                _PersonID = person.PersonID;

                lbPersonIDClient.Text = person.PersonID.ToString();
                lbName.Text = person.FirstName + " " + person.LastName;
                lbEmail.Text = person.Email;
                lbPhone.Text = person.Phone;
                lbPersonClientID2.Text = person.PersonID.ToString();    
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
                MessageBox.Show("Person ID Assigned For Another Person ","Warnning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtFindPerson.Text = "";
                return;
            }

            _ShowPersinInfo();
        }

        //===========================================My Own Function=============================
        private bool IsValid()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtPinCode.Text))
            {
                errorProvider1.SetError(txtPinCode, "PIN Code cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtPinCode, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(txtAccoutnNum.Text))
            {
                errorProvider1.SetError(txtAccoutnNum, "Acc cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtAccoutnNum, string.Empty);
            }


            if (string.IsNullOrWhiteSpace(txtBalance.Text))
            {
                errorProvider1.SetError(txtBalance, "Balance cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtBalance, string.Empty);
            }

            return isValid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         
             if (lbPersonIDClient.Text == "??")
            {
                MessageBox.Show("You Should Enter All Field !");
                return;
            }

            if (!IsValid()) return;

            _Client.PersonId = Convert.ToInt32(lbPersonIDClient.Text);
            _Client.AccountNumber = txtAccoutnNum.Text;
            _Client.Balance = Convert.ToDecimal(txtBalance.Text);
            _Client.PinCode = txtPinCode.Text;

            if (_Client.Save())
            {
                _ClientID = _Client.ClientID;
                lbClientID.Text = _ClientID.ToString();

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
            if (lbPersonIDClient.Text == "")
            {
                MessageBox.Show("Should Enter Person ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                AddUpdatePerson person = new AddUpdatePerson(Convert.ToInt32(lbPersonIDClient.Text));
                person.ShowDialog();
                _ShowPersinInfo();
            }
            catch (Exception)
            {
                MessageBox.Show("You Should First Select Person", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
           

        }


        //=====================================
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                if (clsClients.IsExists(txtAccoutnNum.Text))
                {

                    errorProvider2.SetError(txtAccoutnNum, "This account number already exists.");
                }
                else
                {
                    errorProvider2.SetError(txtAccoutnNum, "");
                }
            }


           


        }




    }
}
