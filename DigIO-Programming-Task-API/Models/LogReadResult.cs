using DigIO_Programming_Task_Services.Models;
using System.Collections.Generic;

namespace DigIO_Programming_Task_API.Models
{
    public class LogReadResult
    {
        public IEnumerable<LogActivity> LogActivities { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
