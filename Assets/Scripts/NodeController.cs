using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
public class NodeController : MonoBehaviour
{
    NodeSystem nodeSystem;
    public string nextLetter = "A";
    public Vector3 nextNode = Vector3.zero;
    public NodeModel selectedNode;
    LSystemController lSystemController;

    // Start is called before the first frame update
    void Start()
    {
        nodeSystem = GetComponent<NodeSystem>();
        lSystemController = GameObject.Find("LSystemController").GetComponent<LSystemController>();

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
        nodeSystem.addNode(letter, node );

        
    }
    public void AddNode()
    {
        Debug.Log("Adding Node " + this.nextLetter);

        nodeSystem.addNode(this.nextLetter, this.nextNode);

    }
    public string GetLetter()
    {
        return nextLetter;
    }
    public void UpdateLetter()
    {
        if (!lSystemController.Tip(selectedNode.index))
        {


            string[] allLetters = new string[nodeSystem.nodes.Keys.Count];
            nodeSystem.nodes.Keys.CopyTo(allLetters, 0);
            char lastLetter = allLetters.Last().ToCharArray().First();
            if (lastLetter == 'A')
            {
                lastLetter++;
            }

            lastLetter++;
            nextLetter = lastLetter.ToString();
        }
        else
        {
            nextLetter = selectedNode.letter;
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

}