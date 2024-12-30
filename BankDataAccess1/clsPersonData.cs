using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataAccess1
{
    public class clsPersonData
    {
        public static int AddPerson(string FirstName, string LastName, string Email, string Phone)
        {
            int PersonID = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"INSERT INTO Persons (FirstName, LastName, Email, Phone) 
                     VALUES (@FirstName, @LastName, @Email, @Phone);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email",Email);
            cmd.Parameters.AddWithValue("@Phone",Phone);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
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

            return PersonID;
        }

        public static bool FindPerson(int PersonID, ref string FirstName, ref string LastName, ref string Email, ref string Phone)
        {
            bool IsFound = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT * FROM Persons WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", PersonID);

            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    FirstName = reader["FirstName"].ToString();
                    LastName = reader["LastName"].ToString();
                    Email = reader["Email"].ToString();
                    Phone = reader["Phone"].ToString();
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

        public static bool UpdatePerson(int PersonID, string FirstName, string LastName, string Email, string Phone)
        {
            bool Updated = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"UPDATE Persons
                     SET FirstName = @FirstName,
                         LastName = @LastName,
                         Email = @Email,
                         Phone = @Phone
                     WHERE Id = @Id;";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", PersonID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email",  Email);
            cmd.Parameters.AddWithValue("@Phone",  Phone);

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

        public static bool DeletePerson(int PersonID)
        {
            bool IsDeleted = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"DELETE FROM Persons WHERE Id = @Id;";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", PersonID);

            try
            {
                Connect.Open();

                int RowsAffected = cmd.ExecuteNonQuery();

                IsDeleted = RowsAffected > 0;
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

        public static DataTable GetAllPersons()
        {
            DataTable DT = new DataTable();

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT * FROM Persons";

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

        public static bool IsPersonIdExists(int personId)
        {
            bool exists = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT CASE 
           WHEN EXISTS (SELECT 1 FROM Clients WHERE PersonId = @personId) 
                OR EXISTS (SELECT 1 FROM Users WHERE PersonId = @personId)
           THEN 1 
           ELSE 0 
          END AS Found;";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@PersonId", personId);

            try
            {
                Connect.Open();

                int RowsAffected = (int)cmd.ExecuteScalar();

                exists = (RowsAffected == 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return exists;
        }





    }
}
