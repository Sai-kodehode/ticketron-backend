public interface IBlobService
{
    Task<string?> UploadImageAsync(IFormFile image, int ticketId);
    Task<bool> DeleteImageAsync(string blobName);
    string GetImageUriWithSasToken(string blobName, int validityInHours);
}