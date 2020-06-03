using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Provider.Controllers
{
    [Route("api/[controller]")]
    public class ProviderController : Controller
    {
        public IActionResult GetValue()
        {
            return Ok(new
            {
                message = "Hello from provider!"
            });
        }
    }
}
