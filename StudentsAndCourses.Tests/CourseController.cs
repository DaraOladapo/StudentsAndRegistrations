using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StudentsAndCourses.Library.Interfaces;
using StudentsAndCourses.Library.Models.Binding;
using StudentsAndCourses.Library.Models.Entity;
using StudentsAndCourses.Library.Models.ViewModels;
using StudentsAndCourses.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StudentsAndCourses.Tests
{
    public class CourseControllerTest
    {
        private Mock<ILogger<CourseController>> _logger;
        private Mock<IRepositoryWrapper> mockRepo;
        private CourseController courseController;
        private AddCourse addCourse;
        private UpdateCourse updateCourse;
        private Course course;
        private List<Course> courses;
        public CourseControllerTest()
        {
            //mock setup
            course = new Course();
            courses = new List<Course>();

            //sample models
            addCourse = new AddCourse { Code = "CS101", Title = "Computing Basics" };
            updateCourse = new UpdateCourse { Code = "CS101", Title = "Understanding Computing Basics" };

            //controller setup
            //courseControllerMock = new Mock<ICourseController>();
            _logger = new Mock<ILogger<CourseController>>();
            var courseResultsMock = new Mock<IActionResult>();

            mockRepo = new Mock<IRepositoryWrapper>();
            var allCourses = GetCourses();
            courseController = new CourseController(_logger.Object, mockRepo.Object);
        }
        [Fact]
        public void AddCourse_Test()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Courses.FindByCondition(c => c.Id == It.IsAny<int>())).Returns(GetCourses());
            //Act
            var controllerActionResult = courseController.Post(addCourse);
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<CourseViewModel>>(controllerActionResult);
        }
        [Fact]
        public void GetAllCourses_Test()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Courses.FindAll()).Returns(GetCourses());
            mockRepo.Setup(repo => repo.Registrations.FindByCondition(r => r.CourseId == It.IsAny<int>())).Returns(GetRegistrations());
            //Act
            var controllerActionResult = courseController.Get();
            //Assert
            Assert.NotNull(controllerActionResult);
        }
        [Fact]
        public void Get_Course_by_ID()
        {
            mockRepo.Setup(repo => repo.Courses.FindByCondition(c => c.Id == It.IsAny<int>())).Returns(GetCourses());
            //mockRepo.Setup(repo => repo.Registrations.FindByCondition(c => c.Id == It.IsAny<int>()).Select(s => s.Student)).Returns(GetStudents());
            //Act
            var controllerActionResult = courseController.Get(1);
            //Assert
            Assert.NotNull(controllerActionResult);
            controllerActionResult.Should().BeOfType<ActionResult<CourseViewModel>>();
        }
        [Fact]
        public void UpdateCourse()
        {
            mockRepo.Setup(repo => repo.Courses.FindByCondition(c => c.Id == It.IsAny<int>())).Returns(GetCourses());
            var controllerActionResult = courseController.Put(1, updateCourse);
            Assert.NotNull(controllerActionResult);
        }
        [Fact]
        public void DeleteCourse_Test()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Courses.FindByCondition(r => r.Id == It.IsAny<int>())).Returns(GetCourses());
            mockRepo.Setup(repo => repo.Registrations.FindByCondition(c => c.Id == It.IsAny<int>())).Returns(GetRegistrations());
            mockRepo.Setup(repo => repo.Courses.Delete(GetCourse()));
            //Act
            var controllerActionResult = courseController.Delete(It.IsAny<int>());
            //Assert
            Assert.NotNull(controllerActionResult);
        }
        private IEnumerable<Course> GetCourses()
        {
            var courses = new List<Course> {
            new Course(){Id=1, Code="CS101", Title="Computing Basics"},
            new Course(){Id=2, Code="CS102", Title="Computing Intermediate"}
            };
            return courses;
        }
        private Course GetCourse()
        {
            return GetCourses().ToList()[0];
        }
        private IEnumerable<Registration> GetRegistrations()
        {
            return new List<Registration>() {
                new Registration { Id = 1, Course = GetCourses().ToList()[0] },
                new Registration { Id = 2, Course = GetCourses().ToList()[1] },
            };
        }
        //private Registration GetRegistration()
        //{
        //    return GetRegistrations().ToList()[0];
        //}
    }
}
