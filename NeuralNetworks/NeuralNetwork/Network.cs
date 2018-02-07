using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Network
    {
        public List<int> Size { get; private set; }

        public List<Neuron> Neurons { get; set; }

        public List<float> Output
        {
            get
            {
                var lustLayerNeyrons = this.Neurons.Where(e => e.Layer == this.LastLayerNumber).ToList();
                List<float> output = new List<float>();
                lustLayerNeyrons.ForEach(e => output.Add(e.Value));
                return output;
            }
        }

        public float[] Input { get; private set; }

        public float[] ExpectedResult { get; private set; }

        public int LastLayerNumber { get; private set; }

        public IAlgorithm Algorithms { get; set; }

        public Network(List<int> size, IAlgorithm algorithms)
        {
            Neurons = new List<Neuron>();
            this.Size = size;
            this.LastLayerNumber = this.Size.Count - 1;
            this.Algorithms = algorithms;
            CreateNeurons();
        }

        private void CreateClearNeurons()
        {
            for (int layer = 0; layer < this.Size.Count; layer++)
            {
                for (int neuronId = 1; neuronId <= this.Size[layer]; neuronId++)
                {
                    Dictionary<NeuronCoordinate, float> weights = default(Dictionary<NeuronCoordinate, float>);
                    Neurons.Add(new Neuron(0, neuronId, layer, weights));
                }
            }
        }


        private void CreateNeurons()
        {
            for (int layer = 0; layer < this.Size.Count; layer++)
            {
                for (int neuronId = 1; neuronId <= this.Size[layer]; neuronId++)
                {

                    Dictionary<NeuronCoordinate, float> weights = layer < (this.Size.Count - 1)
                        ? CeateRandomWeightsForLayerNeurons(layer + 1, layer, neuronId)
                        : default(Dictionary<NeuronCoordinate, float>);
                    Neurons.Add(new Neuron(Helpers.GetRundomFloat(), neuronId, layer, weights));
                }
            }
        }

        private Dictionary<NeuronCoordinate, float> CeateRandomWeightsForLayerNeurons(int destinationLayer, int originLayer, int neuronId)
        {
            int destinationNeuronsCount = this.Size[destinationLayer];

            Dictionary<NeuronCoordinate, float> weights = new Dictionary<NeuronCoordinate, float>();

            for (int i = 0; i < destinationNeuronsCount; i++)
            {
                weights.Add(new NeuronCoordinate(destinationLayer, i), Helpers.GetRundomFloat());
            }

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
                Console.Write(string.Format("Current neuron Layer: {0}\n", neuron.Layer));
                Console.Write(string.Format("Current neuron Id: {0}\n", neuron.Id));
                Console.Write(string.Format("Current neuron Bias: {0}\n", neuron.Bias.ToString("F6")));
                Console.Write(string.Format("Current neuron Value: {0}\n", neuron.Value));
                Console.Write(string.Format("Current neuron Error: {0}\n", neuron.Error));
               // Console.Write(string.Format("Current neuron Error: {0}\n", neuron.AfterTrainingDelta));
                Console.Write(string.Format("Current neuron Error: {0}\n", neuron.AfterTrainingValue));
                if (neuron.WeightsByCoordinates != null)
                {
                    foreach (var item in neuron.WeightsByCoordinates)
                    {
                        Console.Write(string.Format("Neuron Layer - {0}, Neuron Id - {1}, Weights - {2}", item.Key.layer, item.Key.id, item.Value.ToString("F6")));
                    }
                }
                else
                {
                    Console.Write("Neuron does not have weights\n");
                }
            }
        }

        public void DisplayInputOutpuError()
        {
            var firstLayerNeyrons = this.Neurons.Where(e => e.Layer == 0).ToList();
            var lustLayerNeyrons = this.Neurons.Where(e => e.Layer == this.LastLayerNumber).ToList();

            Console.Write("Expexted:\n");
            foreach (var expected in this.ExpectedResult)
            {
                Console.Write(" " + expected.ToString());
            }
            Console.Write(Environment.NewLine);

            Console.Write("Input:\n");
            foreach (var neuron in firstLayerNeyrons)
            {
                Console.Write(" " + neuron.Value.ToString());
            }
            Console.Write(Environment.NewLine);

            Console.Write("Output and error:\n");
            foreach (var neuron in lustLayerNeyrons)
            {
                Console.WriteLine(string.Format("Output - {0}, Error - {1}", neuron.Value.ToString(), neuron.Error.ToString()));
            }
        }

        public void DisplayInputAndError()
        {
            var firstLayerNeyrons = this.Neurons.Where(e => e.Layer == 0).ToList();
            var lustLayerNeyrons = this.Neurons.Where(e => e.Layer == this.LastLayerNumber).ToList();

            Console.Write("Expexted:\n");
            foreach (var expected in this.ExpectedResult)
            {
                Console.Write(" " + expected.ToString("F5"));
            }
            Console.Write(Environment.NewLine);

            Console.Write("Output and error:\n");
            foreach (var neuron in lustLayerNeyrons)
            {
                Console.WriteLine(string.Format("Output - {0}, Error - {1}", neuron.Value.ToString("F5"), neuron.Error.ToString("F5")));
            }
        }

        public void DisplayValues()
        {
            for (int i = 0; i < this.Size.Count; i++)
            {
                var currentLayer = this.Neurons.Where(e => e.Layer == i).ToList();

                Console.Write("Layer - " + i.ToString() + ": ");
                foreach (var neyron in currentLayer)
                {
                    Console.Write(string.Format("{0}, ",neyron.Value));
                }
                Console.Write("\n");
            }
        }

        public void GoForward(float[] input, float[] expectedResult)
        {
            if (input.Length != Size[0] )
            {
                throw new ArgumentException("Input is not match first layer size");
            }

            if (expectedResult.Length != Size[Size.Count - 1])
            {
                throw new ArgumentException("Output is not match lust layer size");
            }

            this.Input = input;
            this.ExpectedResult = expectedResult;
            var firstLayerNeyrons = this.Neurons.Where(e => e.Layer == 0).ToList();

            for (int neyronId = 0; neyronId < this.Size[0]; neyronId++)
            {
                firstLayerNeyrons[neyronId].Value = this.Input[neyronId];
            }

            for (int layer = 1; layer < this.Size.Count; layer++)
            {
                int previousLayer = layer - 1;
                var curLayerNeyrons = this.Neurons.Where(e => e.Layer == layer).ToList();
                var previousLayerNeyrons = this.Neurons.Where(e => e.Layer == previousLayer).ToList();

                for (int curNeyronId = 0; curNeyronId < this.Size[layer]; curNeyronId++)
                {
                    float value = 0;
                    for (int prevNeyronId = 0; prevNeyronId < this.Size[previousLayer]; prevNeyronId++)
                    {
                        value += previousLayerNeyrons[prevNeyronId].WeightsByCoordinates[new NeuronCoordinate(layer, curNeyronId)] * previousLayerNeyrons[prevNeyronId].Value;
                    }

                    curLayerNeyrons[curNeyronId].Value = this.Algorithms.Activation(value + curLayerNeyrons[curNeyronId].Bias);
                }
            }
        }

        private void CalculateErrorForLastLayer(int layer)
        {
            var lustLayerNeyrons = this.Neurons.Where(e => e.Layer == layer).ToList();
            for (int neyronId = 0; neyronId < lustLayerNeyrons.Count; neyronId++)
            {
                lustLayerNeyrons[neyronId].Error = lustLayerNeyrons[neyronId].Value - this.ExpectedResult[neyronId];
            }
        }

        public void GoBackward(float alfa)
        {

            var lustLayerNeyrons = this.Neurons.Where(e => e.Layer == this.LastLayerNumber).ToList();

            //calculate error for last layer
            CalculateErrorForLastLayer(this.LastLayerNumber);

            for (int layerNumber = this.LastLayerNumber; layerNumber >= 0; layerNumber--)
            {
                int previousLayer = layerNumber - 1;
                if (previousLayer < 0)
                {
                    break;
                }
                var curLayerNeyrons = this.Neurons.Where(e => e.Layer == layerNumber).ToList();
                var previousLayerNeyrons = this.Neurons.Where(e => e.Layer == previousLayer).ToList();

                for (int curNeyronId = 0; curNeyronId < this.Size[layerNumber]; curNeyronId++)
                {
                    //  curLayerNeyrons[curNeyronId].AfterTrainingDelta = curLayerNeyrons[curNeyronId].Error * this.Algorithms.OutputToDerivative(curLayerNeyrons[curNeyronId].Value);
                    // curLayerNeyrons[curNeyronId].Bias -= curLayerNeyrons[curNeyronId].Bias * curLayerNeyrons[curNeyronId].AfterTrainingDelta;
                    curLayerNeyrons[curNeyronId].Bias -= curLayerNeyrons[curNeyronId].Bias * curLayerNeyrons[curNeyronId].Error;
                    for (int prevNeyronId = 0; prevNeyronId < this.Size[previousLayer]; prevNeyronId++)
                    {
                        //debug block
                        //var aftertTrData = curLayerNeyrons[curNeyronId].AfterTrainingDelta;
                        //var outputToDer = this.Algorithms.OutputToDerivative(curLayerNeyrons[curNeyronId].Value);
                        //var test1 = previousLayerNeyrons[prevNeyronId].WeightsByCoordinates[new NeuronCoordinate(layerNumber, curNeyronId)];
                        //var test2 = alfa * (previousLayerNeyrons[prevNeyronId].WeightsByCoordinates[new NeuronCoordinate(layerNumber, curNeyronId)] * curLayerNeyrons[curNeyronId].AfterTrainingDelta);
                        //-----------------

                        previousLayerNeyrons[prevNeyronId].WeightsByCoordinates[new NeuronCoordinate(layerNumber, curNeyronId)] -=
                           alfa * (previousLayerNeyrons[prevNeyronId].WeightsByCoordinates[new NeuronCoordinate(layerNumber, curNeyronId)] * curLayerNeyrons[curNeyronId].Error);
                    }
                }
            }
        }
    }
}
