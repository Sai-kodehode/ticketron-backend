using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Ticketron.Interfaces;

namespace Ticketron.Services;


public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;


    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }
    public async Task<string?> UploadImage(IFormFile image)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("images");


        var uniqueId = Guid.NewGuid().ToString();
        var blobName = $"{uniqueId}-{image.FileName}";

        var fileExtension = Path.GetExtension(image.FileName).ToLower();

        string contentType;
        switch (fileExtension)
        {
            case ".jpg":
            case ".jpeg":
                contentType = "image/jpeg";
                break;
            case ".png":
                contentType = "image/png";
                break;
            case ".gif":
                contentType = "image/gif";
                break;
            case ".bmp":
                contentType = "image/bmp";
                break;
            case ".webp":
                contentType = "image/webp";
                break;
            case ".tiff":
                contentType = "image/tiff";
                break;
            case ".svg":
                contentType = "image/svg+xml";
                break;
            case ".ico":
                contentType = "image/x-icon";
                break;

            case ".pdf":
                contentType = "application/pdf";
                break;
            case ".pkpass":
                contentType = "application/vnd.apple.pkpass";
                break;
            case ".txt":
                contentType = "text/plain";
                break;
            case ".rtf":
                contentType = "application/rtf";
                break;
            case ".html":
                contentType = "text/html";
                break;

            case ".doc":
                contentType = "application/msword";
                break;
            case ".docx":
                contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                break;
            default:
                throw new InvalidOperationException("Unsupported file type.");
        }

        var blobClient = containerClient.GetBlobClient(blobName);

        var uploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            }
        };

        await blobClient.UploadAsync(image.OpenReadStream(), uploadOptions);

        return blobClient.Name;
    }

    public async Task<bool> DeleteImage(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            return false;
        }

        try
        {

            var uri = new Uri(imageUrl);
            var blobName = uri.Segments.Last();
            var containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var blobClient = containerClient.GetBlobClient(blobName);

            return await blobClient.DeleteIfExistsAsync();
        }
        catch (Exception)
        {

            return false;
        }
    }

}