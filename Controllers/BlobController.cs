using Microsoft.AspNetCore.Mvc;
using Ticketron.Interfaces;


namespace Ticketron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            var blobName = await _blobService.UploadImage(image);

            if (string.IsNullOrEmpty(blobName))
                return StatusCode(500, "Error uploading the image.");
            string blobUrl = $"https://ticketron-cdne-h4b2a3ehg4a3c9gx.a02.azurefd.net/images/{blobName}";

            return Ok(new { blobUrl });
        }

        [HttpDelete("delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteImage([FromQuery] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest();
            }
            var blobDeleted = await _blobService.DeleteImage(imageUrl);

            if (!blobDeleted)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }

}

