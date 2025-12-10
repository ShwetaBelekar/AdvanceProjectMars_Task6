using AdvanceProjectMars_Task6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.State
{
    public class ProfileDescriptionTestState
    {
        public List<ProfileDescriptionDataModel> Records { get; set; } = new List<ProfileDescriptionDataModel>();
        public ProfileDescriptionDataModel CurrentRecord { get; set; }

        // 3. Status tracking fields (useful for capturing the immediate result of an action).
        public bool IsRecordCreatedSuccessfully { get; set; } = false;
        public string LastActionMessage { get; set; } = string.Empty;
        public string PopupMessage { get; set; }
        public bool IsDescriptionVisible { get; set; }
        public string TestResult { get; set; }

        // 4. Batch status counters. (Replaces SuccessfulRecordsCount/TotalRecordsCount)
        public int SuccessCount { get; set; } = 0;
        public int TotalCount { get; set; } = 0;
    }
}
