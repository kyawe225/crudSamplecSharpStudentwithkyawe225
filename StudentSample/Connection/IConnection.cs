using System;
using Npgsql;

namespace StudentSample.Connection;

public interface IConnection
{
	public void Open();
	public void Close();
	public NpgsqlCommand CreateCommand(string query);
}