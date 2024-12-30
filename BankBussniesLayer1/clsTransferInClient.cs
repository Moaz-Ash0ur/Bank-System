using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBussniesLayer1
{
    public class clsTransferInClient
    {
      
        //just for add transfer log and return all transfer in system

        public int TransferId { get; set; }
        public int Amount { get; set; }
        public string FromAccNum { get; set; }
        public string ToAccNum { get; set; }
        public DateTime Date { get; set; }

        public clsTransferInClient()
        {
            this.TransferId = -1;
            this.Date = DateTime.Now;
            this.Amount = 0;
            this.FromAccNum = string.Empty;
            this.ToAccNum = string.Empty; 
        }

        private bool _AddNew()
        {
            this.TransferId = BankDataAccess1.clsTransactionData.AddTransferLog(this.Date, this.Amount, this.FromAccNum, this.ToAccNum);

            return (this.TransferId != -1);
        }

        public static DataTable GetAllTransferLog()
        {
            return BankDataAccess1.clsTransactionData.GetAllTransferLog();  
        }

        public static void Transfer(string FromAcc, string ToAcc,int transferAmount)
        {
            clsTransactions.Withdrow(FromAcc, transferAmount);
            clsTransactions.Deposit(ToAcc, transferAmount);
        }

        public bool Save()
        {

         if (_AddNew())
        {
            return true;
        }
        else
        {
            return false;
        }

        }


    }
}
