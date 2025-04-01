# **Project Overview**

## **🗂 Structure Overview**

This project follows a clean architecture, separating concerns into distinct layers: Models, Controllers, Services, and Repositories. Each layer has a defined responsibility, ensuring maintainability and scalability.

### **1️⃣ Models (Database Layer)**

- Defines database entities and maps properties to database columns.

### **2️⃣ Repositories (Data Access Layer)**

- Handles direct SQL queries for database operations.

### **3️⃣ Services (Business Logic Layer)**

- Implements core business logic and interacts with repositories.

### **4️⃣ Controllers (API Layer)**

- Manages HTTP requests, processes input, and interacts with services.

---

# **📌 Module Interactions**

```
(API Call) → Controller → Service → Repository → Database (SQL)
```

## **Product Module**

- `POST /products` → `ProductController.createProduct()` → `ProductService.createProduct()` → `ProductRepository.createProduct(data)` → (SQL INSERT)
- `GET /products/:id` → `ProductController.getProductById()` → `ProductService.getProductById()` → `ProductRepository.getProductById(id)` → (SQL SELECT)
- `GET /products` → `ProductController.getAllProducts()` → `ProductService.getAllProducts()` → `ProductRepository.getAllProducts()` → (SQL SELECT)
- `PATCH /products/:id` → `ProductController.updateProduct()` → `ProductService.updateProduct()` → `ProductRepository.updateProduct(id, data)` → (SQL UPDATE)
- `DELETE /products/:id` → `ProductController.archiveProduct()` → `ProductService.archiveProduct()` → `ProductRepository.archiveProduct(id)` → (SQL UPDATE is_archived)

## **User Module**

- `POST /users` → `UserController.createUser()` → `UserService.createUser()` → `UserRepository.createUser(data)` → (SQL INSERT)
- `GET /users/:id` → `UserController.getUserById()` → `UserService.getUserById()` → `UserRepository.getUserById(id)` → (SQL SELECT)
- `GET /users` → `UserController.getAllUsers()` → `UserService.getAllUsers()` → `UserRepository.getAllUsers()` → (SQL SELECT)
- `PATCH /users/:id` → `UserController.updateUser()` → `UserService.updateUser()` → `UserRepository.updateUser(id, data)` → (SQL UPDATE)
- `DELETE /users/:id` → `UserController.archiveUser()` → `UserService.archiveUser()` → `UserRepository.archiveUser(id)` → (SQL UPDATE is_archived)

## **Subscription Order Module**

- `POST /subscriptions` → **`PaymentService.processPayment()`** → `SubscriptionOrderController.createSubscription()` → `SubscriptionOrderService.createSubscription()` → `SubscriptionOrderRepository.createSubscription(data)` → (SQL INSERT Subscription)
- `GET /subscriptions/:id` → `SubscriptionOrderController.getSubscriptionById()` → `SubscriptionOrderService.getSubscriptionById()` → `SubscriptionOrderRepository.getSubscriptionById(id)` → (SQL SELECT)
- `PATCH /subscriptions/:id` → `SubscriptionOrderController.updateSubscription()` → `SubscriptionOrderService.updateSubscription()` → `SubscriptionOrderRepository.updateSubscription(id, data)` → (SQL UPDATE)

## **License Management**

- `POST /licenses` → **`PaymentService.processPayment()`** → `LicenseController.createLicense()` → `LicenseService.createLicense()` → `LicenseRepository.createLicense(data)` → (SQL INSERT License)
- `POST /licenses/activate` → `LicenseActivationController.activateLicense()` → `LicenseActivationService.activateLicense()` → `LicenseActivationRepository.activateLicense(data)` → (SQL INSERT)

## **Blacklisted IPs**

- `POST /blacklist` → `BlacklistedController.blockIP()` → `BlacklistedService.blockIP()` → `BlacklistedRepository.blockIP(ip)` → (SQL INSERT)
- `DELETE /blacklist/:ip` → `BlacklistedController.unblockIP()` → `BlacklistedService.unblockIP()` → `BlacklistedRepository.unblockIP(ip)` → (SQL DELETE)

---

# **📌 Payment Handling**

```
(API Call) → PaymentController → PaymentService → Payment Provider API
```

## **Payment Execution and Order Creation**

- **Subscription Payment**: The order record is created **only after** successful payment.
  - `POST /subscriptions` → `PaymentService.processPayment()`
  - If payment succeeds → `SubscriptionOrderService.createSubscription()` → `SubscriptionOrderRepository.createSubscription(data)` → (SQL INSERT Subscription)

- **License Payment**: Similar flow, ensuring payment validation before inserting a license.
  - `POST /licenses` → `PaymentService.processPayment()`
  - If payment succeeds → `LicenseService.createLicense()` → `LicenseRepository.createLicense(data)` → (SQL INSERT License)

### **Payment Request Body**

```json
{
  "receiverWalletId": string,
  "tierCode": string,
  "productCode": string,
  "description": string,
  "acceptedPaymentMethods": ("wallet" | "bank_card" | "e-DINAR")[],
  "firstName": string,
  "lastName": string,
  "phoneNumber": string,
  "email": string,
  "successUrl": string,
  "failUrl": string
}
```