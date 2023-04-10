using Capstone.Models;

namespace Capstone.DAO
{
    public interface IReadingDAO
    {
        Reading GetReading(int readingId);
    }
}