using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IProfileDAO
    {
        public Profile GetProfile(int profileId);
        public List<Profile> GetAllUserProfiles(int userId);
        public bool AddProfile(Profile profile);   
        public bool UpdateProfile(Profile profile);
        public bool DeleteProfile(int profileId);
    }
}