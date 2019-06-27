﻿using System;
using System.Collections.Generic;
using Pea.Core.Settings;

namespace Pea.Core
{
    public class PeaSettings
    {
        public Type Random { get; set; }

        public Type Algorithm { get; set; }

        public IList<PeaSettingsNamedType> Chromosomes { get; set; } =
            new List<PeaSettingsNamedType>();

        public IList<PeaSettingsNamedValue> ParameterSet { get; set; } =
            new List<PeaSettingsNamedValue>();

        public IList<PeaSettingsNamedType> EntityCreators { get; set; } =
            new List<PeaSettingsNamedType>();

        public IList<PeaSettingsTypeProbability> Selectors { get; set; } =
            new List<PeaSettingsTypeProbability>();

        public IList<PeaSettingsTypeProbability> Reinsertions { get; set; } =
            new List<PeaSettingsTypeProbability>();

        public IStopCriteria StopCriteria { get; set; }

        public Type Fitness { get; set; }
        public Type PhenotypeDecoder { get; set; }
        public Type Evaluation { get; set; }
    }
}