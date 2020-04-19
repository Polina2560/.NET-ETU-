using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using DL.Entities;
using System.Linq;
using BL.Interfaces;


namespace BLTest
{
    class EFHomeworkRepository
    {

        private List<Homework> HomeworkStTch = new List<Homework> {
            new Homework() {HomeworkMark = 4, HomeworkDate = new DateTime(2019, 04, 30),
                    HomeworkTeacher =  new Teacher() { TeacherId = 1, TeacherName = "Fedor Vasiliev" },
                    HomeworkStudent = new Student() { StudentId = 1, StudentName = "Ivam Ivanov" }},
                new Homework() { HomeworkMark = 5, HomeworkDate = new DateTime(2019, 04, 28),
                    HomeworkTeacher =  new Teacher() { TeacherId = 1, TeacherName = "Fedor Vasiliev" },
                    HomeworkStudent = new Student() { StudentId = 2, StudentName = "Petr Petriv" } },
               new Homework() { HomeworkMark = 3, HomeworkDate = new DateTime(2019, 04, 17),
                    HomeworkTeacher =  new Teacher() { TeacherId = 2, TeacherName = "Larisa Urievich" },
                    HomeworkStudent = new Student() { StudentId = 3, StudentName = "Evkakii Epapovich" } }
        };

        private List<Homework> HomeworkSt = new List<Homework> {
            new Homework() {HomeworkMark = 4, HomeworkDate = new DateTime(2019, 04, 30),
                    HomeworkStudent = new Student() { StudentId = 1, StudentName = "Ivam Ivanov" }},
                new Homework() { HomeworkMark = 5, HomeworkDate = new DateTime(2019, 04, 28),
                    HomeworkStudent = new Student() { StudentId = 2, StudentName = "Petr Petriv" } },
               new Homework() { HomeworkMark = 3, HomeworkDate = new DateTime(2019, 04, 17),
                    HomeworkStudent = new Student() { StudentId = 3, StudentName = "Evkakii Epapovich" } }
        };

        private List<Homework> HomeworkTch = new List<Homework> {
            new Homework() {HomeworkMark = 4, HomeworkDate = new DateTime(2019, 04, 30),
                    HomeworkTeacher =  new Teacher() { TeacherId = 1, TeacherName = "Fedor Vasiliev" } },
                new Homework() { HomeworkMark = 5, HomeworkDate = new DateTime(2019, 04, 28),
                    HomeworkTeacher =  new Teacher() { TeacherId = 1, TeacherName = "Fedor Vasiliev" } },
               new Homework() { HomeworkMark = 3, HomeworkDate = new DateTime(2019, 04, 17),
                    HomeworkTeacher =  new Teacher() { TeacherId = 2, TeacherName = "Larisa Urievich" } }
        };

        private List<Homework> Homework = new List<Homework> {
            new Homework() {HomeworkMark = 4, HomeworkDate = new DateTime(2019, 04, 30)},
                new Homework() { HomeworkMark = 5, HomeworkDate = new DateTime(2019, 04, 28)},
               new Homework() { HomeworkMark = 3, HomeworkDate = new DateTime(2019, 04, 17)}
        };

        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(false, true)]
        public void GetAllHomeworksTest(bool includeStudents, bool includeTeachers)
        {
            var moq = new Mock<IHomeworkRepository>();
            moq.Setup(a => a.GetAllHomeworks(false, false)).Returns(Homework);
            moq.Setup(a => a.GetAllHomeworks(true, true)).Returns(HomeworkStTch);
            moq.Setup(a => a.GetAllHomeworks(false, true)).Returns(HomeworkTch);
            moq.Setup(a => a.GetAllHomeworks(true, false)).Returns(HomeworkSt);

            IEnumerable<Homework> realResult = moq.Object.GetAllHomeworks(includeStudents, includeTeachers);

            IEnumerable<Homework> expected;

            if (includeStudents)
            {
                if (includeTeachers)
                    expected = HomeworkStTch;
                else
                    expected = HomeworkSt;
            }
            else
            {
                if (includeTeachers)
                    expected = HomeworkTch;
                else
                    expected = Homework;
            }
 
            Assert.AreEqual(realResult, expected);
        }

        [TestCase(1, true, false)]
        [TestCase(2, false, true)]
        public void GetStudentFromHomeworkTest(int homeworkId, bool includeStudents, bool includeTeachers)
        {
            var moq = new Mock<IHomeworkRepository>();
            moq.Setup(a => a.GetHomeworkById(homeworkId, false, false)).Returns(Homework.ElementAt(homeworkId - 1));
            moq.Setup(a => a.GetHomeworkById(homeworkId, true, true)).Returns(HomeworkStTch.ElementAt(homeworkId - 1));
            moq.Setup(a => a.GetHomeworkById(homeworkId, false, true)).Returns(HomeworkTch.ElementAt(homeworkId - 1));
            moq.Setup(a => a.GetHomeworkById(homeworkId, true, false)).Returns(HomeworkSt.ElementAt(homeworkId - 1));

            Homework realResult = moq.Object.GetHomeworkById(homeworkId, includeStudents, includeTeachers);

            Homework expected;

            if (includeStudents)
            {
                if (includeTeachers)
                    expected = HomeworkStTch.ElementAt(homeworkId - 1);
                else
                    expected = HomeworkSt.ElementAt(homeworkId - 1);
            }
            else
            {
                if (includeTeachers)
                    expected = HomeworkTch.ElementAt(homeworkId - 1);
                else
                    expected = Homework.ElementAt(homeworkId - 1);
            }

            Assert.AreEqual(realResult, expected);
        }


        [TestCase(1)]
        [TestCase(3)]
        public void GetStudentFromHomeworkTest(int homeworkId)
        {
            var moq = new Mock<IHomeworkRepository>();
            moq.Setup(a => a.GetStudentFromHomework(homeworkId)).Returns(HomeworkSt.ElementAt(homeworkId - 1).HomeworkStudent);

            Student realResult = moq.Object.GetStudentFromHomework(homeworkId);
            var expected = HomeworkSt.ElementAt(homeworkId - 1).HomeworkStudent;

            Assert.AreEqual(realResult, expected);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void GetTeacherFromHomeworkTest(int homeworkId)
        {
            var moq = new Mock<IHomeworkRepository>();
            moq.Setup(a => a.GetTeacherFromHomework(homeworkId)).Returns(HomeworkTch.ElementAt(homeworkId - 1).HomeworkTeacher);

            Teacher realResult = moq.Object.GetTeacherFromHomework(homeworkId);
            var expected = HomeworkTch.ElementAt(homeworkId - 1).HomeworkTeacher;

            Assert.AreEqual(realResult, expected);
        }

    }
}
