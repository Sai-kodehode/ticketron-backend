using Microsoft.AspNetCore.Mvc;

namespace Ticketron.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image, [FromQuery] int ticketId)
        {
            if (image == null || image.Length == 0)
                return BadRequest();

            var blobName = await _blobService.UploadImageAsync(image, ticketId);
            return Ok(new { BlobName = blobName });
        }

        [HttpDelete("{blobName}")]
        public async Task<IActionResult> DeleteImage(string blobName)
        {
            bool deleted = await _blobService.DeleteImageAsync(blobName);
            if (!deleted) return NotFound();
            return Ok();
        }

        [HttpGet("{blobName}/sas")]
        public IActionResult GetImageUriWithSasToken(string blobName, [FromQuery] int validityInHours = 1)
        {
            try
            {
                var sasUrl = _blobService.GetImageUriWithSasToken(blobName, validityInHours);
                return Ok(new { sasUrl });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
