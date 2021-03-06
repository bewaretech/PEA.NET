﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Pea.Core.Entity
{
    public class EntityCrossover : IEntityCrossover
    {
        public Dictionary<string, IProvider<ICrossover>> CrossoverProviders { get; } = new Dictionary<string, IProvider<ICrossover>>();

        public EntityCrossover(IDictionary<string, IChromosomeFactory> chromosomeFactories, IRandom random)
        {
            foreach (var key in chromosomeFactories.Keys)
            {
                var factory = chromosomeFactories[key];

                var crossovers = factory.GetCrossovers();
                var crossoverProvider = ProviderFactory.Create<ICrossover>(crossovers.Count(), random);
                foreach (var crossover in crossovers)
                {
                    crossoverProvider.Add(crossover, 1.0);
                }

                CrossoverProviders.Add(key, crossoverProvider);
            }
        }

        public IList<IEntity> Cross(IList<IEntity> parents)
        {
            if (parents.Count < 2) throw new ArgumentException(nameof(parents));

            var children = new List<IEntity>();

            //TODO: operation with more than 2 parent / child entities

            foreach (var chromosomeName in parents[0].Chromosomes.Keys)
            {
                var parent1Chromosome = parents[0].Chromosomes[chromosomeName];
                var parent2Chromosome = parents[1].Chromosomes[chromosomeName];
                var parentChromosomes = new List<IChromosome>()
                {
                    parent1Chromosome,
                    parent2Chromosome
                };

                if (CrossoverProviders.ContainsKey(chromosomeName))
                {
                    var provider = CrossoverProviders[chromosomeName];
                    var crossover = provider.GetOne();

                    try
                    {
                        var crossoveredChromosomes = crossover.Cross(parentChromosomes);

                        if (crossoveredChromosomes.Count > 0)
                        {
                            var child1 = (IEntity) parents[0].Clone();
                            child1.Chromosomes[chromosomeName] = crossoveredChromosomes[0];
                            child1.LastCrossOvers.Add(chromosomeName, crossover.GetType().Name);
                            children.Add(child1);
                        }

                        if (crossoveredChromosomes.Count > 1)
                        {
                            var child2 = (IEntity) parents[1].Clone();
                            child2.Chromosomes[chromosomeName] = crossoveredChromosomes[1];
                            child2.LastCrossOvers.Add(chromosomeName, crossover.GetType().Name);
                            children.Add(child2);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }

            return children;
        }
    }
}
