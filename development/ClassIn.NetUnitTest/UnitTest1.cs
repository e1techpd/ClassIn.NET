using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Beyova.Api.RestApi;
using EF.E1Technology.EEO;
using Beyova;
using System.Text;

namespace EF.E1Technology.EEO.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        static EEORestApiClient client;

        [TestInitialize]
        public void Prepare()
        {
            string sid = System.Configuration.ConfigurationSettings.AppSettings["SID"];
            string secret = System.Configuration.ConfigurationSettings.AppSettings["Secret"];

            client = new EEORestApiClient(sid, secret);
        }

        [TestMethod]
        public void GetUserCourseListUnitTest()
        {
            var courseList = client.GetUserCourseList("23740020110");
            courseList.CheckNullOrEmptyCollection(nameof(courseList));
        }

        [TestMethod]
        public void GetStudentList()
        {
            var studentList = client.GetStudentList();
            studentList.CheckNullOrEmptyCollection(nameof(studentList));
        }

        [TestMethod]
        public void Register()
        {
            string number = (new Random()).CreateRandomNumberString(8);
            var telephone = string.Format("189{0}",number);
            var nickname = "ladybug";
            var password = "";
            var md5password = "Init1234!".ToMD5String(Encoding.UTF8);
            var FileData = "";
            var studentUID = client.Register(telephone,nickname,password,md5password,FileData);
            studentUID.CheckNullObject(nameof(studentUID));

        }

        [TestMethod]
        public void AddTeacher()
        {
            var teacherAccount = string.Format("199{0}",(new Random()).CreateRandomNumberString(8));
            var teacherName = "Newton Wilson";
            var FileData = "";
            var teacherUID = client.AddTeacher(teacherAccount,teacherName,FileData);
            teacherUID.CheckNullObject(nameof(teacherUID));
        }

        [TestMethod]
        public void AddCourse()
        {
            Console.WriteLine("ABC");
        }

        [TestMethod]
        public void AddCourseClass()
        {

        }

        [TestMethod]
        public void AddCourseClassMultiple()
        {

        }

        [TestMethod]
        public void AddClassStudentMultiple()
        {

        }
    }
}
