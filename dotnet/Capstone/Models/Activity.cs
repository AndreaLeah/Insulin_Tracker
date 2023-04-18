using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Activity
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string ActivityName { get; set; }
        public DateTime Time { get; set; }

    }
}
