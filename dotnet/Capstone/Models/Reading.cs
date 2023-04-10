using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Reading
    {
        public int ReadingId { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int UserId { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int ProfileId { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int BloodSugar { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}
