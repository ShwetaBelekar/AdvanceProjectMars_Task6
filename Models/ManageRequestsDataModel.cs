using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Models
{
    public class ManageRequestsDataModel
    {
        [JsonPropertyName("searchSkill")]
        public string searchSkill { get; set; }

        [JsonPropertyName("selectSeller")]
        public string selectSeller { get; set; }

        [JsonPropertyName("selectSkill")]
        public string selectSkill { get; set; }

        [JsonPropertyName("messageToSeller")]
        public string messageToSeller { get; set; }

        [JsonPropertyName("Emailaddress")]
        public string Emailaddress { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
       
        [JsonPropertyName("Sender")]
        public string Sender { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [JsonPropertyName("Pass")]
        public string Pass { get; set; }
    }
    public class ManageRequestsExampleContainer
    {
        // Must be initialized to avoid NullReferenceException
        [JsonPropertyName("data")]
        public List<ManageRequestsDataModel> Data { get; set; } = new List<ManageRequestsDataModel>();
    }
    public class ManageRequestsFileModel
    {
        [JsonPropertyName("scenario_type")]
        public string ScenarioType { get; set; }

        // Captures the "name" property from the JSON root.
        [JsonPropertyName("name")]
        public string Name { get; set; }

        // Captures the "tags" array property from the JSON root.
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new List<string>();
        [JsonPropertyName("examples")]
        public ManageRequestsExampleContainer Examples { get; set; }

        // Optional properties like "scenario_type" and "name" could be added here 
        // but are omitted for simplicity as they aren't used in the test logic loop.
    }
}
