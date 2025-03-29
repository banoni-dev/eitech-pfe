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
            -- Users Table
            INSERT INTO users (first_name, last_name, email, phone, created_at, last_update_at, is_archived) VALUES 
            ('John', 'Doe', 'john.doe@example.com', '1234567890', NOW(), NOW(), FALSE),
            ('Alice', 'Smith', 'alice.smith@example.com', '0987654321', NOW(), NOW(), FALSE);

            -- Products Table
            INSERT INTO products (name, description, product_type, created_at, updated_at, is_archived) VALUES 
            ('Product A', 'Description for product A', 'Licence', NOW(), NOW(), FALSE),
            ('Product B', 'Description for product B', 'Subscription', NOW(), NOW(), FALSE);

            -- Licenses Table
            INSERT INTO licenses (product_id, max_devices, duration, grace_period, public_key, price, created_at, last_update_at, is_archived) VALUES 
            (1, 5, 365, 30, 'public-key-123', 49.99, NOW(), NOW(), FALSE),
            (1, 10, 730, 60, 'public-key-456', 99.99, NOW(), NOW(), FALSE);

            -- LicenseOptions Table
            INSERT INTO license_options (license_id, option_name, description, price, created_at, last_update_at, is_archived) VALUES 
            (1, 'Option A', 'Description for option A', 9.99, NOW(), NOW(), FALSE),
            (2, 'Option B', 'Description for option B', 19.99, NOW(), NOW(), FALSE);

            -- LicenseOrders Table
            INSERT INTO license_orders (user_id, license_id, private_key, purchase_date, status, created_at, last_update_at, is_archived) VALUES 
            (1, 1, 'private-key-123', NOW(), 'Active', NOW(), NOW(), FALSE),
            (2, 2, 'private-key-456', NOW(), 'Expired', NOW(), NOW(), FALSE);

            -- SubscriptionTiers Table
            INSERT INTO subscription_tiers (product_id, tier_name, duration, grace_period, price, created_at, last_update_at, is_archived) VALUES 
            (2, 'Basic Tier', 30, 5, 19.99, NOW(), NOW(), FALSE),
            (2, 'Premium Tier', 365, 30, 199.99, NOW(), NOW(), FALSE);

            -- SubscriptionOrders Table
            INSERT INTO subscription_orders (subscription_tier_id, user_id, purchase_date, start_date, end_date, status, created_at, last_update_at, is_archived) VALUES 
            (1, 1, NOW(), NOW(), DATE_ADD(NOW(), INTERVAL 30 DAY), 'Active', NOW(), NOW(), FALSE),
            (2, 2, NOW(), NOW(), DATE_ADD(NOW(), INTERVAL 365 DAY), 'Canceled', NOW(), NOW(), FALSE);

            -- BlackListed Table
            INSERT INTO black_listed (ip, type, blocked_date, recovery_date, created_at, last_update_at) VALUES 
            ('192.168.1.1', 'IP', NOW(), NULL, NOW(), NOW()),
            ('192.168.1.2', 'IP', NOW(), DATE_ADD(NOW(), INTERVAL 7 DAY), NOW(), NOW());
        ";

        using var command = new MySqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Database seeded successfully!");
    }
}
