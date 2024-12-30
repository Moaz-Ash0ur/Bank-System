using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBussniesLayer1
{
    public class clsTransactions
    {

        public static bool Deposit(string AccNum,int Amount)
        {
           return BankDataAccess1.clsTransactionData.Depoist(AccNum,Amount);
        }
        public static bool Withdrow(string AccNum, int Amount)
        {
            return BankDataAccess1.clsTransactionData.Depoist(AccNum, Amount * -1);
        }
        public static decimal GetTotalBalance()
        {
            return BankDataAccess1.clsTransactionData.GetTotalBalances();
        }
        public static List<string> GetAccountNumbers()
        {
            return BankDataAccess1.clsTransactionData.GetAccountNumbers();
        }
        public static List<string> GetAccountNumbersP()
        {
            return BankDataAccess1.clsTransactionData.GetAccountNumbersP();
        }
        public static decimal GetBalanceByAccountNumber(string acc)
        {
            return BankDataAccess1.clsTransactionData.GetBalanceByAccountNumber(acc);
        }


    }
}
