using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassIn.Net;
using System.Collections.Generic;

namespace ClassIn.NetUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetUserCourseList()
        {
            string sid = "2343898";
            string secret = "yZXleTSR";
            string action = "getUserCourseList";
            string methodType = "POST";

            Dictionary<string, string> criteria = new Dictionary<string, string>();
            criteria.Add("userAccount", "23740020110");
            //criteria.Add("beginTime", "");
            //criteria.Add("endTime", "");

            var result = EEOClientApi.CallEEOApi(sid,
                                    secret,
                                    action,
                                    methodType,
                                    criteria
                                    );

            Assert.IsNotNull(result,"Get User Couse List correct.");
        }
    }
}
