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
    [Scope(Tag = "ProfileDescription")]
    public sealed class ProfileDescriptionFeatureStepDefinitions : CommonDriver
    {
        private readonly ProfileDescriptionTestState _profileDescriptionState;
        public ProfileDescriptionFeatureStepDefinitions(ProfileDescriptionTestState state)
        {
            _profileDescriptionState = state;
        }
        private ProfileDescriptionPage profileDescriptionPageObj = new ProfileDescriptionPage();
        private HomeToProfileDescriptionPage homeToProfileDescriptionPageObj = new HomeToProfileDescriptionPage();
        [Given("I load the description records from the {string} file")]
        public void GivenILoadTheDescriptionRecordsFromTheFile(string relativeFilePath)
        {
            try
            {
                // Finds the JSON file based on the application's current directory (where the assembly is running)
                // and the relative path provided in the Gherkin step.

                // IMPORTANT: Normalizing the path separators for cross-platform compatibility.
                string normalizedPath = relativeFilePath.Replace('\\', Path.DirectorySeparatorChar);
                string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, normalizedPath);
                if (!File.Exists(jsonFilePath))
                {
                    // Adding a check for the file in the immediate execution directory as a common alternative location
                    string fallbackFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(relativeFilePath));

                    if (File.Exists(fallbackFilePath))
                    {
                        jsonFilePath = fallbackFilePath;
                        Console.WriteLine($"[WARNING] Found file in root execution directory: {fallbackFilePath}.");
                    }
                    else
                    {
                        throw new FileNotFoundException($"JSON file not found at expected path: {jsonFilePath}. " +
                            $"Ensure the file is present and 'Copy to Output Directory' is set to 'Copy if newer' in its properties.");
                    }
                }

                string jsonString = File.ReadAllText(jsonFilePath);
                var dataContainer = JsonSerializer.Deserialize<ProfileDescriptionFileModel>(jsonString);
                _profileDescriptionState.Records = dataContainer.Examples.Data;

                // FIX: Initialize total count using the correct property name: TotalCount
                _profileDescriptionState.TotalCount = _profileDescriptionState.Records.Count;

                // FIX: Use the correct property name: TotalCount
                Console.WriteLine($"Successfully loaded {_profileDescriptionState.TotalCount} profileDescription records.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during JSON loading or deserialization: {ex.Message}", ex);
            }
        }

        [Given("I navigate to Description")]
        public void GivenINavigateToDescription()
        {
           homeToProfileDescriptionPageObj.NavigateToProfileDescription();
        }

        [Then("I create Description record")]
        public void ThenICreateDescriptionRecord()
        {
            if (_profileDescriptionState.Records == null || _profileDescriptionState.Records.Count == 0)
            {
                Assert.Fail("ProfileDescription records not found in ProfileDescriptionState. The JSON loading step failed or returned no data.");
            }
            _profileDescriptionState.SuccessCount = 0;
            foreach (var record in _profileDescriptionState.Records)
            {
                // FIX: Store the current record using the correct property name: CurrentRecord
                _profileDescriptionState.CurrentRecord = record;
                _profileDescriptionState.CurrentRecord = _profileDescriptionState.Records.First();
                Console.WriteLine($"--- Processing: {record.Description} ---");
                profileDescriptionPageObj.CreateDescription(record.Description);

            } 
                

               
        }

        [Then("I see Description created successfully")]
        public void ThenISeeDescriptionCreatedSuccessfully()
        {
            //Wait.WaitToBeVisible(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            //IWebElement popupAlert = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
            //string alertText = popupAlert.Text;
            //Console.WriteLine("Alert text: " + alertText);
            Wait.WaitToBeVisible(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            _profileDescriptionState.PopupMessage = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']")).Text;
            Console.WriteLine($"Popup Message: {_profileDescriptionState.PopupMessage}");
            _profileDescriptionState.IsDescriptionVisible = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/div/span")).Text.Contains(_profileDescriptionState.CurrentRecord.Description);
            Console.WriteLine($"Is Description Visible: {_profileDescriptionState.IsDescriptionVisible}");
            if (_profileDescriptionState.IsDescriptionVisible)
            {
                _profileDescriptionState.TestResult = "Passed";
            }
            else
            {
                _profileDescriptionState.TestResult = "Failed";
            }
            Console.WriteLine($"Test Result: {_profileDescriptionState.TestResult}");
        }
        [Then("I create invalid Description record")]
        public void ThenICreateInvalidDescriptionRecord()
        {
            if (_profileDescriptionState.Records == null || _profileDescriptionState.Records.Count == 0)
            {
                Assert.Fail("ProfileDescription records not found in ProfileDescriptionState. The JSON loading step failed or returned no data.");
            }
            _profileDescriptionState.SuccessCount = 0;
            foreach (var record in _profileDescriptionState.Records)
            {
                // FIX: Store the current record using the correct property name: CurrentRecord
                _profileDescriptionState.CurrentRecord = record;
                _profileDescriptionState.CurrentRecord = _profileDescriptionState.Records.First();
                Console.WriteLine($"--- Processing: {record.Description} ---");
                profileDescriptionPageObj.CreateDescription(record.Description);

            }
        }

        [Then("I see invalid description saved successfully Error in the system")]
        public void ThenISeeInvalidDescriptionSavedSuccessfullyErrorInTheSystem()
        {
            Wait.WaitToBeVisible(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            _profileDescriptionState.PopupMessage = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']")).Text;
            Console.WriteLine($"Popup Message: {_profileDescriptionState.PopupMessage}");
            Thread.Sleep(3000);
            _profileDescriptionState.IsInvalidDescriptionVisible = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/div/span")).Text.Contains(_profileDescriptionState.CurrentRecord.Description);
            Console.WriteLine($"Is Invalid Description Visible: {_profileDescriptionState.IsInvalidDescriptionVisible}");
            if (_profileDescriptionState.IsInvalidDescriptionVisible)
            {
                _profileDescriptionState.TestResult = "Passed";
            }
            else
            {
                _profileDescriptionState.TestResult = "Failed";
            }
            Console.WriteLine($"Test Result: {_profileDescriptionState.TestResult}");
        }

    }
}
