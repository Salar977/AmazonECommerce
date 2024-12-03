# Amazon ECommerce

## Description

Amazon ECommerce is a robust web application designed to simulate an e-commerce platform, featuring a clean architecture and seamless integration with modern technologies. This project demonstrates best practices in backend development using .NET Core, Entity Framework, and SQL Server.

## Features

- User authentication and role-based access control (Admin/User).
- Product management (Add, Update, Delete, and View Products).
- Shopping cart functionality.
- Order placement and tracking.

## Tech Stack

- **Backend**: .NET Core
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Identity, JwtBearer
- **Payment Intergration**: Stripe
- **Logging**: Serilog
- **others**
    - FluentValidation
    - AutoMapper

## Setup Instructions

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server
- Any IDE or text editor (Visual Studio, Visual Studio Code, Rider).

### Steps to Run the Project

1. Clone this repository:
   ```bash
   git clone https://github.com/Salar977/AmazonECommerce.git
2. Navigate to project folder
   ```bash
    cd AmazonECommerce/src/AmazonECommerce.Api
3. #### Make sure to add your connectionString in appsettings.json
4. ApplyMigrations to database
   ```bash
   dotnet ef database update
5. #### Run the Application

## Usage

#### its still in early stage and has limited usage

## Contributing
your welcome to send suggestions or fix issues related to the project. Feel free to fork this repository and submit a pull request

