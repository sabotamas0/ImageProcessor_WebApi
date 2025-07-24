using ImageProcessor_Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Threading;
using System.Text;
using Microsoft.Extensions.Logging;
using FluentValidation;


namespace ImageProcessor_WebApi.Controllers
{
    [Route("api/imageprocessing")]
    [ApiController]
    public class ImageProcessingController : ControllerBase
    {
        private IImageProcessingService _service;
        public ImageProcessingController(IImageProcessingService service) 
        {
            _service = service;
        }

        [HttpPost]
        [Route("process_image")]
        public async Task<IActionResult> ProcessImage(IFormFile file, CancellationToken token)
        {
            try
            {
                using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token))
                {
                    var response = await _service.ImageProcessingFormFile(file, linkedCts.Token);

                    return File(response.Data, $"image/{response.ContentType}");
                }
            }
            catch (OperationCanceledException e)
            {
                return Ok("Operation canceled while processing the image.");
            }
            catch (ValidationException ex)
            {
                return BadRequest("During the image processing there were some Validation Errors: " + string.Join(" | ", ex.Errors.Select(x => x.ErrorMessage)));
            }

        }
    }
}
