public class BlobService : IBlobService
{
    private readonly IBlobService _blobService;


    public BlobService(IBlobService blobService)
    {
        _blobService = blobService;
    }

    public async Task<string?> UploadImageAsync(IFormFile image, int ticketId)
    {
        if (image == null) return null;
        return await _blobService.UploadImageAsync(image, ticketId);
    }

    public async Task<bool> DeleteImageAsync(string blobName)
    {
        return await _blobService.DeleteImageAsync(blobName);
    }

    public string GetImageUriWithSasToken(string blobName, int validityInHours)
    {
        return _blobService.GetImageUriWithSasToken(blobName, validityInHours);
    }
}