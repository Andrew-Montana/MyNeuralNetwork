using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANN
{
    delegate double Sigmoid(double value);

    public int numInputs; // neurons
    public int numOutputs; // neurons
    public int numHiddenL; // layers
    public int numNPerHidden; // neurons per hidden layer
    public List<Layer> layers = new List<Layer>();
    public double learningRate;

    public ANN(int numInputs, int numOutputs, int numHL, int numNPH, double lr)
    {
        // init variables
        this.numInputs = numInputs;
        this.numOutputs = numOutputs;
        numHiddenL = numHL;
        numNPerHidden = numNPH;
        learningRate = lr;

        // Creating Layers
        // No sense of creating an empty layer just for abstraction. We actually transmiiting inputs througth method ForwardGo
        // And we start our work from hidden layers, so no need in having non usable layer here in List of Layers. Just imagine that we giving inputValues from actual input layer.
        // I can use input layer actually, but code would be so messy
        if (numHL > 0)
        {
            // Input Layer
            // # layers.Add(new Layer(numInputs));
            // Hidden Layers
            for (int i = 0; i < numHL; i++)
            {
                if (i == 0)
                    layers.Add(new Layer(numNPH, numInputs));
                else
                    layers.Add(new Layer(numNPH, numNPH));
            }
            // Output Layer
            layers.Add(new Layer(numOutputs, numNPH));
        }
        else
        {
            Debug.Log("You need at least 1 hidden layer");
        }

    }

    public List<double> ForwardGo(List<double> inputValues, List<double> desiredOutputs)
    {
        List<double> inputs = new List<double>(inputValues);
        List<double> outputs = new List<double>();

       // inputs = inputValues;

        // Layer
        for (int l = 0; l < layers.Count; l++)
        {
            if (l > 0)
                inputs = new List<double>(outputs);
            outputs.Clear();

            // Neuron
            for (int n = 0; n < layers[l].numNeurons; n++)
            {
                // Neuron Inputs
                double sum = 0;
                layers[l].neurons[n].inputs.Clear();
                for (int i = 0; i < layers[l].neurons[n].numInputs; i++)
                {
                    layers[l].neurons[n].inputs.Add(inputs[i]);
                    sum += layers[l].neurons[n].weights[i] * layers[l].neurons[n].inputs[i];
                }
               // sum -= layers[l].neurons[n].bias;
                layers[l].neurons[n].output = ActivationFunction(sum);
                outputs.Add(layers[l].neurons[n].output);
            }

        }
        UpdateWeights(outputs, desiredOutputs);
        return outputs;
    }

    public double ActivationFunction(double value)
    {
        Sigmoid sigm = (x) =>
        {
            double k = (double)System.Math.Exp(x);
            return k / (1.0f + k);
        };
        return sigm(value);
    }

    public void UpdateWeights(List<double> outputs, List<double> desiredOutputs)
    {
        double error;
        // Layers
        for (int l = layers.Count - 1; l >= 0; l--)
        {
            // Neurons
            for (int n = 0; n < layers[l].numNeurons; n++)
            {
                // errors
                if (l == (layers.Count - 1))
                {
                    error = desiredOutputs[n] - outputs[n]; // delta rule
                    layers[l].neurons[n].errorGradient = outputs[n] * (1 - outputs[n]) * error;
                }
                else
                {
                    layers[l].neurons[n].errorGradient = layers[l].neurons[n].output * (1 - layers[l].neurons[n].output);
                    double errorGradSum = 0;
                    for (int p = 0; p < layers[l + 1].numNeurons; p++)
                    {
                        errorGradSum += layers[l + 1].neurons[p].errorGradient * layers[l + 1].neurons[p].weights[n];
                    }
                    layers[l].neurons[n].errorGradient *= errorGradSum;
                }

                // Neuron Inputs. Actual weights update
                for (int i = 0; i < layers[l].neurons[n].numInputs; i++)
                {
                    if (l == (layers.Count - 1))
                    {
                        error = desiredOutputs[n] - outputs[n];
                        layers[l].neurons[n].weights[i] += learningRate * layers[l].neurons[n].inputs[i] * error;
                    }
                    else
                    {
                        layers[l].neurons[n].weights[i] += learningRate * layers[i].neurons[n].inputs[i] * layers[l].neurons[n].errorGradient;
                    }
                  //  layers[l].neurons[n].bias += learningRate * -1 * layers[l].neurons[n].errorGradient;
                }
            }
        }
    }
}