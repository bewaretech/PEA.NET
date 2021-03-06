﻿using Pea.Core;
using System;
using System.Collections.Generic;

namespace Pea.Chromosome.Implementation.Permutation
{
    public class RelocateRangeMutation : PermutationMutationBase
    {
        public RelocateRangeMutation(IRandom random, IParameterSet parameterSet, IList<IConflictDetector> conflictDetectors = null) : base(random, parameterSet, conflictDetectors)
        {
        }

        public override PermutationChromosome Mutate(PermutationChromosome chromosome)
        {
            if (chromosome == null) return null;
            if (chromosome.Genes.Length < 2) return chromosome;

            //TODO: ConflictCheck, retry

            //int retryCount = ParameterSet.GetInt(ParameterNames.FailedMutationRetryCount);
            //while (true)
            //{
            var sourceRange = GetSourceRange(chromosome);
            int targetPosition = GetRandomPositionWithTabuRange(chromosome, sourceRange);

            if (targetPosition < sourceRange.Position)
            {
                var swapRange = new GeneRange(targetPosition, sourceRange.Position - targetPosition);
                chromosome.Genes = SwapTwoRange(chromosome.Genes, swapRange, sourceRange);
            }
            else
            {
                int sourceEndPosition = sourceRange.Position + sourceRange.Length;
                var swapRange = new GeneRange(sourceEndPosition, targetPosition - sourceEndPosition);
                chromosome.Genes = SwapTwoRange(chromosome.Genes, sourceRange, swapRange);
            }
            //    if (success || retryCount-- < 0) break;
            //}
            return chromosome;

        }
    }
}
