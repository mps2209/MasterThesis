using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SketchBranchView : MonoBehaviour
{

    public Material lineRendererIndicatorMaterial;
    public Color lineRendererIndicatorColor;
    public float indicatorWidth = .2f;
    public GameObject interactableNodePrefab;
    LineRenderer lineRendererIndicator;
    public Dictionary<char, LineRenderer> sketchedBranches;
    SketchingPlatformController platformController;
    MVCInputController inputController;
    MVCLSystem lSystem;

    GameObject rightController;
    Vector3 startingPoint = Vector3.zero;
    [SerializeField]
    SketchedBranchNode selectedNode;
    // Start is called before the first frame update
    void Start()
    {
        platformController = GetComponent<SketchingPlatformController>();
        inputController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MVCInputController>();
        lSystem = GameObject.Find("LSystem").GetComponent<MVCLSystem>();
        rightController = GameObject.Find("RightHand Controller");
        sketchedBranches = new Dictionary<char, LineRenderer>();
        selectedNode = new SketchedBranchNode(0, 'A');
        InitSketchBranchView();
    }
    public void InitSketchBranchView()
    {
        if (sketchedBranches != null) { }
        char[] keys = new char[sketchedBranches.Count];
            
        sketchedBranches.Keys.CopyTo(keys,0);
        foreach(char key in keys)
        {
            Destroy(sketchedBranches[key]);
        }
        foreach(GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            Destroy(node);
        }
        sketchedBranches = new Dictionary<char, LineRenderer>();
        selectedNode = new SketchedBranchNode(0, 'A');
        InitIndicatorLineRenderer();
        InitSketchedBranch(selectedNode.letter, transform.position);
    }
    void InitIndicatorLineRenderer()
    {
        if (lineRendererIndicator != null)
        {
            Destroy(lineRendererIndicator);
        }
        lineRendererIndicator = new GameObject().AddComponent<LineRenderer>().GetComponent<LineRenderer>();
        lineRendererIndicator.gameObject.name = "LRIndicator";
        lineRendererIndicator.material = lineRendererIndicatorMaterial;
        lineRendererIndicator.material.color = lineRendererIndicatorColor;
        lineRendererIndicator.startWidth = indicatorWidth;
        lineRendererIndicator.endWidth = indicatorWidth;
        lineRendererIndicator.positionCount = 2;
        lineRendererIndicator.useWorldSpace = false;
        lineRendererIndicator.gameObject.transform.position = transform.position;
        lineRendererIndicator.gameObject.transform.parent = transform;
        lineRendererIndicator.SetPosition(0, Vector3.zero);
        lineRendererIndicator.SetPosition(1, Vector3.zero);
    }
    void InitSketchedBranch(char letter, Vector3 position)
    {
        LineRenderer sketchedBranch = new GameObject().AddComponent<LineRenderer>().GetComponent<LineRenderer>();
        sketchedBranch.gameObject.name = "Branch" + letter;
        sketchedBranch.material = lineRendererIndicatorMaterial;
        sketchedBranch.material.color = Random.ColorHSV();
        sketchedBranch.startWidth = indicatorWidth;
        sketchedBranch.endWidth = indicatorWidth;
        sketchedBranch.positionCount = 1;
        sketchedBranch.useWorldSpace = false;
        sketchedBranch.gameObject.transform.position = position;
        sketchedBranch.gameObject.transform.parent = transform;
        sketchedBranch.SetPosition(0, Vector3.zero);
        sketchedBranches[letter] = sketchedBranch;

    }

    // Update is called once per frame
    void Update()
    {

        if (!inputController.PlatFormGrabbed())
        {
            if (selectedNode.index < sketchedBranches[selectedNode.letter].positionCount)
            {
                lineRendererIndicator.gameObject.transform.position = sketchedBranches[selectedNode.letter].gameObject.transform.position;

                lineRendererIndicator.SetPosition(0, sketchedBranches[selectedNode.letter].GetPosition(selectedNode.index));

            }
            else
            {
                lineRendererIndicator.gameObject.transform.position = sketchedBranches[selectedNode.letter].gameObject.transform.position;

                lineRendererIndicator.SetPosition(0, sketchedBranches[selectedNode.letter].GetPosition(sketchedBranches[selectedNode.letter].positionCount - 1));
            }

            lineRendererIndicator.SetPosition(1, rightController.transform.position - sketchedBranches[selectedNode.letter].gameObject.transform.position);

        }
        else
        {
            lineRendererIndicator.SetPosition(0, Vector3.zero);

            lineRendererIndicator.SetPosition(1, Vector3.zero);

        }
    }
    public Vector3 GetCurrentDelta()
    {
        return lineRendererIndicator.GetPosition(1) - lineRendererIndicator.GetPosition(0);

    }

    public void UpdateSketchedBranches()
    {
        Debug.Log("UpdateSketchedBranches");
        if (selectedNode.index == sketchedBranches[selectedNode.letter].positionCount - 1)
        {
            Debug.Log("index at tip");

            AddToSketchedBranch();
        }
        else
        {
            Debug.Log("index not at tip");
            if (sketchedBranches.ContainsKey('C'))
            {
                //Not adding additional Branches
                return;
            }
            AddNewBranch();
            AddToSketchedBranch();
        }


    }
    void AddToSketchedBranch()
    {
        selectedNode.index++;

        sketchedBranches[selectedNode.letter].positionCount++;
        sketchedBranches[selectedNode.letter].SetPosition(sketchedBranches[selectedNode.letter].positionCount - 1, rightController.transform.position - sketchedBranches[selectedNode.letter].gameObject.transform.position);
        InstantiateNode();
        Debug.Log("AddToSketchedBranch " + selectedNode.letter + selectedNode.index);


    }
    void InstantiateNode()
    {
        GameObject interactableNode = Instantiate(interactableNodePrefab, rightController.transform.position, Quaternion.identity);
        interactableNode.transform.parent = transform;
        SketchedNodeModel nodeModel = interactableNode.GetComponent<SketchedNodeModel>();
        nodeModel.Index(selectedNode.index);
        nodeModel.Letter(selectedNode.letter);
        Debug.Log("Instantiating Node " + selectedNode.letter + selectedNode.index);
        Debug.Log("Instantiated Node " + nodeModel.Letter() + nodeModel.Index());


    }
    void AddNewBranch()
    {

        if (selectedNode.letter != 'A')
        {
            Debug.Log("Not Adding another Branch " + selectedNode.letter);

            //restricting to 1 branch for now
            return;
        }
        char newLetter = 'C';
        int newIndex = 0;
        Debug.Log("AddNewBranch " + newLetter);
        InitSketchedBranch(newLetter, transform.position+sketchedBranches['A'].GetPosition(selectedNode.index));
        lSystem.AddBranchRule(selectedNode.letter, newLetter, selectedNode.index,1,3,3);
        selectedNode = new SketchedBranchNode(newIndex, newLetter);
        
    }

    public void SetSelectedNode(SketchedNodeModel nodeModel)
    {
        Debug.Log("SetSelectedNode");
        selectedNode = new SketchedBranchNode(nodeModel.Index(), nodeModel.Letter());
    }
}
public class SketchedBranchNode
{
    public int index;
    public char letter;
    public SketchedBranchNode(int index, char letter)
    {
        this.index = index;
        this.letter = letter;


    }
}
