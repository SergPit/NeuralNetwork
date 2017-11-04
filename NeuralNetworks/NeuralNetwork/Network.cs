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

        public List<Neuron> Neyrons { get; set; }

        public Network(List<int> size)
        {
            this.Size = size;
            foreach (var item in size)
            {

            }
        }
    }
}
