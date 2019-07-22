﻿using Pea.Core;
using System;

namespace Pea.Chromosome.Implementation.Permutation
{
    public class InverseRangeMutation : PermutationMutationBase
    {
        public InverseRangeMutation(IRandom random, IParameterSet parameterSet, IConflictDetector conflictDetector = null) : base(random, parameterSet, conflictDetector)
        {
        }

        public override PermutationChromosome Mutate(PermutationChromosome chromosome)
        {
            if (chromosome == null) throw new ArgumentNullException();
            if (chromosome.Genes.Length < 2) return null;

            var genes = chromosome.Genes;
            var range = GetSourceRange(chromosome);
            var rangeEnd = range.Position + range.Length -1;
            var halfLength = range.Length / 2;

            for (int pos = 0; pos < halfLength; pos++)
            {
                var tmp = genes[range.Position + pos];
                genes[range.Position + pos] = genes[rangeEnd - pos];
                genes[rangeEnd - pos] = tmp;
            }

            return chromosome;
        }
    }
}