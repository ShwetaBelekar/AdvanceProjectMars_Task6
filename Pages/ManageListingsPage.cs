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
    public class ManageListingsPage : CommonDriver
    {
        private IWebElement listingTitle => Driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr/td[3]"));
        private IWebElement deleteButton => Driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr/td[8]/div/button[3]/i"));
        private IWebElement yesButton => Driver.FindElement(By.XPath("//button[@class='ui icon positive right labeled button']"));
        

        public void DeleteListing()
        {
            deleteButton.Click();
           
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".ui.tiny.modal.transition.visible.active")));
           yesButton.Click();
            if (yesButton.Displayed && yesButton.Enabled)
            {
                yesButton.Click();
            }
            else
            {
                // Handle the case where the button is not visible or enabled
            }

        }
    }
}
