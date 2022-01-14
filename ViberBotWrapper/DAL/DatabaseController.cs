using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ViberBotWebApp.DAL.DbConnection;
using ViberBotWebApp.Helpers;
using ViberBotWebApp.Models;
using ViberBotWebApp.Models.DbModels;

namespace ViberBotWebApp.DAL
{
    public class DatabaseController
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public DatabaseController (IConfiguration configuration)
        {
            _configuration = configuration;

            ConnectionProvider provider = new(_configuration.GetConnectionString("connectionString"));
            _connection = provider.GetConnection();

        }
        public async Task SaveNewUserToDb(User user)
        {
            try
            {
                _connection.Open();

                SqlCommand sqlCommand = new();
                sqlCommand.Connection = _connection;
                sqlCommand.CommandText = "INSERT INTO Users VALUES (@userid, @name, @avatar, @country, @language, @api_ver, @subscribed)";

                sqlCommand.Parameters.Add(new SqlParameter("@userid", user.id));
                sqlCommand.Parameters.Add(new SqlParameter("@name", user.name));
                sqlCommand.Parameters.Add(new SqlParameter("@avatar", user.avatar));
                sqlCommand.Parameters.Add(new SqlParameter("@country", user.country));
                sqlCommand.Parameters.Add(new SqlParameter("@language", user.language));
                sqlCommand.Parameters.Add(new SqlParameter("@api_ver", user.api_version));
                sqlCommand.Parameters.Add(new SqlParameter("@subscribed", Convert.ToByte(user.subscribed)));

                await sqlCommand.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                var entry = DateTime.Now.ToString()+ ": " + ex.Message;
                await HelperActions.WriteToFile("viber_bot_exeption_log", entry);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task SaveResultToDb(MatchResult result)
        {
            try
            {
                _connection.Open();

                SqlCommand sqlCommand = new();
                sqlCommand.Connection = _connection;
                sqlCommand.CommandText = "INSERT INTO Results VALUES (@userid, @datetime, @opponent_name, @score, @op_score)";

                sqlCommand.Parameters.Add(new SqlParameter("@userid", result.PlayerId));
                sqlCommand.Parameters.Add(new SqlParameter("@datetime", result.Timestamp));
                sqlCommand.Parameters.Add(new SqlParameter("@opponent_name", result.OpponentName));
                sqlCommand.Parameters.Add(new SqlParameter("@score", result.Score));
                sqlCommand.Parameters.Add(new SqlParameter("@op_score", result.OpScore));

                await sqlCommand.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {
                var entry = DateTime.Now.ToString() + ": " + ex.Message;
                await HelperActions.WriteToFile("viber_bot_exeption_log", entry);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<int> GetUsersCount()
        {
            var userCount = 0;
            try
            {
                _connection.Open();

                SqlCommand sqlCommand = new();
                sqlCommand.Connection = _connection;
                sqlCommand.CommandText = "SELECT COUNT (UserId) FROM Users";

                userCount = (int)await sqlCommand.ExecuteScalarAsync();


            }
            catch (Exception ex)
            {
                await HelperActions.WriteToFile("viber_bot_exeption_log", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return userCount;
        }

    }
}
