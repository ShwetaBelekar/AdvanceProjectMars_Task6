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
        private IWebElement hiTony => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));

        private IWebElement changePassword => Driver.FindElement(By.XPath("//a[@class='item' and text()='Change Password']"));
        // WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(2));
        //wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".ui.mini.modal.transition.visible.active")));

        private IWebElement currentPassword => Driver.FindElement(By.XPath("//input[@name='oldPassword']"));
        private IWebElement newPassword => Driver.FindElement(By.XPath("//input[@placeholder='New Password']"));
        private IWebElement confirmPassword => Driver.FindElement(By.XPath("//input[@placeholder='Confirm Password']"));

        private IWebElement saveButton => Driver.FindElement(By.XPath("//button[@class='ui button ui teal button' and text()='Save']"));

    }
}
