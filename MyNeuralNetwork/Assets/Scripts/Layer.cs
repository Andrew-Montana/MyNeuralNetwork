using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer
{
    public List<Neuron> neurons = new List<Neuron>();
    public int numNeurons;

    public Layer(int numNeurons, int numNeuronInputs)
    {
        this.numNeurons = numNeurons;
        for (int i = 0; i < numNeurons; i++)
        {
            neurons.Add(new Neuron(numNeuronInputs));
        }
    }
}
