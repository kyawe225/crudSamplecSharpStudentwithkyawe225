using System;
using Npgsql;
namespace StudentSample.Connection
{
    public class PostgresConnection : IConnection
    {
        private NpgsqlConnection connection;
        public void Open()
        {
            connection = new NpgsqlConnection("Host=localhost;port=5432;Database=aspstudentcrud;user id=postgres;password=kyaw");
            connection.Open();
        }
        public NpgsqlCommand CreateCommand(String query)
        {
            return new NpgsqlCommand(query,connection);
        }
        public void Close()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}

