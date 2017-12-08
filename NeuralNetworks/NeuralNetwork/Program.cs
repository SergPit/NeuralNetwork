using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Network network = new Network(new List<int> { 4, 8, 4 });
            float[] input = { 1, 1, 0, 0};
            network.Goforward(input, input);
            network.DisplayInformation();
            network.GoBackward();
            network.DisplayInformation();
            Console.ReadKey();
        }
    }
}
