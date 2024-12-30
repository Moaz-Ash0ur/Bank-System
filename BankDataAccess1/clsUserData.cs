using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BankDataAccess1
{
    public class clsUserData
    {

        public static int AddUser(string UserName, string Password, int Permissions, int PersonId)
        {
            int UserID = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"INSERT INTO Users (UserName, Password, Permissions, PersonId) 
                         VALUES (@UserName, @Password, @Permissions, @PersonId);
                         SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Permissions", Permissions);
            cmd.Parameters.AddWithValue("@PersonId", PersonId);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    UserID = InsertedID;
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

            return UserID;
        }

        public static bool FindUser(int UserID, ref string UserName, ref string Password, ref int Permissions, ref int PersonId)
        {
            bool IsFound = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT * FROM Users WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", UserID);

            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    Permissions = Convert.ToInt32(reader["Permissions"]);
                    PersonId = reader["PersonId"] != DBNull.Value ? Convert.ToInt32(reader["PersonId"]) : 0;
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

        public static bool UpdateUser(int UserID, string UserName, string Password, int Permissions, int PersonId)
        {
            bool Updated = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"UPDATE Users
                         SET UserName = @UserName,
                             Password = @Password,
                             Permissions = @Permissions,
                             PersonId = @PersonId
                         WHERE Id = @Id;";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", UserID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Permissions", Permissions);
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

        public static bool DeleteUser(int UserID)
        {
            bool IsDeleted = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"DELETE FROM Users WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Id", UserID);

            //delete person with user
            int personId = GetPersonID(UserID);

            string deletePersonQ = "delete from persons where Id = @Id";

            SqlCommand cmd1 = new SqlCommand(deletePersonQ, Connect);
            cmd1.Parameters.AddWithValue("@Id", personId);


            try
            {
                Connect.Open();

                int RowsAffected = cmd.ExecuteNonQuery();
                int RowsAffected2 = cmd1.ExecuteNonQuery();

                IsDeleted = (RowsAffected > 0) && (RowsAffected2 > 0);
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

        public static DataTable GetAllUsers()
        {
            DataTable DT = new DataTable();

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT Users.Id as UserID,Persons.FirstName +' '+ Persons.LastName as FullName, Users.UserName, Users.Password, Users.Permissions, Persons.Email, Persons.Phone
                            FROM  Persons INNER JOIN
                             Users ON Persons.Id = Users.PersonId";

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

        public static bool IsUserExist(string UserName)
        {
            bool IsExists = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT 1 FROM Users WHERE UserName = @UserName";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                IsExists = result != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return IsExists;
        }


        //=================================== use in login
        public static int AddUserLoginLog(int UserId, DateTime Date)
        {
            int LogId = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"INSERT INTO UsersLoginLog (UserId, Date) 
                     VALUES (@UserId, @Date);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Date", Date);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    LogId = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connect.Close();
            }

            return LogId;
        }

        public static bool FindLoginUser(ref int UserID,  string UserName,  string Password, ref int Permissions, ref int PersonId)
        {
            bool IsFound = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"select * from users where Password = @Password and UserName = @UserName";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Username", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);


            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    IsFound = true;

                    UserID = (int)reader[0];
                    UserName = (String)reader[1];
                    Password = (string)reader[2];
                    Permissions = (int)reader[3];
                    PersonId = (int)reader[4];


                }

                reader.Close();


            }
            catch (Exception)
            {
                // IsFound = false;
                Console.WriteLine("TEST");
            }
            finally
            {
                Connect.Close();
            }

            return IsFound;
        }

        public static DataTable GetAllUsersLogin()
        {
            DataTable DT = new DataTable();

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT Users.UserName, Users.Password, UsersLoginLog.Date, Users.Permissions
                            FROM Users INNER JOIN
                            UsersLoginLog ON Users.Id = UsersLoginLog.UserId";

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

        private static int GetPersonID(int UserId)
        {
            int personId = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT  Persons.Id FROM  Persons INNER JOIN
                         Users ON Persons.Id = Users.PersonId where Users.Id = @Id";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", UserId);

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
