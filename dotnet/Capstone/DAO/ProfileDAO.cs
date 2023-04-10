using Capstone.Models;
using System;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class ProfileDAO : IProfileDAO
    {
        private readonly string connectionString;

        public ProfileDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Profile GetProfile(int profileId)
        {
            Profile profile = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT p.profile_id, p.user_id, p.basal_rate, p.min_blood_sugar, " +
                    "p.max_blood_sugar, p.carb_ratio, p.correction_ratio, p.insulin_type, p.insulin_strength " +
                    "FROM profiles p WHERE p.profile_id = @profileId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@profileId", profileId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    profile = GetProfileFromReader(reader);
                }
            }

            return profile;
        }

        private Profile GetProfileFromReader(SqlDataReader reader)
        {
            Profile profile = new Profile();
            profile.ProfileId = Convert.ToInt32(reader["profile_id"]);
            profile.UserId = Convert.ToInt32(reader["user_id"]);
            profile.BasalRate = Convert.ToDecimal(reader["basal_rate"]);
            profile.MinBloodSugar = Convert.ToInt32(reader["min_blood_sugar"]);
            profile.MaxBloodSugar = Convert.ToInt32(reader["max_blood_sugar"]);
            profile.CarbRatio = Convert.ToInt32(reader["carb_ratio"]);
            profile.CorrectionRatio = Convert.ToInt32(reader["correction_ratio"]);
            profile.InsulinType = Convert.ToString(reader["insulin_type"]);
            profile.InsulinStrength = Convert.ToInt32(reader["insulin_strength"]);

            return profile;
        }
    }
}
