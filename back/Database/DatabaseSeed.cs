using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

public class DatabaseSeed
{
    private readonly string _connectionString;

    public DatabaseSeed(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Run()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var commandText = @"
            -- Seed Users
            INSERT INTO users (first_name, last_name, email, phone, created_at, last_update_at, is_archived) VALUES
            ('John', 'Doe', 'john.doe@example.com', '1234567890', NOW(), NOW(), FALSE),
            ('Alice', 'Smith', 'alice.smith@example.com', '0987654321', NOW(), NOW(), FALSE);

            -- Seed Licenses
            INSERT INTO licenses (product_id, max_devices, duration, grace_period, public_key, price, created_at, last_update_at, is_archived) VALUES
            (1, 5, 365, 30, 'public-key-123', 49.99, NOW(), NOW(), FALSE),
            (2, 1, 30, 5, 'public-key-456', 29.99, NOW(), NOW(), FALSE),
            (3, 3, 0, 0, 'public-key-789', 19.99, NOW(), NOW(), FALSE);

            -- Seed License Orders
            INSERT INTO license_orders (user_id, license_id, private_key, purchase_date, status, created_at, last_update_at, is_archived) VALUES
            (1, 1, 'private-key-123', NOW(), 'Active', NOW(), NOW(), FALSE),
            (1, 2, 'private-key-456', NOW(), 'Active', NOW(), NOW(), FALSE),
            (1, 3, 'private-key-789', DATE_SUB(NOW(), INTERVAL 1 YEAR), 'Active', NOW(), NOW(), FALSE);

            -- Seed Subscription Tiers
            INSERT INTO subscription_tiers (product_id, tier_name, duration, grace_period, price, created_at, last_update_at, is_archived) VALUES
            (1, 'Basic Tier', 30, 5, 19.99, NOW(), NOW(), FALSE);

            -- Seed Subscription Orders
            INSERT INTO subscription_orders (subscription_tier_id, user_id, purchase_date, start_date, end_date, status, created_at, last_update_at, is_archived) VALUES
            (1, 1, NOW(), NOW(), DATE_ADD(NOW(), INTERVAL 30 DAY), 'Active', NOW(), NOW(), FALSE),
            (1, 1, NOW(), NOW(), DATE_SUB(NOW(), INTERVAL 1 MONTH), 'Active', NOW(), NOW(), FALSE);
        ";

        using var command = new MySqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Database seeded successfully!");
    }
}
