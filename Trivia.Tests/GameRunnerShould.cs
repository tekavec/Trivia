using System;
using System.Collections.Generic;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using NSubstitute.Core;
using NUnit.Framework;
using Trivia.Infrastructure;

namespace Trivia.Tests
{
    [TestFixture]
    [UseReporter(typeof(VisualStudioReporter))]
    public class GameShould
    {
        private IMyConsole _console; 

        [Test]
        public void DescribeTriviaGame()
        {
            var seedsOne = new[] { 1, 21, 311, 141, 2225, 6 };
            var seedsTwo = new[] {17, 19, 29, 37, 41, 57, 67};

            CombinationApprovals.VerifyAllCombinations(
                (seedOne, seedTwo) => 
                {
                    _console = new TestableConsole();
                    GameRunner.RunGame(
                        new Random(seedOne*seedTwo + seedTwo),
                        _console.WriteLine);
                    return _console.ToString();

                },
                seedsOne,
                seedsTwo);
        }
         
    }

    public class TestableConsole : IMyConsole
    {
        private readonly IList<string> _outputLines = new List<string>();

        public void WriteLine(string line)
        {
            _outputLines.Add(line);
        }

        public void WriteLine()
        {
        }

        public override string ToString()
        {
            return _outputLines.Join("\n");
        }
    }
}