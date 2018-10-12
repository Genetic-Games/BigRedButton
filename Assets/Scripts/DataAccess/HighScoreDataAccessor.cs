using System;
using System.Text;

namespace BigRedButton.DataAccess
{
    static class HighScoreDataAccessor
    {
        /// <summary>
        /// Database to use for connections
        /// @TODO - Put this in a more secure file and actually use it for these queries and connections
        /// </summary>
        private const string _database = "genegame_big_red_button";

        /// <summary>
        /// Sets up the SQL for creating the high score table if not already created
        /// </summary>
        public const string CreateHighScoreTableSql = @"
            CREATE TABLE IF NOT EXISTS genegame_big_red_button.high_scores (
                id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                username NVARCHAR(30) NOT NULL,
                score_value BIGINT UNSIGNED NOT NULL,
                score_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
            );";

        /// <summary>
        /// Sets up the SQL for getting high scores within a date range from the database
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>SQL to use to perform this action</returns>
        public static string GetHighScoresSql(DateTime startDate, DateTime endDate)
        {
            var getHighScoreSql = new StringBuilder();

            getHighScoreSql.AppendLine("DECLARE @startDate DATETIME DEFAULT " + startDate.ToString() + ";");
            getHighScoreSql.AppendLine("DECLARE @endDate DATETIME DEFAULT " + endDate.ToString() + ";");

            getHighScoreSql.Append(@"
                SELECT
                    username,
                    score_value
                FROM genegame_big_red_button.high_scores
                WHERE score_date BETWEEN @startDate AND @endDate
                ORDER BY
                    score_value DESC,
                    score_date ASC,
                    username ASC
                LIMIT 10;
            ");

            return getHighScoreSql.ToString();
        }

        /// <summary>
        /// Sets up the SQL for getting the maximum high score for a particular username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>SQL to use to perform this action</returns>
        public static string GetHighScoreForUserSql (string username)
        {
            var getHighScoreSql = new StringBuilder();

            getHighScoreSql.AppendLine("DECLARE @username VARCHAR(30) DEFAULT '" + username + "';");
            getHighScoreSql.Append(@"
                SELECT 
                    username, 
                    score_value
                FROM genegame_big_red_button.high_scores
                WHERE username = @username
                ORDER BY 
                    score_value DESC
                LIMIT 1;
            ");

            return getHighScoreSql.ToString();
        }

        /// <summary>
        /// Sets up the SQL for inserting a new high score in the database for a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="score"></param>
        /// <returns>SQL to use to perform this action</returns>
        public static string SetNewHighScoreSQL(string username, ulong score)
        {
            var setHighScoreSql = new StringBuilder();

            setHighScoreSql.AppendLine("DECLARE @score BIGINT DEFAULT " + score + ";");
            setHighScoreSql.AppendLine("DECLARE @username VARCHAR(30) DEFAULT '" + username + "';");
            setHighScoreSql.Append(@"
                INSERT INTO genegame_big_red_button.high_scores (
                    username, 
                    score_value
                )
                VALUES (
                    @username,
                    @score
                );
            ");

            return setHighScoreSql.ToString();
        }
    }


}
