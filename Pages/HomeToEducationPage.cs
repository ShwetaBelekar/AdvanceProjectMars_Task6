using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class HomeToEducationPage : CommonDriver
    {
        private IWebElement profileTab => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/a[2]"));
        private IWebElement educationOption => Driver.FindElement(By.XPath("//a[text()='Education']"));
        public void NavigateToEducation()
        {

            Thread.Sleep(3000);
            
            profileTab.Click();
            Thread.Sleep(2000);
           
            educationOption.Click();
        }
    }
}
