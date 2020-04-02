using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using dotnet_api_goat.Controllers;
using dotnet_api_goat.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System;

namespace dotnet_api_goat.Tests
{
    /// <summary>
    /// Summary description for TestGoatsController
    /// </summary>
    [TestClass]
    public class TestGoatsController
    {
        public TestGoatsController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetFile_ShouldReturnNotFound()
        {
            var controller = new GoatsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetFile("no_file.json");

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void GetFile_ShouldReturnFileContent()
        {
            var controller = new GoatsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetFile("dotnet-api-goat.Tests.dll.config");

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            string fileContent;
            Assert.IsTrue(response.TryGetContentValue<String>(out fileContent));
            Assert.IsTrue(fileContent.Contains("configuration"));
        }

        [TestMethod]
        public void GetCommand_ShouldReturnDirectoryListing()
        {
            var controller = new GoatsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetCommand("dir");

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            string commandOutput;
            Assert.IsTrue(response.TryGetContentValue<String>(out commandOutput));
            Assert.IsTrue(commandOutput.Contains("Directory of C:\\"));
        }

        [TestMethod]
        public void GetEcho_ShouldReturnHtml()
        {
            var controller = new GoatsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetEcho("hello");

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            string htmlOutput;
            Assert.IsTrue(response.TryGetContentValue<String>(out htmlOutput));
            Assert.IsTrue(htmlOutput.Equals("<p>You sent this: hello</p>"));
        }
    }
}