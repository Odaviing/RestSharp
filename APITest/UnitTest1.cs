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
            //IWebDriver driver = new ChromeDriver();
            //driver.Navigate().GoToUrl("https://imgbb.com");
            var client = new RestClient("https://imgbb.com");
            client.Timeout = -1;

            //System.Threading.Thread.Sleep(10000);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("content", "/GitArchive/RNA_growthspiral_sebmckinnon.jpg");
            IRestResponse response = client.Execute(request);
            
        }

        [Fact]
        public void Test3()
        {
            //IWebDriver driver = new ChromeDriver();
            //driver.Navigate().GoToUrl("https://imgbb.com");
            var client = new RestClient("https://odaviing.github.io/?src=images/2ad438c7f23a2cc6de8f143bce687bc7.jpg");
            client.Timeout = -1;
            //System.Threading.Thread.Sleep(10000);
            //var request = new RestRequest(Method.POST);
            //request.AddFile("", "/GitArchive/RNA_growthspiral_sebmckinnon.jpg");
            //IRestResponse response = client.Execute(request);
            var request2 = new RestRequest(Method.GET);
            byte[] final = client.DownloadData(request2);
            File.WriteAllBytes(Path.Combine("/GitArchive", "test.jpg"), final);
        }
    }
}
