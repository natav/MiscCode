using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace NUnitMeetingSkillAPI
{

    [TestFixture, Explicit, Category("Integration Tests")] // Explicit attribute causes test fixture to be ignored unless it is explicitly selected for running, like via GUI. Done to prevent running integration tests by the CI.
    public class IntegrationTests
    {
        private IntegrationTestsLib _lib;

        [OneTimeSetUp]
        public void Setup()
        {
            _lib = new IntegrationTestsLib();
        }

        [Test]
        [TestCase("https://webapi.legistar.com/v1/jason/Events?$filter=EventDate+ge+datetime%272020-04-13%27&$top=4", "2020-04-13", "", 4, TestName = "{m}: Get_RequestUrl_1")]
        [TestCase("https://webapi.legistar.com/v1/jason/Events?$filter=EventDate+ge+datetime%272020-04-14%27+and+EventBodyId+eq+139&$top=10", "202-04-14", "138", 10, TestName = "{m}: Get_RequestUrl_2")]
        public void Get_RequestUrl(string expectedUrl, string datetime, string bodyId, int recordsToReturn)
        {
            string actualUrl = _lib.Get_RequestUrl(Convert.ToDateTime(datetime), bodyId, recordsToReturn);

            if (actualUrl.Length > 0)
            {
                Assert.AreEqual(expectedUrl, actualUrl);
            }
        }

        [Test]
        public void Get_Event()
        {
            string eventResponseString = _lib.GetEvents("millwaukee");

            Assert.IsTrue(eventResponseString.Length != 0);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _lib = null; // destroy the _lib
        }
    }
}
