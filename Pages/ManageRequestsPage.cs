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

        public void SendSkillSwapRequestAndReceivedRequest(string searchSkill, string selectSeller, string selectSkill, string messageToSeller, string Emailaddress, string Password, string Sender, string Email, string Pass)
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
            Thread.Sleep(3000);
            IWebElement date = Driver.FindElement(By.XPath("//*[@id=\"sent-request-section\"]/div[2]/div[1]/table/tbody/tr[1]/td[7]"));
            Thread.Sleep(2000);
            if (recipient.Text == selectSeller && date.Text == sentDate)
            {
                Console.WriteLine($"Warning: Recipient '{selectSeller}' has matching name, use full name for clarity. Date '{sentDate}' is correct.");
            }
            else
            {
                Console.WriteLine($"Warning: Recipient '{selectSeller}' has matching name, use full name for clarity. Date '{sentDate}' is incorrect.");
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
            IWebElement SentRequestFrom = Driver.FindElement(By.XPath("//*[@id=\"received-request-section\"]/div[2]/div[1]/table/tbody/tr[1]/td[4]/a"));
            if (SentRequestFrom.Text.Contains(Sender))
            {
                Console.WriteLine($"Request received from {Sender}");
            }
            else
            {
                Console.WriteLine($"Request not received from expected sender: {Sender}, got: {SentRequestFrom.Text}");
            }
            Thread.Sleep(2000);
            IWebElement acceptButton = Driver.FindElement(By.XPath("//button[@type='button' and @class='ui primary basic button' and text()='Accept']"));
            acceptButton.Click();
            Thread.Sleep(5000);
            IWebElement completeButton = Driver.FindElement(By.XPath("//button[@type='button' and @class='ui positive basic button' and text()='Complete']"));
            completeButton.Click();
            Thread.Sleep(5000);
            IWebElement siignOutButton = Driver.FindElement(By.XPath("//button[@class='ui green basic button' and text()='Sign Out']"));
            siignOutButton.Click();
            Thread.Sleep(2000);
            IWebElement siigninButton = Driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));
            siigninButton.Click();
            Thread.Sleep(2000);

            IWebElement eemailAddressTextbox = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[1]/input"));
            eemailAddressTextbox.SendKeys(Email);

            IWebElement paasswordTextbox = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[2]/input"));
            paasswordTextbox.SendKeys(Pass);

            IWebElement looginButton = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button"));
            looginButton.Click();
            Thread.Sleep(5000);
            IWebElement hitony = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));
            if (hitony.Text == "Hi Tony")
            {
                Console.WriteLine("User has logged in successfully. Test Passed!");
            }
            else
            {
                Console.WriteLine("User has not logged in. Test Failed!");
            }
            IWebElement maanageRequest = Driver.FindElement(By.XPath("//div[@class='ui dropdown link item' and @tabindex='0']"));
            maanageRequest.Click();
            Thread.Sleep(2000);
            IWebElement seentRequests = Driver.FindElement(By.XPath("//a[@class='item' and @href='/Home/SentRequest']"));
            seentRequests.Click();
            Thread.Sleep(2000);
            IWebElement completedButton = Driver.FindElement(By.XPath("//button[@type='button' and @class='ui positive basic button' and text()='Completed']"));
            completedButton.Click();
            Thread.Sleep(5000);
        }
    }
}
