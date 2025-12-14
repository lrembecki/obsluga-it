# Forms Module Documentation

## Overview

The Forms module provides a flexible and extensible system for managing dynamic forms in the obsluga-it application. It consists of two main layers:

- **`lrembecki.core.forms`**: Core business logic and data models
- **`lrembecki.presentation.forms`**: API endpoint registration and service initialization

The module supports creating form definitions with custom fields and capturing form submissions with field values.

## Architecture

### Projects

#### lrembecki.core.forms
- **Purpose**: Contains the core domain logic, entities, DTOs, and services
- **Target**: .NET 10.0
- **Key Dependencies**: `lrembecki.core`
- **Visibility**: Internal to infrastructure and presentation layers

#### lrembecki.presentation.forms
- **Purpose**: Exposes the Forms services through REST API endpoints
- **Target**: .NET 10.0
- **Key Dependencies**: `lrembecki.presentation`, `lrembecki.core.forms`
- **Framework**: ASP.NET Core with authorization

## Data Models

### Form Definition (`FormDefinition`)

Represents a template for a form with a defined structure of fields.

#### FormDefinitionVM (View Model)
```csharp
{
  "id": "guid",
  "name": "string",
  "fields": [
    {
      "fieldName": "string",
      "fieldType": "string",
      "isRequired": "boolean"
    }
  ]
}
```

#### FormDefinitionDto (Data Transfer Object)
```csharp
{
  "name": "string",
  "fields": [
    {
      "fieldName": "string",
      "fieldType": "string",
      "isRequired": "boolean"
    }
  ]
}
```

**Fields:**
- `id` (Guid): Unique identifier for the form definition
- `name` (string): Display name of the form definition
- `fields` (List): Array of field definitions

#### FormFieldDefinitionVM
```csharp
{
  "fieldName": "string",
  "fieldType": "string",
  "isRequired": "boolean"
}
```

**Fields:**
- `fieldName` (string): Name/label of the field
- `fieldType` (string): Data type of the field (e.g., "text", "email", "number", "date")
- `isRequired` (boolean): Whether the field is mandatory

### Form (`Form`)

Represents a completed or in-progress form submission based on a form definition.

#### FormVM (View Model)
```csharp
{
  "id": "guid",
  "formDefinitionId": "guid",
  "fields": {
    "fieldDefinitionId": "string value",
    "fieldDefinitionId": "string value"
  }
}
```

#### FormDto (Data Transfer Object)
```csharp
{
  "formDefinitionId": "guid",
  "fields": {
    "fieldDefinitionId": "string value",
    "fieldDefinitionId": "string value"
  }
}
```

**Fields:**
- `id` (Guid): Unique identifier for the form submission
- `formDefinitionId` (Guid): Reference to the form definition
- `fields` (Dictionary<Guid, string>): Mapping of field definition IDs to their submitted values

## API Endpoints

All endpoints require authentication with the `InternalJwtPolicy` authorization policy.

**Base URL**: `/api/forms`

### Response Format

All API endpoints return responses wrapped in a `ServiceCallResult` object:

**Success Response** (GET, POST, PUT):
```json
{
  "success": true,
  "errorMessage": null,
  "data": {}
}
```

**Success Response** (DELETE):
```json
{
  "success": true,
  "errorMessage": null
}
```

**Error Response**:
```json
{
  "success": false,
  "errorMessage": "Error description"
}
```

### Form Definitions Management

#### List All Form Definitions
```
GET /api/forms/form-definitions
```

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null,
  "data": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "name": "Contact Form",
      "fields": [
        {
          "fieldName": "Full Name",
          "fieldType": "text",
          "isRequired": true
        },
        {
          "fieldName": "Email",
          "fieldType": "email",
          "isRequired": true
        },
        {
          "fieldName": "Message",
          "fieldType": "text",
          "isRequired": false
        }
      ]
    }
  ]
}
```

#### Get Form Definition by ID
```
GET /api/forms/form-definitions/{id}
```

**Parameters:**
- `id` (Guid, path): Form definition ID

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null,
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Contact Form",
    "fields": [
      {
        "fieldName": "Full Name",
        "fieldType": "text",
        "isRequired": true
      },
      {
        "fieldName": "Email",
        "fieldType": "email",
        "isRequired": true
      },
      {
        "fieldName": "Message",
        "fieldType": "text",
        "isRequired": false
      }
    ]
  }
}
```

**Error Response** (Not Found): `404 Not Found`
```json
{
  "success": false,
  "errorMessage": "Form definition not found"
}
```

#### Create Form Definition
```
POST /api/forms/form-definitions
```

