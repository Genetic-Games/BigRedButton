using System.Text;

namespace BigRedButton.DataAccess
{
    static class HighScoreDataAccessor
    {
        private const string Database = "genegame_big_red_button";

        public const string CreateHighScoreTable = @"
            CREATE TABLE IF NOT EXISTS genegame_big_red_button.high_scores (
                id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                username NVARCHAR(30) NOT NULL,
                score_value BIGINT UNSIGNED NOT NULL,
                score_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
            );";

        public static string SetNewHighScore(string username, ulong score)
        {
            var setHighScoreSql = new StringBuilder();
            setHighScoreSql.Append(@"
                INSERT INTO genegame_big_red_button.high_scores (
                    username, 
                    score_value
                )
                VALUES (
                    ");

            setHighScoreSql.Append(username);
            setHighScoreSql.Append(@",
                    ");
            setHighScoreSql.Append(score);
            setHighScoreSql.Append(@"
                );");

            return setHighScoreSql.ToString();
        }
    }
}
