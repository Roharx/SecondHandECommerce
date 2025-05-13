# Second-Hand E-Commerce Backend

## Design & Implementation

This backend powers a scalable e-commerce platform for listing and purchasing second-hand items. The system is containerized using Docker and built using .NET 8 with MongoDB and AWS S3 for flexible data and media handling.

---

## 📦 Database Selection

We selected **MongoDB (NoSQL)** for all data due to:
- Flexible schema: Listings and reviews vary in structure.
- High write throughput: Ideal for user-generated content.
- Simplicity in deployment and horizontal scalability.

No relational database was used because:
- Relationships (e.g. between users and listings) are simple and do not require joins.
- The use case benefits more from document storage than relational normalization.

---

## 🧩 Data Schema and Storage Strategy

We defined several primary models:

### User
- `Id: Guid`
- `Name`, `Email`, `PasswordHash`
- `Role`: `Buyer`, `Seller`, or `Admin`
- `Rating`: nullable float
- Stored in **Users** collection

### Listing
- `Id: Guid`, `SellerId`, `Title`, `Description`, `Price`
- `ImageUrls: List<string>`
- `CreatedAt`
- Stored in **Listings** collection

### Order
- `Id: Guid`, `BuyerId`, `ListingId`, `Status`, etc.
- Transactionally created and updated
- Stored in **Orders** collection

### Review
- `Id`, `ReviewerId`, `TargetUserId`, `Rating`, `Comment`
- Stored in **Reviews** collection

All models are indexed by `Id`. MongoDB collections are used to segment data logically.

---

## ☁️ Integration of Cloud Storage

We use **Amazon S3** to store item images.

### Flow:
1. Image is uploaded via `UploadImage` endpoint.
2. Image is stored in an S3 bucket with a unique filename.
3. URL is returned and added to the `ImageUrls` field when creating a listing.

We use **pre-signed URLs** to securely allow read-only access to images from the frontend.

---

## ⚡ Caching Strategy

We use **MemoryCache** (in-memory caching) for:

- Frequently accessed listings (`GetAllListings`, `SearchListings`)
- Cache duration: 5 minutes
- Cache key: Deterministic hash based on parameters

### Invalidation:
- On listing creation or update, associated keys are evicted to keep the cache fresh.

---

## 🧱 CQRS Implementation

We separate queries and commands using **MediatR**.

- **Commands**: `CreateListingCommand`, `RegisterUserCommand`, `PlaceOrderCommand`, etc.
- **Queries**: `GetAllListingsQuery`, `SearchListingsQuery`, `GetUserByIdQuery`, etc.

This:
- Enhances readability and maintainability
- Aligns with SOLID principles and scaling needs
- Supports future migration to message-based/event-driven architecture

---

## 🔐 Transaction Management

MongoDB does not support multi-document ACID transactions in a distributed cluster unless using replica sets.

We use **explicit sessions** and **transactional blocks** in MongoDB for operations such as:
- Creating an order and updating the listing status
- Ensuring `Insert` and `Update` steps are wrapped in a single transaction

All transactional logic is encapsulated in repository methods using `StartSessionAsync` and `WithTransactionAsync`.

---

## Summary

This backend offers a modular, scalable, and maintainable system supporting:
- Secure user roles and JWT authentication
- Reliable and fast data access via caching
- Secure file handling through AWS S3
- Consistent, flexible data through MongoDB
- Clear separation of command/query responsibilities