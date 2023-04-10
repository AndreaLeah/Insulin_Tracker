using Capstone.Models;
using System;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class ReadingDAO : IReadingDAO
    {
        private readonly string connectionString;

        public ReadingDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Reading GetReading(int readingId)
        {
            Reading reading = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT reading_id, user_id, profile_id, blood_sugar, time FROM readings WHERE reading_id = @readingId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@readingId", reading);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    reading = GetReadingFromReader(reader);
                }
            }

            return reading;
        }

        private Reading GetReadingFromReader(SqlDataReader reader)
        {
            Reading reading = new Reading();
            reading.ReadingId = Convert.ToInt32(reader["reading_id"]);
            reading.UserId = Convert.ToInt32(reader["user_id"]);
            reading.ProfileId = Convert.ToInt32(reader["profile_id"]);
            reading.BloodSugar = Convert.ToInt32(reader["blood_sugar"]);
            reading.Time = Convert.ToDateTime(reader["time"]);

            return reading;
        }
    }
}
