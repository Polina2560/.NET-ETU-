using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using DL.Entities;
using System.Linq;
using BL.Interfaces;

namespace BLTest
{
    class EFStudentContractTest
    {
        // without Homework
        private List<Student> studentsNoHW = new List<Student>(){
                new Student() { StudentId = 1, StudentName = "Ivam Ivanov" },
                new Student() { StudentId = 2, StudentName = "Petr Petriv" },
                new Student() { StudentId = 3, StudentName = "Evkakii Epapovich" }
        };

        // with Homework
        private List<Student> studentsHW = new List<Student>(){
                new Student() { StudentId = 1, StudentName = "Ivam Ivanov", Homeworks = new List<Homework>{
                    new Homework(){ HomeworkMark = 4, HomeworkDate = new DateTime(2019, 04, 30)} } },
                new Student() { StudentId = 2, StudentName = "Petr Petriv", Homeworks = new List<Homework>{
                    new Homework(){ HomeworkMark = 5, HomeworkDate = new DateTime(2019, 04, 28) } } },
                new Student() { StudentId = 3, StudentName = "Evkakii Epapovich",  Homeworks = new List<Homework>{
                    new Homework() { HomeworkMark = 3, HomeworkDate = new DateTime(2019, 04, 17)} } }

        };

        [TestCase(true)]
        [TestCase(false)]
        public void GetAllStudentsTest(bool includeHomework)
        {
            var moq = new Mock<IStudentContract>();
            moq.Setup(a => a.GetAllStudents(false)).Returns(studentsNoHW);
            moq.Setup(a => a.GetAllStudents(true)).Returns(studentsHW);

            IEnumerable<Student> realResult = moq.Object.GetAllStudents(includeHomework);

            IEnumerable<Student> expected;
            if (includeHomework)
                expected = studentsHW;
            else
                expected = studentsNoHW;

            Assert.AreEqual(realResult, expected);
        }

        [TestCase(1, true)]
        [TestCase(2, false)]
        public void GetStudentByIdTest(int studentId, bool includeHomeworks)
        {
            var moq = new Mock<IStudentContract>();
            moq.Setup(a => a.GetStudentById(studentId, false)).Returns(studentsNoHW.ElementAt(studentId-1));
            moq.Setup(a => a.GetStudentById(studentId, true)).Returns(studentsHW.ElementAt(studentId - 1));

            Student realResult = moq.Object.GetStudentById(studentId, includeHomeworks);

            Student expected;
            if (includeHomeworks)
                expected = studentsHW.ElementAt(studentId - 1);
            else
                expected = studentsNoHW.ElementAt(studentId - 1);

            Assert.AreEqual(realResult, expected);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void GetStudentHomeworksTest(int studentId)
        {
            var moq = new Mock<IStudentContract>();
            moq.Setup(a => a.GetStudentHomeworks(studentId)).Returns(studentsHW.ElementAt(studentId - 1).Homeworks);

            List<Homework> realResult = moq.Object.GetStudentHomeworks(studentId);
            var expected = studentsHW.ElementAt(studentId - 1).Homeworks;

            Assert.AreEqual(realResult, expected);
        }

        [TestCase(0, false)]
        public void GetStudentByIdTestExeption(int homeworkId, bool includeStudents)
        {
            var moq = new Mock<IStudentContract>();
            moq.Setup(a => a.GetStudentById(homeworkId, includeStudents)).Throws<ArgumentException>();

            Assert.Throws<ArgumentException>(() =>
            {
                moq.Object.GetStudentById(homeworkId, includeStudents);
            });
        }

        [TestCase(0)]
        public void GetStudentHomeworksTestTestExeption(int studentId)
        {
            var moq = new Mock<IStudentContract>();
            moq.Setup(a => a.GetStudentHomeworks(studentId)).Throws<ArgumentException>();

            Assert.Throws<ArgumentException>(() =>
            {
                moq.Object.GetStudentHomeworks(studentId);
            });
        }


        [TestCase(null)]
        public void SaveStudentTestExeption(Student homework)
        {
            var moq = new Mock<IStudentContract>();
            moq.Setup(a => a.SaveStudent(homework)).Throws<ArgumentException>();

            Assert.Throws<ArgumentException>(() =>
            {
                moq.Object.SaveStudent(homework);
            });
        }

        [TestCase(null)]
        public void DeleteStudentTestExeption(Student homework)
        {
            var moq = new Mock<IStudentContract>();
            moq.Setup(a => a.DeleteStudent(homework)).Throws<ArgumentException>();

            Assert.Throws<ArgumentException>(() =>
            {
                moq.Object.DeleteStudent(homework);
            });
        }
    }
}
