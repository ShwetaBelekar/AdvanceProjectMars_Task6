using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class LoginPage : CommonDriver
    {
        private IWebElement signinButton => Driver.FindElement(By.XPath("//a[@class='item' and text()='Sign In']"));
        private IWebElement emailAddressTextbox => Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[1]/input"));
        private IWebElement passwordTextbox => Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[2]/input"));
        private IWebElement loginButton => Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button"));
        private IWebElement hitony => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));
        public void LoginActions()
        {

            //driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("http://localhost:5003/Home");
            Driver.Manage().Window.Maximize();
            Thread.Sleep(3000);

          
            signinButton.Click();
            Thread.Sleep(2000);

            
            emailAddressTextbox.SendKeys("moneytony@ymail.com");

           
            passwordTextbox.SendKeys("Tonymoney@2025");

            
            loginButton.Click();
            Thread.Sleep(5000);
        }

        public void VerifyUserInHomePage()
        {

            

            if (hitony.Text == "Hi Tony")
            {
                Console.WriteLine("User has logged in successfully. Test Passed!");
            }
            else
            {
                Console.WriteLine("User has not logged in. Test Failed!");
            }




        }
    }
}
