
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace AdvanceProjectMars_Task6.StepDefinition
{
    [Binding]
    [Scope(Tag = "Education")]

    public sealed class EducationFeatureStepDefinitions : CommonDriver
    {
        private readonly EducationTestState _educationState;

        public EducationFeatureStepDefinitions(EducationTestState state)
        {
            _educationState = state;
        }
        //[BeforeScenario("Education")]
        //public void BeforeScenario()
        //{



        //    var educationState = new EducationTestState();
        //    ScenarioContext.Current.Set(educationState, "EducationState");

        //}
        //[AfterScenario("Education", "MultipleRecords")]
        //public void AfterMultipleRecordsScenario()
        //{

        //    EducationPage educationPageObj = new EducationPage();
        //    educationPageObj.DeleteAllEducationRecords();

        //}

        //[AfterScenario("Education", "SingleRecord")]
        //public void AfterSingleRecordScenario()
        //{

        //   EducationPage educationPageObj = new EducationPage();
        //    educationPageObj.DeleteAllEducationRecords();

        //}



        // --- Dependencies (Placeholders for your Page Objects) ---
        // Assume these classes exist in your project:
        private EducationPage educationPageObj = new EducationPage();
        private HomeToEducationPage homeToEducationPageObj = new HomeToEducationPage();
        // ------------------------------
        [Given(@"I load the education records from the '([^']*)' file")]
        public void GivenILoadTheEducationRecordsFromTheJsonFile(string relativeFilePath)
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

                // FIX: Deserialize into the root model class, EducationFileModel, which contains the "examples" object.
                var dataContainer = JsonSerializer.Deserialize<EducationFileModel>(jsonString);

                // Store the list of records in the EducationState object
                _educationState.Records = dataContainer.Examples.Data;

                // FIX: Initialize total count using the correct property name: TotalCount
                _educationState.TotalCount = _educationState.Records.Count;

                // FIX: Use the correct property name: TotalCount
                Console.WriteLine($"Successfully loaded {_educationState.TotalCount} education records.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during JSON loading or deserialization: {ex.Message}", ex);
            }
        }

        // Step 2: Implements 'Given I navigate to Education'
        [Given("I navigate to Education")]
        public void GivenINavigateToEducation()
        {
            homeToEducationPageObj.NavigateToEducation();
        }

        // Step 3: Implements 'When I create and verify all education records'
        [When(@"I create and verify all education records")]
        public void WhenICreateAndVerifyAllEducationRecords()
        {

            if (_educationState.Records == null || _educationState.Records.Count == 0)
            {
                Assert.Fail("Education records not found in EducationState. The JSON loading step failed or returned no data.");
            }

            // FIX: Reset successful count using the correct property name: SuccessCount
            _educationState.SuccessCount = 0;

            // FIX: Loop through the correct property name: Records
            foreach (var record in _educationState.Records)
            {
                // FIX: Store the current record using the correct property name: CurrentRecord
                _educationState.CurrentRecord = record;

                Console.WriteLine($"--- Processing: {record.CollegeUniversityName} ({record.YearofGraduation}) ---");

                // ** Action: Call the Page Object Method **
                educationPageObj.CreateEducationRecord(
                    record.CollegeUniversityName,
                    record.CountryofCollegeUniversity,
                    record.Title,
                    record.Degree,
                    record.YearofGraduation.ToString()
                );

                // ** Verification: Check the UI **
                try
                {
                    string YearofGraduationString = record.YearofGraduation.ToString();

                    // Find the elements for the newly created record (assuming it's always the last row)
                    IWebElement newuniversity = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[2]"));
                    IWebElement newcountry = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[1]"));
                    IWebElement newtitle = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[3]"));
                    IWebElement newdegree = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[4]"));
                    IWebElement newgraduationyear = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[5]"));

                    if (newuniversity.Text == record.CollegeUniversityName &&
                        newcountry.Text == record.CountryofCollegeUniversity &&
                        newtitle.Text == record.Title &&
                        newdegree.Text == record.Degree &&
                        newgraduationyear.Text == YearofGraduationString)
                    {
                        Console.WriteLine($"    [SUCCESS] Record verified for {record.CollegeUniversityName}.");
                        // FIX: Increment successful count using the correct property name: SuccessCount
                        _educationState.SuccessCount++;
                    }
                    else
                    {
                        Console.WriteLine($"    [FAIL] Verification mismatch for {record.CollegeUniversityName}. Data on screen did not match expected JSON data.");
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"    [ERROR] Could not find the new record element for {record.CollegeUniversityName}. Creation likely failed or locator is incorrect.");
                }
            }


        }

        // Step 4: Implements 'Then the batch creation process should be successful'
        [Then(@"the batch creation process should be successful")]
        public void ThenTheBatchCreationProcessShouldBeSuccessful()
        {
            if (_educationState.SuccessCount == _educationState.TotalCount)
            {
                // FIX: Use the correct property name: TotalCount
                Assert.Pass($"Successfully created and verified all {_educationState.TotalCount} education records.");
            }
            else
            {
                // This will fail the test if not all records were successful.
                // FIX: Use the correct property names: SuccessCount and TotalCount
                Assert.Fail($"Only {_educationState.SuccessCount} out of {_educationState.TotalCount} records were successfully created and verified. Check previous console output for iteration details.");
            }
        }
        [When("I see education records")]
        public void WhenISeeEducationRecords()
        {
            if (_educationState.Records == null || _educationState.Records.Count == 0)
            {
                Assert.Fail("Education records not found in EducationState. The JSON loading step failed or returned no data.");
            }

            _educationState.SuccessCount = 0;

            foreach (var record in _educationState.Records)
            {
                _educationState.CurrentRecord = record;

                Console.WriteLine($"--- Processing: {record.CollegeUniversityName} ({record.YearofGraduation}) ---");

                // ** Action: Call the Page Object Method **
                educationPageObj.CreateEducationRecord(
                    record.CollegeUniversityName,
                    record.CountryofCollegeUniversity,
                    record.Title,
                    record.Degree,
                    record.YearofGraduation.ToString()
                );

                // ** Verification: Check the UI **
                try
                {
                    string YearofGraduationString = record.YearofGraduation.ToString();

                    // Find the elements for the newly created record (assuming it's always the last row)
                    IWebElement newuniversity = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[2]"));
                    IWebElement newcountry = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[1]"));
                    IWebElement newtitle = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[3]"));
                    IWebElement newdegree = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[4]"));
                    IWebElement newgraduationyear = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[5]"));

                    if (newuniversity.Text == record.CollegeUniversityName &&
                        newcountry.Text == record.CountryofCollegeUniversity &&
                        newtitle.Text == record.Title &&
                        newdegree.Text == record.Degree &&
                        newgraduationyear.Text == YearofGraduationString)
                    {
                        Console.WriteLine($"    [SUCCESS] Record verified for {record.CollegeUniversityName}.");
                        _educationState.SuccessCount++;
                    }
                    else
                    {
                        Console.WriteLine($"    [FAIL] Verification mismatch for {record.CollegeUniversityName}. Data on screen did not match expected JSON data.");
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"    [ERROR] Could not find the new record element for {record.CollegeUniversityName}. Creation likely failed or locator is incorrect.");
                }
            }
            }
        [When("I delete the existing education record")]
        public void WhenIDeleteTheExistingEducationRecord()
        {
            if (_educationState.Records == null || _educationState.Records.Count == 0)
            {
                Assert.Fail("No education record found in EducationState to attempt deletion.");
            }

            // Loop through the records loaded from the JSON file (usually just one for a delete scenario)
            foreach (var record in _educationState.Records)
            {
                Console.WriteLine($"Attempting to delete education record: {record.CollegeUniversityName}");

                // The Page Object method should ideally accept parameters to find the correct row.
                // Assuming DeleteEducationRecord() currently targets the delete button of the known record.
                educationPageObj.DeleteEducationRecord();
            }

        }



        [Then("I should see a message that record deleted successfully")]
        public void ThenIShouldSeeAMessageThatRecordDeletedSuccessfully()
        {
            try
            {
                // Explicit wait for visibility of the success message (Requires an explicit wait utility)
                // Assuming the XPath is correct for the success message popup.
                IWebElement popupAlert = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));

                if (popupAlert.Text.Contains("Education entry successfully removed"))
                {
                    Assert.Pass("Record Deleted Successfully: Message confirmed.");
                }
                else
                {
                    Assert.Fail($"Record not deleted. Expected message not found. Actual message: {popupAlert.Text}");
                }
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Record not deleted. Success alert message was not visible.");
            }
        }




    }
}
