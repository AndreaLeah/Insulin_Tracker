using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

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

                string sql = "SELECT p.profile_id, p.user_id, p.profile_name, p.basal_rate, p.min_blood_sugar, " +
                    "p.max_blood_sugar, p.min_warning_sugar, p.max_warning_sugar, p.carb_ratio, p.correction_ratio, p.insulin_type, p.insulin_strength " +
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

        public List<Profile> GetAllUserProfiles(int userId)
        {
            List<Profile> profiles = new List<Profile>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT p.profile_id, p.profile_name, p.user_id, p.basal_rate, p.min_blood_sugar, p.max_blood_sugar, p.min_warning_sugar, p.max_warning_sugar, " +
                    "p.carb_ratio, p.correction_ratio, p.insulin_type, p.insulin_strength " +
                    "FROM profiles p WHERE p.user_id = @userId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Profile profile = GetProfileFromReader(reader);
                        profiles.Add(profile);
                    }
                }
            }

            return profiles;
        }

        public bool AddProfile(Profile profile)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO profiles " +
                    "(user_id, profile_name, basal_rate, min_blood_sugar, max_blood_sugar, min_warning_sugar, max_warning_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength) " +
                    "VALUES (@userId, @profileName, @basalRate, @min, @max, @minWarn, @maxWarn, @carb, @corr, @type, @strength)";

                SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@userId", profile.UserId);
                command.Parameters.AddWithValue("@profileName", profile.ProfileName);
                command.Parameters.AddWithValue("@basalRate", profile.BasalRate);
                command.Parameters.AddWithValue("@min", profile.MinBloodSugar);
                command.Parameters.AddWithValue("@max", profile.MaxBloodSugar);
                command.Parameters.AddWithValue("@minWarn", profile.MinWarningSugar);
                command.Parameters.AddWithValue("@maxWarn", profile.MaxWarningSugar);
                command.Parameters.AddWithValue("@carb", profile.CarbRatio);
                command.Parameters.AddWithValue("@corr", profile.CorrectionRatio);
                command.Parameters.AddWithValue("@type", profile.InsulinType);
                command.Parameters.AddWithValue("@strength", profile.InsulinStrength);
                int numRows = command.ExecuteNonQuery();

                if (numRows == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool UpdateProfile(Profile profile)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "UPDATE profiles SET profile_name = @profileName, basal_rate = @basalRate, min_blood_sugar = @min, max_blood_sugar = @max, min_warning_sugar = @minWarn," +
                    " max_warning_sugar = @maxWarn," +
                    " carb_ratio = @carb, correction_ratio = @corr, insulin_type = @type, insulin_strength = @strength" +
                    " WHERE profile_id = @profileId";

                SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@profileId", profile.ProfileId);
                command.Parameters.AddWithValue("@profileName", profile.ProfileName);
                command.Parameters.AddWithValue("@basalRate", profile.BasalRate);
                command.Parameters.AddWithValue("@min", profile.MinBloodSugar);
                command.Parameters.AddWithValue("@max", profile.MaxBloodSugar);
                command.Parameters.AddWithValue("@minWarn", profile.MinWarningSugar);
                command.Parameters.AddWithValue("@maxWarn", profile.MaxWarningSugar);
                command.Parameters.AddWithValue("@carb", profile.CarbRatio);
                command.Parameters.AddWithValue("@corr", profile.CorrectionRatio);
                command.Parameters.AddWithValue("@type", profile.InsulinType);
                command.Parameters.AddWithValue("@strength", profile.InsulinStrength);
                int numRows = command.ExecuteNonQuery();

                if (numRows == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool DeleteProfile(int profileId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM profiles WHERE profile_id = @profileId";

                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@profileId", profileId);

                int numRows = command.ExecuteNonQuery();

                if (numRows == 0)
                {
                    return false;
                }
                return true;
            }
        }

        private Profile GetProfileFromReader(SqlDataReader reader)
        {
            Profile profile = new Profile();
            profile.ProfileId = Convert.ToInt32(reader["profile_id"]);
            profile.ProfileName = Convert.ToString(reader["profile_name"]);
            profile.UserId = Convert.ToInt32(reader["user_id"]);
            profile.BasalRate = Convert.ToDecimal(reader["basal_rate"]);
            profile.MinBloodSugar = Convert.ToInt32(reader["min_blood_sugar"]);
            profile.MaxBloodSugar = Convert.ToInt32(reader["max_blood_sugar"]);
            profile.MinWarningSugar = Convert.ToInt32(reader["min_warning_sugar"]);
            profile.MaxWarningSugar = Convert.ToInt32(reader["max_warning_sugar"]);
            profile.CarbRatio = Convert.ToInt32(reader["carb_ratio"]);
            profile.CorrectionRatio = Convert.ToInt32(reader["correction_ratio"]);
            profile.InsulinType = Convert.ToString(reader["insulin_type"]);
            profile.InsulinStrength = Convert.ToInt32(reader["insulin_strength"]);

            return profile;
        }
    }
}
