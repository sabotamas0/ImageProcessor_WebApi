
using FluentValidation;
using ImageProcessor_Application.Interfaces;
using ImageProcessor_Application.Services;
using ImageProcessor_Application.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ImageProcessor_Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IValidator<IFormFile>, FileValidator>();
            services.AddScoped<IImageProcessingService, ImageProcessingService>();
            return services;
        }
    }
}
