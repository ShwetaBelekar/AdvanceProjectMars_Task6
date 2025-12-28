using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class HomeToProfileDescriptionPage : CommonDriver
    {
        private IWebElement profileTab => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/a[2]"));
        private IWebElement descriptionButton => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/div/h3/span"));

        public void NavigateToProfileDescription()
        {
            Thread.Sleep(2000);
            profileTab.Click();
            Thread.Sleep(2000);
            descriptionButton.Click();
            Thread.Sleep(2000);
        }

    }
}
