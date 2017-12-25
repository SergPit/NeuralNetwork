using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Algorithms
{
    public class Sigmoid: IAlgorithm
    {
        public float Activation(float value)
        {
            return (float)(1.0 / (1.0 + Math.Pow(Math.E, -value)));
        }

        public float OutputToDerivative(float output)
        {
            return output * (1 - output);
        }
    }
}
