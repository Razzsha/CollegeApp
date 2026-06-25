namespace CollegeApp.Models
{
    public static class CollegeRepo
    {
        public static List<Student> Students { get; set; } = new List<Student>() {
                new Student
                {
                    id = 1,
                StudentName = "Student 1",
                Email = "student1@email.com",
                Address = "Brj, Nepal"
            },
                 new Student
                {
                     id=2,
                StudentName = "Student 2",
                Email = "student1@email.com",
                Address = "Npj, Nepal"
            },
        };
    }
}
