using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
