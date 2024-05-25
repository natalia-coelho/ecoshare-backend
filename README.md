# EcoShare Back-end
## Description
Welcome to EcoShare! 🌻

This project is a marketplace platform designed to showcase products from sustainable suppliers. The frontend is built using Angular, while the backend utilizes a .NET API with SQL Server as the database. [Check frontend repo here.](https://github.com/natalia-coelho/ecoshare-frontend)
## Setup Instructions

To run the backend locally, follow these steps:

1. **Clone the Repository:**`
2. **Navigate to the Project Directory:**

``` bash
cd sustainable-marketplace-backend
```
    
3. **Install Dependencies:**
```bash
dotnet restore
```

4. **Set Up Database:**
    
    - Ensure that SQL Server is installed and running.
    - Update the connection string in `appsettings.json` to point to your SQL Server instance.
    - Run Entity Framework migrations to create or update the database schema:
        `dotnet ef database update`
5. **Run the Application:**
``` bash
dotnet run
```
6. **Access the API:**  
    The API will be available at the specified base URL (e.g., `http://localhost:5028/` by default).

## Additional Information

- This backend API provides endpoints for CRUD operations on products, suppliers, and other relevant entities. Ensure that the frontend application is configured to communicate with the correct API endpoints.
- Modify any necessary settings in `appsettings.json` to match your local development environment, such as database connection strings or port numbers.
