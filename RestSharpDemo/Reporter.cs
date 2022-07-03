using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo
{
    public static class Reporter
    {
        public static ExtentReports extentReports;
        //public static ExtentReports extent;
        public static ExtentHtmlReporter htmlReporter;
        public static ExtentTest extentTest;

        public static void SetUpExtentReport(string reportName, string documentTitle, dynamic path)
        {
            htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Config.DocumentTitle = documentTitle;
            htmlReporter.Config.ReportName = reportName;

            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
            //extentReports = extent;
        }

        public static void CreateTest(string testName)
        {
            extentTest = extentReports.CreateTest(testName);
        }

        public static void LogToReport(Status status, string message)
        {
            extentTest.Log(status, message);
        }

        public static void FlushReport()
        {
            extentReports.Flush();
        }

        public static void TestStatus(string status)
        {
            if(status.Equals("Pass"))
            {
                extentTest.Pass("Test is passed");
            }
            else
            {
                extentTest.Pass("Test is failed");
            }
        }

    }
}
