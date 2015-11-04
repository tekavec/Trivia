using System;

namespace Trivia.Infrastructure
{
    public interface IRandomNumberGenerator
    {
        int NextRandomNumber(Random rand, int maxValue);
    }

    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        public virtual int NextRandomNumber(Random rand, int maxValue)
        {
            return rand.Next(maxValue);
        }
    }
}