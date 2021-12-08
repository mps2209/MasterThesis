/*using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
public class NodeController : MonoBehaviour
{
    NodeSystem nodeSystem;
    public char nextLetter = 'A';
    public Vector3 nextNode = Vector3.zero;
    public NodeModel selectedNode;
    LSystemController lSystemController;
    FluidVrRenderer fluidVrRenderer;
    List<char> rulesAdded = new List<char>();

    // Start is called before the first frame update
    void Start()
    {
        nodeSystem = GetComponent<NodeSystem>();
        lSystemController = GameObject.Find("LSystemController").GetComponent<LSystemController>();
        fluidVrRenderer = GameObject.Find("VrRenderer").GetComponent<FluidVrRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddNode(string letter, Vector3 node)
    {

        if (selectedNode != null)
        {
        }
        Debug.Log("Adding Node " + letter);
        nodeSystem.addNode(letter, node);


    }
    public void AddNode()
    {
        Debug.Log("Adding Node " + this.nextLetter);

        nodeSystem.addNode(this.nextLetter.ToString(), this.nextNode);

    }
    public char GetLetter()
    {
        return nextLetter;
    }
    public void UpdateLetter()
    {


        Debug.Log("Updating Letter");
        char[] allLetters = new char[fluidVrRenderer.renderers.Keys.Count];
        fluidVrRenderer.renderers.Keys.CopyTo(allLetters, 0);
        char lastLetter = allLetters.Last();
        if (lastLetter == 'A')
        {
            lastLetter++;
        }

        lastLetter++;
        nextLetter = lastLetter;
        if (!rulesAdded.Contains(nextLetter))
        {
            Debug.Log("Letter Not in rules yet, adding letter" + nextLetter + "For Node " + selectedNode.letter+selectedNode.index );
            rulesAdded.Add(nextLetter);
            lSystemController.UpdateRules(nextLetter.ToString(), selectedNode);

        }


    }
    public void PersistNodes()
    {
        nodeSystem.PersistData();
    }
    public void LoadNodes()
    {
        nodeSystem.LoadData();

    }

}*/