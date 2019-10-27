using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    ANN ann;
    double sumSquareError;

    // Start is called before the first frame update
    void Start()
    {
        ann = new ANN(2, 1, 0, 2, 0.8f);

        List<double> result;

        for (int i = 0; i < 1000; i++)
        {
            sumSquareError = 0;
            result = Train(new double[] { 0, 0 }, new double[] { 0 });
            sumSquareError += Mathf.Pow((float) result[0] - 0, 2);
            result = Train(new double[] { 0, 1 }, new double[] { 1 });
            sumSquareError += Mathf.Pow((float)result[0] - 1, 2);
            result = Train(new double[] { 1, 0 }, new double[] { 1 });
            sumSquareError += Mathf.Pow((float)result[0] - 1, 2);
            result = Train(new double[] { 1, 1 }, new double[] { 0 });
            sumSquareError += Mathf.Pow((float)result[0] - 0, 2);
        }

        Debug.Log("SSE = " + sumSquareError);

        result = Train(new double[] { 0, 0 }, new double[] { 0 });
        Debug.Log(string.Format("0 0 {0}", result[0]));
        result = Train(new double[] { 0, 1 }, new double[] { 1 });
        Debug.Log(string.Format("0 1 {0}", result[0]));
        result = Train(new double[] { 1, 0 }, new double[] { 1 });
        Debug.Log(string.Format("1 0 {0}", result[0]));
        result = Train(new double[] { 1, 1 }, new double[] { 0 });
        Debug.Log(string.Format("1 1 {0}", result[0]));
    }


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
    }
}
