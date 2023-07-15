using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
namespace StudentSample.Connection
{
    public class PostgresConnection : IConnection
    {
        private NpgsqlConnection? connection;
        private NpgsqlConnectionStringBuilder builder;
        public PostgresConnection(IConfiguration config) {
            //builder = new NpgsqlConnectionStringBuilder() {
            //    Host="localhost",
            //    Port=5432,
            //    Database="aspstudentcrud",
            //    Username="postgres",
            //    Password="kyaw"
            //};
            //DatabaseObject c=config.GetSection("DatabaseSetting:Host").Get<DatabaseObject>();
            builder = new NpgsqlConnectionStringBuilder()
            {
                Host = config.GetSection("DatabaseSetting:Host").Get<string>(),
                Port = config.GetSection("DatabaseSetting:Port").Get<int>(),
                Database = config.GetSection("DatabaseSetting:Database").Get<string>(),
                Username = config.GetSection("DatabaseSetting:Username").Get<string>(),
                Password = config.GetSection("DatabaseSetting:Password").Get<string>()
            };
        }
        public void Open()
        {
            connection = new NpgsqlConnection(builder.ToString());
            connection.Open();
        }
        public NpgsqlCommand CreateCommand(String query)
        {
            return new NpgsqlCommand(query,connection);
        }
        public void Close()
        {
            if (connection?.State != System.Data.ConnectionState.Closed)
            {
                connection?.Close();
            }
        }
    }
}

