using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public interface IAlgorithm
    {
        float Activation(float value);

        float OutputToDerivative(float output);
    }
}
