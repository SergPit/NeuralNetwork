using NeuralNetwork.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {

            Education education = new Education();
            education.MnistEducation();
            Console.ReadKey();
        }

       
    }
}
