using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDataAccess1;

namespace BankBussniesLayer1
{
    public class clsPersons
    {
        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.UpdateMode;

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Constructor for updating an existing person
        private clsPersons(int personID, string firstName, string lastName, string email, string phone)
        {
            this.PersonID = personID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;

            Mode = enMode.UpdateMode;
        }

        // Default constructor for adding a new person
        public clsPersons()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";

            Mode = enMode.AddNew;
        }

        // Add a new person
        private bool _AddNew()
        {
            this.PersonID = BankDataAccess1.clsPersonData.AddPerson(this.FirstName, this.LastName, this.Email, this.Phone);

            return (this.PersonID != -1);
        }

        // Update an existing person
        private bool _Update()
        {
            return BankDataAccess1.clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.LastName, this.Email, this.Phone);
        }

        // Find a person by ID
        public static clsPersons Find(int personID)
        {
            string firstName = "", lastName = "", email = "", phone = "";

            if (BankDataAccess1.clsPersonData.FindPerson(personID, ref firstName, ref lastName, ref email, ref phone))
            {
                return new clsPersons(personID, firstName, lastName, email, phone);
            }

            return null;
        }

        // Get all persons
        public static DataTable GetAllPersons()
        {
            return BankDataAccess1.clsPersonData.GetAllPersons();
        }

        // Delete a person by ID
        public static bool Delete(int personID)
        {
            return BankDataAccess1.clsPersonData.DeletePerson(personID);
        }
      
        public static bool IsPersonIdExist(int id)
        {
            return BankDataAccess1.clsPersonData.IsPersonIdExists(id);
        }


        // Save (add or update) a person
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
