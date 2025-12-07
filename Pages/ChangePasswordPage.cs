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
    }
}
