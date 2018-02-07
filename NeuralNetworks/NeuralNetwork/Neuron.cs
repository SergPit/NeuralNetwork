using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Neuron
    {
        public float Bias { get; set; }

        public int Id { get; set; }

        public int Layer { get; set; }

        public Dictionary<NeuronCoordinate, float> WeightsByCoordinates { get; set; }

        public float Value { get; set; }

        //public float AfterTrainingDelta { get; set; }

        public float AfterTrainingValue { get; set; }

        public float Error { get; set; }

        public Neuron(float bias, int id, int layer, Dictionary<NeuronCoordinate, float> coordinates)
        {
            this.Bias = bias;
            this.Id = id;
            this.Layer = layer;
            this.WeightsByCoordinates = coordinates; 
            this.Value = 0;
        }
    }
}
