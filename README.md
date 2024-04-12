# School Management Project Using Clean Architecture

## Introduction
This project is a comprehensive school management system built using Clean Architecture principles. It incorporates a wide range of concepts and tools to ensure scalability, maintainability, and efficiency in managing school-related tasks.

## Features
- **CQRS Pattern**: Separates Write and Query operations, enhancing scalability and maintenance.
- **Mediator Pattern**: Facilitates communication between classes without explicit references.
- **Fluent Validation**: Enables easy validation setup with custom error handling middleware.
- **Localization**: Supports Arabic and English languages, delivering responses based on the chosen environment.
- **Logging with Serilog**: Logs errors to the database for easy tracking and debugging.
- **Database Techniques**: Utilizes views and stored procedures and Views and Function for optimized database operations.
- **Pagination Schema**: Organizes large responses efficiently.
- **Confirmation Email System**: Sends confirmation emails with account activation links.
- **Reset Password System**: Encrypts and decrypts reset password codes sent via email.
- **Identity Management**: Includes user and role management functionalities.
- **Role and Claims Manipulation**: Allows modification of user roles and claims for access control.
- **JWT Token Authentication**: Implements token-based authentication with refresh tokens.
- **Routing Schema**: Uses organized routing classes for flexibility.
- **Readable Response Schema**: Provides detailed response structures including status codes, operation status, data, and metadata.
- **Dependency Injection**: Manages dependencies efficiently.
- **Solid Principles**: Adheres to SOLID principles for robust and maintainable code.

## Project Structure
The solution consists of five projects, each with its set of dependencies:
1. **Core**: Contains core business logic and entities.
2. **Infrastructure**: Handles data access and external services integration.
3. **Application**: Implements application services and use cases.
4. **API**: Provides API endpoints for interacting with the system.
5. **Services**: Includes application services.

