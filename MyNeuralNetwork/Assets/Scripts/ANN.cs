using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANN
{
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

        if(numHL > 0)
        {
            // Input Layer
            layers.Add(new Layer(numInputs, numInputs));
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
            layers.Add(new Layer(numOutputs, numInputs));
        }

    }

    public List<double> Go (List<double> inputValues, List<double> desiredOutput)
    { 
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();

        inputs = inputValues;

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
                double result = 0;
                layers[l].neurons[n].inputs = new List<double>(inputs);
                for (int i = 0; i < layers[l].neurons[n].numInputs; i++)
                {
                    result += layers[l].neurons[n].weights[i] * layers[l].neurons[n].inputs[i];
                }
                result -= layers[l].neurons[n].bias;
                layers[l].neurons[n].output = ActivationFunction(result);
                outputs.Add(layers[l].neurons[n].output);
            }

        }

        return outputs;
    }

    public double ActivationFunction(double value)
    {
        return 0;
    }
}
