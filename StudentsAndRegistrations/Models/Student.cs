using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAndRegistrations.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime RegistrationDateAndTime { get; set; }
        public virtual List<Course> Registrations { get; set; }
    }
    public class AddStudent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
    public class Update
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
    public class RegisterForCourse
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
