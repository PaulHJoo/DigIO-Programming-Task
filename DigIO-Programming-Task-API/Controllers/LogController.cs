using DigIO_Programming_Task_API.Models;
using DigIO_Programming_Task_API.Services;
using DigIO_Programming_Task_Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace DigIO_Programming_Task_API.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<ParseLogResponse>> ParseLog([FromForm] IFormFile log)
        {
            if (!FileIsValid(log))
            {
                return BadRequest("Input log is empty or incorrect format.");
            }

            var logReader = new LogReader(log);
            var logActivityList = await logReader.ReadLog();

            var logAnalyser = new LogActivityAnalyser(logActivityList.LogActivities);
            var logAnalysis = logAnalyser.AnalyseWithTopResults(3);

            return new ParseLogResponse
            {
                LogAnalysis = logAnalysis,
                Errors = logActivityList.Errors
            };
        }

        private bool FileIsValid(IFormFile log)
        {
            return log != null
                && log.Length > 0
                && Path.GetExtension(log.FileName).Equals(".log");
        }
    }
}