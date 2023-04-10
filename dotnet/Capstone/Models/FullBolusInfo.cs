using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class FullBolusInfo
    {
        public int UserId { get; set; }
        public int BolusId { get; set; }
        public int ReadingId { get; set; }
        public int ProfileId { get; set; }
        public int BloodSugar { get; set; }
        public decimal BolusAmount { get; set; }
        public DateTime Time { get; set; }
        public decimal BasalRate { get; set; }
        public int MinBloodSugar { get; set; }
        public int MaxBloodSugar { get; set; }
        public int CarbRatio { get; set; }
        public int CorrectionRatio { get; set; }
        public string InsulinType { get; set; }
        public int InsulinStrength { get; set; }
    }
}
