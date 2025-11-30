using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Models
{
    public class EducationRecord
    {
        [JsonPropertyName("CollegeUniversityName")]
        public string CollegeUniversityName { get; set; }

        [JsonPropertyName("CountryofCollegeUniversity")]
        public string CountryofCollegeUniversity { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }


        [JsonPropertyName("Degree")]
        public string Degree { get; set; }

        // Stored as an integer in JSON but will be converted to string for web interaction later.
        [JsonPropertyName("YearofGraduation")]
        public int YearofGraduation { get; set; }
    }
    public class ExampleContainer
    {
        // Must be initialized to avoid NullReferenceException
        [JsonPropertyName("data")]
        public List<EducationRecord> Data { get; set; } = new List<EducationRecord>();
    }

    // Represents the root structure of your JSON file (containing the "examples" key).
    // Renamed from EducationData to EducationDataModel to match the file name.
    public class EducationDataModel
    {
        [JsonPropertyName("examples")]
        public ExampleContainer Examples { get; set; }

        // Optional properties like "scenario_type" and "name" could be added here 
        // but are omitted for simplicity as they aren't used in the test logic loop.
    }
}
