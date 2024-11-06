using Azure.Storage.Blobs;
using Ticketron.Interfaces;

namespace Ticketron.Services;


public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;


    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string?> UploadImageAsync(IFormFile image, int ticketId)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("images");

        var uniqueId = Guid.NewGuid().ToString();
        var blobName = $"{ticketId}-{uniqueId}-{image.FileName}";

        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.UploadAsync(image.OpenReadStream(), true);

        return blobClient.Name;
    }

    public async Task<bool> DeleteImageAsync(string blobName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("images");
        var blobClient = containerClient.GetBlobClient(blobName);

        return await blobClient.DeleteIfExistsAsync();
    }

    public string GetImageUriWithSasToken(string blobName, int validityInHours)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("images");
        var blobClient = containerClient.GetBlobClient(blobName);

        if (!blobClient.Exists())
            throw new InvalidOperationException("Blob not found.");

        var sasBuilder = new Azure.Storage.Sas.BlobSasBuilder
        {
            BlobContainerName = blobClient.BlobContainerName,
            BlobName = blobClient.Name,
            Resource = "b",
            StartsOn = DateTime.UtcNow,
            ExpiresOn = DateTime.UtcNow.AddHours(validityInHours)
        };

        sasBuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);

        var sasToken = sasBuilder.ToSasQueryParameters(
            new Azure.Storage.StorageSharedKeyCredential(
                "your-storage-account-name",
                "your-storage-account-key"
            )).ToString();

        return $"{blobClient.Uri}?{sasToken}";
    }
}