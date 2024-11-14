## Ticketron API Documentation

### Overview
- **Title**: Ticketron
- **Version**: 1.0

### Endpoints

#### Booking

1. **Get Booking by ID**
   - **Endpoint**: `/api/Booking/{bookingId}`
   - **Method**: GET
   - **Description**: Retrieves a booking by its ID.
   - **Parameters**:
     - `bookingId` (required): ID of the booking (integer)
   - **Responses**:
     - **200**: Success (returns booking details)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (booking not found)

2. **Update Booking by ID**
   - **Endpoint**: `/api/Booking/{bookingId}`
   - **Method**: PUT
   - **Description**: Updates a booking by its ID.
   - **Parameters**:
     - `bookingId` (required): ID of the booking (integer)
   - **Request Body**: Updated booking details (JSON format)
   - **Responses**:
     - **204**: No Content (success)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (booking not found)
     - **500**: Server Error

3. **Delete Booking by ID**
   - **Endpoint**: `/api/Booking/{bookingId}`
   - **Method**: DELETE
   - **Description**: Deletes a booking by its ID.
   - **Parameters**:
     - `bookingId` (required): ID of the booking (integer)
   - **Responses**:
     - **204**: No Content (success)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (booking not found)

4. **Get Bookings by User ID**
   - **Endpoint**: `/api/Booking/user/{userId}`
   - **Method**: GET
   - **Description**: Retrieves all bookings for a specified user.
   - **Parameters**:
     - `userId` (required): ID of the user (integer)
   - **Responses**:
     - **200**: Success (returns an array of bookings)
     - **400**: Bad Request (invalid input)

5. **Create a New Booking**
   - **Endpoint**: `/api/Booking/create`
   - **Method**: POST
   - **Description**: Creates a new booking.
   - **Request Body**: Booking details (JSON format)
   - **Responses**:
     - **200**: Success (returns booking details)
     - **400**: Bad Request (invalid input)
     - **500**: Server Error

#### Group

1. **Get Group by ID**
   - **Endpoint**: `/api/Group/{groupId}`
   - **Method**: GET
   - **Description**: Retrieves a group by its ID.
   - **Parameters**:
     - `groupId` (required): ID of the group (integer)
   - **Responses**:
     - **200**: Success (returns group details)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (group not found)

2. **Update Group by ID**
   - **Endpoint**: `/api/Group/{groupId}`
   - **Method**: PUT
   - **Description**: Updates a group by its ID.
   - **Parameters**:
     - `groupId` (required): ID of the group (integer)
   - **Request Body**: Updated group details (JSON format)
   - **Responses**:
     - **204**: No Content (success)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (group not found)
     - **500**: Server Error

3. **Delete Group by ID**
   - **Endpoint**: `/api/Group/{groupId}`
   - **Method**: DELETE
   - **Description**: Deletes a group by its ID.
   - **Parameters**:
     - `groupId` (required): ID of the group (integer)
   - **Responses**:
     - **204**: No Content (success)
     - **404**: Not Found (group not found)
     - **500**: Server Error

4. **Get Groups by User ID**
   - **Endpoint**: `/api/Group/user/{userId}`
   - **Method**: GET
   - **Description**: Retrieves all groups for a specified user.
   - **Parameters**:
     - `userId` (required): ID of the user (integer)
   - **Responses**:
     - **200**: Success (returns an array of groups)
     - **400**: Bad Request (invalid input)

5. **Create a New Group**
   - **Endpoint**: `/api/Group/create`
   - **Method**: POST
   - **Description**: Creates a new group.
   - **Request Body**: Group details (JSON format)
   - **Responses**:
     - **201**: Created (success)
     - **400**: Bad Request (invalid input)
     - **500**: Server Error

#### Group Member

1. **Get Group Member by ID**
   - **Endpoint**: `/api/GroupMember/{groupMemberId}`
   - **Method**: GET
   - **Description**: Retrieves a group member by their ID.
   - **Parameters**:
     - `groupMemberId` (required): ID of the group member (integer)
   - **Responses**:
     - **200**: Success (returns group member details)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (group member not found)

2. **Delete Group Member by ID**
   - **Endpoint**: `/api/GroupMember/{groupMemberId}`
   - **Method**: DELETE
   - **Description**: Deletes a group member by their ID.
   - **Parameters**:
     - `groupMemberId` (required): ID of the group member (integer)
   - **Responses**:
     - **204**: No Content (success)
     - **404**: Not Found (group member not found)
     - **500**: Server Error

