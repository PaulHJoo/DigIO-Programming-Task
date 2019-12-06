using DigIO_Programming_Task_Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace DigIO_Programming_Task_Services.Services
{
    public class LogActivityAnalyser
    {
        private IEnumerable<LogActivity> LogActivities { get; set; }

        public LogActivityAnalyser(IEnumerable<LogActivity> logActivities)
        {
            LogActivities = logActivities;
        }

        public LogAnalysis AnalyseWithTopResults(int top)
        {
            //Error when top is more than entries
            return new LogAnalysis
            {
                NumberOfUniqueIpAddresses = GetNumberOfUniqueIpAddresses(),
                TopThreeVisitedUrls = GetMostVisitedUrls(top),
                TopThreeActiveIpAddresses = GetMostActiveIpAddresses(top)
            };
        }

        private int GetNumberOfUniqueIpAddresses()
        {
            return LogActivities.GroupBy(log => log.IpAddress).Count();
        }

        private IEnumerable<RecordAndCount> GetMostVisitedUrls(int top)
        {
            return LogActivities
                .GroupBy(log => log.Request.Url)
                .Select(grouping => new RecordAndCount
                {
                    Record = grouping.Key,
                    Count = grouping.Count()
                })
                .OrderByDescending(grouping => grouping.Count)
                .Take(top);
        }

        private IEnumerable<RecordAndCount> GetMostActiveIpAddresses(int top)
        {
            return LogActivities
                .GroupBy(log => log.IpAddress)
                .Select(grouping => new RecordAndCount
                {
                    Record = grouping.Key,
                    Count = grouping.Count()
                })
                .OrderByDescending(grouping => grouping.Count)
                .Take(top);
        }
    }
}
