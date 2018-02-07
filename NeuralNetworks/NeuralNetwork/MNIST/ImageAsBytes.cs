namespace NeuralNetwork
{
    public class ImageAsBytes
    {
        public float[] pixels; // 0(white) - 255(black)
        public byte label; // '0' - '9'
        public float size;

        public ImageAsBytes(float[] pixels, byte label, int imageSize)
        {
            this.pixels = pixels;
            this.label = label;
            this.size = imageSize;
        }
    }
}
