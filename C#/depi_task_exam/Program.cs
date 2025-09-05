using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text;

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

class Student : Person
{
    public int Level { get; set; }
    public double GPA { get; set; }
}

class Teacher : Person
{
    public string Subject { get; set; }
}

class Exam
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public DateTime Date { get; set; }
}

class Result
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public double Score { get; set; }
}


class StudentRepository
{
    public List<Student> students = new List<Student>();
    public void Add(Student s) => students.Add(s);
    public List<Student> GetAll() => students;
}

class TeacherRepository
{
    public List<Teacher> teachers = new List<Teacher>();
    public void Add(Teacher t) => teachers.Add(t);
    public List<Teacher> GetAll() => teachers;
}

class ExamRepository
{
    public List<Exam> exams = new List<Exam>();
    public void Add(Exam e) => exams.Add(e);
    public List<Exam> GetAll() => exams;
}

class ResultRepository
{
    public List<Result> results = new List<Result>();
    public void Add(Result r) => results.Add(r);
    public List<Result> GetAll() => results;
}


class Program
{
    static void Main()
    {
        StudentRepository studentRepo = new StudentRepository();
        TeacherRepository teacherRepo = new TeacherRepository();
        ExamRepository examRepo = new ExamRepository();
        ResultRepository resultRepo = new ResultRepository();

        while (true)
        {
            Console.WriteLine("\n===== Student Exam Management System =====");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Teacher");
            Console.WriteLine("3. View/Search/Sort Students");
            Console.WriteLine("4. Add Exam");
            Console.WriteLine("5. Record Result");
            Console.WriteLine("6. Show Student Grades");
            Console.WriteLine("7. Top 3 Students in Subject");
            Console.WriteLine("8. Save Data");
            Console.WriteLine("9. Load Data");
            Console.WriteLine("10. Export CSV");
            Console.WriteLine("11. Exit");
            Console.Write("Choose option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input! Enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1: // Add Student
                    int id;
                    while (true)
                    {
                        Console.Write("Enter Student Id: ");
                        if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                            break;
                        Console.WriteLine("Invalid Id! Must be a positive number.");
                    }

                    string name;
                    while (true)
                    {
                        Console.Write("Enter Name: ");
                        name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                            break;
                        Console.WriteLine("Name cannot be empty!");
                    }

                    string email;
                    while (true)
                    {
                        Console.Write("Enter Email: ");
                        email = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                            break;
                        Console.WriteLine("Invalid Email!");
                    }

                    int level;
                    while (true)
                    {
                        Console.Write("Enter Level: ");
                        if (int.TryParse(Console.ReadLine(), out level) && level > 0)
                            break;
                        Console.WriteLine("Level must be a positive number!");
                    }

                    double gpa;
                    while (true)
                    {
                        Console.Write("Enter GPA: ");
                        if (double.TryParse(Console.ReadLine(), out gpa) && gpa >= 0 && gpa <= 4)
                            break;
                        Console.WriteLine("GPA must be between 0 and 4!");
                    }

                    studentRepo.Add(new Student { Id = id, Name = name, Email = email, Level = level, GPA = gpa });
                    Console.WriteLine("Student Added!");
                    break;

                case 2: // Add Teacher
                    int tid;
                    while (true)
                    {
                        Console.Write("Enter Teacher Id: ");
                        if (int.TryParse(Console.ReadLine(), out tid) && tid > 0)
                            break;
                        Console.WriteLine("Invalid Id! Must be a positive number.");
                    }

                    string tname;
                    while (true)
                    {
                        Console.Write("Enter Name: ");
                        tname = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(tname))
                            break;
                        Console.WriteLine("Name cannot be empty!");
                    }

                    string temail;
                    while (true)
                    {
                        Console.Write("Enter Email: ");
                        temail = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(temail) && temail.Contains("@"))
                            break;
                        Console.WriteLine("Invalid Email!");
                    }

                    string subject;
                    while (true)
                    {
                        Console.Write("Enter Subject: ");
                        subject = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(subject))
                            break;
                        Console.WriteLine("Subject cannot be empty!");
                    }

                    teacherRepo.Add(new Teacher { Id = tid, Name = tname, Email = temail, Subject = subject });
                    Console.WriteLine("Teacher Added!");
                    break;

                case 3: // View/Search/Sort Students
                    Console.WriteLine("\n--- Students List ---");
                    var all = studentRepo.GetAll();
                    var sorted = all.OrderBy(s => s.Name); // LINQ sort by name
                    foreach (var st in sorted)
                        Console.WriteLine($"{st.Id} - {st.Name} - Level {st.Level} - GPA {st.GPA}");
                    break;

                case 4: // Add Exam
                    int eid;
                    while (true)
                    {
                        Console.Write("Enter Exam Id: ");
                        if (int.TryParse(Console.ReadLine(), out eid) && eid > 0)
                            break;
                        Console.WriteLine("Invalid Id! Must be a positive number.");
                    }

