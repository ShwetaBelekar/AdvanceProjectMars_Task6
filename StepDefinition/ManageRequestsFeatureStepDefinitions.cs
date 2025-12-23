using AdvanceProjectMars_Task6.Models;
using AdvanceProjectMars_Task6.Pages;
using AdvanceProjectMars_Task6.State;
using AdvanceProjectMars_Task6.Utilities;
using NUnit.Framework;
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
    [Scope(Tag = "ManageRequests")]
    public sealed class ManageRequestsFeatureStepDefinitions : CommonDriver
    {
        private readonly ManageRequestsTestState _manageRequestsState;
        public ManageRequestsFeatureStepDefinitions(ManageRequestsTestState state)
        {
            _manageRequestsState = state;
        }
        private ManageRequestsPage manageRequestsPageObj = new ManageRequestsPage();
        private HomeToManageRequestsPage homeToManageRequestsPageObj = new HomeToManageRequestsPage();
        [Given("I load the test data from {string} file")]
        public void GivenILoadTheTestDataFromFile(string relativeFilePath)
        {
            try
            {
                string jsonFilePath = GetJsonFilePath(relativeFilePath);
                var dataContainer = JsonSerializer.Deserialize<ManageRequestsFileModel>(File.ReadAllText(jsonFilePath));

                _manageRequestsState.Records = dataContainer.Examples.Data;
                _manageRequestsState.TotalCount = _manageRequestsState.Records.Count;

                Console.WriteLine($"Loaded {_manageRequestsState.TotalCount} manageRequests records.");
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

        

        [Then("I send skill swap request to other user and i login into the user account to check the skill swap request received successfully")]
        public void ThenISendSkillSwapRequestToOtherUserAndILoginIntoTheUserAccountToCheckTheSkillSwapRequestReceivedSuccessfully()
        {
            if (_manageRequestsState.Records?.Count == 0)
            {
                Assert.Fail("ManageRequests records not found in ManageListingsState. The JSON loading step failed or returned no data.");
            }

            _manageRequestsState.SuccessCount = 0;
            foreach (var record in _manageRequestsState.Records)
            {
                _manageRequestsState.CurrentRecord = record;
                Console.WriteLine($"--- Processing: {record.searchSkill} ---");
                manageRequestsPageObj.SendSkillSwapRequestAndReceivedRequest(record.searchSkill, record.selectSeller, record.selectSkill, record.messageToSeller, record.Emailaddress, record.Password, record.Sender);
            }

        }

    }
}
