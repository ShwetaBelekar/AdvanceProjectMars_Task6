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
    [Scope(Tag = "ChangePassword")]
    public sealed class ChangePasswordFeatureStepDefinitions : CommonDriver
    {
        private readonly ChangePasswordTestState _changepasswordState;
        public ChangePasswordFeatureStepDefinitions(ChangePasswordTestState state)
        {
            _changepasswordState = state;
        }
        private ChangePasswordPage changePasswordPageObj = new ChangePasswordPage();
        private HomeToChangePasswordPage homeToChangePasswordPageObj = new HomeToChangePasswordPage();

        [Given("I load the change password records from the {string} file")]
        public void GivenILoadTheChangePasswordRecordsFromTheFile(string relativeFilePath)
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
                var dataContainer = JsonSerializer.Deserialize<ChangePasswordFileModel>(jsonString);
                _changepasswordState.Records = dataContainer.Examples.Data;
                _changepasswordState.TotalCount = _changepasswordState.Records.Count;
                Console.WriteLine($"Successfully loaded {_changepasswordState.TotalCount} changepassword records.");
            }

            catch (Exception ex)
            {
                throw new Exception($"Error during JSON loading or deserialization: {ex.Message}", ex);
            }
        }

        [When("I navigate to change password feature")]
        public void WhenINavigateToChangePasswordFeature()
        {
           homeToChangePasswordPageObj.NavigateToChangePassword();
        }

        [Then("I create new password")]
        public void ThenICreateNewPassword()
        {
            if(_changepasswordState.Records == null || _changepasswordState.Records.Count == 0)
            {
                Assert.Fail("Changepassword records not found in ChangePasswordState. The JSON loading step failed or returned no data.");
            }
            _changepasswordState.SuccessCount = 0;
            foreach (var record in _changepasswordState.Records)
            {
                _changepasswordState.CurrentRecord = record;

                changePasswordPageObj.CreateNewPassword(record.CurrentPassword, record.NewPassword, record.ConfirmPassword);

            }
        }

        [Then("The new password should be created successfully")]
        public void ThenTheNewPasswordShouldBeCreatedSuccessfully()
        {
            Wait.WaitToBeVisible(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            IWebElement popupAlert = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
            string alertText = popupAlert.Text;
            Console.WriteLine("Alert text: " + alertText);
        }

        [Then("I verify and successfully signin with new password")]
        public void ThenIVerifyAndSuccessfullySigninWithNewPassword()
        {
            if (_changepasswordState.Records == null || _changepasswordState.Records.Count == 0)
            {
                Assert.Fail("Changepassword records not found in ChangePasswordState. The JSON loading step failed or returned no data.");
            }
            _changepasswordState.SuccessCount = 0;
            foreach (var record in _changepasswordState.Records)
            {
                _changepasswordState.CurrentRecord = record;

                changePasswordPageObj.VerifySigninwithNewPassword(record.Emailaddress, record.NewPassword);

            }
        }

        [Then("I navigate to change password feature again and set password back to original password")]
        public void ThenINavigateToChangePasswordFeatureAgainAndSetPasswordBackToOriginalPassword()
        {
            if (_changepasswordState.Records == null || _changepasswordState.Records.Count == 0)
            {
                Assert.Fail("Changepassword records not found in ChangePasswordState. The JSON loading step failed or returned no data.");
            }
            _changepasswordState.SuccessCount = 0;
            foreach (var record in _changepasswordState.Records)
            {
                _changepasswordState.CurrentRecord = record;

                changePasswordPageObj.ChangePasswordtoOriginal(record.NewCurrentPassword, record.OriginalNewPassword, record.OriginalConfirmPassword);

            }
        }



        [Then("I see the success message")]
        public void ThenISeeTheSuccessMessage()
        {
            Wait.WaitToBeVisible(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            IWebElement poopupAlert = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
            string aleertText = poopupAlert.Text;
            Console.WriteLine("Alert text: " + aleertText);
        }

    }
}
