﻿using FluentAssertions;
using Pea.Chromosome;
using Pea.Chromosome.Implementation.SortedSubset;
using Pea.Core;
using Xunit;

namespace Pea.Tests.ChromosomeTests.SortedSubsetTests
{
    public class SortedSubsetCrossoverTests
    {
        [Theory]
        [InlineData(new int[] {1, 4, 5, 7}, 2, new int[] {3, 5, 9}, 2, new int[] {1, 4, 9})]
        [InlineData(new int[] { 5, 7 }, 0, new int[] { 3, 5, 9 }, 2, new int[] { 9 })]
        [InlineData(new int[] { 1, 4, 5, 7 }, 2, new int[] { 3, 5, 9 }, 3, new int[] { 1, 4 })]
        public void GivenSortedSubsetOnePointCrossover_WhenMergeSections_ThenShouldBeProper(
            int[] sectionForLeft, int leftEndPosition, 
            int[] sectionForRight, int rightStartPosition, int[] expected)
        {
            bool childAlreadyConflicted = false;
            var random = new PredeterminedRandom(5);
            var parameterSet = new ParameterSet();
            var crossover = new OnePointCrossover(random, parameterSet, AllRightConflictDetector.Instance);

            var result = crossover.MergeSections(sectionForLeft, leftEndPosition, sectionForRight, rightStartPosition,
                ref childAlreadyConflicted);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GivenSortedSubsetChromosomes_WhenOnePointCrossOver_ThenShouldBeProper()
        {
            var chromosomes = SortedSubsetTestData.CreateChromosomes();
            var random = new PredeterminedRandom(5); //Crossover gene value
            var parameterSet = new ParameterSet();
            parameterSet.SetValue(ParameterNames.FailedCrossoverRetryCount, 0);
            var crossover = new OnePointCrossover(random, parameterSet, AllRightConflictDetector.Instance);

            var results = crossover.Cross(chromosomes);

            results[0].Sections[0].Should().BeEquivalentTo(new int[] { 1, 4, 9 });
            results[0].Sections[1].Should().BeEquivalentTo(new int[] { 3, 5, 7 });
            results[0].Sections[2].Should().BeEquivalentTo(new int[] { 2, 6, 8 });
            results[1].Sections[0].Should().BeEquivalentTo(new int[] { 3, 4, 5, 7 });
            results[1].Sections[1].Should().BeEquivalentTo(new int[] { 2, 6, 8 });
            results[1].Sections[2].Should().BeEquivalentTo(new int[] { 1, 9 });
        }

        [Fact]
        public void GivenSortedSubsetChromosomes_WhenOnePointCrossOver_WithConflict_ThenShouldReturnsOne()
        {
            var chromosomes = SortedSubsetTestData.CreateChromosomes();
            var random = new PredeterminedRandom(5); //Crossover gene value
            var parameterSet = new ParameterSet();
            parameterSet.SetValue(ParameterNames.FailedCrossoverRetryCount, 0);
            var conflictDetector = new PredeterminedConflictDetector(false, false, true, false, false, false);
            var crossover = new OnePointCrossover(random, parameterSet, conflictDetector);

            var results = crossover.Cross(chromosomes);

            results.Count.Should().Be(1);
            results[0].Sections[0].Should().BeEquivalentTo(new int[] { 3, 4, 5, 7 });
            results[0].Sections[1].Should().BeEquivalentTo(new int[] { 2, 6, 8 });
            results[0].Sections[2].Should().BeEquivalentTo(new int[] { 1, 9 });
        }

        [Fact]
        public void GivenSortedSubsetChromosomes_WhenOnePointCrossOver_WithConflict_ThenShouldReturnsZero()
        {
            var chromosomes = SortedSubsetTestData.CreateChromosomes();

            var random = new PredeterminedRandom(5); //Crossover gene value
            var parameterSet = new ParameterSet();
            parameterSet.SetValue(ParameterNames.FailedCrossoverRetryCount, 0);
            var conflictDetector = new PredeterminedConflictDetector(false, true, true);
            var crossover = new OnePointCrossover(random, parameterSet, conflictDetector);

            var results = crossover.Cross(chromosomes);

            results.Count.Should().Be(0);
        }

        [Fact]
        public void GivenSortedSubsetChromosomes_WhenOnePointCrossOver_WithConflict_ThenShouldReply()
        {
            var chromosomes = SortedSubsetTestData.CreateChromosomes();

            var random = new PredeterminedRandom(5, 7); //Crossover gene value before and after conflicts
            var parameterSet = new ParameterSet();
            parameterSet.SetValue(ParameterNames.FailedCrossoverRetryCount, 1);
            var conflictDetector = new PredeterminedConflictDetector(false, true, true, false, false, false, false, false, false, false);
            var crossover = new OnePointCrossover(random, parameterSet, conflictDetector);

            var results = crossover.Cross(chromosomes);

            results[0].Sections[0].Should().BeEquivalentTo(new int[] { 1, 4, 5, 9 });
            results[0].Sections[1].Should().BeEquivalentTo(new int[] { 3, 6, 7 });
            results[0].Sections[2].Should().BeEquivalentTo(new int[] { 2, 8 });
            results[1].Sections[0].Should().BeEquivalentTo(new int[] { 3, 4, 7 });
            results[1].Sections[1].Should().BeEquivalentTo(new int[] { 2, 5, 8 });
            results[1].Sections[2].Should().BeEquivalentTo(new int[] { 1, 6, 9 });
        }
    }
}