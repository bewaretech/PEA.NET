﻿using System;
using System.Collections.Generic;
using Pea.Core;
using Pea.Core.Entity;
using Pea.Core.Island;
using Pea.Fitness;

namespace Pea.Configuration.Implementation
{
    public class PeaSettings
    {
        public List<SubProblem> SubProblemList = new List<SubProblem>();
        public List<PeaSettingsNamedValue> ParameterSet = new List<PeaSettingsNamedValue>();
        public MigrationStrategy MigrationStrategy { get; set; }

        public Type EntityType { get; set; } = typeof(Entity);
        public Type Fitness { get; set; } = typeof(ParetoMultiobjective);
        public Type Evaluation { get; set; }
        public Type Random { get; set; } = typeof(FastRandom);

        public IStopCriteria StopCriteria { get; set; }

        public PeaSettings()
        {
            ParameterSet.Add(new PeaSettingsNamedValue(ParameterNames.ArchipelagosCount, 1));
        }

        public PeaSettings GetIslandSettings(MultiKey key)
        {
            var islandSettings = new PeaSettings()
            {
                MigrationStrategy = this.MigrationStrategy,
                EntityType = this.EntityType,
                Fitness = this.Fitness,
                Evaluation = this.Evaluation,
                Random = this.Random,
                StopCriteria = this.StopCriteria
            };

            foreach (var subProblem in SubProblemList)
            {
                if (key.Contains(subProblem.Encoding.Key))
                {
                    islandSettings.SubProblemList.Add(subProblem);
                }
            }

            return islandSettings;
        }
    }
}
