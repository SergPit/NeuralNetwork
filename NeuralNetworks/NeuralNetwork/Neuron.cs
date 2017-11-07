namespace NeuralNetwork
{
    public struct Neuron
    {
        public float bias;

        public int id;

        public int layer;

        public float[,] weights;

        public Neuron(float bias, int id, int layer, float[,] weights)
        {
            this.bias = bias;
            this.id = id;
            this.layer = layer;
            this.weights = weights;
        }
    }
}
