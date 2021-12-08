/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrRenderer : MonoBehaviour
{
    public Material lineRendererMaterial;
    public float lineRendererWidth;

    // Start is called before the first frame update
    LineRenderer currentLineRenderer;
    InputController inputController;
    NodeController nodeController;
    public Dictionary<string, LineRenderer> lineRenderers= new Dictionary<string, LineRenderer>();
    LineRenderer nextBranchIndicator;
    void Start()
    {
        inputController = GameObject.Find("GameController").GetComponent<InputController>();
        nodeController = GameObject.Find("NodeController").GetComponent<NodeController>();

        InitIndicator();
    }

    // Update is called once per frame
    void Update()
    {
        IndicateNextPoint();
    }
    void InitIndicator()
    {
        GameObject lineRendererParent = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        lineRendererParent.AddComponent<LineRenderer>();
        nextBranchIndicator = lineRendererParent.GetComponent<LineRenderer>();
        nextBranchIndicator.positionCount = 0;


        nextBranchIndicator.material = lineRendererMaterial;
        nextBranchIndicator.startWidth = lineRendererWidth;
        nextBranchIndicator.endWidth = lineRendererWidth;
        nextBranchIndicator.material.color = Random.ColorHSV();
        
    }
    public void IndicateNextPoint()
    {
        if (nodeController.selectedNode != null)
        {
            nextBranchIndicator.positionCount = 2;

            nextBranchIndicator.SetPosition(0, nodeController.selectedNode.transform.position);
            nextBranchIndicator.SetPosition(1, inputController.controllerPosition);
        }
        else if(currentLineRenderer!=null&& currentLineRenderer.positionCount > 0)
        {
            nextBranchIndicator.positionCount = 2;

            nextBranchIndicator.SetPosition(0, currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 1));
            nextBranchIndicator.SetPosition(1, inputController.controllerPosition);
        }
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
            Color rndColor = Random.ColorHSV();
            rndColor.a = 0.3f;
            currentLineRenderer.material.color =rndColor;
            lineRenderers[letter] = currentLineRenderer;
        }
       

    }
}
*/