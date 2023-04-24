/*using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystemController : MonoBehaviour
{
    LSystem lSystem;
    NodeRenderer nodeRenderer;
    LSystemRenderer lSystemRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lSystem = GetComponent<LSystem>();
        nodeRenderer = GameObject.Find("NodeController").GetComponent<NodeRenderer>();
        lSystemRenderer = GetComponent<LSystemRenderer>();
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
        lSystemRenderer.RenderLSystem();

    }
    public void StepForward(int numSteps)
    {
        for(int i = 0; i<numSteps;i++)
        {
            lSystem.StepForward();
        }

        lSystemRenderer.RenderLSystem();

    }
    public void StepBack()
    {
        lSystem.StepBack(1);
        //nodeRenderer.RenderLSystem();
        lSystemRenderer.RenderLSystem();

    }
    public void ReRender()
    {
        lSystem.StepBack(0);
        //nodeRenderer.RenderLSystem();
        lSystemRenderer.RenderLSystem();


    }
    public bool Tip(int index)
    {
        return lSystem.Tip(index);
    }
    public void UpdateRules(string letter, NodeModel previousNode)
    {
        //Debug.Log("Adding Rules for" + previousNode.letter+previousNode.index);
        //Debug.Log("New Rule has letter " + letter);

        //lSystem.UpdateRules(letter, previousNode);
    }
}
*/