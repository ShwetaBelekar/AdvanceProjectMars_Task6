using AdvanceProjectMars_Task6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.State
{
    public class ChangePasswordTestState
    {
        public List<ChangePasswordDataModel> Data { get; set; } = new List<ChangePasswordDataModel>();
        public ChangePasswordDataModel CurrentRecord { get; set; }
    }
}
