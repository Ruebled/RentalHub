# Apartment Renting System

## Overview

The Apartment Renting Management System is a WPF (Windows Presentation Foundation) application designed to facilitate the management of apartment rentals. It allows users to manage apartments, tenants, leases, and payments through a user-friendly interface connected to a database.

## Features

- **Apartment Management**: Add, update, delete, and view apartment details.
- **Tenant Management**: Add, update, delete, and view tenant information.
- **Lease Management**: Manage lease agreements between tenants and apartments.
- **Payment Tracking**: Record and track rental payments.
- **Reporting**: Generate reports on rentals, payments, and lease expirations.

## Technologies Used

- **Frontend**: WPF (.NET)
- **Backend**: C#
- **Database**: Oracle/MySQL/SQL Server (Specify which one you're using)
- **ORM**: Entity Framework Core
- **IDE**: Visual Studio

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [Oracle/MySQL/SQL Server Database](https://www.oracle.com/database/technologies/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Getting Started

### Setting Up the Development Environment

1. **Clone the Repository**

   ```bash
   git clone https://github.com/Ruebled/RentalHub
   cd RentalHub
   ```

2. **Set Up the Database**
   - Create a database for the application.
   - Run the provided SQL scripts in the `DatabaseScripts` folder to set up the required tables and seed data.

3. **Configure the Connection String**
   - Update the `appsettings.json` file with your database connection string.

4. **Install Dependencies**
   - Open the solution in Visual Studio.
   - Restore NuGet packages.

5. **Run Database Migrations**
   - Open the Package Manager Console in Visual Studio.
   - Run the following command to apply migrations:

     ```powershell
     Update-Database
     ```

### Running the Application

1. **Build the Solution**
   - In Visual Studio, build the solution (Build -> Build Solution).

2. **Run the Application**
   - Start the application (Debug -> Start Debugging or press F5).

## Usage

1. **Login**
   - Use the default admin credentials to log in (provided in the seed data).

2. **Manage Apartments**
   - Navigate to the "Apartments" section to add, edit, or delete apartment records.

3. **Manage Tenants**
   - Navigate to the "Tenants" section to manage tenant information.

4. **Manage Leases**
   - Create and manage lease agreements in the "Leases" section.

5. **Track Payments**
   - Record rental payments in the "Payments" section.

6. **Generate Reports**
   - Access the "Reports" section to generate various reports on apartments, tenants, leases, and payments.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact

For any questions or suggestions, please contact:

- **Your Name**
- **Email**: <ruebled.nix@gmail.com>
- **GitHub**: [Ruebled](https://github.com/ruebled)

