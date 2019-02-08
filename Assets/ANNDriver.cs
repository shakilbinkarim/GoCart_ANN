using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANNDriver : MonoBehaviour
{
    ANN ann;
    public float visibleDistance = 200;
    public int epochs = 1000;
    public float speed = 30.0f;
    public float rotationSpeed = 64.0f;

    private bool trainingDone = false;
    private float trainingProgress = 0;
    private double sse = 0;
    private double lastSSE = 1;

    public float translation;
    public float rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        ann = new ANN(5, 2, 1, 10, 0.5);
        StartCoroutine(LoadTrainingSet());
    }

    IEnumerator LoadTrainingSet()
    {
        string path = Application.dataPath + "/trainingData.txt";

        throw new NotImplementedException();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(25, 25, 250, 30), "SSE: " + lastSSE);
        GUI.Label(new Rect(25, 40, 250, 30), "Alpha: " + ann.alpha);
        GUI.Label(new Rect(25, 55, 250, 30), "Trained: " + trainingProgress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
