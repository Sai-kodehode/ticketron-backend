
namespace Ticketron.Interfaces
{
    public interface IBlobService
    {
        Task<string?> UploadImage(IFormFile image);
        Task<bool> DeleteImage(string imageUrl);
    }
}
