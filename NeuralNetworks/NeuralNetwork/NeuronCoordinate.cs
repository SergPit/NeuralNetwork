namespace NeuralNetwork
{
    public struct NeuronCoordinate
    {
        public int layer;

        public int id;

        public NeuronCoordinate(int layer, int id)
        {
            this.layer = layer;
            this.id = id;
        }

    }
}
