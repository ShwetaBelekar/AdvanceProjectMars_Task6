using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class HomeToManageRequestsPage : CommonDriver
    {
        private IWebElement profileTab => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/a[2]"));
        private IWebElement manageRequestsTab => Driver.FindElement(By.XPath("//div[@class='ui dropdown link item' and @tabindex='0']"));

        public void NavigateToManageRequests()
        {
            Thread.Sleep(2000);
            profileTab.Click();
            Thread.Sleep(3000);
            manageRequestsTab.Click();
            Thread.Sleep(2000);
        }
    }
}
