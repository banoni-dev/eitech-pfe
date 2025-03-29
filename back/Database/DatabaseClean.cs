using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

public class DatabaseClean
{
    private readonly string _connectionString;

    public DatabaseClean(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Run()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var commandText = @"
            SET FOREIGN_KEY_CHECKS = 0;

            -- Check if the table exists before truncating
            DROP TABLE IF EXISTS users;
            DROP TABLE IF EXISTS black_listed;
            DROP TABLE IF EXISTS products;
            DROP TABLE IF EXISTS licenses;
            DROP TABLE IF EXISTS license_options;
            DROP TABLE IF EXISTS license_orders;
            DROP TABLE IF EXISTS subscription_orders;
            DROP TABLE IF EXISTS subscription_tiers;
            DROP TABLE IF EXISTS license_activations;
            DROP TABLE IF EXISTS license_bundles;
            DROP TABLE IF EXISTS license_order_options;

            SET FOREIGN_KEY_CHECKS = 1;
        ";

        using var command = new MySqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Database cleaned successfully!");
    }
}
