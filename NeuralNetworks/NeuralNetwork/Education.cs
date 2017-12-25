using System;

namespace NeuralNetwork
{
    public class Education
    {
        public void SimpleEducation(float[] input, int iterrations, Network network, float[] ouput, float alfa = 1)
        {
            for (int i = 0; i < iterrations; i++)
            {
                network.Goforward(input, ouput);
                network.GoBackward(alfa);
            }

            Console.WriteLine("----------------Iteration: " + iterrations.ToString() + "----------------");
            network.DisplayInputOutpuError();
            Console.WriteLine(Environment.NewLine);
        }

    }
}
