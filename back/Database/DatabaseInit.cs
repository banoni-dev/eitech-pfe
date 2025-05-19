using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

public class DatabaseInit
{
    private readonly string _connectionString;

    public DatabaseInit(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Run()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var commandText = @"
            -- Users Table
            CREATE TABLE IF NOT EXISTS users (
                user_id INT AUTO_INCREMENT PRIMARY KEY,
                first_name VARCHAR(255) NOT NULL,
                last_name VARCHAR(255) NOT NULL,
                email VARCHAR(255) NOT NULL UNIQUE,
                phone VARCHAR(50) NOT NULL UNIQUE,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                is_archived BOOLEAN NOT NULL DEFAULT FALSE
            );

            -- Products Table
            CREATE TABLE IF NOT EXISTS products (
                id INT AUTO_INCREMENT PRIMARY KEY,
                name VARCHAR(255) NOT NULL UNIQUE,
                description TEXT NOT NULL,
                product_type ENUM('License', 'Subscription') NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                is_archived BOOLEAN NOT NULL DEFAULT FALSE
            );

            -- Licenses Table
            CREATE TABLE IF NOT EXISTS licenses (
                license_id INT AUTO_INCREMENT PRIMARY KEY,
                product_id INT NOT NULL,
                max_devices INT NOT NULL,
                duration INT NOT NULL,
                grace_period INT NOT NULL,
                public_key TEXT NOT NULL UNIQUE,
                price DECIMAL(10,2) NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                is_archived BOOLEAN NOT NULL DEFAULT FALSE,
                FOREIGN KEY (product_id) REFERENCES products(id)
            );

            -- LicenseOptions Table
            CREATE TABLE IF NOT EXISTS license_options (
                option_id INT AUTO_INCREMENT PRIMARY KEY,
                license_id INT NOT NULL,
                option_name VARCHAR(255) NOT NULL UNIQUE,
                description TEXT NOT NULL,
                price DECIMAL(10,2) NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                is_archived BOOLEAN NOT NULL DEFAULT FALSE,
                FOREIGN KEY (license_id) REFERENCES licenses(license_id)
            );

            -- LicenseOrders Table
            CREATE TABLE IF NOT EXISTS license_orders (
                license_order_id INT AUTO_INCREMENT PRIMARY KEY,
                user_id INT NOT NULL,
                license_id INT NOT NULL,
                private_key TEXT NOT NULL,
                purchase_date DATETIME NOT NULL,
                status ENUM('Active', 'Expired', 'Canceled') NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                is_archived BOOLEAN NOT NULL DEFAULT FALSE,
                FOREIGN KEY (user_id) REFERENCES users(user_id),
                FOREIGN KEY (license_id) REFERENCES licenses(license_id)
            );

            -- LicenseOrderOptions Table
            CREATE TABLE IF NOT EXISTS license_order_options (
                order_option_id INT AUTO_INCREMENT PRIMARY KEY,
                license_order_id INT NOT NULL,
                option_id INT NOT NULL,
                price DECIMAL(10,2) NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                FOREIGN KEY (license_order_id) REFERENCES license_orders(license_order_id),
                FOREIGN KEY (option_id) REFERENCES license_options(option_id)
            );

            -- LicenseBundles Table
            CREATE TABLE IF NOT EXISTS license_bundles (
                license_order_id INT NOT NULL,
                option_id INT NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                PRIMARY KEY (license_order_id, option_id),
                FOREIGN KEY (license_order_id) REFERENCES license_orders(license_order_id),
                FOREIGN KEY (option_id) REFERENCES license_options(option_id)
            );

            

            -- SubscriptionTiers Table
            CREATE TABLE IF NOT EXISTS subscription_tiers (
                tier_id INT AUTO_INCREMENT PRIMARY KEY,
                product_id INT NOT NULL,
                tier_name VARCHAR(255) NOT NULL,
                duration INT NOT NULL,
                grace_period INT NOT NULL,
                price DECIMAL(10,2) NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                is_archived BOOLEAN NOT NULL DEFAULT FALSE,
                FOREIGN KEY (product_id) REFERENCES products(id)
            );

            -- SubscriptionOrders Table
            CREATE TABLE IF NOT EXISTS subscription_orders (
                id INT AUTO_INCREMENT PRIMARY KEY, -- Added id as primary key
                subscription_tier_id INT NOT NULL,
                user_id INT NOT NULL,
                purchase_date DATETIME NOT NULL,
                start_date DATETIME NOT NULL,
                end_date DATETIME NOT NULL,
                status ENUM('Active', 'Expired', 'Canceled') NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                is_archived BOOLEAN NOT NULL DEFAULT FALSE,
                FOREIGN KEY (subscription_tier_id) REFERENCES subscription_tiers(tier_id),
                FOREIGN KEY (user_id) REFERENCES users(user_id)
            );

            -- BlackListed Table
            CREATE TABLE IF NOT EXISTS black_listed (
                id INT AUTO_INCREMENT PRIMARY KEY,
                ip VARCHAR(255) NOT NULL,
                type ENUM('IP', 'User', 'Device') NOT NULL,
                blocked_date DATETIME NOT NULL,
                recovery_date DATETIME NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
            );

            -- Admins Table
            CREATE TABLE IF NOT EXISTS admins (
                id INT AUTO_INCREMENT PRIMARY KEY,
                username VARCHAR(255) NOT NULL UNIQUE,
                password VARCHAR(255) NOT NULL,
                api_key VARCHAR(255) NOT NULL UNIQUE,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                last_update_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
            );
        ";

        using var command = new MySqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Database initialized successfully!");
    }
}
