/*
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
    public Color branchColor;

    public float branchThickness;
    //Counts the branches and their length
    Dictionary<char, List<int>> renderedNodes = new Dictionary<char, List<int>>();
    //Stores Positions for branching off
    public List<LineRenderer> savedPositions = new List<LineRenderer>();
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
        while (axiomIterator < renderAxiom.Length)
        {
            char currentLetter = renderAxiom[axiomIterator];
            //Checking if we already rendered a branch of this type
            if (renderedNodes.ContainsKey(currentLetter)){
                //Checking and incrementing how many nodes of said branch are already rendered
                
                int previousCount = renderedNodes[currentLetter].Last();
                previousCount++;
                //Updating Count
                UpdateRenderedNodesCount(currentLetter, previousCount);

            }
            else
            {
                //Setting up node count for new branch
                renderedNodes[currentLetter] = new List<int>();
                renderedNodes[currentLetter].Add(0);
                //Creating a new Color for a new Branch
                if (!branchColors.ContainsKey(currentLetter))
                {
                    branchColors[currentLetter] = Random.ColorHSV();

                }
            }
            List<char> keys = renderedNodes.Keys.ToList();

            switch (currentLetter)
            {

                case '[':
                    //Each time we branch off we need to save the last position, therefore adding another entry to the list
                    savedPositions.Add(CreateNewBranchRenderer(savedPositions.Last().GetPosition(savedPositions.Last().positionCount-1)));
                    //Everytime we branch of we need to start a new nodes count.
                    foreach(char key in keys)
                    {
                        renderedNodes[key].Add(0);
                    }

                    break;
                case ']':
                    //We branch back to the previous position therefore remove the created entries
                    foreach (char key in keys)
                    {
                        renderedNodes[key].Remove(renderedNodes[key].Last());
                    }
                    savedPositions.Remove(savedPositions.Last());
                    break;
                case 'B':
                    //B does nothing
                    break;
                default:
                    // any other letter will be rendered
                    char nodeLetter = renderAxiom[axiomIterator];
                    renderNode(nodeLetter, axiomIterator, renderedNodes[currentLetter].Last());
                    break;
            }
            axiomIterator++;

        }

    }
    void UpdateRenderedNodesCount(char letter, int count) {
        renderedNodes[letter].Remove(renderedNodes[letter].Last());
        renderedNodes[letter].Add(count);
    }

    void DestroyPreviousSystem()
    {
        foreach (GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            Destroy(node);
        }
        foreach (GameObject branch in GameObject.FindGameObjectsWithTag("Branch"))
        {
            Destroy(branch);
        }
        renderedNodes = new Dictionary<char, List<int>>();

        savedPositions = new List<LineRenderer>();
        savedPositions.Add(CreateNewBranchRenderer(nodeSystem.startingPoint));
    }
    LineRenderer CreateNewBranchRenderer(Vector3 startPoint)
    {
        GameObject lineRendererParent = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        lineRendererParent.tag = "Branch";
        lineRendererParent.AddComponent<LineRenderer>();
        LineRenderer currentLineRenderer = lineRendererParent.GetComponent<LineRenderer>();
        currentLineRenderer.positionCount = 1;

        currentLineRenderer.SetPosition(0, startPoint);
        currentLineRenderer.material = branchMaterial;
        currentLineRenderer.startWidth = branchThickness;
        currentLineRenderer.endWidth = branchThickness;
        currentLineRenderer.material.color = branchColor;

        return currentLineRenderer;
    }
    void renderNode(char letter, int index, int letterCounter)
    {
        Debug.Log("Adding node: " + letter + index);
        GameObject newNode = GameObject.Instantiate(interactableNodePrefab,Vector3.zero,Quaternion.identity);
        Vector3 nodePosition = nodeSystem.GetNodePosition(letter.ToString(), letterCounter % nodeSystem.GetNumberOfNodesForLetter(letter.ToString()));
        CreateNewNode(newNode, letter,index, nodePosition);
    }
    void AddPositionToBranchRenderer(Vector3 position)
    {
        savedPositions.Last().positionCount++;
        savedPositions.Last().SetPosition(savedPositions.Last().positionCount - 1, position);
    }
    void CreateNewNode(GameObject newNode, char letter, int index, Vector3 nodePosition)
    {
        newNode.transform.position = savedPositions.Last().GetPosition(savedPositions.Last().positionCount-1) + nodePosition;
        AddPositionToBranchRenderer(newNode.transform.position);
        newNode.transform.localScale = new Vector3(.2f, .2f, .2f);
        newNode.tag = "Node";
        NodeModel newNodeModel = newNode.GetComponent<NodeModel>();
        SetupNodeModel(newNodeModel, letter, index);
    }
    void SetupNodeModel(NodeModel newNodeModel,char letter, int index)
    {
       // newNodeModel.letter = letter;
        //newNodeModel.index = index;
        newNodeModel.SetMaterial(nodeMaterial);
        newNodeModel.SetColors(branchColors[letter], branchColors[letter] * 0.9f);
    }
}
*/