3. **Get Group Members by Group ID**
   - **Endpoint**: `/api/GroupMember/group/{groupId}`
   - **Method**: GET
   - **Description**: Retrieves all members of a specified group.
   - **Parameters**:
     - `groupId` (required): ID of the group (integer)
   - **Responses**:
     - **200**: Success (returns an array of group members)
     - **400**: Bad Request (invalid input)

4. **Add a Group Member to a Group**
   - **Endpoint**: `/api/GroupMember/group/{groupId}`
   - **Method**: POST
   - **Description**: Adds a new member to a group.
   - **Parameters**:
     - `groupId` (required): ID of the group (integer)
   - **Request Body**: Group member details (JSON format)
   - **Responses**:
     - **201**: Created (success)
     - **400**: Bad Request (invalid input)
     - **500**: Server Error

#### Participant

1. **Get Participant by ID**
   - **Endpoint**: `/api/Participant/{participantId}`
   - **Method**: GET
   - **Description**: Retrieves a participant by their ID.
   - **Parameters**:
     - `participantId` (required): ID of the participant (integer)
   - **Responses**:
     - **200**: Success (returns participant details)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (participant not found)

2. **Delete Participant by ID**
   - **Endpoint**: `/api/Participant/{participantId}`
   - **Method**: DELETE
   - **Description**: Deletes a participant by their ID.
   - **Parameters**:
     - `participantId` (required): ID of the participant (integer)
   - **Responses**:
     - **204**: No Content (success)
     - **404**: Not Found (participant not found)
     - **500**: Server Error

3. **Get Participants by Booking ID**
   - **Endpoint**: `/api/Participant/booking/{bookingId}`
   - **Method**: GET
   - **Description**: Retrieves all participants for a specified booking.
   - **Parameters**:
     - `bookingId` (required): ID of the booking (integer)
   - **Responses**:
     - **200**: Success (returns an array of participants)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (booking not found)

4. **Add a Participant to a Booking**
   - **Endpoint**: `/api/Participant/{bookingId}`
   - **Method**: POST
   - **Description**: Adds a new participant to a booking.
   - **Parameters**:
     - `bookingId` (required): ID of the booking (integer)
   - **Request Body**: Participant details (JSON format)
   - **Responses**:
     - **201**: Created (success)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (booking not found)
     - **500**: Server Error

#### Ticket

1. **Get Ticket by ID**
   - **Endpoint**: `/api/Ticket/{ticketId}`
  

 - **Method**: GET
   - **Description**: Retrieves a ticket by its ID.
   - **Parameters**:
     - `ticketId` (required): ID of the ticket (integer)
   - **Responses**:
     - **200**: Success (returns ticket details)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (ticket not found)

2. **Update Ticket by ID**
   - **Endpoint**: `/api/Ticket/{ticketId}`
   - **Method**: PUT
   - **Description**: Updates a ticket by its ID.
   - **Parameters**:
     - `ticketId` (required): ID of the ticket (integer)
   - **Request Body**: Updated ticket details (JSON format)
   - **Responses**:
     - **204**: No Content (success)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (ticket not found)
     - **500**: Server Error

3. **Delete Ticket by ID**
   - **Endpoint**: `/api/Ticket/{ticketId}`
   - **Method**: DELETE
   - **Description**: Deletes a ticket by its ID.
   - **Parameters**:
     - `ticketId` (required): ID of the ticket (integer)
   - **Responses**:
     - **204**: No Content (success)
     - **404**: Not Found (ticket not found)
     - **500**: Server Error

4. **Get Tickets by Booking ID**
   - **Endpoint**: `/api/Ticket/booking/{bookingId}`
   - **Method**: GET
   - **Description**: Retrieves all tickets for a specified booking.
   - **Parameters**:
     - `bookingId` (required): ID of the booking (integer)
   - **Responses**:
     - **200**: Success (returns an array of tickets)
     - **400**: Bad Request (invalid input)

5. **Create a New Ticket**
   - **Endpoint**: `/api/Ticket/create`
   - **Method**: POST
   - **Description**: Creates a new ticket.
   - **Request Body**: Ticket details (JSON format)
   - **Responses**:
     - **201**: Created (success)
     - **400**: Bad Request (invalid input)
     - **500**: Server Error

