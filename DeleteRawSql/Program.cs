using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
namespace DeleteRawSql
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
                Holder = "testing..",
                Balance = 2000
            };

            //create connection to database
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("default"));
            //sql statment
            var sql = "DELETE from Wallets WHERE Id=@Id";
            //Create parameter
            SqlParameter idParameter = new SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                Value = 1,
                Direction = ParameterDirection.Input
            };


            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add(idParameter);

            command.CommandType = CommandType.Text;

            connection.Open();

            //walletToInsert.Id = (int)command.ExecuteScalar();

            //Console.WriteLine($"Wallet for {walletToInsert.Holder} was updated");

            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"Wallet for {walletToInsert.Holder} was deleted ");
            }
            

            connection.Close();

            Console.ReadKey();

        }
    }
}
