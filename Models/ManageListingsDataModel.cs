using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Models
{
    public class ManageListingsDataModel
    {
        [JsonPropertyName("Title")]
        public string Title { get; set; }
    }
    public class ManageListingsExampleContainer
    {
        // Must be initialized to avoid NullReferenceException
        [JsonPropertyName("data")]
        public List<ManageListingsDataModel> Data { get; set; } = new List<ManageListingsDataModel>();
    }
    public class ManageListingsFileModel
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
        public ManageListingsExampleContainer Examples { get; set; }

        // Optional properties like "scenario_type" and "name" could be added here 
        // but are omitted for simplicity as they aren't used in the test logic loop.
    }
}
