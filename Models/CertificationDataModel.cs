using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Models
{
    public class CertificationDataModel
    {
        [JsonPropertyName("CertificateorAward")]
        public string CertificateorAward { get; set; }

        [JsonPropertyName("CertifiedFrom")]
        public string CertifiedFrom { get; set; }

        [JsonPropertyName("Year")]
        public int Year { get; set; }
    }
    public class CertificationExampleContainer
    {
        // Must be initialized to avoid NullReferenceException
        [JsonPropertyName("data")]
        public List<CertificationDataModel> Data { get; set; } = new List<CertificationDataModel>();
    }

    // Represents the root structure of your JSON file (containing the "examples" key).
    public class CertificationFileModel
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
        public CertificationExampleContainer Examples { get; set; }

        // Optional properties like "scenario_type" and "name" could be added here 
        // but are omitted for simplicity as they aren't used in the test logic loop.
    }
}
