# Bookstore Project
#### Video Demo: https://youtu.be/_qLQ_XCSY3M
The Bookstore Project is a full-stack web application developed using ASP.NET Core. It allows users to browse, add, and manage books. Key features include:

- **User Authentication**: Built with ASP.NET Identity, enabling user login, registration, and session management.
- **Category Management**: CRUD functionality for book categories.
- **Product Management**: Manage the book inventory, including CRUD operations.
- **Cart and Payment**: Users can add books to their cart and complete purchases using an integrated payment gateway.
- **Order Management**: Includes features to view and track orders. Admins can manage order statuses as 'Pending' or 'Delivered.'
- **Technologies Used**:
  - ASP.NET Core MVC
  - Entity Framework Core (Code-First)
  - SQL Server for database
  - Razor for views
  - Bootstrap for UI styling
- **Files Overview**:
  - `Controllers`: Includes logic for Categories, Products, Cart, Orders, and User Authentication.
  - `Models`: Represents the database entities like Book, Category, Order, and User.
  - `Views`: Contains Razor templates for the UI, including forms and tables.
  - `wwwroot`: Static files like CSS, JS, and images.
  - `Migrations`: Tracks database schema changes.
  - `README.md`: Provides an overview of the project and its functionality.

#### Design Decisions:
- **Separate Modules**: The project is divided into separate controllers for scalability.
- **Order Status Management**: A new column was added to the Order table for status tracking.
- **Clean UI**: Bootstrap components were used for a user-friendly interface.
- **Efficiency**: Utilized client-side pagination for the product list to enhance performance.


