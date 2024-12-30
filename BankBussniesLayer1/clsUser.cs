using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDataAccess1;
namespace BankBussniesLayer1
{
    public class clsUser
    {

        public enum enPermissions:int
        {//deal with like a binary value represnt the value in one value more than opertion can do 
            eAll = -1, pManagePeople = 1, pManageClient = 2, pManageUser = 4,
            pManageTransaction = 8, pManageLoginRegister = 16
        };

        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.UpdateMode;

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Permissions { get; set; }
        public int PersonId { get; set; }

        // Constructor for updating an existing user
        private clsUser(int userId, int personId, string userName, string password, int permissions)
        {
            this.UserID = userId;
            this.PersonId = personId;
            this.UserName = userName;
            this.Password = password;
            this.Permissions = permissions;

            Mode = enMode.UpdateMode;
        }

        // Default constructor for adding a new user
        public clsUser()
        {
            this.UserID = -1;
            this.PersonId = -1;
            this.UserName = "";
            this.Password = "";
            this.Permissions = 0;

            Mode = enMode.AddNew;
        }

        // Add a new user
        private bool _AddNew()
        {
            this.UserID = BankDataAccess1.clsUserData.AddUser(this.UserName, this.Password, this.Permissions,this.PersonId);

            return (this.UserID != -1);
        }

        // Update an existing user
        private bool _Update()
        {
            return BankDataAccess1.clsUserData.UpdateUser(this.UserID,this.UserName, this.Password, this.Permissions, this.PersonId);
        }

        // Find a user by ID
        public static clsUser Find(int userId)
        {
            string userName = "", password = "";
            int permissions = 0, personId = -1;

            if (BankDataAccess1.clsUserData.FindUser(userId,  ref userName, ref password, ref permissions, ref personId))
            {
                return new clsUser(userId, personId, userName, password, permissions);
            }

            return null;
        }

        // Get all users
        public static DataTable GetAllUsers()
        {
            return BankDataAccess1.clsUserData.GetAllUsers();
        }

        // Delete a user by ID
        public static bool Delete(int userId)
        {
            return BankDataAccess1.clsUserData.DeleteUser(userId);
        }

        public static bool IsUserExist(string username)
        {
            return  BankDataAccess1.clsUserData.IsUserExist(username);
        }

        // Save (add or update) a user
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
                    return false;
            }
        }

        public static clsUser FindUserLogin(string userName, string password)
        {
            int permissions = 0, personId = -1, userId = -1;

            if (BankDataAccess1.clsUserData.FindLoginUser(ref userId,  userName, password, ref permissions, ref personId))
            {
                return new clsUser(userId, personId, userName, password, permissions);
            }

            return null;
        }

        public bool CheckPermissions(enPermissions permissions)
        {
            if ((enPermissions)this.Permissions == enPermissions.eAll)
                return true;

            return ((enPermissions)this.Permissions & permissions) == permissions;
        }

        public static DataTable GetAllUsersLogin()
        {
            return BankDataAccess1.clsUserData.GetAllUsersLogin();
        }


        public static void AddUserLoginLog(int UserID,DateTime dateLogin)
        {
           BankDataAccess1.clsUserData.AddUserLoginLog(UserID,dateLogin);
        }

    }

}
