using FluentValidation;
using ImageProcessor_Application.Interfaces;
using ImageProcessor_Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor_Application.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        private IValidator<IFormFile> _validator;
        public ImageProcessingService(IValidator<IFormFile> validator) 
        { 
            _validator = validator;
        }

        public async Task<ImageProcessResult> ImageProcessingFormFile(IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(file, cancellationToken);
                byte[] imageData;
                byte[] output;

                var extension = Path.GetExtension(file.FileName);

                cancellationToken.ThrowIfCancellationRequested();
                Wrapper.ImageProcessor processor = new Wrapper.ImageProcessor();
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream, cancellationToken);
                    imageData = memoryStream.ToArray();

                    processor.ProcessImage(imageData, extension);
                    output = processor.Data;

                    cancellationToken.ThrowIfCancellationRequested();

                    return new ImageProcessResult
                    {
                        Data = output,
                        ContentType = extension.Replace(".", string.Empty)
                    };
                }
            }
            catch (OperationCanceledException cancel)
            {
                throw;
            }
            catch(ValidationException ex)
            {
                throw;
            }
        }
    }
}
