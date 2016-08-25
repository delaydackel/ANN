using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    class Network
    {
        Random rand = new Random();                                                                             //yay zufall
        private List<int> sizes;                                                                                //liste der anzahl der nodes pro layer        
        private List<List<double>> biases = new List<List<double>>();                                           //liste der listen der biases in einem layer aka alle biases
        private List<List<List<double>>> weights = new List<List<List<double>>>();                              //alle weights

        public Network()
        {
            sizes.Add(0);
            sizes.Add(0);
        }

        public Network(List<int> sizes)
        {
            this.sizes = sizes;
            int numberOfLayers = sizes.Count();
            for (int i = 1; i < sizes.Count(); i++)                                                             //generate biases
            {
                List<double> biasesPerLayer = new List<double>();                                               //liste der biases in einem layer
                for (int j = 0; j < sizes[i]; j++)
                {
                    double bias = rand.NextDouble();
                    biasesPerLayer.Add(bias);
                }
                biases.Add(biasesPerLayer);
            }

            for (int i = 1; i < sizes.Count() - 1; i++)                                                           //generate weights
            {
                List<List<double>> weightsPerLayer = new List<List<double>>();
                for (int j = 0; j < sizes[i]; j++)
                {
                    List<double> weightsPerNeuron = new List<double>();
                    for (int k = 0; k < sizes[i + 1]; k++)
                    {
                        double weight = rand.NextDouble();
                        weightsPerNeuron.Add(weight);
                    }
                    weightsPerLayer.Add(weightsPerNeuron);
                }
                weights.Add(weightsPerLayer);
            }


        }

        public void PrintNetwork()
        {
            foreach (var size in sizes)
            {
                Console.Write(size.ToString() + " ");
            }
            foreach (var setOfBiases in biases)
            {
                Console.Write(Environment.NewLine);
                Console.Write("Biases: ");

                foreach (var bias in setOfBiases)
                {
                    Console.Write(bias.ToString() + " ");
                }
            }
            Console.Write(Environment.NewLine);

            foreach (var setOfWeightsPerLayer in weights)
            {
                Console.Write("weights per layer ");
                Console.Write(Environment.NewLine);
                foreach (var setOfWeights in setOfWeightsPerLayer)
                {
                    Console.Write("weights per neuron ");
                    Console.Write(Environment.NewLine);

                    foreach (var weight in setOfWeights)
                    {
                        Console.Write(weight.ToString() + " ");
                    }
                }
            }


        }
        public void feedforward(List<double> resultsFromPreviousLayer, int layerIndex)          //resultsFromPreviousLayer ^= a
        {
            //laufe über jeden weight vektor, multipliziere a, addiere biases, call self, return list<double> results wenn alle weights+biases abgearbeitet
            int numberOfLayers = weights.Count;
            List<List<double>> currentLayerWeights = weights[layerIndex];
            List<double> currentLayerResults = new List<double>();

            for (int i = 0; i < resultsFromPreviousLayer.Count; i++)
            {
                for (int j = 0; j < weights[layerIndex].Count; j++)
                {
                    foreach (var weight in weights[layerIndex][j])
                    {
                        foreach (var bias in biases[layerIndex])
                        {
                            currentLayerResults.Add(sigmoid((weight * resultsFromPreviousLayer[j]) + bias));
                        }
                    }
                }
                feedforward(currentLayerResults, layerIndex++);
            }

        }

        public void evaluate()
        {
            int numberOfCorrectResults = 0;


            /*         test_results = [(np.argmax(self.feedforward(x)), y)
                        for (x, y) in test_data]
        return sum(int(x == y) for (x, y) in test_results)*/
        }
        public double sigmoid(double z)
        {
            // z = w⋅x + b;

            return 1 / (1 + Math.Exp(-1 * z));
        }
        /*
#### Miscellaneous functions

def sigmoid_prime(z):
    """Derivative of the sigmoid function."""
    return sigmoid(z)*(1-sigmoid(z))
    */

    }
    class Program
    {

        static void Main(string[] args)
        {
            List<int> nodes = new List<int>(new int[] { 7, 15, 3 });
            Network net = new Network(nodes);
            net.PrintNetwork();
            Console.ReadKey();
        }
    }
}
