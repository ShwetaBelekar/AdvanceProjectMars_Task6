using AdvanceProjectMars_Task6.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class ManageRequestsPage : CommonDriver
    {
        private IWebElement signInButton => Driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));

        private IWebElement emailAddressTextbox => Driver.FindElement(By.XPath("//input[@placeholder='Email address']"));

        private IWebElement passwordTextbox => Driver.FindElement(By.XPath("//input[@placeholder='Password']"));

        private IWebElement loginButton => Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button"));

        public void SendSkillSwapRequestAndReceivedRequest(string searchSkill, string selectSeller, string selectSkill, string messageToSeller, string Emailaddress, string Password, string Sender)
        {
            IWebElement searchSkillsSearchIcon = Driver.FindElement(By.XPath("(//i[@class='search link icon'])[1]"));
            searchSkillsSearchIcon.Click();
            Thread.Sleep(2000);
            IWebElement searchSkills = Driver.FindElement(By.XPath("(//input[@placeholder='Search skills'])[2]"));
            searchSkills.SendKeys(searchSkill + Keys.Enter);
            Thread.Sleep(2000);
            IWebElement selectListing = Driver.FindElement(By.XPath("//*[@id=\"service-search-section\"]/div[2]/div/section/div/div[2]/div/div[2]/div/div/div/div[1]/a[2]"));
            Console.WriteLine($"Selected {selectListing.Text}");
            selectListing.Click();
            IWebElement messageToSellerTextbox = Driver.FindElement(By.XPath("//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[1]/textarea"));
            messageToSellerTextbox.SendKeys(messageToSeller);
            Thread.Sleep(3000);
            string sentDate = DateTime.Now.ToString("dd MMM, yyyy");
            IWebElement requestButton = Driver.FindElement(By.XPath("//div[@class='ui teal  button']"));
            requestButton.Click();
            Thread.Sleep(2000);
            IWebElement yesButton = Driver.FindElement(By.XPath("//button[@class='ui button ui teal button' and text()='Yes']"));
            yesButton.Click();

            Wait.WaitToBeClickable(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            IWebElement popupAlert = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
            if (popupAlert.Text == "Request sent")
            {

                Console.WriteLine($"SkillSwap request sent successfully on {sentDate}");
            }
            else
            {
                Console.WriteLine("SkillSwap request not sent");
            }
            IWebElement manageRequest = Driver.FindElement(By.XPath("//div[@class='ui dropdown link item' and @tabindex='0']"));
            manageRequest.Click();
            Thread.Sleep(2000);
            IWebElement sentRequests = Driver.FindElement(By.XPath("//a[@class='item' and @href='/Home/SentRequest']"));
            sentRequests.Click();
            Thread.Sleep(2000);
            IWebElement skillswaprequestsent = Driver.FindElement(By.XPath("//*[@id=\"sent-request-section\"]/div[2]/div[1]/table/tbody/tr[1]/td[2]/a"));
            //skillswaprequestsent.Click(); 
            if (skillswaprequestsent.Text.Contains(selectSkill))
            {
                Console.WriteLine($"Selected listing is correct: {selectSkill}");
            }
            else
            {
                Console.WriteLine($"Selected listing is incorrect. Looking for: {selectSkill}");
            }
            Thread.Sleep(3000);

            IWebElement recipient = Driver.FindElement(By.XPath("//*[@id=\"sent-request-section\"]/div[2]/div[1]/table/tbody/tr[1]/td[4]/a"));
            IWebElement date = Driver.FindElement(By.XPath("//*[@id=\"sent-request-section\"]/div[2]/div[1]/table/tbody/tr[1]/td[7]"));
            Thread.Sleep(2000);
            if (recipient.Text == selectSeller && date.Text == sentDate)
            {
                Console.WriteLine("Recipient name should be full name because if there are two seller with same first name then it can be confusing and date is not correct");
            }
            else
            {
                Console.WriteLine("Recipient name should be full name because if there are two seller with same first name then it can be confusing and date are correct");
            }

            IWebElement signOutButton = Driver.FindElement(By.XPath("//button[@class='ui green basic button' and text()='Sign Out']"));
            signOutButton.Click();
            Thread.Sleep(2000);
            IWebElement signinButton = Driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));
            signinButton.Click();
            Thread.Sleep(2000);

            IWebElement emailAddressTextbox = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[1]/input"));
            emailAddressTextbox.SendKeys(Emailaddress);

            IWebElement passwordTextbox = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[2]/input"));
            passwordTextbox.SendKeys(Password);

            IWebElement loginButton = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button"));
            loginButton.Click();
            Thread.Sleep(5000);
            IWebElement hilock = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));

            if (hilock.Text == "Hi Lock")
            {
                Console.WriteLine("User has logged in successfully. Test Passed!");
            }
            else
            {
                Console.WriteLine("User has not logged in. Test Failed!");
            }
            Thread.Sleep(4000);
            IWebElement manageRequests = Driver.FindElement(By.XPath("//div[@class='ui dropdown link item' and @tabindex='0']"));
            manageRequests.Click();
            Thread.Sleep(3000);
            IWebElement receivedRequests = Driver.FindElement(By.XPath("//a[@class='item' and @href='/Home/ReceivedRequest']"));
            receivedRequests.Click();
            Thread.Sleep(3000);
            IWebElement sentrequest = Driver.FindElement(By.XPath("//*[@id=\"received-request-section\"]/div[2]/div[1]/table/tbody/tr[1]/td[4]/a"));
            if (sentrequest.Text.Contains(Sender))
            {
                Assert.Pass("Request received from Tony");
            }
            else
            {
                Assert.Fail("Request not received");
            }

        }
    }
}
