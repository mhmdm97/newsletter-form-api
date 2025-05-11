# Newsletter Form API

A RESTful API for managing newsletter subscribers, their interests, and communication preferences.

## Overview

This API provides endpoints for managing a newsletter subscription system. It allows users to:

- Create, read, update, and delete subscribers 
- Manage interests categories (Houses, Apartments, etc.)
- Configure communication preferences (Email, SMS)

## Tech Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: PostgreSQL with Entity Framework Core 8.0
- **Documentation**: Swagger/OpenAPI
- **Observability**: OpenTelemetry

## Project Structure

```
├── Controllers/
│   ├── CommunicationPreferenceController.cs
│   ├── InterestController.cs
│   └── SubscriberController.cs
├── Dal/
│   ├── NewsletterDbContext.cs
│   ├── Entities/
│   ├── Enums/
│   └── Repositories/
│       ├── Implementations/
│       └── Interfaces/
├── Helpers/
│   └── EntityMapper.cs
├── Migrations/
├── Models/
│   └── Dtos/
├── Services/
│   ├── Implementations/
│   └── Interfaces/
└── Program.cs
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- PostgreSQL

### Configuration

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=newsletter;Username=your_username;Password=your_password"
  }
}
```

### Running the Application

1. Clone the repository
2. Apply migrations to create the database:
   ```
   dotnet ef database update
   ```
3. Run the application:
   ```
   dotnet run
   ```
4. Access the Swagger UI at `https://localhost:5001/swagger`

## API Endpoints

The API provides the following main endpoints:

- **Subscribers**: CRUD operations for newsletter subscribers
- **Interests**: Manage categories/topics subscribers can select (Houses, Apartments, Shared ownership, Rental, Land sourcing)
- **Communication Preferences**: Manage how subscribers want to be contacted (Email, SMS)

## Data Models

### Subscriber
- Id, Name, Email, PhoneNumber
- Type (enum)
- List of Interests
- List of Communication Preferences

### Interest
- Id, Name

### Communication Preference
- Id, Tag

## Features

- **Entity Mapping**: Conversion between domain entities and DTOs
- **Input Validation**: Request models with validation attributes
- **Result Pattern**: Clean error handling with explicit success/failure returns
- **Error Handling**: Structured API responses
- **Telemetry**: OpenTelemetry integration

## License

This project is licensed under the MIT License.