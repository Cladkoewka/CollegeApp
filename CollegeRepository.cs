using CollegeApp.Models;

namespace CollegeApp
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student
            { Id = 1, Name = "Student 1", Email = "student1mail.com", Address = "Sinichkina 2k1a" },
            new Student
            { Id = 2, Name = "Student 2", Email = "student2mail.com", Address = "Sovetskay 84" },
        };
    }
}
