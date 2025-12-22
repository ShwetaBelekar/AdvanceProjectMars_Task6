using AdvanceProjectMars_Task6.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
        //private IWebElement listingDeleteButton(string Title) => Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]/td[8]/div/button[3]/i"));
        private IWebElement yesButton => Driver.FindElement(By.XPath("//button[@class='ui icon positive right labeled button']"));
        //private IWebElement editButton(string Title) => Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]/td[8]/div/button[2]/i"));
        private IWebElement titleButton => Driver.FindElement(By.XPath("//input[@name='title']"));
        private IWebElement saveButton => Driver.FindElement(By.XPath("//input[@value='Save']"));
        private IWebElement viewButton(string Title) => Driver.FindElement(By.XPath("$\"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]/td[8]/div/button[1]/i"));
      
        private IWebElement listingImage => Driver.FindElement(By.XPath("//img[@class='defaultImage']"));
        

        private IWebElement reviews => Driver.FindElement(By.XPath("//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[2]/div[1]/div/div[1]/a/div/label"));
        private IWebElement chatButton => Driver.FindElement(By.XPath("//a[@class='ui teal button']"));
        // private IWebElement activeButton => Driver.FindElement(By.XPath("$\"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]/td[7]/div/input"));
        private void NavigateToNextPage()
        {
            // Implement logic to navigate to the next page
            // This might involve clicking on a pagination button
            // You'll need to adjust this to fit your specific use case
            IWebElement nextPageButton = Driver.FindElement(By.XPath("//button[@class='ui button otherPage']"));
            nextPageButton.Click();
        }

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
        private IWebElement ListingActiveButton(string Title)
        {
            int currentPage = 1;
            bool listingFound = false;

            while (!listingFound)
            {
                try
                {
                    Thread.Sleep(2000);
                    IWebElement listingRow = Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]"));
                    IWebElement activeButton = listingRow.FindElement(By.XPath("./td[7]/div/input"));
                   
                    listingFound = true;
                    return activeButton;

                }
                catch (NoSuchElementException)
                {
                    // If the listing is not found on the current page, navigate to the next page
                    currentPage++;
                    NavigateToNextPage();
                }
            }
            throw new Exception($"Listing '{Title}' not found");
        }
        public void ActiveListing(string Title)
        {
            Thread.Sleep(3000);
            IWebElement activeButton = ListingActiveButton(Title);
            activeButton.Click();

        }
        private void ClickViewButton(string Title)
        {
            int currentPage = 1;
            bool listingFound = false;

            while (!listingFound)
            {
                try
                {
                    Thread.Sleep(2000);
                    IWebElement listingRow = Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[td[3][contains(text(), '{Title}')]]"));
                    IWebElement viewButton = listingRow.FindElement(By.XPath("./td[8]/div/button[1]"));
                    viewButton.Click();
                    listingFound = true;
                }
                catch (NoSuchElementException)
                {
                    // If the listing is not found on the current page, navigate to the next page
                    currentPage++;
                    NavigateToNextPage();
                }
            }
        }
        public void ViewListing(string Title)
        {
            Thread.Sleep(2000);
            ClickViewButton(Title);

            Thread.Sleep(2000);
            string expectedName = "Tony Money";
            string displayedName = Driver.FindElement(By.XPath("//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[2]/div[1]/div/div[1]/a/h3")).Text;
            Console.WriteLine(expectedName == displayedName ? "User name is displayed correctly." : $"User name is not displayed in full. Expected: {expectedName}, Actual: {displayedName}");


        }

        private IWebElement listingEditButton(string Title, out int pageNumber, out int rowNumber)
        {
            int currentPage = 1;
            bool listingFound = false;
            IWebElement editButton = null;

            while (!listingFound)
            {
                try
                {
                    Thread.Sleep(2000);
                    var listingRows = Driver.FindElements(By.XPath("//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr"));
                    for (int i = 1; i <= listingRows.Count; i++)
                    {
                        var listingRow = Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[{i}]"));
                        var titleElement = listingRow.FindElement(By.XPath("./td[3]"));
                        if (titleElement.Text.Contains(Title))
                        {
                            editButton = listingRow.FindElement(By.XPath("./td[8]/div/button[2]/i"));
                            listingFound = true;
                            pageNumber = currentPage;
                            rowNumber = i;
                            return editButton;
                        }
                    }
                    // If the listing is not found on the current page, navigate to the next page
                    currentPage++;
                    NavigateToNextPage();
                }
                catch (NoSuchElementException)
                {
                    // If the listing is not found on the current page, navigate to the next page
                    currentPage++;
                    NavigateToNextPage();
                }
            }
            pageNumber = -1;
            rowNumber = -1;
            throw new Exception($"Listing '{Title}' not found");
        }

        


        
        public void EditListing(string Title, string EditTitle)
        {
            try
            {
                int pageNumber;
                int rowNumber;
                IWebElement editButton = listingEditButton(Title, out pageNumber, out rowNumber);
                Console.WriteLine($"Listing found on page {pageNumber}, row {rowNumber}");
                editButton.Click();
                Thread.Sleep(2000);
                titleButton.Clear();
                Thread.Sleep(2000);
                titleButton.SendKeys(EditTitle);
                Thread.Sleep(2000);
                Actions actions = new Actions(Driver);
                actions.MoveToElement(saveButton).Perform();
                Thread.Sleep(2000);
                saveButton.Click();
                Console.WriteLine($"Listing '{Title}' edited successfully");
                VerifyListingTitle(EditTitle);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing listing '{Title}': {ex.Message}");
                throw;
            }
        }
        public void VerifyListingTitle(string Title)
        {
            int pageNumber;
            int rowNumber;
            listingEditButton(Title, out pageNumber, out rowNumber);
            for (int i = 1; i < pageNumber; i++)
            {
                NavigateToNextPage();
            }
            IWebElement titleElement = Driver.FindElement(By.XPath($"//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[{rowNumber}]/td[3]"));
            Assert.That(titleElement.Text, Is.EqualTo(Title));
            Console.WriteLine($"Listing title verified on page {pageNumber}, row {rowNumber}");
        }


    }
}
