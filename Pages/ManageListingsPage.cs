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
        private IWebElement listingDeleteButton(string Title)
        {
            int currentPage = 1;
            bool listingFound = false;

            while (!listingFound)
            {
                try
                {
                    Thread.Sleep(2000);
                    IWebElement listingRow = Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]"));
                    IWebElement deleteButton = listingRow.FindElement(By.XPath("./td[8]/div/button[3]/i"));
                    Thread.Sleep(3000);
                    // Try to find the delete button on the current page
                    //IWebElement deleteButton = Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]/td[8]/div/button[3]/i"));
                    listingFound = true;
                    return deleteButton;
                }
                catch (NoSuchElementException)
                {
                    // If the listing is not found on the current page, navigate to the next page
                    currentPage++;
                    NavigateToNextPage();
                }
            }

            // If we've reached this point, it means the listing was not found on any page
            throw new Exception($"Listing '{Title}' not found");
        }

        private void NavigateToNextPage()
        {
            // Implement logic to navigate to the next page
            // This might involve clicking on a pagination button
            // You'll need to adjust this to fit your specific use case
            IWebElement nextPageButton = Driver.FindElement(By.XPath("//button[@class='ui button otherPage']"));
            nextPageButton.Click();
        }
        //private IWebElement listingDeleteButton(string Title) => Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]/td[8]/div/button[3]/i"));
        private IWebElement yesButton => Driver.FindElement(By.XPath("//button[@class='ui icon positive right labeled button']"));
        

        public void DeleteListing(string Title)
        {
            //deleteButton.Click();
            IWebElement deleteButton = listingDeleteButton(Title);
            deleteButton.Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".ui.tiny.modal.transition.visible.active")));
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
