using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Network
    {
        public List<int> Size { get; private set; }

        public List<float[,]> GlobalWeights { get; private set; }

        public List<Neuron> Neurons { get; set; }

        public List<float> output { get; private set; }

        public List<float> input { get; private set; }

        public int LustLayerNumber
        {
            get
            {
                return this.Size.Count - 1;
            }
        }

        public Network(List<int> size)
        {
            GlobalWeights = new List<float[,]>();
            Neurons = new List<Neuron>();
            this.Size = size;
            CreateNeurons();
        }

        private void CreateNeurons()
        {
            var random = new Random();
            for (int layer = 0; layer < this.Size.Count; layer++)
            {
                for (int neuronId = 1; neuronId <= this.Size[layer]; neuronId++)
                {

                    float[,] weights = layer < (this.Size.Count - 1)
                        ? CeateRandomWeightsForLayerNeurons(layer + 1, layer, neuronId)
                        : default(float[,]);
                    Neurons.Add(new Neuron((float)random.NextDouble(), neuronId, layer, weights));
                }
            }
        }

        private float[,] CeateRandomWeightsForLayerNeurons(int destinationLayer, int originLayer, int neuronId)
        {
            int neuronsCount = this.Size[destinationLayer];
            var random = new Random();
            // [x,]: 0 - layer, 1 - neuron id, 2 - weight, 3 - original neuron layer, 4 - original neuron id
            // [,x]: number of connections
            float[,] weights = new float[5, neuronsCount];
            for (int i = 0; i < neuronsCount; i++)
            {
                weights[WeightInfo.Layer, i] = destinationLayer;
                weights[WeightInfo.Neuron_id, i] = i;
                weights[WeightInfo.Weight, i] = (float)random.NextDouble();
                weights[WeightInfo.Original_Neuron_Layer, i] = originLayer;
                weights[WeightInfo.Original_Neuron_Id, i] = neuronId;
            }

            GlobalWeights.Add(weights);

            return weights;
        }

        public void DisplayInformation()
        {
            Console.Write(string.Format("Neuron network layer size - {0} \n", this.Size.Count));
            Console.Write(string.Format("Neuron counts - {0} \n", this.Size.Sum(e => e)));
            Console.Write("Layers size:\n");
            for (int i = 0; i < this.Size.Count; i++)
            {
                Console.Write(string.Format("Layer - {0}: Neuron count - {1}\n", i, this.Size[i]));
            }

            Console.Write("Neuron matrix\n");

            foreach (var neuron in Neurons)
            {
                //test
                Console.Write("\n------\n");
                Console.Write(string.Format("Current neuron Layer: {0}\n", neuron.layer));
                Console.Write(string.Format("Current neuron Id: {0}\n", neuron.id));
                Console.Write(string.Format("Current neuron Bias: {0}\n", neuron.bias));
                Console.Write(string.Format("Current neuron Value: {0}\n", neuron.value));
                if (neuron.weights != null)
                {
                    int rowLength = neuron.weights.GetLength(0);
                    int colLength = neuron.weights.GetLength(1);

                    for (int i = 0; i < rowLength; i++)
                    {
                        switch (i)
                        {
                            case (WeightInfo.Layer):
                                Console.Write("Layer: ");
                                break;
                            case (WeightInfo.Neuron_id):
                                Console.Write("Neuron_id: ");
                                break;
                            case (WeightInfo.Weight):
                                Console.Write("Weight: ");
                                break;
                            case (WeightInfo.Original_Neuron_Layer):
                                Console.Write("Original_Neuron_Layer: ");
                                break;
                            case (WeightInfo.Original_Neuron_Id):
                                Console.Write("Original_Neuron_Id: ");
                                break;
                            default:
                                break;
                        }

                        for (int j = 0; j < colLength; j++)
                        {
                            Console.Write(string.Format("{0} ", neuron.weights[i, j]));
                        }
                        Console.Write(Environment.NewLine);
                    }
                }
                else
                {
                    Console.Write("Neuron does not have weights\n");
                }
            }
        }

        public float Sigmoid(float value)
        {
            return (float)(1.0 / (1.0 + Math.Pow(Math.E, -value)));
        }

        public float sigmoid_output_to_derivative(float output)
        {
            return output * (1 - output);
        }

        public List<float> Goforward(float[] input, float[] expectedResult)
        {
            var firstLayerNeyrons = this.Neurons.Where(e => e.layer == 0).ToList();
            for (int neyronId = 0; neyronId < this.Size[0]; neyronId++)
            {
                firstLayerNeyrons[neyronId].value = input[neyronId];
            }

            for (int layer = 1; layer < this.Size.Count; layer++)
            {
                int previousLayer = layer - 1;
                var curLayerNeyrons = this.Neurons.Where(e => e.layer == layer).ToList();
                var previousLayerNeyrons = this.Neurons.Where(e => e.layer == previousLayer).ToList();

                for (int curNeyronId = 0; curNeyronId < this.Size[layer]; curNeyronId++)
                {
                    float value = 0;
                    for (int prevNeyronId = 0; prevNeyronId < this.Size[previousLayer]; prevNeyronId++)
                    {
                        value += previousLayerNeyrons[prevNeyronId].weights[WeightInfo.Neuron_id, curNeyronId] * previousLayerNeyrons[prevNeyronId].value;
                    }

                    curLayerNeyrons[curNeyronId].value = this.Sigmoid(value + curLayerNeyrons[curNeyronId].bias);
                }
            }

            var lustLayerNeyrons = this.Neurons.Where(e => e.layer == this.LustLayerNumber).ToList();
            List<float> deviation = new List<float>();

            for (int neyronId = 0; neyronId < lustLayerNeyrons.Count; neyronId++)
            {
                deviation.Add(lustLayerNeyrons[neyronId].value - expectedResult[neyronId]);
            }

            return deviation;
        }

        public void GoBackward()
        {
            var lustLayerNeyrons = this.Neurons.Where(e => e.layer == this.LustLayerNumber).ToList();

            for (int layer = LustLayerNumber; layer > 0; layer--)
            {

                for (int curNeyronId = 0; curNeyronId < this.Size[layer]; curNeyronId++)
                {

                }
            }
        }
    }
}
