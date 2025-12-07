using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class ChangePasswordPage : CommonDriver
    {
        

        private IWebElement currentPassword => Driver.FindElement(By.XPath("//input[@name='oldPassword']"));
        private IWebElement newPassword => Driver.FindElement(By.XPath("//input[@placeholder='New Password']"));
        private IWebElement confirmPassword => Driver.FindElement(By.XPath("//input[@placeholder='Confirm Password']"));

        private IWebElement saveButton => Driver.FindElement(By.XPath("//button[@class='ui button ui teal button' and text()='Save']"));
        private IWebElement signOutButton => Driver.FindElement(By.XPath("//button[@class='ui green basic button' and text()='Sign Out']"));
        private IWebElement signinButton => Driver.FindElement(By.XPath("//a[@class='item' and text()='Sign In']"));
        private IWebElement emailAddressTextbox => Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[1]/input"));
        private IWebElement passwordTextbox => Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[2]/input"));
        private IWebElement loginButton => Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button"));
        private IWebElement hitony => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));
        public void CreateNewPassword(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            currentPassword.SendKeys(CurrentPassword);
            Thread.Sleep(2000);
            newPassword.SendKeys(NewPassword);
            Thread.Sleep(2000);
            confirmPassword.SendKeys(ConfirmPassword);
            Thread.Sleep(2000);
            saveButton.Click();
            Wait.WaitToBeVisible(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            IWebElement popupAlert = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
            string alertText = popupAlert.Text;
            Console.WriteLine("Alert text: " + alertText);

        }
        public void VerifySigninwithNewPassword(string NewPassword)
        {
            Thread.Sleep(2000);
            signOutButton.Click();
            Thread.Sleep(3000);
            signinButton.Click();
            emailAddressTextbox.SendKeys();
        }
    }
}