**Request Body**:
```json
{
  "name": "Contact Form",
  "fields": [
    {
      "fieldName": "Full Name",
      "fieldType": "text",
      "isRequired": true
    },
    {
      "fieldName": "Email",
      "fieldType": "email",
      "isRequired": true
    },
    {
      "fieldName": "Message",
      "fieldType": "text",
      "isRequired": false
    }
  ]
}
```

**Response**: `200 OK` (wrapped in ServiceCallResult)
```json
{
  "success": true,
  "errorMessage": null,
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Contact Form",
    "fields": [
      {
        "fieldName": "Full Name",
        "fieldType": "text",
        "isRequired": true
      },
      {
        "fieldName": "Email",
        "fieldType": "email",
        "isRequired": true
      },
      {
        "fieldName": "Message",
        "fieldType": "text",
        "isRequired": false
      }
    ]
  }
}
```

#### Update Form Definition
```
PUT /api/forms/form-definitions/{id}
```

**Parameters:**
- `id` (Guid, path): Form definition ID

**Request Body**:
```json
{
  "name": "Contact Form - Updated",
  "fields": [
    {
      "fieldName": "Full Name",
      "fieldType": "text",
      "isRequired": true
    },
    {
      "fieldName": "Email",
      "fieldType": "email",
      "isRequired": true
    }
  ]
}
```

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null,
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Contact Form - Updated",
    "fields": [
      {
        "fieldName": "Full Name",
        "fieldType": "text",
        "isRequired": true
      },
      {
        "fieldName": "Email",
        "fieldType": "email",
        "isRequired": true
      }
    ]
  }
}
```

#### Delete Form Definition
```
DELETE /api/forms/form-definitions/{id}
```

**Parameters:**
- `id` (Guid, path): Form definition ID

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null
}
```

**Error Response** (Not Found): `404 Not Found`
```json
{
  "success": false,
  "errorMessage": "Form definition not found"
}
```

### Forms Management

#### List All Forms
```
GET /api/forms/forms
```

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null,
  "data": [
    {
      "id": "660e8400-e29b-41d4-a716-446655440001",
      "formDefinitionId": "550e8400-e29b-41d4-a716-446655440000",
      "fields": {
        "660e8400-e29b-41d4-a716-446655440002": "John Doe",
        "660e8400-e29b-41d4-a716-446655440003": "john@example.com",
        "660e8400-e29b-41d4-a716-446655440004": "Hello, I have a question..."
      }
    }
  ]
}
```

#### Get Form by ID
```
GET /api/forms/forms/{id}
```

**Parameters:**
- `id` (Guid, path): Form ID

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null,
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "formDefinitionId": "550e8400-e29b-41d4-a716-446655440000",
    "fields": {
      "660e8400-e29b-41d4-a716-446655440002": "John Doe",
      "660e8400-e29b-41d4-a716-446655440003": "john@example.com"
    }
  }
}
```

**Error Response** (Not Found): `404 Not Found`
```json
{
  "success": false,
  "errorMessage": "Form not found"
}
```

#### Create Form
```
POST /api/forms/forms
```

**Request Body**:
```json
{
  "formDefinitionId": "550e8400-e29b-41d4-a716-446655440000",
  "fields": {
    "660e8400-e29b-41d4-a716-446655440002": "John Doe",
    "660e8400-e29b-41d4-a716-446655440003": "john@example.com",
    "660e8400-e29b-41d4-a716-446655440004": "Hello, I have a question..."
  }
}
```

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null,
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "formDefinitionId": "550e8400-e29b-41d4-a716-446655440000",
    "fields": {
      "660e8400-e29b-41d4-a716-446655440002": "John Doe",
      "660e8400-e29b-41d4-a716-446655440003": "john@example.com",
      "660e8400-e29b-41d4-a716-446655440004": "Hello, I have a question..."
    }
  }
}
```

#### Update Form
```
PUT /api/forms/forms/{id}
```

**Parameters:**
- `id` (Guid, path): Form ID

**Request Body**:
```json
{
  "formDefinitionId": "550e8400-e29b-41d4-a716-446655440000",
  "fields": {
    "660e8400-e29b-41d4-a716-446655440002": "Jane Doe",
    "660e8400-e29b-41d4-a716-446655440003": "jane@example.com"
  }
}
```

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null,
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "formDefinitionId": "550e8400-e29b-41d4-a716-446655440000",
    "fields": {
      "660e8400-e29b-41d4-a716-446655440002": "Jane Doe",
      "660e8400-e29b-41d4-a716-446655440003": "jane@example.com"
    }
  }
}
```

