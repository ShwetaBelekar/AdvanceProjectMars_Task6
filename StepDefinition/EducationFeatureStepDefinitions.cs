
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
    public sealed class EducationFeatureStepDefinitions : CommonDriver
    {
        private EducationState _educationState;

        public EducationFeatureStepDefinitions()
        {
            _educationState = ScenarioContext.Current.Get<EducationState>("EducationState");
        }

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

                // Deserialize the JSON string into the C# model classes
                var dataContainer = JsonSerializer.Deserialize<EducationDataModel>(jsonString);

                // Store the list of records in the EducationState object
                _educationState.EducationRecords = dataContainer.Examples.Data;

                Console.WriteLine($"Successfully loaded {dataContainer.Examples.Data.Count} education records.");
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
            if (_educationState.EducationRecords == null)
            {
                Assert.Fail("Education records not found in EducationState. The JSON loading step failed.");
            }

            int successCount = 0;

            foreach (var record in _educationState.EducationRecords)
            {
                Console.WriteLine($"--- Processing: {record.CollegeUniversityName} ({record.YearofGraduation}) ---");

                // ** Action: Call the Page Object Method **
                // Note: The YearofGraduation (int) is converted to string to match the page object method signature.
                educationPageObj.CreateEducationRecord(
                    record.CollegeUniversityName,
                    record.CountryofCollegeUniversity,
                    record.Title,
                    record.Degree,
                    record.YearofGraduation.ToString()
                );

                // ** Verification: Check the UI (Adapted from your original verification logic) **
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
                        successCount++;
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

            // Store results for the final assertion step
            _educationState.SuccessfulRecordsCount = successCount;
            _educationState.TotalRecordsCount = _educationState.EducationRecords.Count;
        }

        // Step 4: Implements 'Then the batch creation process should be successful'
        [Then(@"the batch creation process should be successful")]
        public void ThenTheBatchCreationProcessShouldBeSuccessful()
        {
            if (_educationState.SuccessfulRecordsCount == _educationState.TotalRecordsCount)
            {
                Assert.Pass($"Successfully created and verified all {_educationState.TotalRecordsCount} education records.");
            }
            else
            {
                // This will fail the test if not all records were successful.
                Assert.Fail($"Only {_educationState.SuccessfulRecordsCount} out of {_educationState.TotalRecordsCount} records were successfully created and verified. Check previous console output for iteration details.");
            }
        }
        [When("I see education records")]
        public void WhenISeeEducationRecords()
        {
            if (_educationState.EducationRecords == null)
            {
                Assert.Fail("Education records not found in EducationState. The JSON loading step failed.");
            }

            int successCount = 0;

            foreach (var record in _educationState.EducationRecords)
            {
                Console.WriteLine($"--- Processing: {record.CollegeUniversityName} ({record.YearofGraduation}) ---");

                // ** Action: Call the Page Object Method **
                // Note: The YearofGraduation (int) is converted to string to match the page object method signature.
                educationPageObj.CreateEducationRecord(
                    record.CollegeUniversityName,
                    record.CountryofCollegeUniversity,
                    record.Title,
                    record.Degree,
                    record.YearofGraduation.ToString()
                );

                // ** Verification: Check the UI (Adapted from your original verification logic) **
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
                        successCount++;
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

            // Store results for the final assertion step
            _educationState.SuccessfulRecordsCount = successCount;
            _educationState.TotalRecordsCount = _educationState.EducationRecords.Count;
        }
        [When("I delete the existing education record")]
        public void WhenIDeleteTheExistingEducationRecord()
        {
            if (_educationState.EducationRecords == null)
            {
                Assert.Fail("Education records not found in EducationState. The JSON loading step failed.");
            }

            foreach (var record in _educationState.EducationRecords)
            {
                educationPageObj.DeleteEducationRecord(record.CollegeUniversityName);
            }
           
        }

        

        [Then("I should see a message that record deleted successfully")]
        public void ThenIShouldSeeAMessageThatRecordDeletedSuccessfully()
        {
            Wait.WaitToBeClickable(Driver, "XPath", "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']", 2);
            IWebElement popupAlert = Driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
            if (popupAlert.Text == "Education entry successfully removed")
            {
                Assert.Pass("Record Deleted Succesfully");
            }
            else
            {
                Assert.Fail("Record not deleted");
            }
        }

        //public sealed class EducationFeatureStepDefinitions : CommonDriver
        //{

        //    private const string EducationRecordsKey = "EducationRecords";

        //    // --- Dependencies (Placeholders for your Page Objects) ---
        //    // Assume these classes exist in your project:
        //    private EducationPage educationPageObj = new EducationPage();
        //    private HomeToEducationPage homeToEducationPageObj = new HomeToEducationPage();
        //    // ------------------------------
        //    [Given(@"I load the education records from the '([^']*)' file")]
        //    public void GivenILoadTheEducationRecordsFromTheJsonFile(string relativeFilePath)
        //    {
        //        try
        //        {
        //            // Finds the JSON file based on the application's current directory (where the assembly is running)
        //            // and the relative path provided in the Gherkin step.

        //            // IMPORTANT: Normalizing the path separators for cross-platform compatibility.
        //            string normalizedPath = relativeFilePath.Replace('\\', Path.DirectorySeparatorChar);
        //            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, normalizedPath);

        //            if (!File.Exists(jsonFilePath))
        //            {
        //                // Adding a check for the file in the immediate execution directory as a common alternative location
        //                string fallbackFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(relativeFilePath));

        //                if (File.Exists(fallbackFilePath))
        //                {
        //                    jsonFilePath = fallbackFilePath;
        //                    Console.WriteLine($"[WARNING] Found file in root execution directory: {fallbackFilePath}.");
        //                }
        //                else
        //                {
        //                    throw new FileNotFoundException($"JSON file not found at expected path: {jsonFilePath}. " +
        //                        $"Ensure the file is present and 'Copy to Output Directory' is set to 'Copy if newer' in its properties.");
        //                }
        //            }

        //            string jsonString = File.ReadAllText(jsonFilePath);

        //            // Deserialize the JSON string into the C# model classes
        //            var dataContainer = JsonSerializer.Deserialize<EducationDataModel>(jsonString);

        //            // Store the list of records in the ScenarioContext for the 'When' step to access
        //            ScenarioContext.Current.Set(dataContainer.Examples.Data, EducationRecordsKey);

        //            Console.WriteLine($"Successfully loaded {dataContainer.Examples.Data.Count} education records.");
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception($"Error during JSON loading or deserialization: {ex.Message}", ex);
        //        }
        //    }

        //    // Step 2: Implements 'Given I navigate to Education'
        //    [Given("I navigate to Education")]
        //    public void GivenINavigateToEducation()
        //    {
        //        homeToEducationPageObj.NavigateToEducation();
        //    }

        //    // Step 3: Implements 'When I create and verify all education records'
        //    [When(@"I create and verify all education records")]
        //    public void WhenICreateAndVerifyAllEducationRecords()
        //    {
        //        if (!ScenarioContext.Current.TryGetValue(EducationRecordsKey, out List<EducationRecord> records))
        //        {
        //            Assert.Fail("Education records not found in ScenarioContext. The JSON loading step failed.");
        //        }

        //        int successCount = 0;

        //        foreach (var record in records)
        //        {
        //            Console.WriteLine($"--- Processing: {record.CollegeUniversityName} ({record.YearofGraduation}) ---");

        //            // ** Action: Call the Page Object Method **
        //            // Note: The YearofGraduation (int) is converted to string to match the page object method signature.
        //            educationPageObj.CreateEducationRecord(
        //                record.CollegeUniversityName,
        //                record.CountryofCollegeUniversity,
        //                record.Title,
        //                record.Degree,
        //                record.YearofGraduation.ToString()
        //            );

        //            // ** Verification: Check the UI (Adapted from your original verification logic) **
        //            try
        //            {
        //                string YearofGraduationString = record.YearofGraduation.ToString();

        //                // Find the elements for the newly created record (assuming it's always the last row)
        //                IWebElement newuniversity = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[2]"));
        //                IWebElement newcountry = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[1]"));
        //                IWebElement newtitle = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[3]"));
        //                IWebElement newdegree = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[4]"));
        //                IWebElement newgraduationyear = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[5]"));

        //                if (newuniversity.Text == record.CollegeUniversityName &&
        //                    newcountry.Text == record.CountryofCollegeUniversity &&
        //                    newtitle.Text == record.Title &&
        //                    newdegree.Text == record.Degree &&
        //                    newgraduationyear.Text == YearofGraduationString)
        //                {
        //                    Console.WriteLine($"    [SUCCESS] Record verified for {record.CollegeUniversityName}.");
        //                    successCount++;
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"    [FAIL] Verification mismatch for {record.CollegeUniversityName}. Data on screen did not match expected JSON data.");
        //                }
        //            }
        //            catch (NoSuchElementException)
        //            {
        //                Console.WriteLine($"    [ERROR] Could not find the new record element for {record.CollegeUniversityName}. Creation likely failed or locator is incorrect.");
        //            }
        //        }

        //        // Store results for the final assertion step
        //        ScenarioContext.Current.Set(successCount, "SuccessfulRecordsCount");
        //        ScenarioContext.Current.Set(records.Count, "TotalRecordsCount");
        //    }

        //    // Step 4: Implements 'Then the batch creation process should be successful'
        //    [Then(@"the batch creation process should be successful")]
        //    public void ThenTheBatchCreationProcessShouldBeSuccessful()
        //    {
        //        int successCount = ScenarioContext.Current.Get<int>("SuccessfulRecordsCount");
        //        int totalCount = ScenarioContext.Current.Get<int>("TotalRecordsCount");

        //        if (successCount == totalCount)
        //        {
        //            Assert.Pass($"Successfully created and verified all {totalCount} education records.");
        //        }
        //        else
        //        {
        //            // This will fail the test if not all records were successful.
        //            Assert.Fail($"Only {successCount} out of {totalCount} records were successfully created and verified. Check previous console output for iteration details.");
        //        }
        //    }
        //    //[Given("I logged into Project Mars successfully")]
        //public void GivenILoggedIntoProjectMarsSuccessfully()
        //{
        //    LoginPage loginPageObj = new LoginPage();
        //    loginPageObj.LoginActions();

        //    loginPageObj.VerifyUserInHomePage();
        //}

        //[Given("I navigate to Education")]
        //public void GivenINavigateToEducation()
        //{

        //    HomeToEducationPage homeToEducationPageObj = new HomeToEducationPage();
        //    homeToEducationPageObj.NavigateToEducation();
        //}

        //[When("I create a {string} and {string} and {string} and {string} and {string} education record")]
        //public void WhenICreateAAndAndAndAndEducationRecord(string CollegeUniversityName, string CountryofCollegeUniversity, string Title, string Degree, string YearofGraduation)
        //{
        //    EducationPage educationPageObj = new EducationPage();
        //    educationPageObj.CreateEducationRecord(CollegeUniversityName, CountryofCollegeUniversity, Title, Degree, YearofGraduation);
        //}

        //[Then("the record for {string} and {string} and {string} and {string} and {string} education should be created successfully")]
        //public void ThenTheRecordForAndAndAndAndEducationShouldBeCreatedSuccessfully(string CollegeUniversityName, string CountryofCollegeUniversity, string Title, string Degree, string YearofGraduation)
        //{
        //    IWebElement newuniversity = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[2]"));
        //    IWebElement newcountry = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[1]"));
        //    IWebElement newtitle = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[3]"));
        //    IWebElement newdegree = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[4]"));
        //    IWebElement newgraduationyear = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[5]"));
        //    if (newuniversity.Text == CollegeUniversityName && newcountry.Text == CountryofCollegeUniversity && newtitle.Text == Title && newdegree.Text == Degree && newgraduationyear.Text == YearofGraduation)
        //    {
        //        Assert.Pass("record created successfully");
        //    }
        //    else
        //    {
        //        Assert.Fail("record creation unsuccessful");
        //    }
        //}




    }
}
