using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RestSharpDemo;
using RestSharpDemo.HelperClass;
using AventStack.ExtentReports;

namespace APITests
{
    [TestClass]
    public class RegressionTests
    {
        public TestContext TestContext { get; set; } //this actually tracks the test case status.

        [ClassInitialize]//will run before the class gets executed
        public static void Setup(TestContext testContext)
        {
            var dir = testContext.TestRunDirectory;
            Reporter.SetUpExtentReport("API Regression Tests", "API Regression test Reports", dir);
        }

        [TestInitialize]//Will run before each time test method execution
        public void SetupTest()
        {
            Reporter.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            var testStatus = TestContext.CurrentTestOutcome;
            Status status;

            switch (testStatus)
            {
                case UnitTestOutcome.Failed:
                    status = Status.Fail;
                    Reporter.TestStatus(status.ToString());
                    break;
                case UnitTestOutcome.Inconclusive:
                    break;
                case UnitTestOutcome.Passed:
                    status = Status.Pass;
                    break;
                case UnitTestOutcome.InProgress:
                    break;
                case UnitTestOutcome.Error:
                    break;
                case UnitTestOutcome.Timeout:
                    break;
                case UnitTestOutcome.Aborted:
                    break;
                case UnitTestOutcome.Unknown:
                    break;
                case UnitTestOutcome.NotRunnable:
                    break;
                default:
                    break;
            }
        }


        //once execution is done, we need to flush so that It can write in the HTML report
        [ClassCleanup]
        public static void CleanUp()
        {
            Reporter.FlushReport();
        }

        [TestMethod]
        public void VerifyListOfUsers()
        {
            var demo = new Demo<ListOfUsersDTO>();
            var user =  demo.GetUsers("api/users?page=2");
            Assert.AreEqual(2, user.Page);
            Reporter.LogToReport(Status.Fail, "Page does not match number");
            Assert.AreEqual("Michael", user.Data[0].first_name);
            Reporter.LogToReport(Status.Fail, "User First name does not match");
        }

        [TestMethod]
        public void CreateNewUser()
        {
            //for creating new user, we need payload/request body
            string payload = @"{
                                 ""name"": ""Mike"",
                                 ""job"": ""Team leader""
                                }";

            //need to create object of this new API class
            var demo = new Demo<CreateUserRequestDTO>();
            var user = demo.CreateUser("api/users", payload);
            
            Assert.AreEqual("Mike", user.Name);
            Assert.AreEqual("Team leader", user.Job);

            var demoGetUsers = new Demo<ListOfUsersDTO>();
            var userGetUsers = demo.GetUsers("api/users?page=2");
            Assert.AreEqual(2, userGetUsers.Page);
            Assert.AreEqual("Michael", userGetUsers.Data[0].first_name);


        }
    }
}
