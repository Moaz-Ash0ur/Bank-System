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
    public partial class ShowUserLogin : Form
    {
        public ShowUserLogin()
        {
            InitializeComponent();
        }
        DataTable dt;
        private void ShowUserLogin_Load(object sender, EventArgs e)
        {
             dt = clsUser.GetAllUsersLogin();
            guna2DataGridView1.DataSource = dt;

            lbCountClient.Text = dt.Rows.Count.ToString() + " Record for Login Found!";
        }

        private void OnCheckedFliterBy(object sender, EventArgs e)
        {
            if (rdUsername.Checked)
            {
                txtFilterFind.PlaceholderText = "Enter Username";

            }else if (rdPermission.Checked)
            {
                txtFilterFind.PlaceholderText = "Enter Permission";
            }
        }

        private void SelectFilter_Click(object sender, EventArgs e)
        {
            if (groupFilter.Visible == false)
            {

                groupFilter.Visible = true;
            }
            else
            {
                groupFilter.Visible = false;
            }
        }

        private void _SortDataShowInDgv()
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

        private void OnCheckedSort(object sender, EventArgs e)
        {
            _SortDataShowInDgv();
        }

        private void _FilterRecord(string filterType)
        {
            if (filterType == "Permissions")
            {
                if (int.TryParse(txtFilterFind.Text, out int perm))
                {
                    var filteredRows1 = dt.Select($"Permissions = {perm}");

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
            //===============================================
            else
            {
                string filter = txtFilterFind.Text.ToLower();
                var filteredRows = dt.Select($"{filterType} like '%{filter}%'");
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
            if (groupFilter.Visible == false)
            {
                MessageBox.Show("Should select way to find", "Warnning", MessageBoxButtons.OK);
                return;
            }

            if (txtFilterFind.PlaceholderText == "Enter Username")
            {
                _FilterRecord("UserName");

            }
            else if (txtFilterFind.PlaceholderText == "Enter Permission")

            {
                _FilterRecord("Permissions");
            }
        }



    }
}
