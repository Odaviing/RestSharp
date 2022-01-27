using System;
using System.Collections.Generic;
using Xunit;
using RestSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp.Extensions;
using System.IO;

namespace APITest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var body = new Dictionary<string, string>
            {
                { "ulogin", "art1613122"},
                { "upassword", "505558545"}
            };

            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/x-www-form-urlencoded"}
            };

            var response = APIHelper.SendJsonApiRequest(body, headers, "https://my.soyuz.in.ua", Method.POST);

            var cookie = APIHelper.ExtractCookie(response, "zbs_lang");
            var cookie2 = APIHelper.ExtractCookie(response, "ulogin");
            var cookie3 = APIHelper.ExtractCookie(response, "upassword");


            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://my.soyuz.in.ua");

            driver.Manage().Cookies.AddCookie(cookie);
            driver.Manage().Cookies.AddCookie(cookie2);
            driver.Manage().Cookies.AddCookie(cookie3);


            driver.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");

            System.Threading.Thread.Sleep(10000);
        }

        [Fact]
        public void Test2()
        {
            //var client = new RestClient("https://imgbb.com");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.RequestFormat = DataFormat.Json;
            //request.AddHeader("Content-Type", "multipart/form-data");
            //request.AddFile("content", "/Users/User/Documents/GitArchives/test.jpg");
            //IRestResponse response = client.Execute(request);
            //Assert.Equal("OK", response.StatusCode.ToString());
            var body = new Dictionary<string, string>
            {
                { "content", "/Users/User/Documents/GitArchives/test.jpg"} 
            };

            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "multipart/form-data"}
            };

            var response = APIHelper.SendJsonApiRequest(body, headers, "https://imgbb.com", Method.POST);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public void Test3()
        {
            byte[] content = APIHelper.SendJsonApiRequest("https://www.nutscomputergraphics.com/wp-content/uploads/2020/02/Seb_cover.jpg");
            File.WriteAllBytes(Path.Combine("/Users/User/Documents/GitArchives", "test3.jpg"), content);
        }
    }
}
