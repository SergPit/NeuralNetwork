using System;

namespace NeuralNetwork
{
    public class Education
    {
        public void SimpleEducation(float[] input, int iterrations, Network network)
        {
            for (int i = 0; i < iterrations; i++)
            {
                network.Goforward(input, input);
                network.GoBackward();
                Console.WriteLine("----------------Iteration: " + i.ToString() + "----------------");
                network.DisplayInputOutpuError();
                Console.WriteLine("----------------Network Info----------------");
                network.DisplayInformation();
            }
        }
    }
}
