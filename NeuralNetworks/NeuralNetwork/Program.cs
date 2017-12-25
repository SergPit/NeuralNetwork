using NeuralNetwork.Algorithms;
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
            float[] input = { 1, 0, 1, 0 }, output = { 0, 1 };
            Education education = new Education();
            IAlgorithm algorithm = new Tanh();

            Network network = new Network(new List<int> { 4, 3, 2 }, algorithm);
            education.SimpleEducation(input, 100, network, output, 1);

            network = new Network(new List<int> { 4, 3, 2 }, algorithm);
            education.SimpleEducation(input, 100, network, output, 5);

            Console.ReadKey();
        }
    }
}
