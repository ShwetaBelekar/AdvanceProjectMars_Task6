using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Models
{
    public class ChangePasswordDataModel
    {
        [JsonPropertyName("CurrentPassword")]
        public string CurrentPassword { get; set; }

        [JsonPropertyName("NewPassword")]
        public string NewPassword { get; set; }

        [JsonPropertyName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [JsonPropertyName("NewCurrentPassword")]
        public string NewCurrentPassword { get; set; }

        [JsonPropertyName("OriginalNewPassword")]
        public string OriginalNewPassword { get; set; }

        [JsonPropertyName("OriginalConfirmPassword")]
        public string OriginalConfirmPassword { get; set; }
    }
    public class ChangePasswordExampleContainer
    {
        // Must be initialized to avoid NullReferenceException
        [JsonPropertyName("data")]
        public List<ChangePasswordDataModel> Data { get; set; } = new List<ChangePasswordDataModel>();
    }
    public class ChangePasswordFileModel
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
        public ChangePasswordExampleContainer Examples { get; set; }

        // Optional properties like "scenario_type" and "name" could be added here 
        // but are omitted for simplicity as they aren't used in the test logic loop.
    }
}
