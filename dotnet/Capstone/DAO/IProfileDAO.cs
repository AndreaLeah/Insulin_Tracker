using Capstone.Models;

namespace Capstone.DAO
{
    public interface IProfileDAO
    {
        Profile GetProfile(int profileId);
    }
}