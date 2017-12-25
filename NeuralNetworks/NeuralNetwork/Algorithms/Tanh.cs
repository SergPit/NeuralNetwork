using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Algorithms
{
    public class Tanh: IAlgorithm
    {
        public float Activation(float value)
        {
            return (float)((2.0 / (1.0 + Math.Pow(Math.E, -(2 * value)))) - 1);
        }

        public float OutputToDerivative(float output)
        {
            return output * (1 - output);
        }
    }
}
