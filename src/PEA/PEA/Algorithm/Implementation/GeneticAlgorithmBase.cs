﻿using System.Collections.Generic;
using Pea.Core;

namespace Pea.Algorithm.Implementation
{
    public abstract class GeneticAlgorithmBase : IGeneticAlgorithm
    {
        public IEngine Engine { get; }
        public IPopulation Population { get; set; }

        public abstract void InitPopulation();
        public abstract void RunOnce();

        private EvaluationDelegate _evaluate;


        protected GeneticAlgorithmBase(IEngine engine)
        {
            Engine = engine;
        }

        public void SetEvaluationCallback(EvaluationDelegate evaluationCallback)
        {
            _evaluate = evaluationCallback;
        }

        protected IEntity CreateEntity()
        {
            var creator = Engine.EntityCreators.GetOne();
            var entity = creator.CreateEntity();
            return entity;
        }

        protected IList<IEntity> Evaluate(IList<IEntity> entities)
        {
            if (entities.Count == 0) return entities;

            return _evaluate(entities);
            //foreach (IEntity entity in entities)
            //{
            //    entity.Fitness = Engine.FitnessCalculator.CalculateFitness(entity.Phenotype);
            //}
        }

        protected void MergeToBests(IList<IEntity> entities)
        {
            Engine.MergeToBests(entities);
        }

        protected IList<IEntity> SelectParents(IList<IEntity> entities)
        {
            var selector = Engine.Selections.GetOne();
            var parents = selector.Select(entities);
            return parents;
        }

        protected IList<IEntity> Crossover(IList<IEntity> parents)
        {
            var children = Engine.EntityCrossover.Cross(parents);

            return children;
        }

        protected IList<IEntity> Mutate(IList<IEntity> children)
        {
            children = Engine.EntityMutation.Mutate(children);
            return children;
        }

        protected void Reinsert(IList<IEntity> targetPopulation, IList<IEntity> offspring, IList<IEntity> parents, IList<IEntity> sourcePopulation)
        {
            var replacement = Engine.Replacements.GetOne();
            replacement.Replace(targetPopulation, offspring, parents, sourcePopulation);
        }
    }
}