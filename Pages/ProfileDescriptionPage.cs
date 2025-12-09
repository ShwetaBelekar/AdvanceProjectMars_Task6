using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class ProfileDescriptionPage : CommonDriver
    {
        
        private IWebElement descriptionTextbox => Driver.FindElement(By.XPath("//textarea[@maxlength='600']"));
        private IWebElement saveButton => Driver.FindElement(By.XPath("//button[@class='ui teal button' and @type='button' and text()='Save']"));

        public void CreateDescription(string Description)
        {
            descriptionTextbox.Clear();
            Thread.Sleep(2000);
            descriptionTextbox.SendKeys(Description);
            Thread.Sleep(3000);
            saveButton.Click();

        }
    }
}
