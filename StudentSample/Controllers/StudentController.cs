using StudentSample.DAO;
using StudentSample.Export;
using StudentSample.Models;

namespace StudentSample.Controllers
{
	public class StudentController
	{
		private readonly StudentDAO student;
		public StudentController(StudentDAO dao)
		{
			student = dao;
		}
		public void insert()
		{
			Console.Write("Enter Name:");
			string? name = Console.ReadLine();
			Console.Write("Enter Score:");
			int i = Convert.ToInt16(Console.ReadLine());
			StudentPartial partial = new StudentPartial
			{
				name = name != null ? name : "",
				score = i
			};
			bool success=student.insert(partial);
			if (success)
			{
				Console.WriteLine("created Successfully");
				return;
			}
            Console.WriteLine("created not Successfully");
        }
        public void update()
		{
            Console.Write("Enter Roll Number:");
            int id = Convert.ToInt16(Console.ReadLine());
            Console.Write("Enter Name:");
            string? name = Console.ReadLine();
            Console.Write("Enter Score:");
            int i = Convert.ToInt16(Console.ReadLine());
            StudentPartial partial = new StudentPartial
            {
                name = name != null ? name : "",
                score = i
            };
            bool success = student.update(partial,id);
            if (success)
            {
                Console.WriteLine("updated Successfully");
                return;
            }
            Console.WriteLine("updated not Successfully");
        }
		public void delete()
		{
            Console.Write("Enter Roll Number:");
            int i = Convert.ToInt16(Console.ReadLine());
            bool success = student.delete(i);
            if (success)
            {
                Console.WriteLine("deleted Successfully");
                return;
            }
            Console.WriteLine("deleted not Successfully");
        }
		public void list()
		{
            IEnumerable<Student> models=student.getAll();
            Console.WriteLine($"No \t Name \t Score");
            foreach (Student s in models)
            {
                Console.WriteLine($"{s.no} \t {s.name} \t {s.score}");
            }
		}
        public void exportExcel()
        {

            StudentExportExcel excel = new StudentExportExcel(student);
            excel.GenerateExcel(AppContext.BaseDirectory+"/student"+DateTime.Now.ToString("yyyymmddhis.fffff")+".xlsx");
            Console.WriteLine("Excel Generated !");
        }
        public void menu()
        {
            int temp = -1;
            while (temp != 0)
            {
                try
                {
                    Console.WriteLine("1 : List");
                    Console.WriteLine("2 : Insert");
                    Console.WriteLine("3 : Update");
                    Console.WriteLine("4 : Delete");
                    Console.WriteLine("5 : Export");
                    Console.WriteLine("0 : Quit");
                    temp = Convert.ToInt16(Console.ReadLine());
                    switch (temp)
                    {
                        case 1:
                            list();
                            break;
                        case 2:
                            insert();
                            break;
                        case 3:
                            update();
                            break;
                        case 4:
                            delete();
                            break;
                        case 5:
                            exportExcel();
                            break;
                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace?.ToString());
                    Console.WriteLine("Please Write a integer");
                    continue;
                }
                
            }
        }
    }
}

