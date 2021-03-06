﻿using System;
using System.Collections.Generic;

namespace Pea.Configuration.Implementation
{
    public class Algorithm
    {
        public Type AlgorithmType { get; set; }

        public Algorithm(Type algorithmType)
        {
            AlgorithmType = algorithmType;
        }

        public List<IBuildAction> Selections = new List<IBuildAction>();

        public List<IBuildAction> Reinsertions = new List<IBuildAction>();
    }
}
