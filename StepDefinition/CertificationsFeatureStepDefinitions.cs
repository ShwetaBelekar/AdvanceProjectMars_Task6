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
    public sealed class CertificationsFeatureStepDefinitions : CommonDriver
    {
        private readonly CertificationTestState _certificationState;
        public CertificationsFeatureStepDefinitions(CertificationTestState state)
        {
            _certificationState = state;
        }
        [BeforeScenario()]
        public void BeforeScenario()
        {

            var certificationState = new CertificationTestState();
            ScenarioContext.Current.Set(certificationState, "CertificationState");

        }
        [AfterScenario("MultipleRecords")]
        public void AfterMultipleRecordsScenario()
        {

            CertificationsPage certificationsPageObj = new CertificationsPage();
            certificationsPageObj.DeleteAllCertificationsRecords();

        }

        [AfterScenario("SingleRecord")]
        public void AfterSingleRecordScenario()
        {

            CertificationsPage certificationsPageObj = new CertificationsPage();
            certificationsPageObj.DeleteAllCertificationsRecords();

        }
        private CertificationsPage certificationsPageObj = new CertificationsPage();
        private HomeToCertificationsPage homeToCertificationsPageObj = new HomeToCertificationsPage();
        [Given("I load the certification records from the {string} file")]
        public void GivenILoadTheCertificationRecordsFromTheFile(string relativeFilePath)
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
                var dataContainer = JsonSerializer.Deserialize<CertificationFileModel>(jsonString);

                // Store the list of records in the EducationState object
                _certificationState.Records = dataContainer.Examples.Data;

                // FIX: Initialize total count using the correct property name: TotalCount
                _certificationState.TotalCount = _certificationState.Records.Count;

                // FIX: Use the correct property name: TotalCount
                Console.WriteLine($"Successfully loaded {_certificationState.TotalCount} education records.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during JSON loading or deserialization: {ex.Message}", ex);
            }
        }



        [Given("I navigate to Certification")]
        public void GivenINavigateToCertification()
        {
            homeToCertificationsPageObj.NavigateToCertifications();
        }

        [When("I create and verify all certification records")]
        public void WhenICreateAndVerifyAllCertificationRecords()
        {
            if (_certificationState.Records == null || _certificationState.Records.Count == 0)
            {
                Assert.Fail("Education records not found in EducationState. The JSON loading step failed or returned no data.");
            }

            // FIX: Reset successful count using the correct property name: SuccessCount
            _certificationState.SuccessCount = 0;

            // FIX: Loop through the correct property name: Records
            foreach (var record in _certificationState.Records)
            {
                // FIX: Store the current record using the correct property name: CurrentRecord
                _certificationState.CurrentRecord = record;

                Console.WriteLine($"--- Processing: {record.CertificateorAward} ({record.Year}) ---");

                // ** Action: Call the Page Object Method **
                certificationsPageObj.CreateCertificationRecord(
                    record.CertificateorAward,
                    record.CertifiedFrom,
                    record.Year.ToString()
                );

                // ** Verification: Check the UI **
                try
                {
                    string YearString = record.Year.ToString();

                    // Find the elements for the newly created record (assuming it's always the last row)
                    IWebElement newcertificateoraward = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[last()]/tr/td[1]"));
                    IWebElement newcertifiedfrom = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[last()]/tr/td[2]"));
                    IWebElement newyear = Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[last()]/tr/td[3]"));

                    if (newcertificateoraward.Text == record.CertificateorAward &&
                        newcertifiedfrom.Text == record.CertifiedFrom &&
                        newyear.Text == YearString)
                    {
                        Console.WriteLine($"    [SUCCESS] Record verified for {record.CertificateorAward}.");
                        // FIX: Increment successful count using the correct property name: SuccessCount
                        _certificationState.SuccessCount++;
                    }
                    else
                    {
                        Console.WriteLine($"    [FAIL] Verification mismatch for {record.CertificateorAward}. Data on screen did not match expected JSON data.");
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"    [ERROR] Could not find the new record element for {record.CertificateorAward}. Creation likely failed or locator is incorrect.");
                }
            }

        }

        [Then("the batch should be created successfully")]
        public void ThenTheBatchShouldBeCreatedSuccessfully()
        {
            if (_certificationState.SuccessCount == _certificationState.TotalCount)
            {
                // FIX: Use the correct property name: TotalCount
                Assert.Pass($"Successfully created and verified all {_certificationState.TotalCount} certification records.");
            }
            else
            {
                // This will fail the test if not all records were successful.
                // FIX: Use the correct property names: SuccessCount and TotalCount
                Assert.Fail($"Only {_certificationState.SuccessCount} out of {_certificationState.TotalCount} records were successfully created and verified. Check previous console output for iteration details.");
            }
        }

    }
}
