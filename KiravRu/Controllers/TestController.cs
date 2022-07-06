using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Controllers
{
    [Route("api/t")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            return Ok("test");
        }
    }
}
