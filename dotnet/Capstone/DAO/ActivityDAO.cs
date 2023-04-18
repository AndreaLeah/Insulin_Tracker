using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

namespace Capstone.DAO
{
    public class ActivityDAO : IActivityDAO
    {
        private readonly string connectionString;

        public ActivityDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Activity> GetAllActivity(int userId)
        {
            List<Activity> activities = new List<Activity>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT log_id, user_id, activity_name, time " +
                    "FROM activity " +
                    "WHERE user_id = @userId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Activity activity = GetProfileFromReader(reader);
                        activities.Add(activity);
                    }
                }

            }

            return activities;

        }

        public bool AddActivityToLog(Activity activity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO activity (user_id, activity_name, time) " +
                    "VALUES (@userId, @activityName, @time)";

                SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@userId", activity.UserId);
                command.Parameters.AddWithValue("@activityName", activity.ActivityName);
                command.Parameters.AddWithValue("@time", activity.Time);

                int numRows = command.ExecuteNonQuery();

                if (numRows == 0)
                {
                    return false;
                }
                return true;
            }
        }
        private Activity GetProfileFromReader(SqlDataReader reader)
        {
            Activity activity = new Activity();
            
            activity.LogId = Convert.ToInt32(reader["log_id"]);
            activity.UserId = Convert.ToInt32(reader["user_id"]);
            activity.ActivityName = Convert.ToString(reader["activity_name"]);
            activity.Time = Convert.ToDateTime(reader["time"]);

            return activity;
        }
    }

    }

