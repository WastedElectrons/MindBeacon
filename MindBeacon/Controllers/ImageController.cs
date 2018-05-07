using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindBeacon.Models;
using MindBeacon.Service;

namespace MindBeacon.Controllers
{
    [Produces("application/json")]
    [Route("api/Image")]
    public class ImageController : Controller
    {
        // GET: api/Image
        [HttpGet]
        public IEnumerable<Image> Get()
        {
            return Image.Repo.GetAll();
        }
    }
}
