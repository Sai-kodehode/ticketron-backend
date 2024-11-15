### API Overview: Ticketron v2.0

**Base URL:** `/api`

---

### Endpoints

---

#### **Booking Endpoints**

---

**1. Get Booking by ID**  
`GET /Booking/{bookingId}`

- **Description:** Retrieve details of a specific booking.  
- **Path Parameters:**  
  - `bookingId` (UUID, required): The unique identifier of the booking.  

- **Responses:**  
  - **200 OK:** Returns a `BookingResponseDto` object.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **404 Not Found:** Returns a `ProblemDetails` object if the booking is not found.  

---

**2. Delete Booking by ID**  
`DELETE /Booking/{bookingId}`

- **Description:** Deletes a specific booking.  
- **Path Parameters:**  
  - `bookingId` (UUID, required): The unique identifier of the booking to delete.  

- **Responses:**  
  - **204 No Content:** The booking was successfully deleted.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **404 Not Found:** Returns a `ProblemDetails` object if the booking is not found.  

---

**3. Get Booking Summary by ID**  
`GET /Booking/summary/{bookingId}`

- **Description:** Retrieve summary details for a specific booking.  
- **Path Parameters:**  
  - `bookingId` (UUID, required): The unique identifier of the booking.  

- **Responses:**  
  - **200 OK:** Returns a `BookingSummaryResponseDto` object containing summary information.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **404 Not Found:** Returns a `ProblemDetails` object if the booking is not found.  

---

**4. Get User Bookings**  
`GET /Booking/user/{userId}`

- **Description:** Retrieve all bookings for a specific user.  
- **Path Parameters:**  
  - `userId` (UUID, required): The unique identifier of the user.  

- **Responses:**  
  - **200 OK:** Returns an array of `BookingResponseDto` objects.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  

---

**5. Create Booking**  
`POST /Booking/create`

- **Description:** Creates a new booking.  
- **Request Body:**  
  - **Content Types:**  
    - `application/json`  
    - `text/json`  
    - `application/*+json`  

  - **Body Schema:**  
    - `BookingCreateDto`:  
      - **title** (string, nullable): The title of the booking.  
      - **startDate** (date-time, required): The start date and time of the booking.  
      - **endDate** (date-time, required): The end date and time of the booking.  

- **Responses:**  
  - **200 OK:** Returns a `BookingResponseDto` object with the created booking's details.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **500 Internal Server Error:** A generic error occurred on the server.  

---

**6. Update Booking**  
`PUT /Booking/update`

- **Description:** Updates an existing booking.  
- **Request Body:**  
  - **Content Types:**  
    - `application/json`  
    - `text/json`  
    - `application/*+json`  

  - **Body Schema:**  
    - `BookingUpdateDto`:  
      - **id** (UUID, required): The unique identifier of the booking.  
      - **title** (string, nullable): The title of the booking.  
      - **startDate** (date-time, nullable): The updated start date and time of the booking.  
      - **endDate** (date-time, nullable): The updated end date and time of the booking.  

- **Responses:**  
  - **204 No Content:** The booking was successfully updated.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **404 Not Found:** Returns a `ProblemDetails` object if the booking is not found.  
  - **500 Internal Server Error:** A generic error occurred on the server.  

---

#### **Participant Endpoints**

---

**1. Get Participant by ID**  
`GET /Participant/{participantId}`

- **Description:** Retrieve participant details by their ID.  
- **Path Parameters:**  
  - `participantId` (UUID, required): The unique identifier of the participant.  

- **Responses:**  
  - **200 OK:** Returns a `ParticipantResponseDto` object.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **404 Not Found:** Returns a `ProblemDetails` object if the participant is not found.  

---

**2. Delete Participant by ID**  
`DELETE /Participant/{participantId}`

- **Description:** Deletes a participant by their ID.  
- **Path Parameters:**  
  - `participantId` (UUID, required): The unique identifier of the participant to delete.  

- **Responses:**  
  - **204 No Content:** The participant was successfully deleted.  
  - **404 Not Found:** Returns a `ProblemDetails` object if the participant is not found.  

---

**3. Get Participants by Booking ID**  
`GET /Participant/booking/{bookingId}`

- **Description:** Retrieve all participants associated with a specific booking.  
- **Path Parameters:**  
  - `bookingId` (UUID, required): The unique identifier of the booking.  

- **Responses:**  
  - **200 OK:** Returns an array of `ParticipantResponseDto` objects.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **404 Not Found:** Returns a `ProblemDetails` object if no participants are found for the given booking.  

---

**4. Create Participant**  
`POST /Participant/create`

