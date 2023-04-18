using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IActivityDAO
    {
        public List<Activity> GetAllActivity(int userId);
        public bool AddActivity(Activity activity);
    }
}
