using AdvanceProjectMars_Task6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.State
{
    public class ManageRequestsTestState
    {
        public List<ManageRequestsDataModel> Records { get; set; } = new List<ManageRequestsDataModel>();

        public ManageRequestsDataModel CurrentRecord { get; set; }

        public bool IsRecordCreatedSuccessfully { get; set; } = false;
        public string LastActionMessage { get; set; } = string.Empty;
        public string PopupMessage { get; set; }
        // 4. Batch status counters. (Replaces SuccessfulRecordsCount/TotalRecordsCount)
        public int SuccessCount { get; set; } = 0;
        public int TotalCount { get; set; } = 0;
    }
}
