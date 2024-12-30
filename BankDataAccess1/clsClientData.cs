using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataAccess1
{
    public class clsClientData
    {

        public static int AddClient(string AccountNumber, string PINCode, decimal Balance, int PersonId)
        {
            int ClientID = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"INSERT INTO Clients (AccountNumber, PINCode, Balance, PersonId) 
                     VALUES (@AccountNumber, @PINCode, @Balance, @PersonId);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            cmd.Parameters.AddWithValue("@PINCode", PINCode);
            cmd.Parameters.AddWithValue("@Balance", Balance);
            cmd.Parameters.AddWithValue("@PersonId", PersonId);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ClientID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return ClientID;
        }

        public static bool FindClient(int ClientID, ref string AccountNumber, ref string PINCode, ref decimal Balance, ref int PersonId)
        {
            bool IsFound = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT * FROM Clients WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", ClientID);

            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    AccountNumber = reader["AccountNumber"].ToString();
                    PINCode = reader["PINCode"].ToString();
                    Balance = Convert.ToDecimal(reader["Balance"]);
                    PersonId = Convert.ToInt32(reader["PersonId"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return IsFound;
        }

        public static bool UpdateClient(int ClientID, string AccountNumber, string PINCode, decimal Balance, int PersonId)
        {
            bool Updated = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"UPDATE Clients
                     SET AccountNumber = @AccountNumber,
                         PINCode = @PINCode,
                         Balance = @Balance,
                         PersonId = @PersonId
                     WHERE Id = @Id;";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", ClientID);
            cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            cmd.Parameters.AddWithValue("@PINCode", PINCode);
            cmd.Parameters.AddWithValue("@Balance", Balance);
            cmd.Parameters.AddWithValue("@PersonId", PersonId);

            try
            {
                Connect.Open();

                int RowsAffected = cmd.ExecuteNonQuery();

                Updated = RowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return Updated;
        }

        public static bool DeleteClient(int ClientID)
        {
            bool IsDeleted = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"DELETE FROM Clients WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", ClientID);

            //Delete Person related wiht client depand ID

             int personId =  GetPersonID(ClientID);

            string deletePersonQ = "delete from persons where Id = @Id";

            SqlCommand cmd1 = new SqlCommand(deletePersonQ, Connect);
            cmd1.Parameters.AddWithValue("@Id", personId);


            try
            {
                Connect.Open();

                int RowsAffected = cmd.ExecuteNonQuery();
                int RowsAffected1 = cmd1.ExecuteNonQuery();

                IsDeleted = (RowsAffected > 0) && (RowsAffected1 > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return IsDeleted;
        }

        public static DataTable GetAllClients()
        {
            DataTable DT = new DataTable();

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT        Clients.AccountNumber, Persons.FirstName +' '+ Persons.LastName as FullName, Clients.PINCode, Clients.Balance, Persons.Email, Persons.Phone 
FROM            Persons INNER JOIN
                         Clients ON Persons.Id = Clients.PersonId";

            SqlCommand cmd = new SqlCommand(query, Connect);

            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    DT.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return DT;
        }


        //For Check and Getcolumn for excute opertion success

        public static bool IsClientExist(string AccountNumber)
        {
            bool IsExists = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT 1 FROM Clients WHERE AccountNumber = @AccountNumber";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

               if(result != null)
                {
                    IsExists = true;
                }

            }
            catch (Exception ex)
            {
               // Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return IsExists;
        }

        public static int GetClientID(string AccountNumber)
        {
            int ClientID = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"select Id from Clients where AccountNumber = @AccountNumber";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(),out int ReturnID)) {

                    ClientID = ReturnID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return ClientID;
        }

        private static int GetPersonID(int Id)
        {
            int personId = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT  Persons.Id FROM Clients INNER JOIN
                         Persons ON Clients.PersonId = Persons.Id where Clients.Id = @Id";

            SqlCommand cmd = new SqlCommand(query,conn);

            cmd.Parameters.AddWithValue("@Id", Id);

            try
            {
                conn.Open();

                personId = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }

            return personId;
        }


    }
}
