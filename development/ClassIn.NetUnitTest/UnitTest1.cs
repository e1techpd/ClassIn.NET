using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Beyova.Api.RestApi;
using EF.E1Technology.EEO;
using Beyova;

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
    }
}
