//using Azure.Messaging.EventGrid;
//using Azure.Messaging.EventGrid.SystemEvents;
//using Microsoft.AspNetCore.Mvc;
//using Ticketron.Interfaces;
//using Ticketron.Models;

//namespace Ticketron.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AzureEventsController : ControllerBase
//    {
//        private readonly IUserRepository _userRepository;
//        public AzureEventsController(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        [HttpPost]
//        public async Task<IActionResult> HandleAzureEvents()
//        {
//            string response = string.Empty;
//            BinaryData events = BinaryData.FromStream(Request.Body);

//            EventGridEvent[] egEvents = EventGridEvent.ParseMany(events);

//            foreach (EventGridEvent e in egEvents)
//            {
//                if (e.TryGetSystemEventData(out object eventData))
//                {
//                    if (eventData is SubscriptionValidationEventData subscriptionValidationEventData)
//                    {
//                        var responseData = new
//                        {
//                            validationResponse = subscriptionValidationEventData.ValidationCode
//                        };

//                        return new OkObjectResult(responseData);
//                    }
//                }
//            }


//            //try
//            //{
//            //    var eventGridEvents = JsonSerializer.Deserialize<List<AzureEvents>>(events.GetRawText());

//            //    if (eventGridEvents == null)
//            //    {
//            //        return BadRequest("No events found in request body");
//            //    }

//            //    foreach (var eventGridEvent in eventGridEvents)
//            //    {
//            //        if (eventGridEvent.EventType == "Microsoft.EventGrid.SubscriptionValidationEvent")
//            //        {
//            //            var validationEventData = JsonSerializer.Deserialize<SubscriptionValidationEventData>(eventGridEvent.Data.ToString());

//            //            if (validationEventData == null)
//            //            {
//            //                return BadRequest("Could not deserialize validation event data");
//            //            }

//            //            return Ok(new { validationResponse = validationEventData.ValidationCode });
//            //        }
//            //        else if (eventGridEvent.EventType == "Microsoft.Entra.UserCreated")
//            //        {
//            //            var userEventData = JsonSerializer.Deserialize<EntraUserEventData>(eventGridEvent.Data.ToString());
//            //            HandleNewUser(userEventData);
//            //        }
//            //    }

//            //    return BadRequest("Event type not supported");
//            //}
//            //catch (Exception ex)
//            //{
//            //    return BadRequest($"Error processing Event Grid event: {ex.Message}");
//            //}
//        }

//        private void HandleNewUser(EntraUserEventData userEventData)
//        {
//            var objectIdString = userEventData.ObjectId;

//            if (string.IsNullOrEmpty(objectIdString))
//                throw new UnauthorizedAccessException("User object id is missing.");

//            if (!Guid.TryParse(objectIdString, out var objectId))
//                throw new UnauthorizedAccessException("Invalid user object id.");

//            if (!_userRepository.UserExists(objectId))
//            {
//                var newUser = new User
//                {
//                    Name = userEventData.DisplayName,
//                    AzureObjectId = objectId,
//                    Email = userEventData
//                };

//                _userRepository.CreateUser(newUser);
//            }

//        }
//    }
//}
