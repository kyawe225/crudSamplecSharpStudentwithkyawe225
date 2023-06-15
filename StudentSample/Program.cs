using StudentSample.Connection;
using StudentSample.Controllers;
using StudentSample.DAO;

IConnection conn = new PostgresConnection();
StudentDAO s = new StudentDAO(conn);
StudentController sc = new StudentController(s);

sc.menu();
