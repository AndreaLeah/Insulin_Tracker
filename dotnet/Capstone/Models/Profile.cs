using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Capstone.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int UserId { get; set; }
        [Required, Range(0, 10000)]
        public decimal BasalRate { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int MinBloodSugar { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int MaxBloodSugar { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int CarbRatio { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int CorrectionRatio { get; set; }
        [MaxLength(100)]
        public string InsulinType { get; set; }
        public int InsulinStrength { get; set; }
    }
}
