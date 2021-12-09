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
    int step = 0;
    LineRenderer currentLineRenderer;
    Dictionary<char, List<int>> branchSectionCounter = new Dictionary<char, List<int>>();
    Dictionary<char, List<LineRenderer>> renderedBranches = new Dictionary<char, List<LineRenderer>>();
    List<Vector3> savedPositions = new List<Vector3>();
    SketchBranchView sketchedBranchView;
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
        
        turtle.transform.rotation = Quaternion.identity;
        LineRenderer newLineRenderer = new GameObject().AddComponent<LineRenderer>().GetComponent<LineRenderer>();
        newLineRenderer.gameObject.name = "Branch";
        newLineRenderer.gameObject.tag = "Branch";

        newLineRenderer.material = lineRendererMaterial;
        newLineRenderer.startWidth = indicatorWidth;
        newLineRenderer.endWidth = indicatorWidth;
        newLineRenderer.positionCount = 1;
        newLineRenderer.gameObject.transform.position = transform.position + savedPositions.Last();
        newLineRenderer.gameObject.transform.parent = transform;
        newLineRenderer.SetPosition(0, turtle.transform.position);
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
        turtle.transform.position = transform.position;
        renderedBranches = new Dictionary<char, List<LineRenderer>>();
        branchSectionCounter = new Dictionary<char, List<int>>();

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
                    savedPositions.Add(turtle.transform.position);
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
                        renderedBranches[key].Remove(renderedBranches[key].Last()) ;
                        branchSectionCounter[key].Remove(branchSectionCounter[key].Last());

                    }
                    break;    
                default:
                    RenderSection(currentAxiom[axiomCounter]);
                    break;
            }
            axiomCounter++;
        }
    }
    void RenderSection(char letter)
    {
        //Debug.Log("Rendering Section");
        if (!branchSectionCounter.ContainsKey(letter))
        {
            //Debug.Log("No Entry Yet");

            branchSectionCounter[letter] = new List<int>();
            branchSectionCounter[letter].Add(0);
            turtle.transform.rotation = Quaternion.identity;
            InitNewLineRenderer(letter);

        }
        currentLineRenderer = renderedBranches[letter].Last();
        Debug.Log("Rendering" + letter + branchSectionCounter[letter].Last());
        Vector3 drawingDistance = GetDistance(letter, branchSectionCounter[letter].Last());
        Quaternion drawingRotation = GetRotation(drawingDistance);
        SketchedBranchSection currentBranchSection = new SketchedBranchSection(branchSectionCounter[letter].Last(), letter, drawingDistance.magnitude, drawingRotation);
        turtle.transform.rotation = Quaternion.identity;
        turtle.transform.rotation = currentBranchSection.GetRotation();
        turtle.transform.position += turtle.transform.forward * currentBranchSection.distance;
        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, turtle.transform.position);
        branchSectionCounter[letter][branchSectionCounter[letter].Count-1]++;

    }

    Vector3 GetDistance(char letter, int index) {
        LineRenderer branchRenderer = sketchedBranchView.sketchedBranches[letter];

        index = index % (branchRenderer.positionCount - 1);

        
        return branchRenderer.GetPosition(index + 1) - branchRenderer.GetPosition(index);

    }
    Quaternion GetRotation(Vector3 delta)
    {
        return Quaternion.LookRotation(delta, Vector3.up);

    }


    /*
     * 
     * 
    int i = 0;
    int k = 0;
    char[] keys = new char[sketchBranchModel.sketchedBranches.Keys.Count];
    int currentLetter = 0;
    sketchBranchModel.sketchedBranches.Keys.CopyTo(keys, 0);
    if (i > renderStep)
    {
        return false;
    }
    while (i < renderStep)
    {

        while (currentLetter < keys.Length &&
            sketchBranchModel.sketchedBranches[keys[currentLetter]].sections.Length <= k)
        {
            currentLetter++;
            k = 0;
        }
        if (currentLetter < keys.Length && k < sketchBranchModel.sketchedBranches[keys[currentLetter]].sections.Length)
        {
            SketchedBranchSection currentBranchSection = sketchBranchModel.sketchedBranches[keys[currentLetter]].sections[k];
            turtle.transform.rotation = currentBranchSection.rotation;
            turtle.transform.position += turtle.transform.forward * currentBranchSection.distance;
            currentLineRenderer.positionCount++;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, turtle.transform.position);

        }
        else
        {
            return false;
        }

        //Todo render LineRendere
        k++;
        i++;
    }*/


}
