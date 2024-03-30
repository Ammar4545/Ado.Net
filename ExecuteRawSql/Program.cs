using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
namespace ExecuteRawSql
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

            //create connection to database
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("default"));

            var sql = "Select * from Wallets";
            
            SqlCommand command = new SqlCommand(sql, connection);

            command.CommandType = CommandType.Text;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            Wallet wallet;

            while(reader.Read())
            {
                wallet = new Wallet
                {
                    Id = reader.GetInt32("Id"),
                    Holder = reader.GetString("Holder"),
                    Balance = reader.GetDecimal("Balance"),
                };
                Console.WriteLine(wallet);
            }
            connection.Close();

            Console.ReadKey();

        }
    }
}
