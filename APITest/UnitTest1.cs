using System;
using System.Collections.Generic;
using Xunit;
using RestSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace APITest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                {"ulogin", "art1613122"},
                {"upassword", "505558545"}
            };

            IRestResponse response = APIHelper.SendJsonApiRequest("https://my.soyuz.in.ua/index.php", "Content-Type", "application/x-www-form-urlencoded", body);
            var cookie = APIHelper.ExtractCookie(response);

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://my.soyuz.in.ua");

            foreach (Cookie cookies in APIHelper.ExtractCookie(response))
            {
                driver.Manage().Cookies.AddCookie(cookies);
            }

            driver.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");

            driver.Navigate().Refresh(); 
            System.Threading.Thread.Sleep(10000);
            //Assert.Equal("200", response.StatusCode.ToString());
        }
    }
}
