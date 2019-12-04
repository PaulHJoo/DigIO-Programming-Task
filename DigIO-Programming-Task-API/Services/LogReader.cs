using DigIO_Programming_Task_API.Models;
using DigIO_Programming_Task_Services.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DigIO_Programming_Task_API.Services
{
    public class LogReader
    {
        private IFormFile LogFile { get; }

        public LogReader(IFormFile log)
        {
            LogFile = log;
        }

        public async Task<LogReadResult> ReadLog()
        {
            var logActivityList = new List<LogActivity>();
            var logActivityErrorsList = new List<string>();
            using (var reader = new StreamReader(LogFile.OpenReadStream()))
            {
                var lineNumber = 0;
                while (reader.Peek() >= 0)
                {
                    lineNumber++;
                    var activityLine = await reader.ReadLineAsync();
                    var errors = LogActivityValidator.Validate(activityLine, lineNumber);
                    if (errors.Count != 0)
                    {
                        logActivityErrorsList.AddRange(errors);
                        continue;
                    }
                    logActivityList.Add(LogActivityMapper.Map(activityLine));
                }
            }

            return new LogReadResult
            {
                LogActivities = logActivityList,
                Errors = logActivityErrorsList
            };
        }
    }
}
