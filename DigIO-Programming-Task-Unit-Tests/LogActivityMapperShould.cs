using DigIO_Programming_Task_API.Services;
using Xunit;

namespace DigIO_Programming_Task_Unit_Tests
{
    public class LogActivityMapperShould
    {
        [Fact]
        public void CreateCorrectLogActivity()
        {
            var activityLine = "79.125.00.21 - - [10/Jul/2018:20:03:40 +0200]" +
                " \"GET /newsletter/ HTTP/1.1\" 200 3574 \"-\"" +
                " \"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)\"";
            var logActivity = LogActivityMapper.Map(activityLine);

            Assert.Equal("79.125.00.21", logActivity.IpAddress);
            Assert.Equal("/newsletter/", logActivity.Request.Url);
        }
    }
}
