using System;
using System.Collections.Generic;

namespace DigIO_Programming_Task_Services.Models
{
    public class LogActivity
    {
        public string IpAddress { get; set; }
        public string Identifier { get; set; }
        public string Auth { get; set; }
        public DateTimeOffset Date { get; set; }
        public Request Request { get; set; }
        public int Status { get; set; }
        public int Bytes { get; set; }
    }
}
