using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
namespace ExecuteInsertRawSql
{
     class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

            var walletToInsert = new Wallet
            {
                Holder = "Essam",
                Balance = 1500
            };

            //create connection to database
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("default"));
            //sql statment
            var sql = "Insert into Wallets (Holder, Balance) Values (@Holder,@Balance)" +
                $"" ;
            //Create parameter
            SqlParameter holderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                SqlDbType = SqlDbType.VarChar,
                Value = walletToInsert.Holder,
                Direction = ParameterDirection.Input
            };

            SqlParameter balanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType = SqlDbType.Decimal,
                Value = walletToInsert.Balance,
                Direction = ParameterDirection.Input
            };

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add(holderParameter);
            command.Parameters.Add(balanceParameter);
            command.CommandType = CommandType.Text;

            connection.Open();

            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"Wallet for {walletToInsert.Holder} was added");
            }
            else
            {
                Console.WriteLine($"Wallet for {walletToInsert.Holder} was not added");
            }

            connection.Close();

            Console.ReadKey();
        }
    }
}
