﻿using System.Collections.Generic;
using Pea.Chromosome.Implementation.SortedSubset;

namespace Pea.Tests.ChromosomeTests
{
    public class SortedSubsetTestData
    {
        public static SortedSubsetChromosome CreateChromosome()
        {
            var chromosome1 = new int[] { 1, 4, 5, 7 };
            var chromosome2 = new int[] { 3, 6, 8 };
            var chromosome3 = new int[] { 2, 9 };

            var chromosomes = new int[][]
            {
                chromosome1, chromosome2, chromosome3
            };

            var chromosome = new SortedSubsetChromosome(chromosomes);

            return chromosome;
        }

        public static SortedSubsetChromosome CreateOtherChromosome()
        {
            var chromosome1 = new int[] { 3, 4, 9 };
            var chromosome2 = new int[] { 2, 5, 7 }; 
            var chromosome3 = new int[] { 1, 6, 8 }; 

            var chromosomes = new int[][]
            {
                chromosome1, chromosome2, chromosome3
            };

            var chromosome = new SortedSubsetChromosome(chromosomes);

            return chromosome;
        }

        public static IList<SortedSubsetChromosome> CreateChromosomes()
        {
            return new List<SortedSubsetChromosome>()
            {
                CreateChromosome(),
                CreateOtherChromosome()
            };
        }
    }
}