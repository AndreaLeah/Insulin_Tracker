using Capstone.Models;
using System;
using System.Collections.Generic;
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

                string sql = "SELECT reading_id, user_id, profile_id, blood_sugar, carbs, time FROM readings WHERE reading_id = @readingId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@readingId", readingId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    reading = GetReadingFromReader(reader);
                }
            }

            return reading;
        }

        public List<Reading> GetUserReadings(int userId)
        {
            List<Reading> readings = new List<Reading>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT reading_id, user_id, profile_id, blood_sugar, carbs, time FROM readings WHERE user_id = @userId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Reading reading = GetReadingFromReader(reader);
                        readings.Add(reading);
                    }
                }
            }

            return readings;
        }

        public List<Reading> GetUserPreviousReadings(int userId, int amount)
        {
            List<Reading> readings = new List<Reading>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT TOP @amount reading_id, user_id, profile_id, blood_sugar, carbs, time FROM readings WHERE user_id = @userId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reading reading = GetReadingFromReader(reader);
                        readings.Add(reading);
                    }
                }
            }

            return readings;
        }

        public List<Reading> GetReadingsByProfile(int profileId)
        {
            List<Reading> readings = new List<Reading>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT reading_id, user_id, profile_id, blood_sugar, carbs, time FROM readings WHERE profile_id = @profileId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@profileId", profileId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reading reading = GetReadingFromReader(reader);
                        readings.Add(reading);
                    }
                }
            }

            return readings;
        }

        public bool AddReading(Reading reading)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO readings " +
                    "(user_id, profile_id, blood_sugar, carbs, time) " +
                    "VALUES (@userId, @profileId, @bloodSugar, @carbs, @Time)";

                SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@userId", reading.UserId);
                command.Parameters.AddWithValue("@profileId", reading.ProfileId);
                command.Parameters.AddWithValue("@bloodSugar", reading.BloodSugar);
                command.Parameters.AddWithValue("@carbs", reading.Carbs);
                command.Parameters.AddWithValue("@time", reading.Time);
                
                int numRows = command.ExecuteNonQuery();

                if (numRows == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public List<BSReading> GetHistoricMeasurmentsByTimeframe(int timeframe, int profileId)
        {
            List<BSReading> measurments = new List<BSReading>();
            BSReading bsreading = new BSReading();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT reading_id, user_id, profile_id, blood_sugar, carbs, time FROM readings WHERE profile_id = @profileId AND time between DateAdd(DD, -@timeframe, GETDATE()) and GETDATE()";

                SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@timeframe", timeframe);
                command.Parameters.AddWithValue("@profileId", profileId);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Reading reading = GetReadingFromReader(reader);
                        
                        bsreading.BloodSugar = reading.BloodSugar;
                        bsreading.Time = reading.Time;

                        measurments.Add(bsreading);
                    }
                }
            }

            return measurments;
        }

        private Reading GetReadingFromReader(SqlDataReader reader)
        {
            Reading reading = new Reading();
            reading.ReadingId = Convert.ToInt32(reader["reading_id"]);
            reading.UserId = Convert.ToInt32(reader["user_id"]);
            reading.ProfileId = Convert.ToInt32(reader["profile_id"]);
            reading.BloodSugar = Convert.ToInt32(reader["blood_sugar"]);
            reading.Carbs = Convert.ToInt32(reader["carbs"]);
            reading.Time = Convert.ToDateTime(reader["time"]);

            return reading;
        }
    }
}
