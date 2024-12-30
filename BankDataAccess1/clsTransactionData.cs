using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BankDataAccess1
{
    public class clsTransactionData
    {
         // same fun use for withdrow | just multiply the amoiunt * -1   
        public static bool Depoist(string AccNum,int Amount)
        {

            bool IsDone = true;

            SqlConnection Connectoin = new SqlConnection(clsDataAccessSetting.Connection);

            string Query = @"Update Clients 
                           set Balance = Balance + @Amount
                           where AccountNumber = @AccountNumber";

            SqlCommand cmd = new SqlCommand(Query,Connectoin);
            cmd.Parameters.AddWithValue("@AccountNumber",AccNum);
            cmd.Parameters.AddWithValue("@Amount", Amount);

            try
            {
                Connectoin.Open();

                int RowsAffected = cmd.ExecuteNonQuery();

                IsDone = RowsAffected > 0;
            }
            catch (Exception ex)
            {
               // Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connectoin.Close();
            }

            return IsDone;


        }

        public static decimal GetTotalBalances()
        {

            decimal totalBalances = 0;

            SqlConnection Connectoin = new SqlConnection(clsDataAccessSetting.Connection);

            string Query = @"select SUM(Balance)
                             from Clients";

            SqlCommand cmd = new SqlCommand(Query,Connectoin);

            try
            {
                Connectoin.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    totalBalances = Convert.ToDecimal(reader[0]);
                }


            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connectoin.Close();
            }

            return totalBalances;
        }

        public static DataTable GetAllTransferLog()
        {

            DataTable DT = new DataTable();

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT * FROM TransferLog";

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
               // Console.WriteLine(ex.Message);
            }
            finally
            {
                Connect.Close();
            }

            return DT;

        }

        public static int AddTransferLog(DateTime date, decimal amount, string fromAccountNumber, string toAccountNumber)
        {
            int transferLogId = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"INSERT INTO TransferLog(Date, Amount, FromAccountNumber, ToAccountNumber) 
        VALUES (@Date, @Amount, @FromAccountNumber, @ToAccountNumber);
        SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@FromAccountNumber", fromAccountNumber);
            cmd.Parameters.AddWithValue("@ToAccountNumber", toAccountNumber);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    transferLogId = insertedId;
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

            return transferLogId;
        }

        public static List<string> GetAccountNumbers()
        {

            List<string> accountNumbers = new List<string>();

            SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT AccountNumber FROM Clients";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   accountNumbers.Add(reader[0].ToString());
                }
                
            }
            catch (Exception)
            {

            }
            finally {
            
            conn.Close();
            
            }

            return accountNumbers;
        }

        public static decimal GetBalanceByAccountNumber(string accountNumber)
        {
            decimal balance = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT Balance FROM Clients WHERE AccountNumber = @AccountNumber";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && decimal.TryParse(result.ToString(),out decimal retuendBalance))
                {
                    balance = retuendBalance;
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
            }

            return balance;
        }

        public static List<string> GetAccountNumbersP()
        {

            List<string> accountNumbers = new List<string>();

            SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT AccountNumber FROM Clients WHERE Balance > 0";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    accountNumbers.Add(reader[0].ToString());
                }

            }
            catch (Exception)
            {

            }
            finally
            {

                conn.Close();

            }

            return accountNumbers;
        }





    }
}
