using System.Security.Claims;
using Ticketron.Interfaces;

namespace Ticketron.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserObjectId()
        {
            var objectIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier");

            if (string.IsNullOrEmpty(objectIdString))
                throw new UnauthorizedAccessException("User object id is missing.");

            if (!Guid.TryParse(objectIdString, out var objectId))
                throw new UnauthorizedAccessException("Invalid user object id.");

            return objectId;
        }
    }
}

// Usage:

//Guid currentUserId;
//try
//{
//    currentUserId = _userContextService.GetUserObjectId();
//}
//catch (UnauthorizedAccessException ex)
//{
//    return Unauthorized(ex.Message);
//}