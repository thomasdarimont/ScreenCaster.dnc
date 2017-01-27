using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScreenCaster
{
    [Route("api/[controller]")]
    public class ScreenCasterController : Controller
    {

        private readonly ScreenGrabber _screenGrabber;

        public ScreenCasterController(ScreenGrabber screenGrabber){
            _screenGrabber = screenGrabber;
        }

        [HttpGet]
        public string Ping()
        {
            return $"Server Time: {DateTime.Now}";
        }
 
        [HttpGet("get_image")]
        public FileResult GetImage()
        {
            return new FileStreamResult(new MemoryStream(_screenGrabber.WaitForNextAvailableImage()), "image/jpg");
        }
    }
}