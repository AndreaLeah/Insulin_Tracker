namespace Capstone.Models
{
    public class Bolus
    {
        public int BolusId { get; set; }
        public int ReadingId { get; set; }
        public decimal BolusAmount { get; set; }
    }
}