- **Description:** Creates a new participant for a booking.  
- **Request Body:**  
  - **Content Types:**  
    - `application/json`  
    - `text/json`  
    - `application/*+json`  

  - **Body Schema:**  
    - `ParticipantCreateDto`:  
      - **bookingId** (UUID, required): The ID of the associated booking.  
      - **userId** (UUID, nullable): The ID of the associated user, if any.  
      - **unregUserId** (UUID, nullable): The ID of the associated unregistered user, if any.  

- **Responses:**  
  - **201 Created:** The participant was successfully created.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **500 Internal Server Error:** A generic error occurred on the server.  

---

#### **Ticket Endpoints**

---

**1. Get Ticket by ID**  
`GET /Ticket/{ticketId}`

- **Description:** Retrieve ticket details by its ID.  
- **Path Parameters:**  
  - `ticketId` (UUID, required): The unique identifier of the ticket.  

- **Responses:**  
  - **200 OK:** Returns a `TicketResponseDto` object.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **404 Not Found:** Returns a `ProblemDetails` object if the ticket is not found.  

---

**2. Delete Ticket by ID**  
`DELETE /Ticket/{ticketId}`

- **Description:** Deletes a ticket by its ID.  
- **Path Parameters:**  
  - `ticketId` (UUID, required): The unique identifier of the ticket to delete.  

- **Responses:**  
  - **204 No Content:** The ticket was successfully deleted.  
  - **404 Not Found:** Returns a `ProblemDetails` object if the ticket is not found.  

---

**3. Get Tickets by Booking ID**  
`GET /Ticket/booking/{bookingId}`

- **Description:** Retrieve all tickets associated with a specific booking.  
- **Path Parameters:**  
  - `bookingId` (UUID, required): The unique identifier of the booking.  

- **Responses:**  
  - **200 OK:** Returns an array of `TicketResponseDto` objects.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  

---

**4. Create Ticket**  
`POST /Ticket/create`

- **Description:** Creates a new ticket for a booking.  
- **Request Body:**  
  - **Content Types:**  
    - `application/json`  
    - `text/json`  
    - `application/*+json`  

  - **Body Schema:**  
    - `TicketCreateDto`:  
      - **title** (string, nullable): The title of the ticket.  
      - **startDate** (date-time, required): The start date and time of the ticket validity.  
      - **endDate** (date-time, required): The end date and time of the ticket validity.  
      - **price** (integer, nullable): The price of the ticket.  
      - **purchaseDate** (date-time, nullable): The date when the ticket was purchased.  
      - **category** (string, required): The category of the ticket.  
      - **imageUrl** (string, nullable): A URL to an image of the ticket.  
      - **

participantId** (UUID, nullable): The ID of the associated participant.  
      - **bookingId** (UUID, required): The ID of the associated booking.  

- **Responses:**  
  - **201 Created:** Returns a `TicketResponseDto` object with the created ticket's details.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  
  - **500 Internal Server Error:** A generic error occurred on the server.  

---

**5. Update Ticket**  
`PUT /Ticket/update`

- **Description:** Updates an existing ticket.  
- **Request Body:**  
  - **Content Types:**  
    - `application/json`  
    - `text/json`  
    - `application/*+json`  

  - **Body Schema:**  
    - `TicketUpdateDto`:  
      - **id** (UUID, required): The unique identifier of the ticket.  
      - **title** (string, nullable): The title of the ticket.  
      - **startDate** (date-time, nullable): The updated start date and time.  
      - **endDate** (date-time, nullable): The updated end date and time.  
      - **price** (integer, nullable): The updated price of the ticket.  
      - **purchaseDate** (date-time, nullable): The updated purchase date.  
      - **category** (string, nullable): The updated category of the ticket.  
      - **imageUrl** (string, nullable): The updated image URL for the ticket.  
      - **participantId** (UUID, nullable): The updated participant ID associated with the ticket.  

- **Responses:**  
  - **204 No Content:** The ticket was successfully updated.  
  - **400 Bad Request:** Returns a `ProblemDetails` object describing the error.  

---

### Models

---

#### **Booking Models**

---

**1. BookingCreateDto**  
Used when creating a new booking.  
- **Properties:**  
  - `title` (string, nullable): The title of the booking.  
  - `startDate` (date-time, required): Start date and time of the booking.  
  - `endDate` (date-time, required): End date and time of the booking.  

---

**2. BookingResponseDto**  
Represents the details of a booking.  
- **Properties:**  
  - `id` (UUID, required): The unique identifier of the booking.  
  - `title` (string, nullable): The title of the booking.  
  - `startDate` (date-time, required): The start date of the booking.  
  - `endDate` (date-time, required): The end date of the booking.  
  - `participantIds` (array of UUIDs, nullable): IDs of participants linked to the booking.  
  - `tickets` (array of `TicketResponseDto`, nullable): Tickets linked to the booking.  

