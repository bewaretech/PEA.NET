﻿using System;
using System.Collections.Generic;
using System.Text;
using Pea.Core;

namespace Pea.Population
{
    public class Population : IPopulation
    {
        public IList<IEntity> Bests { get; set; }
        public int MaxNumberOfEntities { get; set; }
        public int MinNumberOfEntities { get; set; }
        public IList<IEntity> Entities { get; set; }
        public void Add(IEntity entity)
        {
            Entities.Add(entity);
        }
    }
}