### Ticketron API Documentation

---

#### **API Version**: 1.0  
#### **Title**: Ticketron

---

## **Endpoints**

### **Blob Management**

---

#### **Upload Blob**

**Endpoint**: `POST /api/Blob/upload`  
**Description**: Upload an image blob.

**Request Body**:
- **Content-Type**: `multipart/form-data`
  - **image** (binary): The image file to upload.

**Responses**:
- **200 OK**: Upload successful.

---

#### **Delete Blob**

**Endpoint**: `DELETE /api/Blob/delete`  
**Description**: Delete a blob by URL.

**Query Parameters**:
- **imageUrl** (string): URL of the image to delete.

**Responses**:
- **200 OK**: Deletion successful.
- **400 Bad Request**: Invalid request.
- **500 Internal Server Error**: Server error.

---

### **Booking Management**

---

#### **Get Booking by ID**

**Endpoint**: `GET /api/Booking/{bookingId}`  
**Description**: Retrieve booking details.

**Path Parameters**:
- **bookingId** (UUID, required): ID of the booking.

**Responses**:
- **200 OK**: Returns `BookingResponseDto`.
- **400 Bad Request**: Invalid request.
- **404 Not Found**: Booking not found.

---

#### **Delete Booking**

**Endpoint**: `DELETE /api/Booking/{bookingId}`  
**Description**: Delete a booking by ID.

**Path Parameters**:
- **bookingId** (UUID, required): ID of the booking.

**Responses**:
- **204 No Content**: Deletion successful.
- **400 Bad Request**: Invalid request.
- **404 Not Found**: Booking not found.

---

#### **Get Booking Summary**

**Endpoint**: `GET /api/Booking/summary/{bookingId}`  
**Description**: Retrieve a summary of booking details.

**Path Parameters**:
- **bookingId** (UUID, required): ID of the booking.

**Responses**:
- **200 OK**: Returns `BookingSummaryResponseDto`.
- **400 Bad Request**: Invalid request.
- **404 Not Found**: Booking not found.

---

#### **Get Bookings by User**

**Endpoint**: `GET /api/Booking/user/{userId}`  
**Description**: Retrieve all bookings for a specific user.

**Path Parameters**:
- **userId** (UUID, required): ID of the user.

**Responses**:
- **200 OK**: Returns an array of `BookingResponseDto`.
- **400 Bad Request**: Invalid request.

---

#### **Create Booking**

**Endpoint**: `POST /api/Booking/create`  
**Description**: Create a new booking.

**Request Body**:
- **BookingCreateDto**: 
  - **title** (string, required): Title of the booking.
  - **startDate** (date-time): Start date.
  - **endDate** (date-time): End date.
  - **userIds** (array, UUID, nullable): Associated user IDs.
  - **unregUserIds** (array, UUID, nullable): Associated unregistered user IDs.
  - **groupIds** (array, UUID, nullable): Associated group IDs.

**Responses**:
- **200 OK**: Returns `BookingResponseDto`.
- **400 Bad Request**: Invalid data.
- **500 Internal Server Error**: Server error.

---

#### **Update Booking**

**Endpoint**: `PUT /api/Booking/update`  
**Description**: Update an existing booking.

**Request Body**:
- **BookingUpdateDto**: 
  - **id** (UUID, required): Booking ID.
  - **title** (string, nullable): Title.
  - **startDate** (date-time, nullable): Start date.
  - **endDate** (date-time, nullable): End date.
  - **createdBy** (UUID, nullable): User ID of creator. Initially set to the current user creating the booking.

**Responses**:
- **204 No Content**: Update successful.
- **400 Bad Request**: Invalid data.
- **404 Not Found**: Booking not found.
- **500 Internal Server Error**: Server error.

---

### **Group Management**

---

#### **Get Group by ID**

**Endpoint**: `GET /api/Group/{groupId}`  
**Description**: Retrieve group details.

**Path Parameters**:
- **groupId** (UUID, required): ID of the group.

**Responses**:
- **200 OK**: Returns `GroupResponseDto`.
- **400 Bad Request**: Invalid request.
- **404 Not Found**: Group not found.

