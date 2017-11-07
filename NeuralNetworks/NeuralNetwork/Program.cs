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
            Network network = new Network(new List<int> { 1, 2, 3 });
            Console.ReadKey();
        }
    }
}
