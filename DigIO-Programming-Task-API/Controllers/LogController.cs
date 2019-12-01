using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigIO_Programming_Task_API.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        [HttpPost]
        public ActionResult<IEnumerable<string>> ParseLog(IFormFile file)
        {
            return new string[] { "value1", "value2" };
        }
    }
}