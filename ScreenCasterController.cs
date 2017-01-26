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

        private readonly ScreenGrabber screenGrabber;

        public ScreenCasterController(ScreenGrabber screenGrabber){
            this.screenGrabber = screenGrabber;
        }

        [HttpGet]
        public string Ping()
        {
            return string.Format("{0}",DateTime.Now);
        }
 
        [HttpGet("get_image")]
        public FileResult GetImage()
        {
            return new FileStreamResult(new MemoryStream(screenGrabber.WaitForNextAvailableImage()), "image/jpg");
        }
    }
}