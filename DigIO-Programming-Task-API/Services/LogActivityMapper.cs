using DigIO_Programming_Task_Services.Models;

namespace DigIO_Programming_Task_API.Services
{
    public class LogActivityMapper
    {
        public static LogActivity Map(string activityLine)
        {
            return new LogActivity
            {
                IpAddress = GetIpAddress(activityLine),
                Request = new Request
                {
                    Url = GetRequestUrl(activityLine)
                }
            };
        }

        public static string GetIpAddress(string activityLine)
        {
            return activityLine.Split(' ')[0];
        }

        public static string GetRequestUrl(string activityLine)
        {
            return  activityLine.Split(' ')[6];
        }
    }
}
