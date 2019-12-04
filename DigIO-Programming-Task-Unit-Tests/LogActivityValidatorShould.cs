using DigIO_Programming_Task_API.Services;
using Xunit;

namespace DigIO_Programming_Task_Unit_Tests
{
    public class LogActivityValidatorShould
    {
        [Fact]
        public void LogIncorrectNumberOfFields()
        {
            var activityLine = "79.125.00.21 - - [10/Jul/2018:20:03:40 +0200]" +
                " \"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)\"";
            var errors = LogActivityValidator.Validate(activityLine, 1);

            Assert.Single(errors);
            Assert.Equal("Log on line 1 is incorrectly formatted.", errors[0]);
        }

        [Fact]
        public void LogIncorrectNumberOfIpUserAndDateFields()
        {
            var activityLine = "79.125.00.21 - [10/Jul/2018:20:03:40 +0200]" +
                " \"GET /newsletter/ HTTP/1.1\" 200 3574 \"-\"" +
                " \"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)\"";
            var errors = LogActivityValidator.Validate(activityLine, 1);

            Assert.Single(errors);
            Assert.Equal("Log on line 1 is incorrectly formatted.", errors[0]);
        }

        [Fact]
        public void LogIncorrectNumberRequestFields()
        {
            var activityLine = "79.125.00.21 - - [10/Jul/2018:20:03:40 +0200]" +
                " \"GET /newsletter/\" 200 3574 \"-\"" +
                " \"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)\"";
            var errors = LogActivityValidator.Validate(activityLine, 1);

            Assert.Single(errors);
            Assert.Equal("Log on line 1 is incorrectly formatted.", errors[0]);
        }

        [Fact]
        public void LogIncorrectNumberOfIpAddressBits()
        {
            var activityLine = "79.125.00 - - [10/Jul/2018:20:03:40 +0200]" +
                " \"GET /newsletter/ HTTP/1.1\" 200 3574 \"-\"" +
                " \"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)\"";
            var errors = LogActivityValidator.Validate(activityLine, 1);

            Assert.Single(errors);
            Assert.Equal("IP Address on line number 1 is incorrectly formatted.", errors[0]);
        }

        [Fact]
        public void LogNonNumericInIpAddress()
        {
            var activityLine = "79.125.00.21a - - [10/Jul/2018:20:03:40 +0200]" +
                " \"GET /newsletter/ HTTP/1.1\" 200 3574 \"-\"" +
                " \"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)\"";
            var errors = LogActivityValidator.Validate(activityLine, 1);

            Assert.Single(errors);
            Assert.Equal("IP Address on line number 1 contains non-numeric characters.", errors[0]);
        }

        [Fact]
        public void LogInvalidUrl()
        {
            var activityLine = "79.125.00.21 - - [10/Jul/2018:20:03:40 +0200]" +
                " \"GET newsletter/ HTTP/1.1\" 200 3574 \"-\"" +
                " \"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)\"";
            var errors = LogActivityValidator.Validate(activityLine, 1);

            Assert.Single(errors);
            Assert.Equal("URL on line number 1 is incorrectly formatted.", errors[0]);
        }
    }
}
