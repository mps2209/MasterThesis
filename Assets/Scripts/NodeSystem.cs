using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSystem : MonoBehaviour
{
    NodeRenderer nodeRenderer;
    Dictionary<string, List<Vector3>> nodes = new Dictionary<string, List<Vector3>>();
    // Start is called before the first frame update
    LSystemController lSystemController;
    void Start()
    {
        lSystemController = GameObject.Find("LSystemController").GetComponent<LSystemController>();
        nodeRenderer = GetComponent<NodeRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNode(string letter, Vector3 node)
    {
        if (nodes.ContainsKey(letter))
        {
            nodes[letter].Add(node);
        }
        else
        {
            nodes.Add(letter, new List<Vector3>());
            nodes[letter].Add(node);
        }
        lSystemController.AddStep(letter);
        nodeRenderer.RenderLSystem();
    }
    public Vector3 GetNodePosition(string letter, int index)
    {
        return nodes[letter][index];
    }
}