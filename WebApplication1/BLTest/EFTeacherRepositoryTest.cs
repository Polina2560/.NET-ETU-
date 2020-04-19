using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using DL.Entities;
using System.Linq;
using BL.Interfaces;

namespace BLTest
{
    class EFTeacherRepositoryTest
    {
        // without Homework
        private List<Teacher> teachersNoHW = new List<Teacher>(){
                new Teacher() { TeacherId = 1, TeacherName = "Fedor Vasiliev" },
                new Teacher() { TeacherId = 2, TeacherName = "Larisa Urievich" }
        };

        // with Homework
        private List<Teacher> teachersHW = new List<Teacher>(){
                new Teacher() { TeacherId = 1, TeacherName = "Fedor Vasiliev", Homeworks = new List<Homework>{
                    new Homework(){ HomeworkMark = 4, HomeworkDate = new DateTime(2019, 04, 30)} ,
                    new Homework(){ HomeworkMark = 5, HomeworkDate = new DateTime(2019, 04, 28) } } },
                new Teacher() { TeacherId = 2, TeacherName = "Larisa Urievich", Homeworks = new List<Homework>{
                    new Homework() { HomeworkMark = 3, HomeworkDate = new DateTime(2019, 04, 17)} } }

        };

        [TestCase(true)]
        [TestCase(false)]
        public void GetAllTeachersTest(bool includeHomework)
        {
            var moq = new Mock<ITeacherRepository>();
            moq.Setup(a => a.GetAllTeachers(false)).Returns(teachersNoHW);
            moq.Setup(a => a.GetAllTeachers(true)).Returns(teachersHW);

            IEnumerable<Teacher> realResult = moq.Object.GetAllTeachers(includeHomework);

            IEnumerable<Teacher> expected;
            if (includeHomework)
                expected = teachersHW;
            else
                expected = teachersNoHW;

            Assert.AreEqual(realResult, expected);
        }

        [TestCase(1, true)]
        [TestCase(2, false)]
        public void GetTeacherByIdTest(int teacherId, bool includeHomeworks)
        {
            var moq = new Mock<ITeacherRepository>();
            moq.Setup(a => a.GetTeacherById(teacherId, false)).Returns(teachersNoHW.ElementAt(teacherId - 1));
            moq.Setup(a => a.GetTeacherById(teacherId, true)).Returns(teachersHW.ElementAt(teacherId - 1));

            Teacher realResult = moq.Object.GetTeacherById(teacherId, includeHomeworks);

            Teacher expected;
            if (includeHomeworks)
                expected = teachersHW.ElementAt(teacherId - 1);
            else
                expected = teachersNoHW.ElementAt(teacherId - 1);

            Assert.AreEqual(realResult, expected);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void GetTeacherHomeworksTest(int teacherId)
        {
            var moq = new Mock<ITeacherRepository>();
            moq.Setup(a => a.GetTeacherHomeworks(teacherId)).Returns(teachersHW.ElementAt(teacherId - 1).Homeworks);

            List<Homework> realResult = moq.Object.GetTeacherHomeworks(teacherId);
            var expected = teachersHW.ElementAt(teacherId - 1).Homeworks;

            Assert.AreEqual(realResult, expected);
        }

    }
}
