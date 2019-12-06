using DigIO_Programming_Task_Services.Models;
using DigIO_Programming_Task_Services.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DigIO_Programming_Task_Unit_Tests
{
    public class LogAnalyserShould
    {
        [Fact]
        public void GetCorrectNumberOfUniqueIpAddresses()
        {
            var logActivities = LogActivities();

            var logAnalyser = new LogActivityAnalyser(logActivities);
            var analysis = logAnalyser.AnalyseWithTopResults(3);

            Assert.Equal(4, analysis.NumberOfUniqueIpAddresses);
        }

        [Fact]
        public void GetCorrectMostVisitedUrls()
        {
            var logActivities = LogActivities();

            var logAnalyser = new LogActivityAnalyser(logActivities);
            var topThreeVisitedUrls = logAnalyser.AnalyseWithTopResults(3)
                .TopThreeVisitedUrls
                .ToList();

            var expectedResult = new List<RecordAndCount>
            {
                new RecordAndCount
                {
                    Record = "/this/page/does/not/exist/",
                    Count = 5
                },
                new RecordAndCount
                {
                    Record = "/intranet-analytics/",
                    Count = 2
                },
                new RecordAndCount
                {
                    Record = "http://example.net/faq/",
                    Count = 1
                }
            };

            AssertListsAreEqual(expectedResult, topThreeVisitedUrls);
        }

        [Fact]
        public void GetCorrectMostActiveIpAddresses()
        {
            var logActivities = LogActivities();

            var logAnalyser = new LogActivityAnalyser(logActivities);
            var topThreeVisitedUrls = logAnalyser.AnalyseWithTopResults(3)
                .TopThreeActiveIpAddresses
                .ToList();

            var expectedResult = new List<RecordAndCount>
            {
                new RecordAndCount
                {
                    Record = "168.41.191.40",
                    Count = 7
                },
                new RecordAndCount
                {
                    Record = "177.71.128.21",
                    Count = 3
                },
                new RecordAndCount
                {
                    Record = "168.41.191.41",
                    Count = 1
                }
            };

            AssertListsAreEqual(expectedResult, topThreeVisitedUrls);
        }

        private void AssertListsAreEqual(IList<RecordAndCount> expectedResult, IList<RecordAndCount> actualResult)
        {
            Assert.Equal(expectedResult.Count(), actualResult.Count());

            for (var i = 0; i < expectedResult.Count(); i++)
            {
                Assert.Equal(expectedResult[i].Record, actualResult[i].Record);
                Assert.Equal(expectedResult[i].Count, actualResult[i].Count);
            }
        }

        private IEnumerable<LogActivity> LogActivities()
        {
            return new List<LogActivity>()
            {
                new LogActivity
                {
                    IpAddress = "177.71.128.21",
                    Request = new Request
                    {
                        Url = "/intranet-analytics/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.40",
                    Request = new Request
                    {
                        Url = "http://example.net/faq/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.41",
                    Request = new Request
                    {
                        Url = "/this/page/does/not/exist/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.40",
                    Request = new Request
                    {
                        Url = "http://example.net/blog/category/meta/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "177.71.128.21",
                    Request = new Request
                    {
                        Url = "/blog/2018/08/survey-your-opinion-matters/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.40",
                    Request = new Request
                    {
                        Url = "/intranet-analytics/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.40",
                    Request = new Request
                    {
                        Url = "/intranet-analytics/ "
                    }
                },
                new LogActivity
                {
                    IpAddress = "177.71.128.21",
                    Request = new Request
                    {
                        Url = "/this/page/does/not/exist/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.40",
                    Request = new Request
                    {
                        Url = "/this/page/does/not/exist/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.43",
                    Request = new Request
                    {
                        Url = "/this/page/does/not/exist/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.40",
                    Request = new Request
                    {
                        Url = "/this/page/does/not/exist/"
                    }
                },
                new LogActivity
                {
                    IpAddress = "168.41.191.40",
                    Request = new Request
                    {
                        Url = "/blog/category/community/"
                    }
                }
            };
        }
    }
}