#### Delete Form
```
DELETE /api/forms/forms/{id}
```

**Parameters:**
- `id` (Guid, path): Form ID

**Response**: `200 OK`
```json
{
  "success": true,
  "errorMessage": null
}
```

**Error Response** (Not Found): `404 Not Found`
```json
{
  "success": false,
  "errorMessage": "Form not found"
}
```

## Error Handling

All endpoints follow the global exception handling strategy. The error response format is consistent across all endpoints:

```json
{
  "success": false,
  "errorMessage": "Error description"
}
```

### HTTP Status Codes

| Status Code | Condition | Exception Type |
|-------------|-----------|-----------------|
| 200 OK | Request successful | N/A |
| 404 Not Found | Resource not found | `ArgumentNullException` |
| 500 Internal Server Error | Server error | Other exceptions |
| 401 Unauthorized | Authentication required | Handled by authorization middleware |
| 403 Forbidden | Insufficient permissions | Handled by authorization middleware |

### Error Scenarios

**Resource Not Found** ? `404 Not Found`
```json
{
  "success": false,
  "errorMessage": "Object reference not set to an instance of an object."
}
```

**Internal Server Error** ? `500 Internal Server Error`
```json
{
  "success": false,
  "errorMessage": "An error occurred while processing your request."
}
```

## Service Layer

### IFormService
Provides CRUD operations for forms.

**Interface Definition** (from `ICrudService<FormDto, FormVM>`):
```csharp
public interface ICrudService<Dto, VM>
{
    Task<VM> CreateAsync(Dto model, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<List<VM>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<VM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<VM> UpdateAsync(Guid id, Dto model, CancellationToken cancellationToken = default);
}
```

**Endpoint Methods** (auto-generated by `MapCrud`):
- `GET /api/forms/forms` - Returns `ServiceCallResult<List<FormVM>>`
- `GET /api/forms/forms/{id}` - Returns `ServiceCallResult<FormVM>` (via `GetByIdAsync`)
- `POST /api/forms/forms` - Returns `ServiceCallResult<FormVM>`
- `PUT /api/forms/forms/{id}` - Returns `ServiceCallResult<FormVM>`
- `DELETE /api/forms/forms/{id}` - Returns `ServiceCallResult`

**Implementation Notes**:
- Automatically creates form field entities from the submitted field dictionary
- Integrates with the Unit of Work pattern for transaction management
- Validates form definition reference exists
- All responses are wrapped in `ServiceCallResult<T>` by the `ToServiceCallResult()` extension

### IFormDefinitionService
Provides CRUD operations for form definitions.

**Interface Definition** (from `ICrudService<FormDefinitionDto, FormDefinitionVM>`):
```csharp
public interface ICrudService<Dto, VM>
{
    Task<VM> CreateAsync(Dto model, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<List<VM>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<VM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<VM> UpdateAsync(Guid id, Dto model, CancellationToken cancellationToken = default);
}
```

**Endpoint Methods** (auto-generated by `MapCrud`):
- `GET /api/forms/form-definitions` - Returns `ServiceCallResult<List<FormDefinitionVM>>`
- `GET /api/forms/form-definitions/{id}` - Returns `ServiceCallResult<FormDefinitionVM>` (via `GetByIdAsync`)
- `POST /api/forms/form-definitions` - Returns `ServiceCallResult<FormDefinitionVM>`
- `PUT /api/forms/form-definitions/{id}` - Returns `ServiceCallResult<FormDefinitionVM>`
- `DELETE /api/forms/form-definitions/{id}` - Returns `ServiceCallResult`

**Implementation Notes**:
- Manages both form definitions and their associated field definitions
- Updates cascade to recreate field definitions when a definition is updated
- Automatically cleans up old field definitions during updates
- All responses are wrapped in `ServiceCallResult<T>` by the `ToServiceCallResult()` extension

## Integration

### Dependency Injection

Register the Forms services in the host application using the extension method from `RegisterServices.cs`:

```csharp
// In Program.cs or host configuration
builder.AddForms();
```

This registers:
- `IFormService` ? `FormService` (Scoped)
- `IFormDefinitionService` ? `FormDefinitionService` (Scoped)

### Endpoint Mapping

Map the Forms API endpoints using the extension method from `RegisterServices.cs`:

```csharp
// In Program.cs after app is built
app.MapForms();
```

This creates endpoint groups:
- `/api/forms/forms` - Form CRUD endpoints (GET, POST, PUT, DELETE)
- `/api/forms/form-definitions` - Form Definition CRUD endpoints (GET, POST, PUT, DELETE)

