using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class HomeToCertificationsPage : CommonDriver
    {
        private IWebElement profileTab => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/a[2]"));
        private IWebElement certificationOption => Driver.FindElement(By.XPath("//a[text()='Certifications']"));
        public void NavigateToCertifications()
        {

            Thread.Sleep(2000);
           
            profileTab.Click();
            Thread.Sleep(2000);
            
            certificationOption.Click();
        }
    }
}
