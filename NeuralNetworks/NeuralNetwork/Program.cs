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
            Network network = new Network(new List<int> { 10, 5, 1 });
            float[] input = { 1, 0, 1, 0, 0, 0, 1, 1, 0, 0 };
            network.Feedforward(input);
            network.DisplayInformation();
            Console.ReadKey();
        }
    }
}
