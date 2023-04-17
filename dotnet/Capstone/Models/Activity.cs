using System;

namespace Capstone.Models
{
    public class Activity
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string ActivityName { get; set; }
        public DateTime Time { get; set; }

    }
}
