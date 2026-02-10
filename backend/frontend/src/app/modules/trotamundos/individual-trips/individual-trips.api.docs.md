Title: Individual Trip API Documentation (Frontend Specification)
=================================================================

Summary
-------
This document describes the backend API for **Individual Trips** feature, including data models, endpoints, and expected behavior for the Angular frontend.

Backend References
------------------
- Main DTO: `lrembecki.core.trotamundos.IndividualTrips.IndividualTripDto` (`src\trotamundos\lrembecki.core.trotamundos\IndividualTrips\IndividualTripDto.cs`)
- Nested DTOs: `IndividualTripItemDto`, `IndividualTripStepItemDto`
- Entities: `IndividualTripEntity`, `IndividualTripItemEntity`, `IndividualTripStepItemEntity` (`src\trotamundos\lrembecki.core.trotamundos\IndividualTrips\*`)

---

## Data Models

### TypeScript Interfaces

```typescript
export type Guid = string; // UUID string

/**
 * Storage/Image metadata returned by backend
 */
export interface StorageDto {
  id: Guid;
  url: string;
  fileName?: string;
  contentType?: string;
  size?: number;
  // Add other storage fields as needed
}

/**
 * Individual trip item (pricing option)
 */
export interface IndividualTripItemDto {
  order: number;
  title: string;
  description: string;
  price: number;          // decimal on backend
  uom: string;            // Unit of Measure (e.g., "per person", "per day")
  imageId?: Guid | null;
  image: StorageDto;
}

/**
 * Individual trip step (process/workflow step)
 */
export interface IndividualTripStepItemDto {
  order: number;
  title: string;
  description: string;
}

/**
 * Main Individual Trip DTO
 */
export interface IndividualTripDto {
  title: string;
  description: string;
  items: IndividualTripItemDto[];
  steps: IndividualTripStepItemDto[];
}

/**
 * Individual Trip View Model (with ID)
 */
export interface IndividualTripVM extends IndividualTripDto {
  id: Guid;
  name?: string | null;
  isActive: boolean;
  isDisabled: boolean;
  subscriptionId: Guid;
}
```

---

## API Endpoints

### Base URL
```
/api/trotamundos/individual-trips
```

### 1. Get All Individual Trips
**Endpoint:** `GET /api/trotamundos/individual-trips`

**Description:** Retrieve all individual trips for the current subscription.

**Response:** `200 OK`
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Custom Amazon Tour",
    "isActive": true,
    "isDisabled": false,
    "subscriptionId": "1234567-...",
    "title": "Personalized Amazon Adventure",
    "description": "Create your own custom Amazon experience",
    "items": [
      {
        "order": 1,
        "title": "Standard Package",
        "description": "Basic tour package with accommodation",
        "price": 1500.00,
        "uom": "per person",
        "imageId": "abc123...",
        "image": {
          "id": "abc123...",
          "url": "https://storage.example.com/images/standard-package.jpg",
          "fileName": "standard-package.jpg",
          "contentType": "image/jpeg",
          "size": 245678
        }
      },
      {
        "order": 2,
        "title": "Premium Package",
        "description": "Luxury tour with premium accommodation",
        "price": 2500.00,
        "uom": "per person",
        "imageId": "def456...",
        "image": {
          "id": "def456...",
          "url": "https://storage.example.com/images/premium-package.jpg",
          "fileName": "premium-package.jpg",
          "contentType": "image/jpeg",
          "size": 312456
        }
      }
    ],
    "steps": [
      {
        "order": 1,
        "title": "Choose Your Package",
        "description": "Select from our standard or premium packages"
      },
      {
        "order": 2,
        "title": "Customize Your Experience",
        "description": "Add optional activities and experiences"
      },
      {
        "order": 3,
        "title": "Book Your Trip",
        "description": "Complete booking and payment"
      }
    ]
  }
]
```

---

### 2. Get Individual Trip by ID
**Endpoint:** `GET /api/trotamundos/individual-trips/{id}`

**Description:** Retrieve a specific individual trip by ID.

**Parameters:**
- `id` (path, required): GUID of the individual trip

**Response:** `200 OK`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Custom Amazon Tour",
  "isActive": true,
  "isDisabled": false,
  "subscriptionId": "1234567-...",
  "title": "Personalized Amazon Adventure",
  "description": "Create your own custom Amazon experience",
  "items": [...],
  "steps": [...]
}
```

**Response:** `404 Not Found`
```json
{
  "error": "Individual trip not found",
  "traceId": "00-abc123..."
}
```

---

### 3. Create Individual Trip
**Endpoint:** `POST /api/trotamundos/individual-trips`

