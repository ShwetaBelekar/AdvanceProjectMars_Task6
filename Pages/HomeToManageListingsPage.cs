using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class HomeToManageListingsPage : CommonDriver
    {
        private IWebElement profileTab => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/a[2]"));
        private IWebElement manageListingsTab => Driver.FindElement(By.XPath("//a[@class='item' and @href='/Home/ListingManagement']"));
         
        public void NavigateToManageListings()
        {
            Thread.Sleep(2000);
            profileTab.Click();
            Thread.Sleep(2000);
            manageListingsTab.Click();
            Thread.Sleep(2000);
        }
    }
}
