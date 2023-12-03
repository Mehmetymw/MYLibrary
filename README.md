# LibraryView - Book Management Application

## Overview
LibraryView is a comprehensive application designed for efficiently managing books in a library. It provides a user-friendly interface with features such as listing books, searching for specific titles, adding new books to the library, and facilitating book lending transactions.

## Features
- Lists books in the library
- Searches for a book in the library
- Adds new books to the library
- Lends a book from the library to someone

## Technologies Used
- **Service:** .Net Core Web API
- **Frontend:** .Net Core MVC, jQuery, HTML, CSS, Bootstrap
- **Database:** MSSQL
- **ORM:** Entity Framework Core

## Enhancements
- **Session Control:**
  - Implemented session control using API keys for enhanced service security.

- **Extended Classes:**
  - Added Borrower and Category classes to provide more detailed book management.

- **Logging:**
  - Replaced the Default Logger with the option to use a Custom Logger or the Serilog library for greater flexibility.

- **Paging:**
  - Implemented paging functionality while listing books for an improved user experience and optimized performance.

## Getting Started
1. Clone the repository.
2. Set up the database using MSSQL and Entity Framework Core.
3. Configure API keys for session control.
4. Customize logging preferences based on your needs.
5. Run the application and start efficiently managing your library books.

## Configuration
- **Database Setup:**
  - Configure MSSQL database connection in the app settings.
  - Run Entity Framework Core migrations to create the necessary tables.

- **API Key Setup:**
  - Generate and configure API keys for session control.

- **Logger Configuration:**
  - Choose between the Default Logger, a Custom Logger, or the Serilog library based on your logging preferences.

## Contributors
- [Your Name]

## License
This project is licensed under the [License Name] - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgments
- Special thanks to [Acknowledgment Name] for [specific contribution].
