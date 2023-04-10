using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class BolusDAO : IBolusDAO
    {
        private readonly string connectionString;

        public BolusDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Bolus GetBolus(int bolusId)
        {
            Bolus bolus = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT bolus_id, reading_id, bolus_amount FROM boluses WHERE bolus_id = @bolusId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bolusId", bolusId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    bolus = GetBolusFromReader(reader);
                }
            }

            return bolus;
        }

        public List<Bolus> GetUserBoluses(int userId)
        {
            List<Bolus> boluses = new List<Bolus>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT b.bolus_id, r.reading_id, bolus_amount " +
                    "FROM users u " +
                    "INNER JOIN readings r ON r.user_id = u.user_id " +
                    "INNER JOIN boluses b ON b.reading_id = r.reading_id " +
                    "WHERE u.user_id = @userId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Bolus bolus = GetBolusFromReader(reader);
                        boluses.Add(bolus);
                    }
                }
            }

            return boluses;
        }

        public FullBolusInfo GetFullBolusInfo(int bolusId)
        {
            FullBolusInfo bolus = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT bolus_id, u.user_id, bolus_amount, r.reading_id, p.profile_id, blood_sugar, time, basal_rate, min_blood_sugar," +
                    " max_blood_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength " +
                    "FROM boluses b " +
                    "INNER JOIN readings r ON r.reading_id = b.reading_id " +
                    "INNER JOIN profiles p ON p.profile_id = r.profile_id " +
                    "INNER JOIN users u ON u.user_id = p.user_id " +
                    "WHERE b.bolus_id = @bolusId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bolusId", bolusId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    bolus = GetFullBolusInfoFromReader(reader);
                }
            }

            return bolus;
        }

        public bool AddBolus(Bolus bolus)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO boluses (reading_id, bolus_amount) VALUES (@readingId, @bolusAmount);";

                SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@readingId", bolus.ReadingId);
                command.Parameters.AddWithValue("@bolusAmount", bolus.BolusAmount);
               
                int numRows = command.ExecuteNonQuery();

                if (numRows == 0)
                {
                    return false;
                }
                return true;
            }
        }

        private Bolus GetBolusFromReader(SqlDataReader reader)
        {
            Bolus bolus = new Bolus();
            bolus.BolusId = Convert.ToInt32(reader["bolus_id"]);
            bolus.ReadingId = Convert.ToInt32(reader["reading_id"]);
            bolus.BolusAmount = Convert.ToDecimal(reader["bolus_amount"]);

            return bolus;
        }

        private FullBolusInfo GetFullBolusInfoFromReader(SqlDataReader reader)
        {
            FullBolusInfo bolus = new FullBolusInfo();

            bolus.UserId = Convert.ToInt32(reader["user_id"]);
            bolus.BolusId = Convert.ToInt32(reader["bolus_id"]);
            bolus.ReadingId = Convert.ToInt32(reader["reading_id"]);
            bolus.ProfileId = Convert.ToInt32(reader["profile_id"]);
            bolus.BloodSugar = Convert.ToInt32(reader["blood_sugar"]);
            bolus.BolusAmount = Convert.ToDecimal(reader["bolus_amount"]);
            bolus.Time = Convert.ToDateTime(reader["time"]);
            bolus.BasalRate = Convert.ToInt32(reader["basal_rate"]);
            bolus.MinBloodSugar = Convert.ToInt32(reader["min_blood_sugar"]);
            bolus.MaxBloodSugar = Convert.ToInt32(reader["max_blood_sugar"]);
            bolus.CarbRatio = Convert.ToInt32(reader["carb_ratio"]);
            bolus.CorrectionRatio = Convert.ToInt32(reader["correction_ratio"]);
            bolus.InsulinType = Convert.ToString(reader["insulin_type"]);
            bolus.InsulinStrength = Convert.ToInt32(reader["insulin_strength"]);

            return bolus;
        }
    }
}
