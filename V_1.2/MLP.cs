using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_1._2
{
    class MLP
    {
        private double[] input;
        private double[][] weights;
        private double[] output;
        private Func<double, double> activationFunction;

        public MLP(int inputSize, int[] hiddenLayerSizes, int outputSize, Func<double, double> activationFunction)
        {
            input = new double[inputSize];
            output = new double[outputSize];
            this.activationFunction = activationFunction;

            weights = new double[hiddenLayerSizes.Length + 1][];
            weights[0] = new double[inputSize];

            Random random = new Random();

            for (int i = 1; i <= hiddenLayerSizes.Length; i++)
            {
                weights[i] = new double[hiddenLayerSizes[i - 1]];
                for (int j = 0; j < hiddenLayerSizes[i - 1]; j++)
                {
                    weights[i][j] = random.NextDouble() * 2 - 1;
                }
            }

            weights[weights.Length - 1] = new double[outputSize];
            for (int i = 0; i < outputSize; i++)
            {
                weights[weights.Length - 1][i] = random.NextDouble() * 2 - 1;
            }
        }

        public double[] FeedForward(double[] input)
        {
            this.input = input;

            for (int i = 0; i < weights.Length - 1; i++)
            {
                double[] nextLayerOutput = new double[weights[i + 1].Length];
                for (int j = 0; j < weights[i + 1].Length; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < weights[i].Length; k++)
                    {
                        sum += input[k] * weights[i][k];
                    }
                    nextLayerOutput[j] = activationFunction(sum);
                }

                input = nextLayerOutput;
            }

            for (int i = 0; i < output.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < weights[weights.Length - 1].Length; j++)
                {
                    sum += input[j] * weights[weights.Length - 1][j];
                }
                output[i] = activationFunction(sum);
            }

            return output;
        }

        public void Backpropagation(double[] target, double learningRate)
        {
            double[] errors = new double[output.Length];

            for (int i = 0; i < output.Length; i++)
            {
                errors[i] = target[i] - output[i];
            }

            for (int i = weights.Length - 1; i >= 0; i--)
            {
                double[] gradients = new double[weights[i].Length];

                if (i == weights.Length - 1)
                {
                    for (int j = 0; j < weights[i].Length; j++)
                    {
                        gradients[j] = errors[j] * (1 - output[j] * output[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < weights[i].Length; j++)
                    {
                        double sum = 0;
                        for (int k = 0; k < weights[i + 1].Length && k < gradients.Length; k++) // Добавляем проверку для размера массива gradients
                        {
                            sum += weights[i + 1][k] * gradients[k];
                        }
                        gradients[j] = sum * (1 - input[j] * input[j]);
                    }
                }

                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i + 1].Length && k < input.Length; k++) // Добавляем проверку для размера массива input
                    {
                        weights[i][j] += learningRate * gradients[k] * input[j];
                    }
                }

                input = gradients;
            }
        }
    }
}
