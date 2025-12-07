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
    public class HomeToChangePasswordPage : CommonDriver
    {
        private IWebElement hiTony => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));

        private IWebElement changePassword => Driver.FindElement(By.XPath("//a[@class='item' and text()='Change Password']"));
       
        public void NavigateToChangePassword()
        {

            Thread.Sleep(2000);

          hiTony.Click();
            changePassword.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".ui.mini.modal.transition.visible.active")));


        }
    }
}
