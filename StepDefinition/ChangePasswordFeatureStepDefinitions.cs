using AdvanceProjectMars_Task6.State;
using AdvanceProjectMars_Task6.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.StepDefinition
{
    public sealed class ChangePasswordFeatureStepDefinitions : CommonDriver
    {
        private readonly ChangePasswordTestState _changepasswordState;
        public ChangePasswordFeatureStepDefinitions(ChangePasswordTestState state)
        {
            _changepasswordState = state;
        }
    }
}
