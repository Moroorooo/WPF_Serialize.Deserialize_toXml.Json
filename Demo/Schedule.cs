using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Net1702_221_ASM2_SE161142_NGUYENMINHTRIET
{
    public class Schedule
    {
        public long ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime StartTimeFrame { get; set; }
        public DateTime EndTimeFrame { get; set; }
        public double Price { get; set; }
        public double TotalHours { get; set; }

    }
}
