﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    ANN ann;
    double sumSquareError = 0;
    List<double> result;

    void Start()
    {
        ann = new ANN(2, 1, 2, 3, 0.8);
    }

    public void btnTrain()
    {
        Debug.Log("########################");
        for (int i = 0; i < 1000; i++)
        {
            sumSquareError = 0;
            result = Train(1, 1, 0);
            sumSquareError += Mathf.Pow((float)result[0] - 0, 2);
            result = Train(1, 0, 1);
            sumSquareError += Mathf.Pow((float)result[0] - 1, 2);
            result = Train(0, 1, 1);
            sumSquareError += Mathf.Pow((float)result[0] - 1, 2);
            result = Train(0, 0, 0);
            sumSquareError += Mathf.Pow((float)result[0] - 0, 2);
        }
        Debug.Log("SSE: " + sumSquareError);

        result = Train(1, 1, 0);
        Debug.Log(" 1 1 " + result[0]);
        result = Train(1, 0, 1);
        Debug.Log(" 1 0 " + result[0]);
        result = Train(0, 1, 1);
        Debug.Log(" 0 1 " + result[0]);
        result = Train(0, 0, 0);
        Debug.Log(" 0 0 " + result[0]);
    }

    List<double> Train(double i1, double i2, double o)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(i1);
        inputs.Add(i2);
        outputs.Add(o);
        return (ann.ForwardGo(inputs, outputs));
    }

    /*
    public List<double> Train(double[] inputValues, double[] outputs)
    {
        List<double> inputs = new List<double>();
        List<double> desiredOutputs = new List<double>();
        foreach(var i in inputValues)
        {
            inputs.Add(i);
        }
        foreach (var o in outputs)
        {
            desiredOutputs.Add(o);
        }
        return ann.Go(inputs, desiredOutputs);
    } */
}
