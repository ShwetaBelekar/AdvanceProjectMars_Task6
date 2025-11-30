using AdvanceProjectMars_Task6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.State
{
    public class EducationState
    {
        public List<EducationRecord> EducationRecords { get; set; }
        public EducationRecord CurrentEducationRecord { get; set; }
        public string CurrentUniversityName { get; set; }
        public string CurrentCountry { get; set; }
        public string CurrentTitle { get; set; }
        public string CurrentDegree { get; set; }
        public string CurrentGraduationYear { get; set; }
        public bool IsEducationRecordCreated { get; set; }
        public string EducationRecordCreationMessage { get; set; }
        public int SuccessfulRecordsCount { get; set; }
        public int TotalRecordsCount { get; set; }


        public EducationState()
        {
            EducationRecords = new List<EducationRecord>();
        }

    }
}
