using System;
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
            -- Seed Admins
            INSERT INTO admins (username, password, api_key, created_at, last_update_at) VALUES
            ('admin', '$2a$11$abcdefghijklmnopqrstuv', 'seeded-api-key', NOW(), NOW());

            -- Seed Products
            INSERT INTO products (name, description, product_type, created_at, updated_at, is_archived) VALUES
            ('Pro License', 'Full license product', 'License', NOW(), NOW(), FALSE),
            ('Basic Subscription', 'Entry-level subscription plan', 'Subscription', NOW(), NOW(), FALSE);

            -- Seed Users
            INSERT INTO users (first_name, last_name, email, phone, created_at, last_update_at, is_archived) VALUES
            ('John', 'Doe', 'john@example.com', '123456789', NOW(), NOW(), FALSE),
            ('Jane', 'Smith', 'jane@example.com', '987654321', NOW(), NOW(), FALSE);

            -- Seed Licenses
            INSERT INTO licenses (product_id, max_devices, duration, grace_period, public_key, price, created_at, last_update_at, is_archived) VALUES
            (1, 3, 365, 30, 'public-key-1', 49.99, NOW(), NOW(), FALSE);

            -- Seed License Orders
            INSERT INTO license_orders (user_id, license_id, private_key, purchase_date, status, created_at, last_update_at, is_archived) VALUES
            (1, 1, 'private-key-1', NOW(), 'Active', NOW(), NOW(), FALSE);

            -- Seed License Options
            INSERT INTO license_options (license_id, option_name, description, price, created_at, last_update_at, is_archived) VALUES
            (1, 'Option A', 'Feature A', 9.99, NOW(), NOW(), FALSE),
            (1, 'Option B', 'Feature B', 4.99, NOW(), NOW(), FALSE);

            -- Seed License Order Options
            INSERT INTO license_order_options (license_order_id, option_id, price, created_at, last_update_at) VALUES
            (1, 1, 9.99, NOW(), NOW()),
            (1, 2, 4.99, NOW(), NOW());

            -- Seed License Activation
            INSERT INTO license_activations (license_order_id, device_fingerprint, activation_date, created_at, last_update_at, is_archived) VALUES
            (1, 'device-abc123', NOW(), NOW(), NOW(), FALSE);

            -- Seed Subscription Tiers
            INSERT INTO subscription_tiers (product_id, tier_name, duration, grace_period, price, created_at, last_update_at, is_archived) VALUES
            (2, 'Standard Plan', 30, 5, 19.99, NOW(), NOW(), FALSE);

            -- Seed Subscription Orders
            INSERT INTO subscription_orders (id, subscription_tier_id, user_id, purchase_date, start_date, end_date, status, created_at, last_update_at, is_archived) VALUES
            (1, 1, 2, NOW(), NOW(), DATE_ADD(NOW(), INTERVAL 30 DAY), 'Active', NOW(), NOW(), FALSE);

            -- Seed Blacklist
            INSERT INTO black_listed (ip, type, blocked_date, recovery_date, created_at, last_update_at) VALUES
            ('192.168.1.1', 'IP', NOW(), NULL, NOW(), NOW());

            -- Seed License Bundles
            INSERT INTO license_bundles (license_order_id, option_id, created_at, last_update_at) VALUES
            (1, 1, NOW(), NOW());
        ";

        using var command = new MySqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Database seeded successfully!");
    }
}
