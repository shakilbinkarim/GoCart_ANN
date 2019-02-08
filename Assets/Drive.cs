using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 20.0f;
    public float visibleDistance = 200.0f;

    private List<string> collectedTrainingData = new List<string>();

    private float translation;
    private float rotation;
    private float translationInput;
    private float rotationInput;
    StreamWriter tdf;

    private void Start()
    {
        string path = Application.dataPath + "/trainingData.txt";
        tdf = File.CreateText(path);
    }

    private void OnApplicationQuit()
    {
        foreach (string td in collectedTrainingData)
        {
            tdf.WriteLine(td);
        }
        tdf.Close();
    }

    // Update is called once per frame
    void Update()
    {
        ControlDriving();
        DrawDebugLines();
        DoRayCasting();
    }

    float Round(float x)
    {
        return (float) System.Math.Round(x, System.MidpointRounding.AwayFromZero) / 2.0f;
    }

    private void DoRayCasting()
    {
        RaycastHit hit;
        float fDist = 0.0f, rDist = 0.0f, lDist = 0.0f, r45Dist = 0.0f, l45Dist = 0.0f;
        if (Physics.Raycast(transform.position, this.transform.forward, out hit, visibleDistance))
        {
            fDist = 1 - Round(hit.distance / visibleDistance); // since neurons fire when Input is 1.0
        }
        if (Physics.Raycast(transform.position, this.transform.right, out hit, visibleDistance))
        {
            rDist = 1 - Round(hit.distance / visibleDistance);
        }
        if (Physics.Raycast(transform.position, -this.transform.forward, out hit, visibleDistance))
        {
            lDist = 1 - Round(hit.distance / visibleDistance);
        }
        if (Physics.Raycast(transform.position, Quaternion.AngleAxis(45, Vector3.up) * this.transform.right, out hit, visibleDistance))
        {
            r45Dist = 1 - Round(hit.distance / visibleDistance);
        }
        if (Physics.Raycast(transform.position, Quaternion.AngleAxis(45, Vector3.up) * -this.transform.right, out hit, visibleDistance))
        {
            l45Dist = 1 - Round(hit.distance / visibleDistance);
        }
        string trainingData = fDist + "," + rDist + "," + lDist + "," + r45Dist + "," + l45Dist + "," + Round(translationInput) + "," + Round(rotationInput);
        if (!collectedTrainingData.Contains(trainingData))
        {
            collectedTrainingData.Add(trainingData);
        }
    }

    private void ControlDriving()
    {
        translationInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
        translation = Time.deltaTime * speed * translationInput;
        rotation = Time.deltaTime * rotationSpeed * rotationInput;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }

    private void DrawDebugLines()
    {
        Debug.DrawRay(transform.position, this.transform.forward * visibleDistance, Color.red);
        Debug.DrawRay(transform.position, this.transform.right * visibleDistance, Color.red);
    }
}
