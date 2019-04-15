using MySql.Data.MySqlClient;
using BigRedButton.DataAccess.PrivateData;

namespace BigRedButton.DataAccess
{
    public static class DatabaseConnector
    {
        // @TODO - Comment me
        private static MySqlConnectionStringBuilder _mySqlConnection;

        static DatabaseConnector()
        {
            if (_mySqlConnection == null)
            {
                /* DEVELOPER NOTE:
                 * 
                 * PrivateConnectionData.cs is not checked into source control for security reasons.
                 * To maintain functionality, cloned repositories will have to implement this class.
                 * Template included in PrivateData folder as PrivateConnectionDataTemplate.cs file.
                 * See template file for additional instructions.
                 */

                _mySqlConnection = new MySqlConnectionStringBuilder
                {
                    Server = PrivateConnectionData.Server,
                    UserID = PrivateConnectionData.UserId,
                    Password = PrivateConnectionData.Password,
                    Database = PrivateConnectionData.Database,
                    CharacterSet = PrivateConnectionData.CharacterSet,
                    SslMode = PrivateConnectionData.SslMode,
                    ConnectionTimeout = PrivateConnectionData.ConnectionTimeout
                };
            }
        }

        public static string GetMySqlConnectionString()
        {
            return _mySqlConnection.GetConnectionString(true);
        }
    }
}
