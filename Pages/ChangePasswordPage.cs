using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class ChangePasswordPage : CommonDriver
    {

        private IWebElement hiTony => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));

        private IWebElement changePassword => Driver.FindElement(By.XPath("//a[@class='item' and text()='Change Password']"));

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
          

        }
        public void VerifySigninwithNewPassword(string Emailaddress, string NewPassword)
        {
            Thread.Sleep(2000);
            signOutButton.Click();
            Thread.Sleep(3000);
            signinButton.Click();
            emailAddressTextbox.SendKeys(Emailaddress);
            Thread.Sleep(2000);
            passwordTextbox.SendKeys(NewPassword);
            Thread.Sleep(2000);
            loginButton.Click();
            Thread.Sleep(4000);
            if (hitony.Text == "Hi Tony")
            {
                Console.WriteLine("User has logged in successfully. Test Passed!");
            }
            else
            {
                Console.WriteLine("User has not logged in. Test Failed!");
            }

        }
        public void ChangePasswordtoOriginal(string NewCurrentPassword, string OriginalNewPassword, string OriginalConfirmPassword)
        {

            Thread.Sleep(2000);

            hiTony.Click();
            Thread.Sleep(3000);
            changePassword.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".ui.mini.modal.transition.visible.active")));
            currentPassword.SendKeys(NewCurrentPassword);
            Thread.Sleep(2000);
            newPassword.SendKeys(OriginalNewPassword);
            Thread.Sleep(2000);
            confirmPassword.SendKeys(OriginalConfirmPassword);
            Thread.Sleep(2000);
            saveButton.Click();
           
        }
    }
}
