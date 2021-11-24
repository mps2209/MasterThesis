using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class NodeSystem : MonoBehaviour
{
    NodeRenderer nodeRenderer;
    
    public Dictionary<string, List<Vector3>> nodes = new Dictionary<string, List<Vector3>>();
    [SerializeField]
    public Vector3 startingPoint;
    bool startSet = false;
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
        if (!startSet)
        {
            Debug.Log("starting LSystem");
            this.startingPoint=node;
            startSet = true;
            return;
        }

        if (nodes.ContainsKey(letter))
        {
            nodes[letter].Add(node);
            Debug.Log(letter + " already has " + nodes[letter].Count + "entries");

        }
        else
        {
            nodes.Add(letter, new List<Vector3>());
            nodes[letter].Add(node);
            Debug.Log(letter + " already has " + nodes[letter].Count + "entries");

        }
        if (letter == "A")
        {
            lSystemController.StepForward();
        }
        else
        {
            lSystemController.ReRender();

        }
    }
    public Vector3 GetNodePosition(string letter, int index)
    {
        Debug.Log(letter + " at " + index);
        Debug.Log(" is at position " + nodes[letter][index]);

        return nodes[letter][index];
    }
    public int GetNumberOfNodesForLetter(string letter)
    {
        Debug.Log(letter + " has ");
        Debug.Log(nodes[letter].Count+ " entries");
        return nodes[letter].Count;
    }

    public void PersistData()
    {
        if (File.Exists(Application.persistentDataPath
                     + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
                          + "/MySaveData.dat");

        }
        Debug.Log("Saving Nodes");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
                     + "/MySaveData.dat");
        List<Node> listToSave = new List<Node>();
        listToSave.Add(new Node("", startingPoint.x, startingPoint.y, startingPoint.z));
        foreach (string key in nodes.Keys)
        {
            foreach(Vector3 node in nodes[key])
            {
                listToSave.Add(new Node(key, node.x, node.y, node.z));
            }
            

        }
        Debug.Log("Saving "+ listToSave.Count+ " Nodes");

        bf.Serialize(file, listToSave);

        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadData()
    {

        if (File.Exists(Application.persistentDataPath
                       + "/MySaveData.dat"))
        {
            
            List <Node> loadedNodes = new List<Node>();
            using (Stream stream = File.Open(Application.persistentDataPath
                       + "/MySaveData.dat", FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

               loadedNodes = (List<Node>)bformatter.Deserialize(stream);
            }
            Debug.Log("Loaded " + loadedNodes.Count + "Nodes");
            Node startingNode = loadedNodes.First();
            startingPoint = new Vector3(startingNode.x, startingNode.y, startingNode.z);
            startSet = true;
            loadedNodes.Remove(loadedNodes.First());
            nodes = new Dictionary<string, List<Vector3>>();

            foreach (Node node in loadedNodes)
            {
                addNode(node.letter, new Vector3(node.x, node.y, node.z));
            }

        }
        else
            Debug.LogError("There is no save data!");
    }

}

[Serializable]
class Node
{
    public string letter;
    public float x;
    public float y;
    public float z;
    public Node(string letter, float x,float y,float z)
    {
        this.letter = letter;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}