#### Unregistered User (UnregUser)

1. **Get Unregistered User by ID**
   - **Endpoint**: `/api/UnregUser/{unregUserId}`
   - **Method**: GET
   - **Description**: Retrieves an unregistered user by their ID.
   - **Parameters**:
     - `unregUserId` (required): ID of the unregistered user (integer)
   - **Responses**:
     - **200**: Success (returns user details)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (user not found)

2. **Delete Unregistered User by ID**
   - **Endpoint**: `/api/UnregUser/{unregUserId}`
   - **Method**: DELETE
   - **Description**: Deletes an unregistered user by their ID.
   - **Parameters**:
     - `unregUserId` (required): ID of the unregistered user (integer)
   - **Responses**:
     - **204**: No Content (success)
     - **404**: Not Found (user not found)
     - **500**: Server Error

3. **Get Unregistered Users by User ID**
   - **Endpoint**: `/api/UnregUser/user/{userId}`
   - **Method**: GET
   - **Description**: Retrieves all unregistered users associated with a specified user.
   - **Parameters**:
     - `userId` (required): ID of the user (integer)
   - **Responses**:
     - **200**: Success (returns an array of unregistered users)
     - **400**: Bad Request (invalid input)
     - **404**: Not Found (user not found)

4. **Create a New Unregistered User**
   - **Endpoint**: `/api/UnregUser/{userId}`
   - **Method**: POST
   - **Description**: Creates a new unregistered user associated with a specified user.
   - **Parameters**:
     - `userId` (required): ID of the user (integer)
   - **Request Body**: Unregistered user details (JSON format)
   - **Responses**:
     - **201**: Created (success)
     - **400**: Bad Request (invalid input)
     - **500**: Server Error

#### User

1. **Get All Users**
   - **Endpoint**: `/api/User`
   - **Method**: GET
   - **Description**: Retrieves a list of all users.
   - **Responses**:
     - **200**: Success (returns an array of users)

2. **Create a New User**
   - **Endpoint**: `/api/User`
   - **Method**: POST
   - **Description**: Creates a new user.
   - **Request Body**: User details (JSON format)
   - **Responses**:
     - **200**: Success (user created)

3. **Get User by ID**
   - **Endpoint**: `/api/User/{userId}`
   - **Method**: GET
   - **Description**: Retrieves a user by their ID.
   - **Parameters**:
     - `userId` (required): ID of the user (integer)
   - **Responses**:
     - **200**: Success (returns user details)

4. **Update User by ID**
   - **Endpoint**: `/api/User/{userId}`
   - **Method**: PUT
   - **Description**: Updates a user by their ID.
   - **Parameters**:
     - `userId` (required): ID of the user (integer)
   - **Request Body**: Updated user details (JSON format)
   - **Responses**:
     - **200**: Success (user updated)

5. **Delete User by ID**
   - **Endpoint**: `/api/User/{userId}`
   - **Method**: DELETE
   - **Description**: Deletes a user by their ID.
   - **Parameters**:
     - `userId` (required): ID of the user (integer)
   - **Responses**:
     - **200**: Success (user deleted)

### Components (Schemas)

#### Booking
- **Properties**:
  - `id` (integer)
  - `title` (string, nullable)
  - `startDate` (string, date-time)
  - `endDate` (string, date-time)
  - `userId` (integer)

#### Group
- **Properties**:
  - `id` (integer)
  - `name` (string, nullable)
  - `userId` (integer)

#### Group Member
- **Properties**:
  - `id` (integer)
  - `userId` (integer, nullable)
  - `unregUserId` (integer, nullable)
  - `isUser` (boolean)

#### Participant
- **Properties**:
  - `id` (integer)
  - `addedBy` (integer)
  - `isUser` (boolean)

#### Ticket
- **Properties**:
  - `id` (integer)
  - `title` (string, nullable)
  - `startDate` (string, date-time)
  - `endDate` (string, date-time)
  - `participantId` (integer)
  - `bookingId` (integer)

#### Unregistered User (UnregUser)
- **Properties**:
  - `id` (integer)
  - `name` (string, nullable)

#### User
- **Properties**:
  - `id` (integer)
  - `name` (string, nullable)
  - `email` (string, nullable)
  - `phone` (string, nullable)
