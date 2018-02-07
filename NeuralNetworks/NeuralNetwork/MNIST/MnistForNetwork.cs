using System;
using System.IO;

namespace NeuralNetwork
{
    public class MnistForNetwork
    {
        public static ImageAsBytes[] LoadData(string pixelFile, string labelFile, int imageSize)
        {
            // Load MNIST training set of 60,000 images into memory
            // remove static to access listBox1
            int numImages = 60000;
            ImageAsBytes[] result = new ImageAsBytes[numImages];

            float[] pixels = new float[imageSize];

            FileStream ifsPixels = new FileStream(pixelFile, FileMode.Open);
            FileStream ifsLabels = new FileStream(labelFile, FileMode.Open);

            BinaryReader brImages = new BinaryReader(ifsPixels);
            BinaryReader brLabels = new BinaryReader(ifsLabels);

            int magic1 = brImages.ReadInt32(); // stored as Big Endian
            magic1 = ReverseBytes(magic1); // convert to Intel format

            int imageCount = brImages.ReadInt32();
            imageCount = ReverseBytes(imageCount);

            int numRows = brImages.ReadInt32();
            numRows = ReverseBytes(numRows);
            int numCols = brImages.ReadInt32();
            numCols = ReverseBytes(numCols);

            int magic2 = brLabels.ReadInt32();
            magic2 = ReverseBytes(magic2);

            int numLabels = brLabels.ReadInt32();
            numLabels = ReverseBytes(numLabels);

            // each image
            for (int im = 0; im < numImages; ++im)
            {
                for (int i = 0; i < imageSize; i++)
                {
                    byte b = brImages.ReadByte();
                    if (b>0)
                    {
                        int test = 1;
                    }
                    pixels[i] = MnistForNetwork.ByteToFloat(b);
                }

                byte lbl = brLabels.ReadByte(); // get the label
                ImageAsBytes dImage = new ImageAsBytes(pixels, lbl, imageSize);
                result[im] = dImage;
            } // each image

            ifsPixels.Close(); brImages.Close();
            ifsLabels.Close(); brLabels.Close();

            return result;
        }

        // ==== Code Logic methods =======================================================================

        //public static int ReverseBytes(int v) // 32 bits = 4 bytes
        //{
        //  // bit-manipulation version
        //  return (v & 0x000000FF) << 24 | (v & 0x0000FF00) << 8 |
        //         (v & 0x00FF0000) >> 8 | ((int)(v & 0xFF000000)) >> 24;
        //}

        private static int ReverseBytes(int v)
        {
            byte[] intAsBytes = BitConverter.GetBytes(v);
            Array.Reverse(intAsBytes);
            return BitConverter.ToInt32(intAsBytes, 0);
        }

        private static float ByteToFloat(float b)
        {
            return (float)(b / 255);

        }
    }
}
