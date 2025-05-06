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
            ('Alice', 'Smith', 'alice.smith@example.com', '0987654321', NOW(), NOW(), FALSE),
            ('Bob', 'Brown', 'bob.brown@example.com', '1122334455', NOW(), NOW(), FALSE);

            -- Products Table
            INSERT INTO products (name, description, product_type, created_at, updated_at, is_archived) VALUES 
            ('License Product A', 'License for Product A', 'Licence', NOW(), NOW(), FALSE),
            ('License Product B', 'License for Product B', 'Licence', NOW(), NOW(), FALSE),
            ('Subscription Product A', 'Subscription for Product A', 'Subscription', NOW(), NOW(), FALSE),
            ('Subscription Product B', 'Subscription for Product B', 'Subscription', NOW(), NOW(), FALSE);

            -- Licenses Table
            INSERT INTO licenses (product_id, max_devices, duration, grace_period, public_key, price, created_at, last_update_at, is_archived) VALUES 
            (1, 5, 365, 30, 'public-key-123', 49.99, NOW(), NOW(), FALSE),
            (1, 10, 730, 60, 'public-key-456', 99.99, NOW(), NOW(), FALSE),
            (2, 3, 180, 15, 'public-key-789', 29.99, NOW(), NOW(), FALSE);

            -- LicenseOptions Table
            INSERT INTO license_options (license_id, option_name, description, price, created_at, last_update_at, is_archived) VALUES 
            (1, 'Option A', 'Extra feature A', 9.99, NOW(), NOW(), FALSE),
            (2, 'Option B', 'Extra feature B', 19.99, NOW(), NOW(), FALSE),
            (3, 'Option C', 'Extra feature C', 14.99, NOW(), NOW(), FALSE);

            -- LicenseOrders Table
            INSERT INTO license_orders (user_id, license_id, private_key, purchase_date, status, created_at, last_update_at, is_archived) VALUES 
            (1, 1, 'private-key-123', DATE_SUB(NOW(), INTERVAL 100 DAY), 'Active', NOW(), NOW(), FALSE),
            (2, 2, 'private-key-456', DATE_SUB(NOW(), INTERVAL 800 DAY), 'Expired', NOW(), NOW(), FALSE),
            (3, 3, 'private-key-789', DATE_SUB(NOW(), INTERVAL 50 DAY), 'Active', NOW(), NOW(), FALSE);

            -- SubscriptionTiers Table
            INSERT INTO subscription_tiers (product_id, tier_name, duration, grace_period, price, created_at, last_update_at, is_archived) VALUES 
            (3, 'Basic Tier', 30, 5, 19.99, NOW(), NOW(), FALSE),
            (3, 'Premium Tier', 365, 30, 199.99, NOW(), NOW(), FALSE),
            (4, 'Standard Tier', 90, 10, 49.99, NOW(), NOW(), FALSE);

            -- SubscriptionOrders Table
            INSERT INTO subscription_orders (subscription_tier_id, user_id, purchase_date, start_date, end_date, status, created_at, last_update_at, is_archived) VALUES 
            (1, 1, DATE_SUB(NOW(), INTERVAL 20 DAY), DATE_SUB(NOW(), INTERVAL 20 DAY), DATE_ADD(NOW(), INTERVAL 10 DAY), 'Active', NOW(), NOW(), FALSE),
            (2, 2, DATE_SUB(NOW(), INTERVAL 400 DAY), DATE_SUB(NOW(), INTERVAL 400 DAY), DATE_SUB(NOW(), INTERVAL 35 DAY), 'Expired', NOW(), NOW(), FALSE),
            (3, 3, DATE_SUB(NOW(), INTERVAL 50 DAY), DATE_SUB(NOW(), INTERVAL 50 DAY), DATE_ADD(NOW(), INTERVAL 40 DAY), 'Active', NOW(), NOW(), FALSE);

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
