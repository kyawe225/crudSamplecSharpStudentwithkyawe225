using System;
using Npgsql;
namespace StudentSample.Connection
{
    public class PostgresConnection : IConnection
    {
        private NpgsqlConnection? connection;
        private NpgsqlConnectionStringBuilder builder;
        public PostgresConnection() {
            builder = new NpgsqlConnectionStringBuilder() {
                Host="localhost",
                Port=5432,
                Database="aspstudentcrud",
                Username="postgres",
                Password="kyaw"
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

