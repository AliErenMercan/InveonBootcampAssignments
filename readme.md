# Library Management System

This is a library management system built using **ASP.Net Core MVC** and **SQL Server**. The project allows managing roles and permissions for users and provides role-based access control using **custom authorization attributes**.

## Technologies Used
- **ASP.Net Core MVC**: Web application framework for building dynamic web apps.
- **SQL Server**: Used as the database management system.
- **Identity**: For user management and role-based authentication.

## Features
- **User-Role Relationship**: Users are assigned roles, and the system manages the many-to-one relationship between users and roles.
- **Role Authorization using Permissions**: Instead of traditional role-based access, this project uses **permissions** for controlling access. Permissions are assigned to roles and users based on permissions, offering more fine-grained control.
- **Custom Authorization Attribute**: A custom authorization attribute is used to manage access control based on permissions instead of roles alone.
- **Role-Permission Relationship**: There is a **many-to-many relationship** between `AppRole` and `Permission` entities. This relationship is managed using the `RolePermission` entity to link roles with permissions.

## Database Structure

The database contains the following important tables:

1. **Users**: Users of the library system, managed by ASP.Net Identity.
2. **Roles**: Roles assigned to users (Admin, User, etc.).
3. **Permissions**: Specific permissions (e.g., view, edit, delete books).
4. **RolePermissions**: Linking table to establish the **many-to-many relationship** between roles and permissions.

![Database Schema](https://via.placeholder.com/500x300?text=Database+Schema)

## Key Relationships
- **User to Role**: One-to-many relationship (a user can have one role).
- **Role to Permissions**: Many-to-many relationship (roles can have multiple permissions).
- **RolePermission**: This entity is used to manage the relationship between `AppRole` and `Permission`.

## How It Works
1. **Role Creation and Permission Management**:
   - Roles are created using the `RoleManager` class.
   - Permissions are assigned to roles through a **Role-Permission** relationship, allowing fine-grained control.
   - A custom **`MinimumAgeAuthorize`** attribute is used for managing user permissions based on their roles and assigned permissions.
   
2. **User-Role Assignment**:
   - Users can be assigned to roles, and roles can be updated or removed.
   - Permissions are granted dynamically based on role assignments.

## Automatic Admin Account
Upon first-time setup, an **admin account** is created automatically in the system. 

### Admin Login Details:
- **Username**: `admin`
- **Password**: `AdminPassword123!`

You can use this account to manage roles and permissions.

## Running the Project

### Step 1: Set up the SQL Server Database
Make sure SQL Server is installed and accessible. Update the `appsettings.json` file with the correct connection string for your database.

### Step 2: Migrate the Database
Run the following commands in the terminal to create and update the database schema:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
