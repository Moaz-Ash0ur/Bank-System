using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankBussniesLayer1
{
    public class clsClients
    {
        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.UpdateMode;

        public int ClientID { get; set; }
        public string AccountNumber { get; set; }
        public string PinCode { get; set; }
        public decimal Balance { get; set; }
        public int PersonId { get; set; }

        // Constructor for updating an existing client
        private clsClients(int clientID, string accountNumber, string pinCode, decimal balance, int personId)
        {
            this.ClientID = clientID;
            this.AccountNumber = accountNumber;
            this.PinCode = pinCode;
            this.Balance = balance;
            this.PersonId = personId;

            Mode = enMode.UpdateMode;
        }

        // Default constructor for adding a new client
        public clsClients()
        {
            this.ClientID = -1;
            this.AccountNumber = "";
            this.PinCode = "";
            this.Balance = 0;
            this.PersonId = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNew()
        {
            this.ClientID = BankDataAccess1.clsClientData.AddClient(this.AccountNumber, this.PinCode, this.Balance, this.PersonId);

            return (this.ClientID != -1);
        }

        private bool _Update()
        {
            return BankDataAccess1.clsClientData.UpdateClient(this.ClientID, this.AccountNumber, this.PinCode, this.Balance, this.PersonId);
        }

        public static clsClients Find(int clientID)
        {
            string accountNumber = "", pinCode = "";
            decimal balance = 0; int personId = -1;

            if (BankDataAccess1.clsClientData.FindClient(clientID, ref accountNumber, ref pinCode, ref balance, ref personId))
            {
                return new clsClients(clientID, accountNumber, pinCode, balance, personId);
            }

            return null;
        }

        public static DataTable GetAllClients()
        {
            return BankDataAccess1.clsClientData.GetAllClients();
        }

        public static bool Delete(int clientID)
        {
            return BankDataAccess1.clsClientData.DeleteClient(clientID);
        }

        public static bool IsExists(string Acc)
        {   
            return BankDataAccess1.clsClientData.IsClientExist(Acc);
        }

        public static int GetClientID(string Acc)
        {
            return BankDataAccess1.clsClientData.GetClientID(Acc);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.UpdateMode:
                    return _Update();

                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    break;
            }
            return true;
        }





    }
}
