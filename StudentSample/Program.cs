using StudentSample.Connection;
using StudentSample.Controllers;
using StudentSample.DAO;

using Microsoft.Extensions.Configuration;


ConfigurationBuilder builder = new ConfigurationBuilder();
builder.SetBasePath(AppContext.BaseDirectory);
builder.AddJsonFile("appsettings.json");
builder.AddEnvironmentVariables();


IConfiguration config = builder.Build();

IConnection conn = new PostgresConnection(config);
StudentDAO s = new StudentDAO(conn);
StudentController sc = new StudentController(s);

sc.menu();