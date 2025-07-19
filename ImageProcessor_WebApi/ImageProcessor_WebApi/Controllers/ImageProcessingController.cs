using Microsoft.AspNetCore.Mvc;

namespace ImageProcessor_WebApi.Controllers
{
    public class ImageProcessingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
