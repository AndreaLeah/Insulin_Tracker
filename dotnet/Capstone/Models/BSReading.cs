using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class BSReading
    {
        [Required]
        public int BloodSugar { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}
