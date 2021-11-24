using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrRenderer : MonoBehaviour
{
    public Material lineRendererMaterial;
    public float lineRendererWidth;

    // Start is called before the first frame update
    LineRenderer currentLineRenderer;
    public Dictionary<string, LineRenderer> lineRenderers= new Dictionary<string, LineRenderer>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(string letter, Vector3 position)
    {
        UpdateCurrentLineRenderer(letter);
        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount-1,position);

    }
    public Vector3 GetDelta()
    {
        if (currentLineRenderer.positionCount == 1)
        {
            return currentLineRenderer.GetPosition(0);
        }
        else
        {
            return currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 1) - currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 2);
        }
    }
    public void UpdateCurrentLineRenderer(string letter)
    {
        if (lineRenderers.ContainsKey(letter))
        {
            currentLineRenderer = lineRenderers[letter];
        }
        else{
            GameObject lineRendererParent=Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
            lineRendererParent.AddComponent<LineRenderer>();
            currentLineRenderer = lineRendererParent.GetComponent<LineRenderer>();
            currentLineRenderer.positionCount = 0;


            currentLineRenderer.material = lineRendererMaterial;
            currentLineRenderer.startWidth = lineRendererWidth;
            currentLineRenderer.endWidth = lineRendererWidth;
            currentLineRenderer.material.color = Random.ColorHSV();
            lineRenderers[letter] = currentLineRenderer;
        }
       

    }
}
