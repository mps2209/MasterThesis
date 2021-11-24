using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystemController : MonoBehaviour
{
    LSystem lSystem;
    NodeRenderer nodeRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lSystem = GetComponent<LSystem>();
        nodeRenderer = GameObject.Find("NodeController").GetComponent<NodeRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddStep(string letter)
    {
        lSystem.AddStep(letter);
        nodeRenderer.RenderLSystem();

    }
    public void StepForward()
    {
        lSystem.StepForward();
        nodeRenderer.RenderLSystem();

    }
    public void StepBack()
    {
        lSystem.StepBack(1);
        nodeRenderer.RenderLSystem();

    }
    public void ReRender()
    {
        lSystem.StepBack(0);

    }
    public bool Tip(int index)
    {
        return lSystem.Tip(index);
    }
}
