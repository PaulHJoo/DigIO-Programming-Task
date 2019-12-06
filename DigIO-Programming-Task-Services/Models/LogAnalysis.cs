using System.Collections.Generic;

namespace DigIO_Programming_Task_Services.Models
{
    public class LogAnalysis
    {
        public int NumberOfUniqueIpAddresses { get; set; }
        public IEnumerable<RecordAndCount> TopThreeVisitedUrls { get; set; }
        public IEnumerable<RecordAndCount> TopThreeActiveIpAddresses { get; set; }
    }
}
