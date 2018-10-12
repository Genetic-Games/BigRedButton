using System;
using System.Text;

namespace BigRedButton.DataAccess
{
    static class HighScoreDataAccessor
    {
        private const string EndStatement = ";\n";

        private const string Database = "genegame_big_red_button";

        public const string CreateHighScoreTable = @"
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
        public static string GetHighScores(DateTime startDate, DateTime endDate)
        {
            var getHighScoreSql = new StringBuilder();

            getHighScoreSql.Append("DECLARE DATETIME @startDate = " + startDate.ToString() + EndStatement);
            getHighScoreSql.Append("DECLARE DATETIME @endDate = " + endDate.ToString() + EndStatement);

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
        /// Sets up the SQL for inserting a new high score in the database for a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="score"></param>
        /// <returns>SQL to use to perform this action</returns>
        public static string SetNewHighScore(string username, ulong score)
        {
            var setHighScoreSql = new StringBuilder();

            setHighScoreSql.Append("DECLARE BIGINT @score = " + score + EndStatement);
            setHighScoreSql.Append("DECLARE VARCHAR(30) @username = '" + username + EndStatement);
            setHighScoreSql.Append(@"
                DECLARE BIGINT @score = 
                INSERT INTO genegame_big_red_button.high_scores (
                    username, 
                    score_value
                )
                VALUES (
                    @username,
                    @score
                );");

            return setHighScoreSql.ToString();
        }
    }


}
