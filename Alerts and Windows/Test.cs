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
            // This creates a refrence point to the main window in the form of a string.
            String parentWindow = driver.CurrentWindowHandle;           
            driver.FindElement(By.Id("footer_term")).Click();
            
            foreach (string window in driver.WindowHandles)
            {
                // This switchs control to the child window. 
                driver.SwitchTo().Window(window);                       
            }
            Assert.True(driver.Url.Contains("term"));

            // This closes the child window.
            driver.Close();                                            
            driver.SwitchTo().Window(parentWindow);
            Assert.True(driver.Title.Contains("Home"));
        }

        [Test]
        public void shouldBeAbleToClosePopup()
        {
            driver.FindElement(By.Id("contact_link")).Click();
            // Popup shopuld be triggered now. 
            driver.FindElement(By.Id("submit_message")).Click();         

            IAlert popup = driver.SwitchTo().Alert();
            // Verify Popup
            Assert.True(popup.Text.Contains("empty"));

            // Accept Popup
            popup.Accept();
            // Verify that we are back on the main page of the screen. 
            Assert.True(driver.Title.Contains("Contact"));

  

            // *** Fill out data fields. *** 
            driver.FindElement(By.Name("name_field")).SendKeys("Bob Smith");
            driver.FindElement(By.Name("address_field")).SendKeys(" 123 Elm street");
            driver.FindElement(By.Name("postcode_field")).SendKeys("04101");
            driver.FindElement(By.Name("email_field")).SendKeys("bob.smith@email.com");
            driver.FindElement(By.Id("submit_message")).Click();
            // Verify the submit with confirmation page. 
            Assert.True(driver.Url.Contains("contact_confirm"));



        }
    }
}
