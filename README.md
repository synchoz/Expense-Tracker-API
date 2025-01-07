
# Expense Management API

A RESTful API built with **.NET Core 8.0** to manage user authentication and expense tracking. The API provides endpoints for user registration, login, and managing expenses with support for filtering by date and secure authentication using JWT tokens.

---

## Inspiration
This project was inspired by the [Backend Roadmap](https://roadmap.sh/projects/expense-tracker-api), which provides a comprehensive guide for building a expense tracking API.

## Features

### **Authentication**
- User registration with hashed passwords.
- Login with username and password, returning a JWT for secure access.
- Authorization using JWT tokens for all protected endpoints.

### **Expense Management**
- **CRUD operations**:
  - Create, update, delete, and fetch expenses.
- Filter expenses by date range and username.

---

## Endpoints

### **AuthController**
Base URL: `/api/auth`

| Method | Endpoint      | Description                |
|--------|---------------|----------------------------|
| POST   | `/register`   | Register a new user.       |
| POST   | `/login`      | Login and retrieve a JWT.  |

#### **Request Examples**

##### **Register a User**
```json
POST /api/auth/register
{
    "username": "johndoe",
    "email": "johndoe@example.com",
    "password": "securepassword"
}
```

##### **Login a User**
```json
POST /api/auth/login
{
    "username": "johndoe",
    "password": "securepassword"
}
```

Response:
```json
{
    "token": "<JWT-TOKEN>"
}
```

### **ExpensesController**
Base URL: `/api/expenses`

| Method | Endpoint          | Description                                     |
|--------|-------------------|-------------------------------------------------|
| GET    | `/{username}`     | Fetch expenses for a user, optionally by date. |
| PUT    | `/`               | Update an expense by username, type, and name. |
| POST   | `/`               | Create a new expense.                          |
| DELETE | `/{id}`           | Delete an expense by ID.                       |

#### **Request Examples**

##### **Get Expenses**
```http
GET /api/expenses/johndoe?startDate=2025-01-01&endDate=2025-01-31
Authorization: Bearer <JWT-TOKEN>
```

##### **Create an Expense**
```json
POST /api/expenses
{
    "username": "johndoe",
    "type": "Groceries",
    "name": "Supermarket",
    "moneySpent": 100.50,
    "note": "Weekly groceries"
}
```

##### **Update an Expense**
```http
PUT /api/expenses?username=johndoe&type=Groceries&name=Supermarket
Authorization: Bearer <JWT-TOKEN>
```
```json
{
    "moneySpent": 120.00,
    "note": "Updated groceries note"
}
```

##### **Delete an Expense**
```http
DELETE /api/expenses/1
Authorization: Bearer <JWT-TOKEN>
```

---

## Quick Setup and Usage

### **Prerequisites**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/)
- SQL Server (or another database provider compatible with EF Core)
- Postman or any API testing tool

### **Setup**
1. Clone the repository:
   ```bash
   git clone https://github.com/synchoz/Expense-Tracker-API.git
   cd your-repo-folder
   ```

2. Configure the database connection in `appsettings.json`:
   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Your-SQL-Server-Connection-String"
       },
       "Jwt": {
           "Key": "Your-Secret-Key",
           "Issuer": "Your-Issuer",
           "Audience": "Your-Audience"
       }
   }
   ```

3. Run database migrations:
   ```bash
   dotnet ef database update
   ```

4. Start the API:
   ```bash
   dotnet run
   ```

### **Testing**
- Use Postman or cURL to test endpoints.
- Authenticate users and use the JWT token for accessing protected resources.

---

## Authentication Flow

1. **User Registration**:
   - Call `/api/auth/register` with `username`, `email`, and `password`.
2. **User Login**:
   - Call `/api/auth/login` with the registered credentials.
   - On successful login, a JWT token is returned.
3. **Access Protected Endpoints**:
   - Include the JWT token in the `Authorization` header for all protected routes:
     ```http
     Authorization: Bearer <JWT-TOKEN>
     ```

---

## Technologies Used

- **ASP.NET Core 8.0**: Web framework for building APIs.
- **Entity Framework Core**: Database ORM for interacting with SQL Server.
- **JWT Authentication**: Secure user authentication and session management.
- **LINQ**: For querying data efficiently.
- **Dependency Injection**: Ensures clean and testable architecture.

---

## Future Improvements
- Add pagination for retrieving expenses.
- Implement detailed role-based access control.
- Enhance logging and error handling.

Feel free to contribute or suggest improvements! 😊
