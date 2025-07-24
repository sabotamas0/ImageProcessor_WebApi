using ImageProcessor_Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor_Application.Interfaces
{
    public interface IImageProcessingService
    {
        Task<ImageProcessResult> ImageProcessingFormFile(IFormFile file, CancellationToken cancellationToken);
    }
}
