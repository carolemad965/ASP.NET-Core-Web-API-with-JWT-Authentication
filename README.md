# ASP.NET Core Web API with JWT Authentication E-Commerce
This project demonstrates building a secure ASP.NET Core Web API with JWT (JSON Web Token) authentication. It includes features such as user registration, login, and role-based authorization. The API utilizes a code-first approach, integrates with a MSSQL database using Entity Framework Core, and follows modern development practices for maintainability and scalability.

# Features
1- JWT Authentication: Implements token-based authentication using JWT to secure user access to the API endpoints. 

2- User Registration and Login: Provides endpoints for user registration and login, with password hashing for security.

3- Role-Based Authorization: Restricts access to certain actions based on user roles. Only admin users have authorization to perform administrative actions.

4- Repository Pattern: Utilizes the repository pattern to separate data access logic, improving code organization and testability.

5- Dependency Injection: Leverages ASP.NET Core's built-in dependency injection for loosely coupled components and better code maintainability.

6- Identity Framework Integration: Integrates with Identity Framework for user authentication and authorization, enabling role-based access control.

# Technologies Used:
1-ASP.NET Core.

2-Entity Framework Core.

3-MSSQL.

4-JWT Token Authentication.

5-Dependency Injection.

6-Repository Pattern.

7-LINQ.

8-Identity Framework.

# How It Works
1- User Registration: Users can register with the API by providing their username, email, and password. Upon successful registration, the user is added to the database, and a JWT token is generated for authentication.

2- User Login: Registered users can log in using their credentials. The API verifies the user's credentials, and if valid, issues a JWT token for subsequent requests.

3- Role-Based Authorization: Certain actions in the API are restricted to admin users. To grant admin privileges to a user, you can uncomment the line await userManager.AddToRoleAsync(user, "Admin"); in the Register action of the AccountController. This will assign the "Admin" role to the user upon registration.

4- Accessing Protected Endpoints: Users can access protected endpoints by including the JWT token in the Authorization header of the HTTP request. The token is validated by the API to ensure the user has the necessary permissions.

# Getting Started
1-Clone the Repository: Clone this repository to your local machine using git clone.

2-Set Up the Database: Configure a MSSQL database and update the connection string in the appsettings.json file.

3-Run Migrations: Run the Entity Framework Core migrations to create the database schema.

4-Build and Run the Project: Build and run the ASP.NET Core Web API project using Visual Studio or the .NET CLI.

5-Test the Endpoints: Use tools like Postman or curl to test the API endpoints for user registration, login, and other actions.

# Important Note
To grant admin privileges to a user for the first time, uncomment the line await userManager.AddToRoleAsync(user, "Admin"); in the Register action of the AccountController. This will make the user an admin upon registration.
# Contributing
Contributions are welcome! If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

