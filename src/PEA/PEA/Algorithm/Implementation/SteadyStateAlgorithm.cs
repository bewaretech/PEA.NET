﻿using Pea.Core;

namespace Pea.Algorithm.Implementation
{
    public class SteadyStateAlgorithm : AlgorithmBase
    {
        public SteadyStateAlgorithm(IEngine engine, EvaluationDelegate evaluation) : base(engine, evaluation)
        {
        }

        public override void InitPopulation()
        {
            Population = new Population.Population();

            var maxNumberOfEntities = Engine.Parameters.GetInt(ParameterNames.MaxNumberOfEntities);
            for (int i = 0; i < Population.MaxNumberOfEntities; i++)
            {
                var entity = CreateEntity();
                Population.Add(entity);
            }

            Evaluate(Population.Entities);
            MergeToBests(Population.Entities);
        }

        public override void RunOnce()
        {
            var parents = SelectParents(Population.Entities);
            var children = Crossover(parents);
            children = Mutate(children);
            Evaluate(children);
            //TODO: Reduction (children) ?
            MergeToBests(children);
            Reinsert(Population.Entities, children, parents, Population.Entities);
        }
    }
}