using System;

namespace NeuralNetwork
{
    public static class Helpers
    {
        public static float GetRundomFloat()
        {
            return (float)new Random(Guid.NewGuid().GetHashCode()).NextDouble();
        }
    }
}
