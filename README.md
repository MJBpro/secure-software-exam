
# Monorepo Setup Guide

This guide will help you get started with the monorepo, which contains both a Vue 3 JavaScript frontend and a .NET backend with Entity Framework (EF) migrations.

## Prerequisites

Ensure you have the following installed on your machine:

- Node.js (v14.x or later)
- npm (v6.x or later)
- .NET SDK (v8.x or later)
- A database server (e.g., SQL Server, PostgreSQL)

## Getting Started

### Clone the Repository

```bash
git clone <https://github.com/MJBpro/secure-software-exam.git>
cd <secure-software-exam>
```

### Setup the Frontend

1. Navigate to the frontend directory:

    ```bash
    cd frontend
    ```

2. Install dependencies:

    ```bash
    npm install
    ```

3. Run the development server:

    ```bash
    npm run serve
    ```

### Setup the Backend

1. Navigate to the backend directory:

    ```bash
    cd backend
    ```

2. Update the database connection string in `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Your_Database_Connection_String"
      }
    }
    ```

3. Apply the EF migrations to set up the database:

    ```bash
    dotnet ef database update
    ```

4. Run the backend server:

    ```bash
    dotnet run
    ```

## Project Structure

- `frontend/`: Contains the Vue 3 frontend application.
- `backend/`: Contains the .NET backend application with EF migrations.

## Additional Commands

### Frontend

- To build the frontend for production:

    ```bash
    npm run build
    ```

- To run frontend tests:

    ```bash
    npm run test
    ```

### Backend

- To add a new migration:

    ```bash
    dotnet ef migrations add <MigrationName>
    ```

- To update the database:

    ```bash
    dotnet ef database update
    ```