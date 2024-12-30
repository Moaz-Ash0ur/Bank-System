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
using TheArtOfDevHtmlRenderer.Adapters.Entities;

namespace BankManagementSystem
{
    public partial class ManageClient : Form
    {

        public ManageClient()
        {
            InitializeComponent();
        }

        //My Priv Var
        int _ClientID = -1;
        string _AccNum ="";
        DataTable dt;

        private void _ShowAllClients()
        {
            dt = clsClients.GetAllClients();
            guna2DataGridView1.DataSource = dt;

            lbCountClient.Text = guna2DataGridView1.Rows.Count.ToString() + " Client (s) Found";
        }

        private void _SortDataGridView()
        {
            string columnName = guna2DataGridView1.Columns[0].Name; 

            if (rdAsc.Checked)
            {
                guna2DataGridView1.Sort(guna2DataGridView1.Columns[columnName], System.ComponentModel.ListSortDirection.Ascending);
            }
            else if (rdDesc.Checked)
            {
                guna2DataGridView1.Sort(guna2DataGridView1.Columns[columnName], System.ComponentModel.ListSortDirection.Descending);
            } 
        }

        private void ManageClient_Load(object sender, EventArgs e)
        {
            _ShowAllClients();
        }

        private void SelectFilter_Click(object sender, EventArgs e)
        {
            if(groupFilter.Visible==false)
            {

                groupFilter.Visible = true;          
            }
            else
            {
                groupFilter.Visible = false;
            }

        }


        private void OnCheckedSort(object sender, EventArgs e)
        {
            _SortDataGridView();
        }

        private void rdName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdName.Checked)
            {
                txtFilterFind.PlaceholderText = "Enter Name";
            }
        }

        private void rdAccNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAccNo.Checked)
            {
                txtFilterFind.PlaceholderText = "Enter AccountNo";
            }
        }

        //CRUD Opertion

        private void _FilterRecord(string FilterType)
        {
            if (FilterType == "AccountNumber")
            {
                string filter = txtFilterFind.Text.ToLower();

                var filteredRows = dt.Select($"{FilterType} LIKE '%{filter}%'");

                if (filteredRows.Length > 0)
                {
                    guna2DataGridView1.DataSource = filteredRows.CopyToDataTable();
                }
                else
                {
                    guna2DataGridView1.DataSource = null;
                }
            }
            //===============================================
            else
            {
                string filter = txtFilterFind.Text.ToLower();
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

        private void txtFilterFind_TextChanged(object sender, EventArgs e)
        {

            if(groupFilter.Visible == false)
            {
                MessageBox.Show("Should select way to find","Warnning",MessageBoxButtons.OK);
                return;
            }

            if (string.IsNullOrEmpty(txtFilterFind.Text))
            {
                guna2DataGridView1.DataSource = dt;
                return;
            }

            if (txtFilterFind.PlaceholderText == "Enter Name")
            {
                _FilterRecord("FullName");

            }
            else if (txtFilterFind.PlaceholderText == "Enter AccountNo")

            {
                _FilterRecord("AccountNumber");
            }

           
        }


        //Crud opertion excute use another form
        private void addNewCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ClientID = -1;
            AddUpdateClients  AddUpdateClient = new AddUpdateClients(_ClientID);
            AddUpdateClient.ShowDialog();
            _ShowAllClients();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ClientID =clsClients.GetClientID((string)guna2DataGridView1.CurrentRow.Cells[0].Value);

            AddUpdateClients AddUpdateClient = new AddUpdateClients(_ClientID);
            AddUpdateClient.ShowDialog();
            _ShowAllClients();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this [" + guna2DataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {
                //Perform Delele and refresh
                if (clsClients.Delete(clsClients.GetClientID((string)guna2DataGridView1.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Client Deleted Successfully.");
                    _ShowAllClients();
                }
                else
                    MessageBox.Show("Client is not deleted.");
            }
        }

      
    }
}
