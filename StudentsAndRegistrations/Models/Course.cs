using System.Collections.Generic;

namespace StudentsAndRegistrations.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public virtual List<Student> Registrations { get; set; }
    }
    public class AddCourse
    {
        public string Title { get; set; }
        public Level Level { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
    public enum Level
    {
        Basic = 1,
        Intermediate,
        Advanced,
        Expert
    }
    public class UpdateCourse
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}