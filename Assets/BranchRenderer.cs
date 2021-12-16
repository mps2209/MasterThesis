using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BranchRenderer : MonoBehaviour
{
    public GameObject turtle;
    public Material lineRendererMaterial;
    public Color lineRendererColor;
    public float indicatorWidth = .2f;
    MVCLSystem lSystem;
    SketchBranchModel sketchBranchModel;
    LineRenderer currentLineRenderer;
    Dictionary<char, List<int>> branchSectionCounter = new Dictionary<char, List<int>>();
    Dictionary<char, List<LineRenderer>> renderedBranches = new Dictionary<char, List<LineRenderer>>();
    List<Vector3> savedPositions = new List<Vector3>();
    SketchBranchView sketchedBranchView;
    bool branchedOff = false;
    GameObject initalLineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        sketchedBranchView = GameObject.Find("SketchingPlatform").GetComponent<SketchBranchView>();
        lSystem = GameObject.Find("LSystem").GetComponent<MVCLSystem>();
        sketchBranchModel = GameObject.Find("SketchBranchController").GetComponent<SketchBranchModel>();
        renderedBranches = new Dictionary<char, List<LineRenderer>>();
        branchSectionCounter = new Dictionary<char, List<int>>();
        savedPositions.Add(Vector3.zero);

    }
    void InitNewLineRenderer(char letter)
    {
        //Quaternion diffRotation = Quaternion.LookRotation(Vector3.up, Vector3.up) * Quaternion.Inverse(turtle.transform.rotation);
        // turtle.transform.rotation = diffRotation * turtle.transform.rotation;
        turtle.transform.rotation = Quaternion.identity;
        LineRenderer newLineRenderer = new GameObject().AddComponent<LineRenderer>().GetComponent<LineRenderer>();
        newLineRenderer.gameObject.name = "Branch";
        newLineRenderer.gameObject.tag = "Branch";
        newLineRenderer.useWorldSpace = false;
        newLineRenderer.material = lineRendererMaterial;
        newLineRenderer.startWidth = indicatorWidth;
        newLineRenderer.endWidth = indicatorWidth;
        newLineRenderer.positionCount = 1;
        if (savedPositions.Last() == Vector3.zero)
        {
            newLineRenderer.transform.position = transform.position;

        }
        else {
            newLineRenderer.transform.position = savedPositions.Last();
        }
        newLineRenderer.gameObject.AddComponent<RotationController>();

        newLineRenderer.SetPosition(0, Vector3.zero);
        if(renderedBranches.ContainsKey(letter) && renderedBranches[letter].Count>0)
        {
            newLineRenderer.transform.parent = renderedBranches[letter].Last().transform;
        }
        else
        {
            if (initalLineRenderer == null)
            {
                newLineRenderer.transform.parent = transform;
                initalLineRenderer = newLineRenderer.gameObject;
                newLineRenderer.gameObject.GetComponent<RotationController>().branchedOff=-1;
            }
            else
            {
                newLineRenderer.transform.parent = initalLineRenderer.transform;

            }

        }
        if (!renderedBranches.ContainsKey(letter))
        {
            renderedBranches[letter] = new List<LineRenderer>();
        }

        renderedBranches[letter].Add(newLineRenderer);

    }
    // Update is called once per frame
    void Update()
    {

    }


    public void RenderTree()
    {
        ResetBranchRotation();
        turtle.transform.position = transform.position;
        savedPositions = new List<Vector3>();
        savedPositions.Add(Vector3.zero);
        renderedBranches = new Dictionary<char, List<LineRenderer>>();
        branchSectionCounter = new Dictionary<char, List<int>>();
        initalLineRenderer = null;
        foreach (GameObject branch in GameObject.FindGameObjectsWithTag("Branch"))
        {
            Destroy(branch);
        }
        string currentAxiom = lSystem.axiom.text;
        int axiomCounter = 0;

        while (axiomCounter < currentAxiom.Length)
        {
            char[] keys = renderedBranches.Keys.ToArray();

            switch (currentAxiom[axiomCounter])
            {
                case 'B':
                    break;
                case '[':


                    branchedOff = true;
                    savedPositions.Add(turtle.transform.position);
                    currentLineRenderer.GetComponent<RotationController>().branchedOff++;

                    foreach (char key in keys)
                    {
                        InitNewLineRenderer(key);
                        branchSectionCounter[key].Add(0);

                    }
                    break;
                case ']':

                    turtle.transform.position = savedPositions.Last();
                    savedPositions.Remove(savedPositions.Last());
                    foreach (char key in keys)
                    {
                        renderedBranches[key].Remove(renderedBranches[key].Last());
                        branchSectionCounter[key].Remove(branchSectionCounter[key].Last());

                    }
                    
                    break;
                default:
                    RenderSection(currentAxiom[axiomCounter]);
                    branchedOff = false;
                    break;
            }
            axiomCounter++;
        }
        foreach (GameObject branch in GameObject.FindGameObjectsWithTag("Branch"))
        {
            if (branch.GetComponent<LineRenderer>().positionCount <= 1)
            {
                Destroy(branch);

            }
        }
        UpdateBranchRotation();
    }
    void RenderSection(char letter)
    {
        int parentBranches = 0;
        if (currentLineRenderer != null)
        {
            parentBranches = currentLineRenderer.GetComponent<RotationController>().branchedOff;

        }

        //Debug.Log("Rendering Section");
        if (!branchSectionCounter.ContainsKey(letter))
        {
            //Debug.Log("No Entry Yet");

            branchSectionCounter[letter] = new List<int>();
            branchSectionCounter[letter].Add(0);
            //turtle.transform.rotation = Quaternion.identity;
            InitNewLineRenderer(letter);

        }
        currentLineRenderer = renderedBranches[letter].Last();

        // this could fix rotating branches trying without first
        //turtle.transform.parent = currentLineRenderer.transform;
        Vector3 drawingDistance = GetDistance(letter, branchSectionCounter[letter].Last());
        
        //SketchedBranchSection currentBranchSection = new SketchedBranchSection(branchSectionCounter[letter].Last(), letter, drawingDistance.magnitude, drawingRotation);

        //turtle.transform.rotation = Quaternion.identity;

        //turtle.transform.rotation = diffRotation * turtle.transform.rotation;
        turtle.transform.rotation = Quaternion.identity;
        Quaternion drawingRotation = currentLineRenderer.transform.rotation * GetRotation(drawingDistance);
        if (branchedOff)
        {
            currentLineRenderer.GetComponent<RotationController>().branchNumber= parentBranches;
            

        }
        //Look at target
        turtle.transform.rotation = drawingRotation * turtle.transform.rotation;
        turtle.transform.position += turtle.transform.forward * drawingDistance.magnitude;
        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, turtle.transform.position- currentLineRenderer.transform.position);
        branchSectionCounter[letter][branchSectionCounter[letter].Count - 1]++;

    }

    Vector3 GetDistance(char letter, int index)
    {
        LineRenderer branchRenderer = sketchedBranchView.sketchedBranches[letter];

        index = index % (branchRenderer.positionCount - 1);


        return branchRenderer.GetPosition(index + 1) - branchRenderer.GetPosition(index);

    }
    Quaternion GetRotation(Vector3 delta)
    {
        return Quaternion.LookRotation(delta, turtle.transform.up);

    }

    /*
    public void RotateBranches()
    {
        Debug.Log("Rotating Branches");
        rotateCounter++;
        int realBranchesFound = 0;
        int timesToRotate = 1;
        foreach(GameObject branch in GameObject.FindGameObjectsWithTag("Branch"))
        {
            if (branch.GetComponent<LineRenderer>().positionCount < 1)
            {

            }
            else
            {

                if (realBranchesFound == rotateCounter)
                {
                    Debug.Log("found real Branch at " + rotateCounter);
                    Debug.Log("rotating " +  timesToRotate);

                    branch.transform.Rotate(Vector3.up, 137* timesToRotate );
                    return;
                }
                if (branch.transform.childCount > 0)
                {
                    Debug.Log("Has ChildBranches " + timesToRotate);

                    timesToRotate++;
                }
                else
                {
                    Debug.Log("Has No Child Branches " + timesToRotate);
                    timesToRotate = 0;
                }
                realBranchesFound++;

            }

        }
    }*/
    public void ResetBranchRotation()
    {
        foreach (GameObject branch in GameObject.FindGameObjectsWithTag("Branch"))
        {
            branch.transform.rotation = Quaternion.identity;
        }
    }
    public void UpdateBranchRotation()
    {
        foreach (GameObject branch in GameObject.FindGameObjectsWithTag("Branch"))
        {
            RotationController rotationController= branch.GetComponent<RotationController>();
            rotationController.UpdateBranchRotation();
        }
    }

    public void ResetTree()
    {
        foreach (GameObject branch in GameObject.FindGameObjectsWithTag("Branch"))
        {
            Destroy(branch);
        }
        Reset();


    }
    private void Reset()
    {
        
    }
}
