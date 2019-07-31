﻿using System;
using System.Collections.Generic;

namespace Pea.Core
{
    public interface IEntity : ICloneable
    {
        int IndexOfList { get; set; }
        MultiKey OriginIslandKey { get; }
        IDictionary<string, IChromosome> Chromosomes { get; }
        IFitness Fitness { get; set; }
        Dictionary<string, string> LastCrossOvers { get; set; }
        Dictionary<string, string> LastMutations { get; set; }
    }
}