**Description:** Create a new individual trip.

**Request Body:**
```json
{
  "title": "Personalized Amazon Adventure",
  "description": "Create your own custom Amazon experience",
  "items": [
    {
      "order": 1,
      "title": "Standard Package",
      "description": "Basic tour package with accommodation",
      "price": 1500.00,
      "uom": "per person",
      "imageId": "abc123-..."
    }
  ],
  "steps": [
    {
      "order": 1,
      "title": "Choose Your Package",
      "description": "Select from our standard or premium packages"
    }
  ]
}
```

**Response:** `201 Created`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Personalized Amazon Adventure",
  "description": "Create your own custom Amazon experience",
  "items": [...],
  "steps": [...]
}
```

**Response:** `400 Bad Request`
```json
{
  "errors": {
    "Title": ["Title is required"],
    "Items[0].ImageId": ["Image is required for item"]
  },
  "traceId": "00-abc123..."
}
```

---

### 4. Update Individual Trip
**Endpoint:** `PUT /api/trotamundos/individual-trips/{id}`

**Description:** Update an existing individual trip.

**Parameters:**
- `id` (path, required): GUID of the individual trip

**Request Body:** Same as Create

**Response:** `200 OK`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Updated Title",
  "description": "Updated description",
  "items": [...],
  "steps": [...]
}
```

**Response:** `404 Not Found`
```json
{
  "error": "Individual trip not found"
}
```

---

### 5. Delete Individual Trip
**Endpoint:** `DELETE /api/trotamundos/individual-trips/{id}`

**Description:** Delete an individual trip.

**Parameters:**
- `id` (path, required): GUID of the individual trip

**Response:** `204 No Content`

**Response:** `404 Not Found`
```json
{
  "error": "Individual trip not found"
}
```

---

## Field Specifications and Validation

### IndividualTripDto
| Field | Type | Required | Max Length | Notes |
|-------|------|----------|------------|-------|
| `title` | string | ? | 250 | Main title of the individual trip |
| `description` | string | ? | 5000 | Detailed description |
| `items` | array | ? | - | Pricing/package options (can be empty) |
| `steps` | array | ? | - | Process steps (can be empty) |

### IndividualTripItemDto
| Field | Type | Required | Max Length | Notes |
|-------|------|----------|------------|-------|
| `order` | number | ? | - | Display order (1-based, unique within trip) |
| `title` | string | ? | 250 | Item title |
| `description` | string | ? | 5000 | Item description |
| `price` | number | ? | - | Decimal with 2 decimal places (18,2 precision) |
| `uom` | string | ? | 50 | Unit of measure (e.g., "per person", "per day") |
| `imageId` | Guid | ? | - | Required - must reference valid storage |
| `image` | StorageDto | ? | - | Populated by backend on read |

### IndividualTripStepItemDto
| Field | Type | Required | Max Length | Notes |
|-------|------|----------|------------|-------|
| `order` | number | ? | - | Display order (1-based, unique within trip) |
| `title` | string | ? | 250 | Step title |
| `description` | string | ? | 5000 | Step description |

---

## Frontend Implementation Notes

### 1. Ordering & Sorting
- Both `items` and `steps` have an `order` field
- Always sort by `order` before displaying: 
  ```typescript
  items.sort((a, b) => a.order - b.order)
  ```
- When creating/updating, ensure `order` values are sequential and unique

### 2. Price Handling
- `price` is a decimal on backend (precision 18,2)
- Display with 2 decimal places: 
  ```typescript
  price.toFixed(2)
  ```
- Use currency pipe in Angular: 
  ```html
  {{ item.price | currency:'USD':'symbol':'1.2-2' }}
  ```

### 3. Image Handling
- `imageId` is **required** for each item
- Upload image first, then use returned GUID in `imageId`
- Backend auto-includes `image` object in responses
- Display fallback if `image.url` is missing
  ```typescript
  item.image?.url || 'assets/placeholder.jpg'
  ```

### 4. Update Behavior
- **Items:** Completely replaced on update (cleared and re-added)
- **Steps:** Smart update based on `order`:
  - New orders ? created
  - Existing orders ? updated
  - Missing orders ? deleted
- When updating, include ALL steps you want to keep