---

**3. BookingSummaryResponseDto**  
A summary of a booking.  
- **Properties:**  
  - `id` (UUID, required): The unique identifier of the booking.  
  - `title` (string, nullable): The title of the booking.  
  - `startDate` (date-time, required): Start date of the booking.  
  - `endDate` (date-time, required): End date of the booking.  

---

**4. BookingUpdateDto**  
Used for updating a booking.  
- **Properties:**  
  - `id` (UUID, required): The unique identifier of the booking.  
  - `title` (string, nullable): The updated title of the booking.  
  - `startDate` (date-time, nullable): The updated start date and time of the booking.  
  - `endDate` (date-time, nullable): The updated end date and time of the booking.  

---

#### **Participant Models**

---

**1. ParticipantCreateDto**  
Used when creating a new participant.  
- **Properties:**  
  - `bookingId` (UUID, required): The ID of the booking the participant is associated with.  
  - `userId` (UUID, nullable): The ID of the registered user, if applicable.  
  - `unregUserId` (UUID, nullable): The ID of the unregistered user, if applicable.  

---

**2. ParticipantResponseDto**  
Represents the details of a participant.  
- **Properties:**  
  - `id` (UUID, required): The unique identifier of the participant.  
  - `createdBy` (UUID, required): The ID of the user who created the participant.  
  - `bookingId` (UUID, required): The ID of the booking the participant belongs to.  
  - `userId` (UUID, nullable): The ID of the associated user, if applicable.  
  - `unregUserId` (UUID, nullable): The ID of the associated unregistered user, if applicable.  
  - `groupId` (UUID, nullable): The ID of the group, if the participant belongs to one.  

---

#### **Ticket Models**

---

**1. TicketCreateDto**  
Used when creating a new ticket.  
- **Properties:**  
  - `title` (string, nullable): The title of the ticket.  
  - `startDate` (date-time, required): The start date and time of the ticket validity.  
  - `endDate` (date-time, required): The end date and time of the ticket validity.  
  - `price` (integer, nullable): The price of the ticket.  
  - `purchaseDate` (date-time, nullable): The purchase date of the ticket.  
  - `category` (string, required): The category of the ticket.  
  - `imageUrl` (string, nullable): The image URL associated with the ticket.  
  - `participantId` (UUID, nullable): The ID of the associated participant.  
  - `bookingId` (UUID, required): The ID of the associated booking.  

---

**2. TicketResponseDto**  
Represents the details of a ticket.  
- **Properties:**  
  - `id` (UUID, required): The unique identifier of the ticket.  
  - `title` (string, nullable): The title of the ticket.  
  - `startDate` (date-time, required): The start date of the ticket.  
  - `endDate` (date-time, required): The end date of the ticket.  
  - `price` (integer, nullable): The price of the ticket.  
  - `purchaseDate` (date-time, nullable): The purchase date of the ticket.  
  - `category` (string, required): The category of the ticket.  
  - `participant` (`ParticipantResponseDto`, required): Details of the participant linked to the ticket.  
  - `bookingId` (UUID, required): The ID of the associated booking.  
  - `imageUrl` (string, nullable): The image URL associated with the ticket.  

---

**3. TicketUpdateDto**  
Used for updating a ticket.  
- **Properties:**  
  - `id` (UUID, required): The unique identifier of the ticket.  
  - `title` (string, nullable): The updated title of the ticket.  
  - `startDate` (date-time, nullable): The updated start date of the ticket.  
  - `endDate` (date-time, nullable): The updated end date of the ticket.  
  - `price` (integer, nullable): The updated price of the ticket.  
  - `purchaseDate` (date-time, nullable): The updated purchase date of the ticket.  
  - `category` (string, nullable): The updated category of the ticket.  
  - `imageUrl` (string, nullable): The updated image URL of the ticket.  
  - `participantId` (UUID, nullable): The updated participant ID associated with the ticket.  

---

#### **Error Model**

---

**ProblemDetails**  
Represents error information in a standardized format.  
- **Properties:**  
  - `type` (string, nullable): A URI reference to the type of error.  
  - `title` (string, nullable): A short, human-readable summary of the error.  
  - `status` (integer, nullable): The HTTP status code for the error.  
  - `detail` (string, nullable): A detailed description of the error.  
  - `instance` (string, nullable): A URI reference that identifies the specific occurrence of the problem.  

---

This detailed documentation provides a clear and comprehensive view of each endpoint and model, ensuring frontend developers have all the necessary information for implementation.