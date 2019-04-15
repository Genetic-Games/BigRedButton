using BigRedButton.DataAccess;
using MySql.Data.MySqlClient;
using System;
using UnityEngine;

namespace BigRedButton.DataControllers
{
    public class HighScoreController
    {
        // @TODO - Comment me
        private static string _connectionString = _connectionString ?? DatabaseConnector.GetMySqlConnectionString();

        public bool SetHighScoreForUser(string username, long highScore)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var sql = HighScoreDataAccessor.SetNewHighScoreSQL;
                    using (var command = new MySqlCommand(sql))
                    {
                        command.Connection = connection;
                        connection.Open();
                        var rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // @TODO - Catch errors better here
                if (Debug.isDebugBuild)
                {
                    Debug.LogException(ex);
                }
                return false;
            }
            return true;
        }
    }
}
