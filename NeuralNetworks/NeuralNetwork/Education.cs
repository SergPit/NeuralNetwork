using NeuralNetwork.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Education
    {
        public void SimpleEducation(float[] input, int iterrations, Network network, float[] ouput, float alfa = 1)
        {
            for (int i = 0; i < iterrations; i++)
            {
                network.GoForward(input, ouput);
                network.GoBackward(alfa);
            }
        }

        public void ComplicatedEducation()
        {
            IAlgorithm algorithm = new Tanh();
            List<int> size = new List<int> { 30, 10, 6 };
            Network network = new Network(size, algorithm);

            float[] input = { 1, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0 };
            int iterrations = 2;
            float[] ouput = { 0, 0, 0, 1, 0, 0 };
            float alfa = 1;

            for (int i = 0; i < iterrations; i++)
            {
                network.GoForward(input, ouput);
                network.GoBackward(alfa);
                this.DisplayInformation(network, i);
                //DisplayAllValuesInformation(network, i);
            }

            // this.DisplayInformation(network, iterrations);
        }

        public void MnistEducation()
        {
            int alfa = 1; 
            Network network;
            ImageAsBytes[] images;
            GetPictures(out network, out images);

            for (int i = 0; i < images.Length; i++)
            {
                float[] expectedResult = new float[10];
                expectedResult[images[i].label] = 1;
                network.GoForward(images[i].pixels, expectedResult);
                network.GoBackward(alfa);
                DisplayAllValuesInformation(network, i);
                if (i % 100 == 0)
                {
                    DisplayInformation(network, i);
                }

            }
        }

        public void MnistTestOnePic()
        {
            int alfa = 1;
            int pictureNumber = 1;

            Network network;
            ImageAsBytes[] images;
            GetPictures(out network, out images);

            float[] expectedResult = new float[10];
            expectedResult[images[pictureNumber].label] = 1;

            for (int i = 0; i < 2; i++)
            {
                network.GoForward(images[pictureNumber].pixels, expectedResult);
                network.GoBackward(alfa);
                DisplayAllValuesInformation(network, pictureNumber);
            }
        }

        private static void GetPictures(out Network network, out ImageAsBytes[] images)
        { 
            MnistNetwork(out network);
            int imageSize = network.Size[0];
            string pixelFile = @"D:\NeuralNetworks\MnistUI\MnistUI\Data\train-images.idx3-ubyte";
            string labelFile = @"D:\NeuralNetworks\MnistUI\MnistUI\Data\train-labels.idx1-ubyte";

            images = MnistForNetwork.LoadData(pixelFile, labelFile, imageSize);
        }

        public void DisplayInformation(Network network, int iteration)
        {
            Console.WriteLine("----------------Iteration: " + iteration.ToString() + "----------------");
            network.DisplayInputAndError();
            Console.WriteLine(Environment.NewLine);
        }

        public void DisplayAllValuesInformation(Network network, int iteration)
        {
            Console.WriteLine("----------------Iteration: " + iteration.ToString() + "----------------");
            network.DisplayValues();
            Console.WriteLine(Environment.NewLine);
        }

        private static void MnistNetwork(out Network network)
        {
            int imageSize = 28 * 28;
            IAlgorithm algorithm = new Tanh();
            List<int> size = new List<int> { imageSize, 100, 10 };
            network = new Network(size, algorithm);
        }

       
    }
}
