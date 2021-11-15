using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    NodeSystem nodeSystem;
    public string nextLetter="A";
    public Vector3 nextNode;
    public NodeModel selectedNode;
    // Start is called before the first frame update
    void Start()
    {
        nodeSystem = GetComponent<NodeSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNode(string letter, Vector3 node)
    {
        nodeSystem.addNode(letter,node);
    }
    public void AddNode()
    {
        nodeSystem.addNode(this.nextLetter, this.nextNode);

    }


}
