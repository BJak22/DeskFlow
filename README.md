# DeskFlow API

A modern API for managing desk reservations in office spaces (Hot-desking). The project is built using Clean Architecture, making it easy for employees to book workspaces and for administrators to manage desks.

## Technologies
* **Framework:** .NET 10 (ASP.NET Core Web API)
* **Database:** PostgreSQL
* **ORM:** Entity Framework Core
* **Authentication:** JSON Web Tokens (JWT) + BCrypt
* **API Documentation:** Swagger / OpenAPI

## Main Features
* **Desk Management (CRUD):** Adding, removing, and viewing available desks (including marking desks with dual monitors).
* **Reservation System:** Booking desks for a specific time.
* **Authentication and Registration:** Secure user account system with password hashing and JWT-based verification.

## Running the project locally

1. Clone the repository:
   ```bash
   git clone https://github.com/BJak22/DeskFlow.git
   ```
2. Update the `appsettings.Development.json` file with your PostgreSQL database credentials and a secure JWT key.
3. Build and update the database:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run
   ```
5. Navigate to `http://localhost:5000/swagger` to explore and test the API.

## Authentication (JWT)
To access protected resources, first create an account using the `/api/Auth/register` endpoint, then log in via `/api/Auth/login`. Use the received JWT token in the request header (Authorization: Bearer `your_token`).
