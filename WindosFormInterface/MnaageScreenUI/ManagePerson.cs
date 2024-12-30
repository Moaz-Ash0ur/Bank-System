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
    public partial class ManagePerson : Form
    {
        private void _OpenFormEasy()
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.2;
            }
            else
            {
                timer1.Stop();
            }
        }
        //===============================
        public ManagePerson()
        {
            InitializeComponent();
            this.Opacity = 0.0;
        }

        clsPersons _Person;
        int _PersonModeID = -1;


        DataTable dt;
        private void _ShowAllPersons()
        {
            dt = clsPersons.GetAllPersons();
            guna2DataGridView1.DataSource = dt;
        }

        private void ManagePerson_Load(object sender, EventArgs e)
        {
          timer1.Start();
          _ShowAllPersons();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _OpenFormEasy();
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxFilter.SelectedItem.ToString() == "FirstName")
            {
                textboxFilter.Visible = true; 
            }
            else if (comboBoxFilter.SelectedItem.ToString() == "ID")
            {
                textboxFilter.Visible = true; 

            }else if (comboBoxFilter.SelectedItem.ToString() == "Email")
            {
                textboxFilter.Visible = true;
            }
            else
            {
                textboxFilter.Visible = false; 
            }


        }

        private void _FilterRecord(string FilterType)
        {

            if (FilterType == "ID")
            {
                if (int.TryParse(textboxFilter.Text, out int id))
                {
                    var filteredRows1 = dt.Select($"ID = {id}");

                    if (filteredRows1.Length > 0)
                    {
                        guna2DataGridView1.DataSource = filteredRows1.CopyToDataTable();
                    }
                    else
                    {
                        guna2DataGridView1.DataSource = null;
                    }
                }
            }
            //for another filter type
            else
            {
                string filter = textboxFilter.Text.ToLower();
                var filteredRows = dt.Select($"{FilterType} like '%{filter}%'");
                if (filteredRows.Length > 0)
                {
                    guna2DataGridView1.DataSource = filteredRows.CopyToDataTable();
                }
                else
                {
                    guna2DataGridView1.DataSource = null; 
                }

            }


            



        }

        private void textboxFilter_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textboxFilter.Text))
            {
                guna2DataGridView1.DataSource = dt; 
                return;
            }

            if (comboBoxFilter.SelectedItem.ToString() == "FirstName")
            {
                _FilterRecord("FirstName");

            }
            else if (comboBoxFilter.SelectedItem.ToString() == "ID")
             
            {
                _FilterRecord("ID");

            }

            else if (comboBoxFilter.SelectedItem.ToString() == "Email")

            {
                _FilterRecord("Email");

            }

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            _PersonModeID = -1;
            AddUpdatePerson addUpdatePerson = new AddUpdatePerson(_PersonModeID);
            addUpdatePerson.ShowDialog();
            _ShowAllPersons();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            _PersonModeID = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
            AddUpdatePerson addUpdatePerson = new AddUpdatePerson(_PersonModeID);
            addUpdatePerson.ShowDialog();
            _ShowAllPersons();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this [" + guna2DataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {
                //Perform Delele and refresh
                if (clsPersons.Delete((int)guna2DataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.");
                    _ShowAllPersons();
                }
                else
                    MessageBox.Show("Person is not deleted.");
            }

        }



    }
}
