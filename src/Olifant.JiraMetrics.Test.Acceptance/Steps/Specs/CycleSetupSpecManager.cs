using System;
using System.Collections.Generic;
using System.Linq;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps.Specs
{
    public class CycleSetupSpecManager
    {
        private readonly IEnumerable<CycleSetupSpec> _cycleSetupSpecs;

        public CycleSetupSpecManager(IEnumerable<CycleSetupSpec> cycleSetupSpecs)
        {
            this._cycleSetupSpecs = cycleSetupSpecs;
        }

        public List<string> PreCycleStatuses
        {
            get
            {
                return this.GetStatuses(spec => spec.PrecycleStatus);
            }
        }

        public List<string> CycleStatuses
        {
            get
            {
                return this.GetStatuses(spec => spec.CycleStatus);
            }
        }

        public List<string> PostCycleStatuses
        {
            get
            {
                return this.GetStatuses(spec => spec.PostcycleStatus);
            }
        }

        private List<string> GetStatuses(Func<CycleSetupSpec, string> specField)
        {
            return this._cycleSetupSpecs
                .Select(specField)
                .Where(field => !string.IsNullOrEmpty(field))
                .ToList();
        }
    }
}