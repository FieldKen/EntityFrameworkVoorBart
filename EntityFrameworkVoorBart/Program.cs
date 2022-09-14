using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

/*Console.Write("Student name: ");
string studentName = Console.ReadLine();

Console.Write("Student age: ");
int studentAge = Convert.ToInt32(Console.ReadLine());

Console.Write("Student address: ");
string studentAddress = Console.ReadLine();

using (MyDataContext ctx = new MyDataContext())
{
    ctx.Students.Add(new Student(studentName, studentAge, studentAddress));
    ctx.SaveChanges();
}*/


/*using (MyDataContext ctx = new MyDataContext())
{
    Course informaticabeheer5 = new Course("Informaticabeheer", 5);
    ctx.Courses.Add(informaticabeheer5);
    ctx.Students.Add(new Student("Ken", 26, "Straat") { Course = informaticabeheer5 });
    ctx.Students.Add(new Student("Bart", 37, "Straat2", informaticabeheer5));
    ctx.SaveChanges();
}*/

/*using (MyDataContext ctx = new MyDataContext())
{
    ctx.Students.Add(new Student("Michiel", 36, "Programmer Lane 5")
    {
        Course = ctx.Courses.FirstOrDefault(c => c.Id == 2)
    });
    ctx.SaveChanges();
}*/



using (MyDataContext ctx = new MyDataContext())
{
    foreach (var item in ctx.Students.Join(ctx.Courses,
            s => s.CourseId,
            c => c.Id,
            (s, c) => new { s, c }))
    {
        Console.WriteLine($"{item.s.Name}:");
        Console.WriteLine($"* Age: {item.s.Age}");
        Console.WriteLine($"* Address: {item.s.Address}");
        Console.WriteLine($"* Course: {item.c.Grade} {item.c.Name}");
        Console.WriteLine("---");
    }
    Console.WriteLine();
}


public class MyDataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MijnAllerEersteDatabase;Trusted_Connection=True;");
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
}

public class Student
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    public Course Course { get; set; }
    public int CourseId { get; set; }

    public Student(string name, int age, string address)
    {
        Name = name;
        Age = age;
        Address = address;
    }

    public Student(string name, int age, string address, Course course) : this(name, age, address)
    {
        Course = course;
    }
}

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Grade { get; set; }
    public ICollection<Student> Students { get; set; }

    public Course(string name, int grade)
    {
        Name = name;
        Grade = grade;
    }
}