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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BankManagementSystem
{
    public partial class ManageTransaction : Form
    {
        public ManageTransaction()
        {
            InitializeComponent();
        }
        private void _ShowAllClient()
        {
           DataTable dt = clsClients.GetAllClients();
            guna2DataGridView1.DataSource = dt;

            lbCountClient.Text = guna2DataGridView1.Rows.Count.ToString() + " Client (s) Found";
            lbTotalBalance.Text = clsTransactions.GetTotalBalance().ToString() + " $";

        }

        private void _FillAccoutNumbersList()
        {
            List<string> accountNumbers = clsTransactions.GetAccountNumbers();
            List<string> accountNumbers2 = clsTransactions.GetAccountNumbersP();

            //for balance > 0 or == 0
            ComboBoxAccNo1.DataSource = accountNumbers;
            ComboBoxAccNoWith.DataSource = accountNumbers2;

            CbAccountFrom.DataSource = accountNumbers2;
            CbAccountTo.DataSource = accountNumbers;

        }

        private void ManageTransaction_Load(object sender, EventArgs e)
        {
            timer1.Start();
            _ShowAllClient();
            _FillAccoutNumbersList();
            _ShowAllTransferLog();
            DateTime today = DateTime.Now.Date;
            lblDate.Text = today.ToString("yyyy-MM-dd");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            lblTime.Text = currentTime.ToString("hh:mm:ss tt");
        }

        //Deposit Opertion
        private void ComboBoxAccNo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string AccNo = ComboBoxAccNo1.SelectedItem.ToString();

            if (AccNo != "")
            {
                decimal balance = clsTransactions.GetBalanceByAccountNumber(AccNo);

                lbBalance.Text = balance.ToString()+" $";
            }
            else
            {
                lbBalance.Text = string.Empty;
            }

  
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            _DepositWithdraw();
        }

        //Withdrow Opertion
        private void ComboBoxAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string AccNo = ComboBoxAccNoWith.SelectedItem.ToString();

            if (AccNo != "")
            {
                decimal balance = clsTransactions.GetBalanceByAccountNumber(AccNo);

                lbBalanceWithdrow.Text = balance.ToString() + " $";
            }
            else
            {
                lbBalanceWithdrow.Text = string.Empty;
            }
        }

        private bool _SetSettingTransaction(ref string TransactionType,ref int amount,ref string SelectAccount)
        {
           
            if (tabControl1.SelectedIndex == 0)
            {
                TransactionType = "Deposit";
                SelectAccount = ComboBoxAccNo1.SelectedItem?.ToString();

                if ((int)guna2NumericUpDown1.Value != 0)
                {
                    amount = (int)guna2NumericUpDown1.Value;
                }
                else
                {
                    MessageBox.Show("Should Enter Amount", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            else if (tabControl1.SelectedIndex == 1)
            {
                TransactionType = "Withdraw";
                SelectAccount = ComboBoxAccNoWith.SelectedItem?.ToString();

                if ((int)guna2NumericUpDown2.Value == 0)
                {                
                  MessageBox.Show("Should Enter Amount", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   return false;
                }

               amount = (int)guna2NumericUpDown2.Value;

               string balanceText = lbBalanceWithdrow.Text.Replace("$", "").Trim();

               if (amount > Convert.ToDecimal(balanceText))
               {
                   MessageBox.Show("The withdrawal amount exceeds the available balance.", "Insufficient Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   return false;
               }
               amount *= -1;
                
                

            }
            return true;
        }

        private void _OnUpdateChanges()
        {
            lbBalance.Text = clsTransactions.GetBalanceByAccountNumber(ComboBoxAccNo1.SelectedItem.ToString()).ToString() + " $";
            lbBalanceWithdrow.Text = clsTransactions.GetBalanceByAccountNumber(ComboBoxAccNoWith.SelectedItem.ToString()).ToString() + " $";
            lbTotalBalance.Text = clsTransactions.GetTotalBalance().ToString() + " $";
            lbBalanceAccFr.Text = clsTransactions.GetBalanceByAccountNumber(CbAccountFrom.SelectedItem.ToString()).ToString() + " $";
            lbBalanceAccTo.Text = clsTransactions.GetBalanceByAccountNumber(CbAccountTo.SelectedItem.ToString()).ToString() + " $";
            _ShowAllClient();
        }

        private void _DepositWithdraw()
        {                       
            string TransactionType = "";
            int amount = 0;
            string selectedAccount = "";

            //by ref beacuse the value take from  method refletced it here
            if (!_SetSettingTransaction(ref TransactionType, ref amount,ref selectedAccount)) return;


                var result = MessageBox.Show($"Are You Sure You Want to {TransactionType}[{amount}] To Account With Numbers[{selectedAccount}]", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    if (clsTransactions.Deposit(selectedAccount, amount))
                    {
                    _OnUpdateChanges();
                    MessageBox.Show($"{TransactionType} Done Successfully", "Confirm", MessageBoxButtons.OK);
                    }


                }

        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            _DepositWithdraw();
        }



        //=====================Transfer Opertion=================================

        clsTransferInClient _Transfer;

        private bool _CheckFieldIsNull()
        {

            if (lbBalanceAccFr.Text==""|| lbBalanceAccTo.Text == "" || AmountTransfer.Value == 0)
            {
                MessageBox.Show("Please ensure all fields are correctly filled and the transfer amount does not exceed the available balance.",
                       "Invalid Input",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void _TransferSaveAndExcute()
        {
            _Transfer = new clsTransferInClient();

            _Transfer.FromAccNum = CbAccountFrom.SelectedItem.ToString();
            _Transfer.ToAccNum = CbAccountTo.SelectedItem.ToString();
            _Transfer.Date = DateTime.Now;
            _Transfer.Amount = Convert.ToInt32(AmountTransfer.Value);

            clsTransferInClient.Transfer(_Transfer.FromAccNum, _Transfer.ToAccNum, _Transfer.Amount);

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            _CheckFieldIsNull();
            _TransferSaveAndExcute();

            string message = $"Are you sure you want to transfer {AmountTransfer.Value} $ " +
                   $"from account {CbAccountFrom.SelectedItem} to account {CbAccountTo.SelectedItem}?";
            string caption = "Confirm Transfer";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);


            if (result == DialogResult.OK)
            {
                if (_Transfer.Save())
                {
                    MessageBox.Show("The transfer was successful!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    _OnUpdateChanges();
                    _ShowAllTransferLog();
                }
                else
                {
                    MessageBox.Show("The transfer was Faild!","Failed",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void CbAccountFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbBalanceAccFr.Text = clsTransactions.GetBalanceByAccountNumber(CbAccountFrom.SelectedItem.ToString()).ToString() + " $";
        }

        private void CbAccountTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbBalanceAccTo.Text = clsTransactions.GetBalanceByAccountNumber(CbAccountTo.SelectedItem.ToString()).ToString() + " $";
        }

        //===============================Show Transfer Log===================================
        private void _ShowAllTransferLog()
        {
            DataTable dt = clsTransferInClient.GetAllTransferLog();
            guna2DataGridView2.DataSource = dt;
            lbRecordTransfer.Text = guna2DataGridView2.Rows.Count.ToString() + " Record (s) Found";
        }

    }
}
