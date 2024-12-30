using BankBussniesLayer1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanksConsoleTest
{
    internal class Program
    {

        static void Add()
        {
            clsPersons person = new clsPersons();

            person.FirstName = "Test";
            person.LastName = "Test";
            person.Email = "Test";
            person.Phone = "Test";

            if(person.Save())
            {
                Console.WriteLine("Person  addedd succefully wiht id "+person.PersonID);
            }
            else
            {
                Console.WriteLine("Person fail succefully wiht id ");
            }



        }

        static void update(int id)
        {
           clsPersons person = clsPersons.Find(id);

            if(person != null)
            {
                person.FirstName = "Test Update";
                person.LastName = "Test Update";
                person.Email = "Test Update";
                person.Phone = "Test Update";

                if (person.Save())
                {
                    Console.WriteLine("Person  updated succefully wiht id " + person.PersonID);
                }
                else
                {
                    Console.WriteLine("Person updated succefully wiht id ");
                }
            }
            else
            {
                Console.WriteLine("Person not found  wiht id ");

            }




        }

        static void Find(int id)
        {
            clsPersons person = clsPersons.Find(id);

            if (person != null)
            {
                Console.WriteLine(person.PersonID);
                Console.WriteLine(person.FirstName);
                Console.WriteLine(person.LastName);
                Console.WriteLine(person.Email);
                Console.WriteLine(person.Phone);

            }

        }

        //=================================================
        static void AddClient()
        {
            clsClients client = new clsClients();

            client.AccountNumber = "123456789";
            client.PinCode = "1234";
            client.Balance = 1000.50m;
            client.PersonId = 1; // Assuming a valid PersonId for demonstration

            if (client.Save())
            {
                Console.WriteLine("Client added successfully with ID " + client.ClientID);
            }
            else
            {
                Console.WriteLine("Failed to add client.");
            }
        }

        static void UpdateClient(int id)
        {
            clsClients client = clsClients.Find(id);

            if (client != null)
            {
                client.AccountNumber = "987654321";
                client.PinCode = "5678";
                client.Balance = 2000.75m;
                client.PersonId = 6; // Assuming a new valid PersonId for demonstration

                if (client.Save())
                {
                    Console.WriteLine("Client updated successfully with ID " + client.ClientID);
                }
                else
                {
                    Console.WriteLine("Failed to update client.");
                }
            }
            else
            {
                Console.WriteLine("Client not found with ID " + id);
            }
        }

        static void FindClient(int id)
        {
            clsClients client = clsClients.Find(id);

            if (client != null)
            {
                Console.WriteLine("Client ID: " + client.ClientID);
                Console.WriteLine("Account Number: " + client.AccountNumber);
                Console.WriteLine("Pin Code: " + client.PinCode);
                Console.WriteLine("Balance: " + client.Balance);
                Console.WriteLine("Person ID: " + client.PersonId);
            }
            else
            {
                Console.WriteLine("Client not found with ID " + id);
            }
        }

        //=================================================
        static void AddUser()
        {
            clsUser user = new clsUser();

            user.UserName = "TestUser";
            user.Password = "TestPass";
            user.Permissions = 1; // Example: 1 for Admin, 2 for User
            user.PersonId = 1; // Assuming a valid PersonId for demonstration

            if (user.Save())
            {
                Console.WriteLine("User added successfully with ID " + user.UserID);
            }
            else
            {
                Console.WriteLine("Failed to add user.");
            }
        }

        static void UpdateUser(int id)
        {
            clsUser user = clsUser.Find(id);

            if (user != null)
            {
                user.UserName = "UpdatedUser";
                user.Password = "UpdatedPass";
                user.Permissions = 2; // Example: Changed permissions
                user.PersonId = 6; // Assuming a new valid PersonId for demonstration

                if (user.Save())
                {
                    Console.WriteLine("User updated successfully with ID " + user.UserID);
                }
                else
                {
                    Console.WriteLine("Failed to update user.");
                }
            }
            else
            {
                Console.WriteLine("User not found with ID " + id);
            }
        }

        static void FindUser(int id)
        {
            clsUser user = clsUser.Find(id);

            if (user != null)
            {
                Console.WriteLine("User ID: " + user.UserID);
                Console.WriteLine("User Name: " + user.UserName);
                Console.WriteLine("Password: " + user.Password); // Consider masking this in real applications
                Console.WriteLine("Permissions: " + user.Permissions);
                Console.WriteLine("Person ID: " + user.PersonId);
            }
            else
            {
                Console.WriteLine("User not found with ID " + id);
            }
        }

        //=================================================

        static void AddTranfer(string from1,string to1,int amount)
        {
            clsTransferInClient Trans = new clsTransferInClient();

            Trans.FromAccNum = from1;
            Trans.ToAccNum = to1;
            Trans.Date = DateTime.Now;
            Trans.Amount = amount;


            if(Trans.Save()) {

                Console.WriteLine("Transfer register success");

            }
            else
            {
                Console.WriteLine("Faild");
            }



        }

        static void GetAllTransfer()
        {
            DataTable dt = clsTransferInClient.GetAllTransferLog();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine($"id {dr[0]} date{dr[1]} amount{dr[2]} from{dr[3]} toacc{dr[4]} ");
            }
        }


        static void Main(string[] args)
        {
            Console.ReadKey();

        }
    }
}
