﻿using Newtonsoft.Json;
using NUnit.Framework;
using Optimization.Base;
using Optimization.Genetic;

namespace Optimization.Tests
{
    [TestFixture]
    class GeneticSharpExtensionsTests
    {
        [Test]
        public void LogicalOrTerminationTest()
        {
            // Config
            var config = JsonConvert.DeserializeObject<OptimizerConfiguration>(@"{
            ""genes"": [
            {
                ""key"": ""period"",
                ""min"": 60,
                ""max"": 300
            },
            {
                ""key"": ""mult"",
                ""min"": 1.5,
                ""max"": 2.9
            }
            ],
            ""population-initial-size"": 4,
            ""start-date"": ""2017 - 01 - 02"",
            ""end-date"": ""2019-06-10"",
            ""fitness-score"": ""SharpeRatio"",
            }");

            var ga = new GeneticAlgorithm(
                new PopulationRandom(config.GeneConfigArray, config.PopulationInitialSize),
                new OptimizerFitness(config.StartDate, config.EndDate, config.FitnessScore, true),
                new LinearTaskExecutor())
            {
                Termination = new LogicalOrTermination()
            };

            // Assert that LogicalOrTermination with no actual termination added does not throw and returns boolean value ->
            Assert.False(ga.Termination.HasReached(ga));
            Assert.DoesNotThrow(() => ga.Termination.HasReached(ga));
        }
    }
}
