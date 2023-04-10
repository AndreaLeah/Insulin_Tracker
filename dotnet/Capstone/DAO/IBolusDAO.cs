using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IBolusDAO
    {
        Bolus GetBolus(int bolusId);
        public FullBolusInfo GetFullBolusInfo(int bolusId);
        public List<Bolus> GetUserBoluses(int userId);
        public bool AddBolus(Bolus bolus);
    }
}