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
    public partial class ManageUser : Form
    {
        public ManageUser()
        {
            InitializeComponent();
        }

      //private var 
      private int _UserID = -1;
      private DataTable dt;

        //Logical Part function
        private void _ShowAllUsers()
        {
            dt = clsUser.GetAllUsers();
            guna2DataGridView1.DataSource = dt;

            lbCountClient.Text = guna2DataGridView1.Rows.Count.ToString() + " User (s) Found";
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

        private void ManageUser_Load(object sender, EventArgs e)
        {
            _ShowAllUsers();
        }

        private void OnCheckedSort(object sender, EventArgs e)
        {
            _SortDataGridView();
        }

        private void OnCheckFilterFind(object sender, EventArgs e)
        {
            if (rdUserId.Checked)
            {
              txtFilterFind.PlaceholderText = "Enter UserID";

            }else if (rdUsername.Checked)
            {             
              txtFilterFind.PlaceholderText = "Enter Username";
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
                txtFilterFind.PlaceholderText = "";
                groupFilter.Visible = false;
            }
        }


        private void _FilterRecord(string FilterType)
        {
            if (FilterType == "UserID")
            {
                if (int.TryParse(txtFilterFind.Text, out int id))
                {
                    var filteredRows1 = dt.Select($"UserID = {id}");

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

            if (groupFilter.Visible == false)
            {
                MessageBox.Show("Should select way to find", "Warnning", MessageBoxButtons.OK);
                return;
            }


            if (txtFilterFind.PlaceholderText == "Enter Username")
            {
                _FilterRecord("UserName");

            }

            if (txtFilterFind.PlaceholderText == "Enter UserID")
            {
                _FilterRecord("UserID");

            }



        }

        //CRUD Opertion


        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateUsers user = new AddUpdateUsers(-1);
            user.ShowDialog();
            _ShowAllUsers();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _UserID = ((int)guna2DataGridView1.CurrentRow.Cells[0].Value);

            AddUpdateUsers User = new AddUpdateUsers(_UserID);
            User.ShowDialog();
            _ShowAllUsers(); 
        }

        private void _DeleteUser()
        {

            string username = (string)guna2DataGridView1.CurrentRow.Cells[2].Value;

            if (username.ToLower() == "admin")
            {
                MessageBox.Show("Admin Can`t be Deleted !", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }


            if (MessageBox.Show("Are you sure you want to delete this [" + guna2DataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {
                if (clsUser.Delete((int)guna2DataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.");
                    _ShowAllUsers();
                }
                else
                    MessageBox.Show("User is not deleted.");
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DeleteUser();
        }

      




    }
}
