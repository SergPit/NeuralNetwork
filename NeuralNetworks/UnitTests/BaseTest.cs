using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NeuralNetwork;
using System.Linq;
using NeuralNetwork.Algorithms;

namespace UnitTests
{
    [TestClass]
    public class BaseTest
    {
        [TestMethod]
        public void TestSimple()
        {
            Network testNetwork = InitializeNetwork();
            float[] input = { 1, 0, 1, 0 };
            float[] expected = { 0 };
            testNetwork.GoForward(input, expected);
            var firstIteration = testNetwork.Neurons.Where(e => e.Layer == testNetwork.LastLayerNumber).First().Value;
            testNetwork.GoBackward(1);
            testNetwork.GoForward(input, expected);
            var secondIteration = testNetwork.Neurons.Where(e => e.Layer == testNetwork.LastLayerNumber).First().Value;
        }

        internal Network InitializeNetwork()
        {
            IAlgorithm algorithm = new Sigmoid();
            Network network = new Network(new List<int> { 4, 3, 1 }, algorithm);

            var firstLayerNeyrons = network.Neurons.Where(e => e.Layer == 0).ToList();
            firstLayerNeyrons[0].WeightsByCoordinates = new Dictionary<NeuronCoordinate, float>
            {
                {new NeuronCoordinate(1, 0), 0.007323372f },
                {new NeuronCoordinate(1, 1), 0.7420637f },
                {new NeuronCoordinate(1, 2), 0.06648151f },
            };

            firstLayerNeyrons[1].WeightsByCoordinates = new Dictionary<NeuronCoordinate, float>
            {
                {new NeuronCoordinate(1, 0), 0.8113588f },
                {new NeuronCoordinate(1, 1), 0.6027133f },
                {new NeuronCoordinate(1, 2), 0.04425582f },
            };

            firstLayerNeyrons[2].WeightsByCoordinates = new Dictionary<NeuronCoordinate, float>
            {
                {new NeuronCoordinate(1, 0), 0.6548705f },
                {new NeuronCoordinate(1, 1), 0.2263567f },
                {new NeuronCoordinate(1, 2), 0.7537105f },
            };

            firstLayerNeyrons[3].WeightsByCoordinates = new Dictionary<NeuronCoordinate, float>
            {
                {new NeuronCoordinate(1, 0), 0.6666188f },
                {new NeuronCoordinate(1, 1), 0.4808353f },
                {new NeuronCoordinate(1, 2), 0.963493f },
            };

            var secondLayerNeyrons = network.Neurons.Where(e => e.Layer == 1).ToList();

            secondLayerNeyrons[0].WeightsByCoordinates = new Dictionary<NeuronCoordinate, float>
            {
                {new NeuronCoordinate(2, 0), 0.3413954f }
            };

            secondLayerNeyrons[0].Bias = 0.9309797f;

            secondLayerNeyrons[1].WeightsByCoordinates = new Dictionary<NeuronCoordinate, float>
            {
                {new NeuronCoordinate(2, 0), 0.8267857f }
            };

            secondLayerNeyrons[1].Bias = 0.8839321f;

            secondLayerNeyrons[2].WeightsByCoordinates = new Dictionary<NeuronCoordinate, float>
            {
                {new NeuronCoordinate(2, 0), 0.8584864f }
            };

            secondLayerNeyrons[2].Bias = 0.06931701f;

            var thirdLayerNeyrons = network.Neurons.Where(e => e.Layer == 2).ToList();
            thirdLayerNeyrons[0].Bias = 0.7517884f;

            return network;
        }
    }
}
