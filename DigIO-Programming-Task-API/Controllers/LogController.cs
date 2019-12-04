using DigIO_Programming_Task_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DigIO_Programming_Task_API.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<IEnumerable<string>>> ParseLog([FromForm] IFormFile log)
        {
            if (!FileIsValid(log))
            {
                //Return Log Error
                throw new ArgumentException();
            }

            var logReader = new LogReader(log);
            var logActivityList = await logReader.ReadLog();

            return new string[] { "value1", "value2" };
        }

        private bool FileIsValid(IFormFile log)
        {
            return log != null
                && log.Length > 0
                && Path.GetExtension(log.FileName).Equals(".log");
        }
    }
}