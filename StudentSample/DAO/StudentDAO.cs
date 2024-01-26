using System;
using System.Xml.Linq;
using Npgsql;
using StudentSample.Connection;
using StudentSample.Models;

namespace StudentSample.DAO
{
    public class StudentDAO
    {
        private IConnection con;
        public StudentDAO(IConnection conn)
        {
            con = conn;
        }
        public IEnumerable<Student> getAll()
        {
            con.Open();
            string sql = "select * from students;";
            NpgsqlCommand command = con.CreateCommand(sql);
            NpgsqlDataReader reader = command.ExecuteReader();
            ICollection<Student> students = new List<Student>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Student s = new Student()
                    {
                        no = reader.GetInt16(0),
                        name = reader.GetString(1),
                        score = reader.GetInt32(2)
                    };
                    students.Add(s);
                }

            }
            reader.Close();
            con.Close();
            return students;
        }
        public bool insert(StudentPartial partial)
        {
            con.Open();
            string sql = "insert into students(name,score) values(@name,@score);";
            NpgsqlCommand command = con.CreateCommand(sql);
            command.Parameters.AddWithValue("name", partial.name);
            command.Parameters.AddWithValue("score", partial.score);
            int rows = command.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            con.Close();
            return false;

        }
        public bool update(StudentPartial partial, int id)
        {
            con.Open();
            string sql = "update students set name=@name,score=@score where no = @id;";
            NpgsqlCommand command = con.CreateCommand(sql);
            command.Parameters.AddWithValue("name", partial.name);
            command.Parameters.AddWithValue("score", partial.score);
            command.Parameters.AddWithValue("id", id);
            int rows = command.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            con.Close();
            return false;

        }
        public bool delete(int id)
        {
            con.Open();
            string sql = "delete from students where no = @id;";
            NpgsqlCommand command = con.CreateCommand(sql);
            command.Parameters.AddWithValue("id", id);
            int rows = command.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            con.Close();
            return false;

        }
    }
}

