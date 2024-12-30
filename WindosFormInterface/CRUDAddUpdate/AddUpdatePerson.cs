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
using System.Xml.Linq;

namespace BankManagementSystem
{
    public partial class AddUpdatePerson : Form
    {

       public enum enMode { AddNew = 0, Update = 1 };
       private enMode _Mode;


      public clsPersons _Person;
      public int _PersonID;


        public AddUpdatePerson(int id)
        {
            InitializeComponent();

            _PersonID = id;

            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;


        }

        private void AddUpdatePerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        //============================================================
        private void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                _Person = new clsPersons();
                return;
            }

            _Person = clsPersons.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID);
                this.Close();

                return;
            }

            lbAddUpdate.Text = "Update Person";
            lbPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;

        }

        private bool IsValid()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                errorProvider1.SetError(txtFirstName, "Name cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtFirstName, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                errorProvider1.SetError(txtLastName, "Name cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtLastName, string.Empty);
            }


            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Email cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtEmail, string.Empty);
            }


            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                errorProvider1.SetError(txtPhone, "Phone cannot be empty.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtPhone, string.Empty);
            }




            return isValid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (IsValid())
            {

                _Person.FirstName = txtFirstName.Text;
                _Person.LastName = txtLastName.Text;
                _Person.Email = txtEmail.Text;
                _Person.Phone = txtPhone.Text;


                if (_Person.Save())
                {
                    _PersonID = _Person.PersonID;
                    lbPersonID.Text = _Person.PersonID.ToString();

                    if (MessageBox.Show("Data Saved Successfully.", "Information", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK);

                _Mode = enMode.Update;

            }

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }




    }
}
