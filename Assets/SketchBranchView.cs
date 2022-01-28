using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SketchBranchView : MonoBehaviour
{

    public Material lineRendererIndicatorMaterial;
    public Color lineRendererIndicatorColor;
    public Color trunkColor;
    public Color branchColor;

    public float indicatorWidth = .2f;
    public GameObject interactableNodePrefab;
    LineRenderer lineRendererIndicator;
    public Dictionary<char, LineRenderer> sketchedBranches;
    SketchingPlatformController platformController;
    MVCInputController inputController;
    MVCLSystem lSystem;
    TutorialController tutorialController;
    GameObject rightController;
    Vector3 startingPoint = Vector3.zero;
    [SerializeField]
    SketchedBranchNode selectedNode;
    SketchedBranchNode branchOffNode;
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
        tutorialController = GameObject.Find("Tutorial").GetComponent<TutorialController>();

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
        InitSketchedBranch(selectedNode.letter, transform.position,false);
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
    void InitSketchedBranch(char letter, Vector3 position,bool branch)
    {
        LineRenderer sketchedBranch = new GameObject().AddComponent<LineRenderer>().GetComponent<LineRenderer>();
        sketchedBranch.gameObject.name = "Branch" + letter;
        sketchedBranch.material = lineRendererIndicatorMaterial;
        if (branch)
        {
            sketchedBranch.material.SetColor("_Color", branchColor);
        }
        else
        {
            sketchedBranch.material.SetColor("_Color", trunkColor);

        }
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
        bool hideIndicator = false;
        if (inputController.PlatFormGrabbed())
        {
            hideIndicator = true;
        }
        if (inputController.activateTutorial)
        {
             if(tutorialController.tutorialState == TutorialPoint.GrabPlatform)
            {
                hideIndicator = true;
            }
          
        }
        if (!hideIndicator)
        {
            if (IsTip(selectedNode) || sketchedBranches.Count < 2)
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
        if (selectedNode.index == sketchedBranches[selectedNode.letter].positionCount - 1)
        {
            if (inputController.activateTutorial)
            {
                if (tutorialController.tutorialState == TutorialPoint.AddTrunk)
                {
                    tutorialController.AdvanceTutorial();
                }
            }

            AddToSketchedBranch();
        }
        else
        {
            if (sketchedBranches.ContainsKey('C'))
            {
                //Not adding additional Branches
                return;
            }
            if (inputController.activateTutorial)
            {
                if (tutorialController.tutorialState == TutorialPoint.AddBranch)
                {
                    tutorialController.AdvanceTutorial();
                }
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


    }
    void InstantiateNode()
    {
        GameObject interactableNode = Instantiate(interactableNodePrefab, rightController.transform.position, Quaternion.identity);
        interactableNode.transform.parent = sketchedBranches[selectedNode.letter].transform;
        SketchedNodeModel nodeModel = interactableNode.GetComponent<SketchedNodeModel>();
        nodeModel.Index(selectedNode.index);
        nodeModel.Letter(selectedNode.letter);
        interactableNode.name = selectedNode.letter + selectedNode.index.ToString()+ "InteractableNode";

    }
    void AddNewBranch()
    {

        if (selectedNode.letter != 'A')
        {

            //restricting to 1 branch for now
            return;
        }
        char newLetter = 'C';
        int newIndex = 0;
        InitSketchedBranch(newLetter, transform.position+sketchedBranches['A'].GetPosition(selectedNode.index),true);
        lSystem.AddBranchRule(selectedNode.letter, newLetter, selectedNode.index, selectedNode.index,1, 3,3);
        branchOffNode = selectedNode;
        ToggleBranchOffNode(true);
        selectedNode = new SketchedBranchNode(newIndex, newLetter);
    }
    void ToggleBranchOffNode(bool branchOff)
    {
        GameObject.Find(branchOffNode.letter + branchOffNode.index.ToString() + "InteractableNode").GetComponent<SketchedNodeModel>().SetBranchOffNode(branchOff);

    }
    public void SetSelectedNode(SketchedNodeModel nodeModel)
    {

        
        selectedNode = new SketchedBranchNode(nodeModel.Index(), nodeModel.Letter());

    }
    public bool IsTip(SketchedNodeModel nodeModel)
    {
        return sketchedBranches[nodeModel.Letter()].positionCount - 1 == nodeModel.Index();
    }
    public bool IsTip(SketchedBranchNode nodeModel)
    {
        return sketchedBranches[nodeModel.letter].positionCount - 1 == nodeModel.index;
    }
    public void UpdateNodePosition(SketchedNodeModel sketchedNodeModel)
    {
        if (inputController.activateTutorial)
        {
            if (tutorialController.tutorialState == TutorialPoint.MoveSection)
            {
                tutorialController.AdvanceTutorial();
            }
        }


        this.sketchedBranches[sketchedNodeModel.Letter()].SetPosition(sketchedNodeModel.Index(), sketchedNodeModel.transform.position - sketchedBranches[selectedNode.letter].gameObject.transform.position);
        if (branchOffNode != null)
        {
            if (sketchedNodeModel.Index() == branchOffNode.index && sketchedNodeModel.Letter() == branchOffNode.letter)
            {
                this.sketchedBranches['C'].transform.position = transform.position+ this.sketchedBranches[sketchedNodeModel.Letter()].GetPosition(sketchedNodeModel.Index());
            }
        }
    }
    public void DestroyNode(SketchedNodeModel sketchedNodeModel)
    {
        SetSelectedNode(sketchedNodeModel);
        if (inputController.activateTutorial)
        {
            if (tutorialController.tutorialState == TutorialPoint.DeleteSection)
            {
                tutorialController.AdvanceTutorial();
            }
        }
        Debug.Log("Trying to destroy node "+ sketchedNodeModel.Letter()+sketchedNodeModel.Index());

        if (branchOffNode!=null && sketchedNodeModel.Letter() == branchOffNode.letter && sketchedNodeModel.Index() <= branchOffNode.index)
        {
            Debug.Log("Trying to destroy branch off node");
            Destroy(sketchedBranches['C'].gameObject);
            sketchedBranches.Remove('C');
            lSystem.UpdateRulesNoBranch();
            if(sketchedNodeModel.Index() == branchOffNode.index) { 

                ToggleBranchOffNode(false);

                branchOffNode = null;
                return;
            }
            ToggleBranchOffNode(false);

            branchOffNode = null;

        }
        sketchedBranches[sketchedNodeModel.Letter()].positionCount = sketchedNodeModel.Index() + 1;

        foreach (GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            SketchedNodeModel nodeModel = node.GetComponent<SketchedNodeModel>();
            Debug.Log("Found node " + nodeModel.Letter() + nodeModel.Index());
            if (nodeModel.Letter() == sketchedNodeModel.Letter()&&nodeModel.Index()> sketchedNodeModel.Index())
            {
                Debug.Log("Destroying node");
                Destroy(node);
            }
        }
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