                    string esub;
                    while (true)
                    {
                        Console.Write("Enter Subject: ");
                        esub = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(esub))
                            break;
                        Console.WriteLine("Subject cannot be empty!");
                    }

                    DateTime date;
                    while (true)
                    {
                        Console.Write("Enter Date (yyyy-mm-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out date))
                            break;
                        Console.WriteLine("Invalid date format!");
                    }

                    examRepo.Add(new Exam { Id = eid, Subject = esub, Date = date });
                    Console.WriteLine("Exam Added!");
                    break;

                case 5: // Record Result
                    int rid;
                    while (true)
                    {
                        Console.Write("Enter Result Id: ");
                        if (int.TryParse(Console.ReadLine(), out rid) && rid > 0)
                            break;
                        Console.WriteLine("Invalid Id! Must be a positive number.");
                    }

                    int rsid;
                    while (true)
                    {
                        Console.Write("Enter Student Id: ");
                        if (int.TryParse(Console.ReadLine(), out rsid) && studentRepo.students.Any(s => s.Id == rsid))
                            break;
                        Console.WriteLine("Student Id does not exist!");
                    }

                    int reid;
                    while (true)
                    {
                        Console.Write("Enter Exam Id: ");
                        if (int.TryParse(Console.ReadLine(), out reid) && examRepo.exams.Any(e => e.Id == reid))
                            break;
                        Console.WriteLine("Exam Id does not exist!");
                    }

                    double score;
                    while (true)
                    {
                        Console.Write("Enter Score: ");
                        if (double.TryParse(Console.ReadLine(), out score) && score >= 0 && score <= 100)
                            break;
                        Console.WriteLine("Score must be between 0 and 100!");
                    }

                    resultRepo.Add(new Result { Id = rid, StudentId = rsid, ExamId = reid, Score = score });
                    Console.WriteLine("Result Recorded!");
                    break;

                case 6: // Show Student Grades
                    int sid;
                    while (true)
                    {
                        Console.Write("Enter Student Id: ");
                        if (int.TryParse(Console.ReadLine(), out sid) && studentRepo.students.Any(s => s.Id == sid))
                            break;
                        Console.WriteLine("Student Id does not exist!");
                    }

                    var results = resultRepo.GetAll().Where(r => r.StudentId == sid);
                    foreach (var r in results)
                    {
                        var exam = examRepo.GetAll().FirstOrDefault(e => e.Id == r.ExamId);
                        Console.WriteLine($"Exam: {exam.Subject}, Score: {r.Score}");
                    }
                    break;

                case 7: // Top 3 Students in Subject
                    string sub;
                    while (true)
                    {
                        Console.Write("Enter Subject: ");
                        sub = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(sub))
                            break;
                        Console.WriteLine("Subject cannot be empty!");
                    }

                    var query = from r in resultRepo.GetAll()
                                join e in examRepo.GetAll() on r.ExamId equals e.Id
                                join s in studentRepo.GetAll() on r.StudentId equals s.Id
                                where e.Subject == sub
                                orderby r.Score descending
                                select new { s.Name, r.Score };

                    foreach (var item in query.Take(3))
                        Console.WriteLine($"{item.Name} - {item.Score}");
                    break;

                case 8: // Save Data JSON
                    File.WriteAllText("students.json", JsonSerializer.Serialize(studentRepo.GetAll()));
                    File.WriteAllText("teachers.json", JsonSerializer.Serialize(teacherRepo.GetAll()));
                    File.WriteAllText("exams.json", JsonSerializer.Serialize(examRepo.GetAll()));
                    File.WriteAllText("results.json", JsonSerializer.Serialize(resultRepo.GetAll()));
                    Console.WriteLine("Data Saved!");
                    break;

                case 9: // Load Data JSON
                    if (File.Exists("students.json"))
                        studentRepo.students = JsonSerializer.Deserialize<List<Student>>(File.ReadAllText("students.json"));
                    if (File.Exists("teachers.json"))
                        teacherRepo.teachers = JsonSerializer.Deserialize<List<Teacher>>(File.ReadAllText("teachers.json"));
                    if (File.Exists("exams.json"))
                        examRepo.exams = JsonSerializer.Deserialize<List<Exam>>(File.ReadAllText("exams.json"));
                    if (File.Exists("results.json"))
                        resultRepo.results = JsonSerializer.Deserialize<List<Result>>(File.ReadAllText("results.json"));
                    Console.WriteLine("Data Loaded!");
                    break;

                case 10: // Export CSV
                    StringBuilder csv = new StringBuilder();
                    csv.AppendLine("StudentId,ExamId,Score");
                    foreach (var r in resultRepo.GetAll())
                        csv.AppendLine($"{r.StudentId},{r.ExamId},{r.Score}");
                    File.WriteAllText("results.csv", csv.ToString());
                    Console.WriteLine("CSV Exported!");
                    break;

                case 11: // Exit
                    return;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
}

