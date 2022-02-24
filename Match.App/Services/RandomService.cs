using System;

namespace Match.App.Services
{
    public class RandomService : IRandomService
    {
        private readonly Random _random;

        public RandomService()
        {
            _random = new Random();
        }

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