---

#### **Delete Group**

**Endpoint**: `DELETE /api/Group/{groupId}`  
**Description**: Delete a group by ID.

**Path Parameters**:
- **groupId** (UUID, required): ID of the group.

**Responses**:
- **204 No Content**: Deletion successful.
- **404 Not Found**: Group not found.
- **500 Internal Server Error**: Server error.

---

#### **Get Groups by User**

**Endpoint**: `GET /api/Group/user/{userId}`  
**Description**: Retrieve all groups associated with a user.

**Path Parameters**:
- **userId** (UUID, required): User ID.

**Responses**:
- **200 OK**: Returns an array of `Group`.
- **400 Bad Request**: Invalid request.

---

#### **Create Group**

**Endpoint**: `POST /api/Group/create`  
**Description**: Create a new group.

**Request Body**:
- **GroupCreateDto**: 
  - **name** (string, required): Name of the group.
  - **userIds** (array, UUID, nullable): Associated user IDs.
  - **unregUserIds** (array, UUID, nullable): Associated unregistered user IDs.

**Responses**:
- **201 Created**: Group created successfully.
- **400 Bad Request**: Invalid data.
- **500 Internal Server Error**: Server error.

---

#### **Update Group**

**Endpoint**: `PUT /api/Group/update`  
**Description**: Update group details.

**Request Body**:
- **GroupUpdateDto**: 
  - **id** (UUID, required): Group ID.
  - **name** (string, nullable): Group name.
  - **userIds** (array, UUID, nullable): User IDs.
  - **unregUserIds** (array, UUID, nullable): Unregistered user IDs.

**Responses**:
- **204 No Content**: Update successful.
- **400 Bad Request**: Invalid data.
- **404 Not Found**: Group not found.
- **500 Internal Server Error**: Server error.

---

### **Ticket Management**

---

#### **Get Ticket by ID**

**Endpoint**: `GET /api/Ticket/{ticketId}`  
**Description**: Retrieve ticket details.

**Path Parameters**:
- **ticketId** (UUID, required): Ticket ID.

**Responses**:
- **200 OK**: Returns `TicketResponseDto`.
- **400 Bad Request**: Invalid request.
- **404 Not Found**: Ticket not found.

---

#### **Delete Ticket**

**Endpoint**: `DELETE /api/Ticket/{ticketId}`  
**Description**: Delete a ticket by ID.

**Path Parameters**:
- **ticketId** (UUID, required): Ticket ID.

**Responses**:
- **204 No Content**: Deletion successful.
- **404 Not Found**: Ticket not found.
- **500 Internal Server Error**: Server error.

---

#### **Get Tickets by Booking**

**Endpoint**: `GET /api/Ticket/booking/{bookingId}`  
**Description**: Retrieve tickets associated with a booking.

**Path Parameters**:
- **bookingId** (UUID, required): Booking ID.

**Responses**:
- **200 OK**: Returns an array of `TicketResponseDto`.
- **400 Bad Request**: Invalid request.

---

#### **Create Ticket**

**Endpoint**: `POST /api/Ticket/create`  
**Description**: Create a new ticket.

**Request Body**:
- **TicketCreateDto**: 
  - **title** (string, required): Title.
  - **category** (string, required): Category.
  - **startDate** (date-time): Start date.
  - **endDate** (date-time): End date.
  - **price** (integer, nullable): Price.
  - **imageUrl** (string, nullable): Image URL.
  - **purchasedBy** (UUID, nullable): User ID of purchaser.
  - **assignedUserId** (UUID, nullable, required*): User ID of assigned user.
  - **assignedUnregUserId** (UUID, nullable, required*): Unregistered user ID.
  - **bookingId** (UUID, required): Associated booking ID.

**NOTE:**
- Either assignedUserId or assignedUnregUserId must be provided. But never both.

**Responses**:
- **201 Created**: Returns `TicketResponseDto`.
- **400 Bad Request**: Invalid data.
- **500 Internal Server Error**: Server error.

---

#### **Update Ticket**

**Endpoint**: `PUT /api/Ticket/update`  
**Description**: Update an existing ticket.

