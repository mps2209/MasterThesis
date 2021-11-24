using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class NodeRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<string, LineRenderer> renderedBranches = new Dictionary<string, LineRenderer>();
    Dictionary<char, Color> branchColors = new Dictionary<char, Color>();

    LSystem lSystem;
    NodeSystem nodeSystem;
    public Material branchMaterial;
    public Material nodeMaterial;
    public Color nodeActive;
    public Color nodeInactive;

    public float branchThickness;
    Dictionary<char, List<int>> renderedNodes = new Dictionary<char, List<int>>();
    public List<Vector3> savedPositions = new List<Vector3>();
    public GameObject interactableNodePrefab;
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
        int axiomIterator = 0;
        char previousLetter = '0';
        int letterCounter = 0;
        while (axiomIterator < renderAxiom.Length)
        {
            char currentLetter = renderAxiom[axiomIterator];

            if (renderedNodes.ContainsKey(currentLetter)){
                int previousCount= renderedNodes[currentLetter].Last();
                previousCount++;
                renderedNodes[currentLetter].Remove(renderedNodes[currentLetter].Last());
                renderedNodes[currentLetter].Add(previousCount);
            }
            else
            {
                renderedNodes[currentLetter] = new List<int>();
                renderedNodes[currentLetter].Add(0);
                if (!branchColors.ContainsKey(currentLetter))
                {
                    branchColors[currentLetter] = Random.ColorHSV();

                }
            }
            previousLetter = currentLetter;
            List<char> keys = renderedNodes.Keys.ToList();

            switch (currentLetter)
            {

                case '[':
                    savedPositions.Add(savedPositions.Last());
                    foreach(char key in keys)
                    {
                        renderedNodes[key].Add(0);
                    }

                    break;
                case ']':
                    foreach (char key in keys)
                    {
                        renderedNodes[key].Remove(renderedNodes[key].Last());
                    }
                    savedPositions.Remove(savedPositions.Last());
                    break;
                case 'B':
                    break;
                default:
                    char nodeLetter = renderAxiom[axiomIterator];
                    renderNode(nodeLetter, axiomIterator, renderedNodes[currentLetter].Last());
                    break;
            }
            axiomIterator++;

        }

    }

    void DestroyPreviousSystem()
    {
        foreach (GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            Destroy(node);
        }
        renderedNodes = new Dictionary<char, List<int>>();

        savedPositions = new List<Vector3>();
        savedPositions.Add(nodeSystem.startingPoint);
    }

    void renderNode(char letter, int index, int letterCounter)
    {
        Debug.Log("Adding node: " + letter + index);
        GameObject newNode = GameObject.Instantiate(interactableNodePrefab,Vector3.zero,Quaternion.identity);
        
        Vector3 nodePosition = nodeSystem.GetNodePosition(letter.ToString(), letterCounter % nodeSystem.GetNumberOfNodesForLetter(letter.ToString()));
        newNode.transform.position = savedPositions.Last() + nodePosition;
        savedPositions.Remove(savedPositions.Last());
        savedPositions.Add(newNode.transform.position);
        newNode.transform.localScale = new Vector3(.2f, .2f, .2f);
        newNode.tag = "Node";
        NodeModel newNodeModel = newNode.GetComponent<NodeModel>();
        newNodeModel.letter = letter.ToString();
        newNodeModel.index = index;
        newNodeModel.SetMaterial(nodeMaterial);
        newNodeModel.SetColors(branchColors[letter], branchColors[letter] * 0.9f);
    }

}

