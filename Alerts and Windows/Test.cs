using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace Alerts_and_Windows
{

    [TestFixture]
    class WebDriverTestAlertsandWindows
    {
        IWebDriver driver;
        [SetUp]
        public void setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://thetestroom.com/webapp");
        }
        [TearDown]
        public void teardown()
        {
            driver.Close();
        }
        [Test]
        public void shouldOpenTermsWindowAndCloseIt()
        {
            driver.FindElement(By.Id("footer_term")).Click();
            
        }

        [Test]
        public void shouldBeAbleToClosePopup()
        {

        }
    }
}
