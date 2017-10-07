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
            driver.Quit();
        }
        [Test]
        public void shouldOpenTermsWindowAndCloseIt()
        {
            String parentWindow = driver.CurrentWindowHandle;           //This creates a refrence point to teh main window in the form of a string.
            driver.FindElement(By.Id("footer_term")).Click();
            
            foreach (string window in driver.WindowHandles)
            {
                driver.SwitchTo().Window(window);                       // This switchs control to the child window. 
            }
            Assert.True(driver.Url.Contains("term"));

            driver.Close();                                            // This closes the child window. 
            driver.SwitchTo().Window(parentWindow);
            Assert.True(driver.Title.Contains("Home"));
        }

        [Test]
        public void shouldBeAbleToClosePopup()
        {
            driver.FindElement(By.Id("contact_link")).Click();
            driver.FindElement(By.Id("submit_message")).Click();          //Popup shopuld be triggered now. 

            IAlert popup = driver.SwitchTo().Alert();
            Assert.True(popup.Text.Contains("empty"));                  //Verify Popup

            popup.Accept();                                             // Accept Popup
            Assert.True(driver.Title.Contains("Contact"));                //Verify that we are back on the maine page of the screen. 

        }
    }
}
