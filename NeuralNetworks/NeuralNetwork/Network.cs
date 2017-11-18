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
                    float[,] weights = CeateRandomWeightsForLayerNeurons(this.Size[layer + 1], this.Size[layer], neuronId);
                    Neurons.Add(new Neuron((float)random.NextDouble(), neuronId, layer, weights));
                }
            }
        }

        private float[,] CeateRandomWeightsForLayerNeurons(int destinationLayer, int originLayer, int neuronId)
        {
            int neuronsCount = Size[destinationLayer];
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
                weights[WeightInfo.Original_Neuron_Layer, i] = neuronId;
            }

            GlobalWeights.Add(weights);

            return weights;
        }

        public static float Sigmoid(float value)
        {
            return (float)(1.0 / (1.0 + Math.Pow(Math.E, -value)));
        }

        public int[] Feedforward(float[] input)
        {
            for (int layer = 0; layer < this.Size.Count; layer++)
            {
                var test = Neurons.Where(e => e.layer == layer).Select(e => e.weights).ToArray();


                //for (int neuronId = 1; neuronId <= this.Size[layer]; neuronId++)
                //{
                //    input = Neurons.Where(e => e.layer == layer)
                //}
            }
        }
    }
}