**Request Body**:
- **TicketUpdateDto**: 
  - **id** (UUID, required): Ticket ID.
  - **title** (string, nullable): Title.
  - **category** (string, nullable): Category.
  - **startDate** (date-time, nullable): Start date.
  - **endDate** (date-time, nullable): End date.
  - **purchaseDate** (date-time, nullable): Purchase date. 
  - **price** (integer, nullable): Price.
  - **imageUrl** (string, nullable): Image URL.
  - **purchasedBy** (UUID, nullable): User ID of purchaser.
  - **assignedUserId** (UUID, nullable): User ID of assigned user.
  - **assignedUnregUserId** (UUID, nullable): UnregUser ID for assigned unregistered user. 

**NOTE:**
- If assignedUserId or assignedUnregUserId is provided, only one of them should be present.

**Responses**:
- **204 No Content**: Update successful.
- **400 Bad Request**: Invalid data.
- **404 Not Found**: Ticket not found.
- **500 Internal Server Error**: Server error.

---

### **User Management**

---

#### **Get All Users**

**Endpoint**: `GET /api/User`  
**Description**: Retrieve a list of all users.

**Responses**:
- **200 OK**: Returns an array of `UserResponseDto`.
- **400 Bad Request**: Invalid request.

---

#### **Get User by ID**

**Endpoint**: `GET /api/User/{userId

}`  
**Description**: Retrieve user details.

**Path Parameters**:
- **userId** (UUID, required): User ID.

**Responses**:
- **200 OK**: Returns `UserResponseDto`.
- **400 Bad Request**: Invalid request.
- **404 Not Found**: User not found.

---

#### **Create User**

**Endpoint**: `POST /api/User/create`  
**Description**: Create a new user.

**Request Body**:
- **UserCreateDto**: 
  - **name** (string, required): Name.
  - **email** (string, required): Email.
  - **phone** (string, nullable): Phone.

**NOTE:**
- Only to be used in a POST call after a new user registers (to Entra ID).

**Responses**:
- **201 Created**: User created successfully.
- **400 Bad Request**: Invalid data.
- **409 Conflict**: User already exists.
- **500 Internal Server Error**: Server error.

---

#### **Update User**

**Endpoint**: `PUT /api/User/update`  
**Description**: Update user details. ID is retrieved from the token.

**Request Body**:
- **UserUpdateDto**: 
  - **name** (string, nullable): Name.
  - **phone** (string, nullable): Phone.

**Responses**:
- **204 No Content**: Update successful.
- **400 Bad Request**: Invalid data.
- **404 Not Found**: User not found.
- **500 Internal Server Error**: Server error.

---

### **Unregistered User Management**

---

#### **Get Unregistered User by ID**

**Endpoint**: `GET /api/UnregUser/{unregUserId}`  
**Description**: Retrieve details of an unregistered user.

**Path Parameters**:
- **unregUserId** (UUID, required): Unregistered user ID.

**Responses**:
- **200 OK**: Returns `UnregUserResponseDto`.
- **404 Not Found**: Unregistered user not found.

---

#### **Delete Unregistered User**

**Endpoint**: `DELETE /api/UnregUser/{unregUserId}`  
**Description**: Delete an unregistered user.

**Path Parameters**:
- **unregUserId** (UUID, required): Unregistered user ID.

**Responses**:
- **204 No Content**: Deletion successful.
- **404 Not Found**: Unregistered user not found.

---

#### **Get Unregistered Users by User**

**Endpoint**: `GET /api/UnregUser/user/{userId}`  
**Description**: Get unregistered users associated with a user.

**Path Parameters**:
- **userId** (UUID, required): User ID.

**Responses**:
- **200 OK**: Returns an array of `UnregUserResponseDto`.
- **404 Not Found**: No unregistered users found.

---

#### **Create Unregistered User**

**Endpoint**: `POST /api/UnregUser/create`  
**Description**: Create a new unregistered user. Creator ID is retrieved from the token.

**Request Body**:
- **UnregUserCreateDto**: 
  - **name** (string, required): Name.

**Responses**:
- **201 Created**: User created successfully.
- **500 Internal Server Error**: Server error.