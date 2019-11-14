using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public double bias;
    public double output;
    public List<double> inputs = new List<double>();
    public List<double> weights = new List<double>();
    public int numInputs;
    public double errorGradient;

    public Neuron(int numInputs)
    {
        this.numInputs = numInputs;
        bias = 1f;
        for (int i = 0; i < numInputs; i++)
        {
            weights.Add(UnityEngine.Random.Range(-1f, 1f));
        }
    }

    /*
    public Neuron(int numInputs, List<double> weights, double bias)
    {
        this.numInputs = numInputs;
        this.weights = weights;
        this.bias = bias;
    }
    */

    /*
    public Neuron()
    {
    }
    */
}
