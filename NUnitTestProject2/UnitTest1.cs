using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace NUnitTestProject1
{
    public class Tests
    {
        IWebDriver driver;


        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            
            options.AddArgument("--disable-notifications");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://5karmanov.ru/");
        }


        [Test]
        public void Filter()
        {
            driver.FindElement(By.XPath("//*[contains(@class,'link-inner-block')][1]")).Click();
            driver.FindElement(By.XPath("//*[contains(@class,'bx_filter_parameters_box_title title rounded3 box-shadow-sm')]")).Click();
            driver.FindElement(By.XPath("//*[contains(@class,'min-price')]")).SendKeys("1500");
            driver.FindElement(By.XPath("//*[contains(@class,'max-price')]")).SendKeys("3500");
            driver.FindElement(By.XPath("//*[contains(@class,'bx_filter_parameters_box_title title rounded3 box-shadow-sm')]")).Click();

            var webPrices = driver.FindElements(By.CssSelector("price_value"));

            int[] actualPrices = webPrices.Select(webPrice => Int32.Parse(webPrice.Text.Trim())).ToArray();
            actualPrices.ToList().ForEach(price => Assert.IsTrue(price >= 1500 && price <= 3500));
        }
        [Test]
       public void Add()
        {   
            driver.FindElement(By.XPath("//*[contains(@class,'link-inner-block')][1]")).Click();
            driver.FindElement(By.XPath("//*[contains(@class,'icons-basket-wrapper offer_buy_block ce_cmp_hidden')][1]")).Click();
            driver.FindElement(By.XPath("//*[contains(@id,'basket_form')]"));//
        }

        [TearDown]
        public void CleanUp()
        {
            //driver.Quit();
        }
    }
}