### 5. Validation Rules
```typescript
// Example Angular form validation
individualTripForm = this.fb.group({
  title: ['', [Validators.required, Validators.maxLength(250)]],
  description: ['', [Validators.required, Validators.maxLength(5000)]],
  items: this.fb.array([]),
  steps: this.fb.array([])
});

createItemFormGroup(): FormGroup {
  return this.fb.group({
    order: [0, [Validators.required, Validators.min(1)]],
    title: ['', [Validators.required, Validators.maxLength(250)]],
    description: ['', [Validators.required, Validators.maxLength(5000)]],
    price: [0, [Validators.required, Validators.min(0)]],
    uom: ['', [Validators.required, Validators.maxLength(50)]],
    imageId: [null, Validators.required]
  });
}

createStepFormGroup(): FormGroup {
  return this.fb.group({
    order: [0, [Validators.required, Validators.min(1)]],
    title: ['', [Validators.required, Validators.maxLength(250)]],
    description: ['', [Validators.required, Validators.maxLength(5000)]]
  });
}
```

### 6. Example Service Implementation
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IndividualTripsService {
  private readonly apiUrl = '/api/trotamundos/individual-trips';

  constructor(private http: HttpClient) {}

  getAll(): Observable<IndividualTripVM[]> {
    return this.http.get<IndividualTripVM[]>(this.apiUrl);
  }

  getById(id: Guid): Observable<IndividualTripVM> {
    return this.http.get<IndividualTripVM>(`${this.apiUrl}/${id}`);
  }

  create(dto: IndividualTripDto): Observable<IndividualTripVM> {
    return this.http.post<IndividualTripVM>(this.apiUrl, dto);
  }

  update(id: Guid, dto: IndividualTripDto): Observable<IndividualTripVM> {
    return this.http.put<IndividualTripVM>(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: Guid): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
```

---

## Common Scenarios

### Scenario 1: Display Individual Trip with Sorted Items
```typescript
this.individualTripsService.getById(tripId).subscribe(trip => {
  // Sort items by order
  this.sortedItems = [...trip.items].sort((a, b) => a.order - b.order);
  
  // Sort steps by order
  this.sortedSteps = [...trip.steps].sort((a, b) => a.order - b.order);
  
  this.individualTrip = trip;
});
```

### Scenario 2: Add New Item with Image Upload
```typescript
// 1. Upload image first
const imageId = await this.uploadImage(imageFile);

// 2. Add item with imageId
const newItem: IndividualTripItemDto = {
  order: this.getNextOrder(trip.items),
  title: 'New Package',
  description: 'Package description',
  price: 1000.00,
  uom: 'per person',
  imageId: imageId
};

// 3. Update trip
const updatedDto: IndividualTripDto = {
  ...trip,
  items: [...trip.items, newItem]
};

this.individualTripsService.update(trip.id, updatedDto).subscribe(...);
```

### Scenario 3: Reorder Steps
```typescript
// Update order values
const reorderedSteps = trip.steps.map((step, index) => ({
  ...step,
  order: index + 1
}));

const updatedDto: IndividualTripDto = {
  ...trip,
  steps: reorderedSteps
};

this.individualTripsService.update(trip.id, updatedDto).subscribe(...);
```

---

## Error Handling

### Common Error Responses

**Validation Error (400)**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Title": ["The Title field is required."],
    "Items[0].Price": ["The field Price must be between 0 and 999999999."]
  },
  "traceId": "00-abc123..."
}
```

**Not Found (404)**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "traceId": "00-abc123..."
}
```

**Server Error (500)**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  "title": "An error occurred while processing your request.",
  "status": 500,
  "traceId": "00-abc123..."
}
```

### Recommended Error Handling
```typescript
this.individualTripsService.create(dto).subscribe({
  next: (result) => {
    this.router.navigate(['/individual-trips', result.id]);
  },
  error: (error) => {
    if (error.status === 400) {
      // Handle validation errors
      this.displayValidationErrors(error.error.errors);
    } else if (error.status === 404) {
      this.notificationService.error('Trip not found');
    } else {
      this.notificationService.error('An error occurred. Please try again.');
    }
  }
});
```

---

## Additional Notes

1. **Authentication:** All endpoints require authentication. Include bearer token in Authorization header.

2. **Subscription Context:** Individual trips are scoped to the current subscription. Users can only see/modify trips in their subscription.

3. **Cascade Delete:** Deleting an individual trip will cascade delete all associated items and steps.

4. **Concurrent Updates:** Backend uses optimistic concurrency. Last write wins.

5. **Image Upload:** Use separate storage API endpoint to upload images before creating/updating items:
   ```
   POST /api/storage/upload
   ```

6. **Performance:** Items and steps are auto-included in responses. No separate calls needed.

---

## Contact

For questions or issues with this API, contact the backend team or refer to:
- Backend source: `src\trotamundos\lrembecki.core.trotamundos\IndividualTrips\`
- Entity configurations: `src\trotamundos\lrembecki.infrastructure.trotamundos\Entities\IndividualTrip*.cs`
