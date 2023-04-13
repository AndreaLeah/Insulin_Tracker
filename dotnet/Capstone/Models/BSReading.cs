using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class BSReading
    {
        [Required, Range(0, int.MaxValue)]
        public int BloodSugar { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}
