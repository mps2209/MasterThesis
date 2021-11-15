using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<string, LineRenderer> renderedBranches = new Dictionary<string, LineRenderer>();
    LSystem lSystem;
    NodeSystem nodeSystem;
    public Material branchMaterial;
    public Material nodeMaterial;
    public Color nodeActive;
    public Color nodeInactive;

    public float branchThickness;
    Dictionary<char, int> renderedNodes = new Dictionary<char, int>();
    void Start()
    {
        lSystem = GameObject.Find("LSystemController").GetComponent<LSystem>();
        nodeSystem = GetComponent<NodeSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RenderLSystem()
    {
        DestroyPreviousSystem();
        
        string renderAxiom = lSystem.axiom.text;
        Debug.Log("Rendering: " + renderAxiom);
        int l = 0;
        while (l < renderAxiom.Length)
        {
            if (renderAxiom[l] != 'B')
            {


                if (!renderedNodes.ContainsKey(renderAxiom[l]))
                {
                    renderedNodes[renderAxiom[l]] = 0;
                }
                else
                {
                    renderedNodes[renderAxiom[l]]++;
                }
                string nodeLetter = renderAxiom[l].ToString();
                renderNode(nodeLetter, renderedNodes[renderAxiom[l]]);

            }
            l++;
        }
    }
    void DestroyPreviousSystem()
    {
        foreach (GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            Destroy(node);
        }
        renderedNodes = new Dictionary<char, int>();
    }

    void renderNode(string letter, int index)
    {
        Debug.Log("Adding node: " + letter + index);
        GameObject newNode = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newNode.transform.position= nodeSystem.GetNodePosition(letter, index);
        newNode.transform.localScale = new Vector3(.2f, .2f, .2f);
        newNode.tag = "Node";
        NodeModel newNodeModel = newNode.AddComponent<NodeModel>();
        newNodeModel.letter = letter;
        newNodeModel.index = index;
        newNodeModel.SetMaterial(nodeMaterial);
        newNodeModel.SetColors(nodeActive, nodeInactive);
    }

}

