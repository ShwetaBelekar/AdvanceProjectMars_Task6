using AdvanceProjectMars_Task6.Models;
using AdvanceProjectMars_Task6.Pages;
using AdvanceProjectMars_Task6.State;
using AdvanceProjectMars_Task6.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.StepDefinition
{
    [Binding]
    [Scope(Tag = "ManageListings")]
    public sealed class ManageListingsFeatureStepDefinitions : CommonDriver
    {
        private readonly ManageListingsTestState _manageListingsState;
        public ManageListingsFeatureStepDefinitions(ManageListingsTestState state)
        {
            _manageListingsState = state;
        }
        private ManageListingsPage manageListingsPageObj = new ManageListingsPage();
        private HomeToManageListingsPage homeToManageListingsPageObj = new HomeToManageListingsPage();

        [Given("I load the listing records that i want to delete from the {string} file")]
        public void GivenILoadTheListingRecordsThatIWantToDeleteFromTheFile(string relativeFilePath)
        {
            try
            {
                string jsonFilePath = GetJsonFilePath(relativeFilePath);
                var dataContainer = JsonSerializer.Deserialize<ManageListingsFileModel>(File.ReadAllText(jsonFilePath));

                _manageListingsState.Records = dataContainer.Examples.Data;
                _manageListingsState.TotalCount = _manageListingsState.Records.Count;

                Console.WriteLine($"Loaded {_manageListingsState.TotalCount} manageListings records.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading JSON: {ex.Message}", ex);
            }
        }

        string GetJsonFilePath(string relativeFilePath)
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFilePath.Replace('\\', Path.DirectorySeparatorChar));
            if (!File.Exists(jsonFilePath))
            {
                string fallbackFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(relativeFilePath));
                if (File.Exists(fallbackFilePath))
                {
                    Console.WriteLine($"[WARNING] Found file in root execution directory: {fallbackFilePath}.");
                    return fallbackFilePath;
                }
                throw new FileNotFoundException($"JSON file not found: {jsonFilePath}");
            }
            return jsonFilePath;

        }

        [When("I navigate to manage listings")]
        public void WhenINavigateToManageListings()
        {
          homeToManageListingsPageObj.NavigateToManageListings();
        }

        [Then("I delete the listing")]
        public void ThenIDeleteTheListing()
        {
            if (_manageListingsState.Records?.Count == 0)
            {
                Assert.Fail("ManageListings records not found in ManageListingsState. The JSON loading step failed or returned no data.");
            }

            _manageListingsState.SuccessCount = 0;
            foreach (var record in _manageListingsState.Records)
            {
                _manageListingsState.CurrentRecord = record;
                Console.WriteLine($"--- Processing: {record.Title} ---");
                manageListingsPageObj.DeleteListing(record.Title);
            }
           
        }

        [Then("the listing should be delete successfully")]
        public void ThenTheListingShouldBeDeleteSuccessfully()
        {
            Wait.WaitToBeVisible(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            _manageListingsState.PopupMessage = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']")).Text;
            Console.WriteLine($"Popup Message: {_manageListingsState.PopupMessage}");
            Thread.Sleep(3000);
        }
        [Then("I edit the listing")]
        public void ThenIEditTheListing()
        {
            if (_manageListingsState.Records?.Count == 0)
            {
                Assert.Fail("ManageListings records not found in ManageListingsState. The JSON loading step failed or returned no data.");
            }

            _manageListingsState.SuccessCount = 0;
            foreach (var record in _manageListingsState.Records)
            {
                _manageListingsState.CurrentRecord = record;
                Console.WriteLine($"--- Processing: {record.Title} ---");
                manageListingsPageObj.EditListing(record.Title, record.EditTitle);
            }
        }

        [Then("the listing should be edited successfully")]
        public void ThenTheListingShouldBeEditedSuccessfully()
        {
            
            
        }


    }
}