Both endpoint groups:
- Require the `InternalJwtPolicy` authorization policy
- Are tagged with "Forms" in OpenAPI documentation
- Use automatic response wrapping via `ToServiceCallResult()`
- Integrate with the global exception handler for transaction management

### Endpoint Generation

The `MapCrud<TService, TDto, TVM>` extension from `CrudEndpointExtensions.cs` automatically generates four endpoints:

```csharp
// GET /{endpoint}
MapGet($"/{endpoint}", async (
    [FromServices] Tservice service,
    CancellationToken ct
) => (await service.GetAllAsync(ct)).ToServiceCallResult())

// POST /{endpoint}
MapPost($"/{endpoint}", async (
    [FromBody] Tdto model,
    [FromServices] Tservice service,
    CancellationToken ct
) => (await service.CreateAsync(model, ct)).ToServiceCallResult())

// PUT /{endpoint}/{id:guid}
MapPut($"/{endpoint}/{id:guid}", async (
    [FromRoute] Guid id,
    [FromBody] Tdto model,
    [FromServices] Tservice service,
    CancellationToken ct
) => (await service.UpdateAsync(id, model, ct)).ToServiceCallResult())

// DELETE /{endpoint}/{id:guid}
MapDelete($"/{endpoint}/{id:guid}", (
    [FromRoute] Guid id,
    [FromServices] Tservice service,
    CancellationToken ct
) => service.DeleteAsync(id, ct).ToServiceCallResult())
```

Each endpoint is tagged with its HTTP method name ("Get", "Post", "Put", "Delete").

## Database Schema

The module uses the following entities:

### FormEntity
- `Id` (Guid, Primary Key)
- `FormDefinitionId` (Guid, Foreign Key)
- `SubscriptionId` (Guid, inherited from SubscriptionBaseEntity)
- `CreatedAt` (DateTime, inherited)
- `UpdatedAt` (DateTime, inherited)
- `Fields` (List<FormFieldEntity>, navigation property)

### FormFieldEntity
- `FormId` (Guid, composite key)
- `FormDefinitionFieldId` (Guid, composite key)
- `Value` (string)

### FormDefinitionEntity
- `Id` (Guid, Primary Key)
- `Name` (string)
- `SubscriptionId` (Guid, inherited from SubscriptionBaseEntity)
- `CreatedAt` (DateTime, inherited)
- `UpdatedAt` (DateTime, inherited)
- `Fields` (List<FormFieldDefinitionEntity>, navigation property)

### FormFieldDefinitionEntity
- `FormDefinitionId` (Guid, composite key)
- `FieldName` (string)
- `FieldType` (string)
- `IsRequired` (boolean)

## Field Types

The `fieldType` property supports standard data types:

| Type | Description | Example |
|------|-------------|---------|
| `text` | Plain text input | "John Doe" |
| `email` | Email address | "user@example.com" |
| `number` | Numeric value | "42" |
| `date` | Date value | "2024-01-15" |
| `textarea` | Multi-line text | "Long description..." |
| `select` | Single selection | "option1" |
| `checkbox` | Boolean value | "true" |
| `radio` | Single choice from options | "option1" |

## Subscription Integration

All form entities are subscription-scoped (inherit from `SubscriptionBaseEntity`), meaning:
- Forms and form definitions are isolated per subscription
- Each entity tracks creation and modification timestamps
- Subscription context is automatically applied through the Unit of Work pattern

## Authorization

All Forms API endpoints require authentication with the `InternalJwtPolicy`. This policy should be configured in the host application's authentication setup.

## Example Usage

### Creating a Form Definition

```http
POST /api/forms/form-definitions
Content-Type: application/json

{
  "name": "Customer Feedback",
  "fields": [
    {
      "fieldName": "Name",
      "fieldType": "text",
      "isRequired": true
    },
    {
      "fieldName": "Email",
      "fieldType": "email",
      "isRequired": true
    },
    {
      "fieldName": "Rating",
      "fieldType": "number",
      "isRequired": true
    },
    {
      "fieldName": "Comments",
      "fieldType": "textarea",
      "isRequired": false
    }
  ]
}
```

### Submitting a Form

```http
POST /api/forms/forms
Content-Type: application/json

{
  "formDefinitionId": "550e8400-e29b-41d4-a716-446655440000",
  "fields": {
    "660e8400-e29b-41d4-a716-446655440002": "Alice Smith",
    "660e8400-e29b-41d4-a716-446655440003": "alice@example.com",
    "660e8400-e29b-41d4-a716-446655440004": "5",
    "660e8400-e29b-41d4-a716-446655440005": "Great product, very satisfied!"
  }
}