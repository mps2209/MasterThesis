
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LSystemRenderer : MonoBehaviour
{
    FluidVrRenderer fluidVrRenderer;
    LSystem lSystem;
    public Dictionary<char, List<LineRenderer>> renderedBranches = new Dictionary<char, List<LineRenderer>>();
    public Dictionary<char, List<int>> sectionsRenderedCounter = new Dictionary<char, List<int>>();


    public List<Vector3> savedPositions = new List<Vector3>();
    public Material branchMaterial;
    public float branchThickness;
    public Color branchColor;
    public Vector3 startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        fluidVrRenderer = GameObject.Find("VrRenderer").GetComponent<FluidVrRenderer>();
        lSystem = GetComponent<LSystem>();
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
            switch (currentLetter)
            {
                case '[':
                    //branching Off
                    BranchOff();

                    break;
                case ']':
                    BranchBack();

                    break;
                case 'B':
                    //B does nothing
                    break;
                default:
                    AddToBranch(currentLetter);
                    break;
            }
            axiomIterator++;

        }
    }
    void BranchOff()
    {
        char[] keys = new char[renderedBranches.Keys.Count];
        renderedBranches.Keys.CopyTo(keys, 0);
        foreach (char key in keys)
        {
            renderedBranches[key].Add(CreateNewBranchRenderer(savedPositions.Last()));
            sectionsRenderedCounter[key].Add(0);
        }
        savedPositions.Add(savedPositions.Last());
    }
    void InitDictionaries(char letter)
    {
        savedPositions = new List<Vector3>();
        savedPositions.Add(Vector3.zero);
        renderedBranches[letter] = new List<LineRenderer>();
        sectionsRenderedCounter[letter] = new List<int>();

        renderedBranches[letter].Add(CreateNewBranchRenderer(savedPositions.Last()));
        sectionsRenderedCounter[letter].Add(0);

    }
    void BranchBack()
    {
        savedPositions.Remove(savedPositions.Last());
        char[] keys = new char[renderedBranches.Keys.Count];
        renderedBranches.Keys.CopyTo(keys, 0);
        foreach (char key in keys)
        {
            renderedBranches[key].Remove(renderedBranches[key].Last());
            sectionsRenderedCounter[key].Remove(sectionsRenderedCounter[key].Last());
        }
    }
    void AddToBranch(char letter)
    {
        //Check if we already renderered a branch
        if (!sectionsRenderedCounter.ContainsKey(letter))
        {
            InitDictionaries(letter);

        }
        //the number of sections we added for that branch
        int branchSectionsRendered = sectionsRenderedCounter[letter].Last();
        //we translate the section we render to indexes of the skletched line renderer
        int startIndex = fluidVrRenderer.numSections[letter][branchSectionsRendered % fluidVrRenderer.numSections[letter].Count];
        int endIndex = fluidVrRenderer.numSections[letter][(branchSectionsRendered + 1) % fluidVrRenderer.numSections[letter].Count];
        //increasing the count of sections rendered
        sectionsRenderedCounter[letter].Remove(sectionsRenderedCounter[letter].Last());
        sectionsRenderedCounter[letter].Add(branchSectionsRendered + 1);

        for (int i = startIndex; i < endIndex; i++)
        {
            Vector3 delta = GetDelta(letter, i);
            renderedBranches[letter].Last().positionCount++;

            // we have to add delta to the previous rendered position;
            Vector3 lastRenderedPosition = savedPositions.Last();
            Vector3 worldPosition = delta + lastRenderedPosition;
            renderedBranches[letter].Last().SetPosition(renderedBranches[letter].Last().positionCount - 1, worldPosition);
            savedPositions.Remove(savedPositions.Last());

            savedPositions.Add(renderedBranches[letter].Last().GetPosition(renderedBranches[letter].Last().positionCount - 1));
        }

    }

    Vector3 GetDelta(char letter, int i)
    {
        if (i == 0)
        {
            //at the start of the branch, delta is just the position
            //Only at the very start of the system
            
            if (renderedBranches[letter].Last().positionCount < 1)
            {
                return fluidVrRenderer.renderers[letter].GetPosition(i);
            }
            else
            {
                return fluidVrRenderer.renderers[letter].GetPosition(fluidVrRenderer.renderers[letter].positionCount - 1)- fluidVrRenderer.renderers[letter].GetPosition(fluidVrRenderer.renderers[letter].positionCount - 2);

            }
        }
        else
        {
            // at any other point we get the difference to the previous position
            return fluidVrRenderer.renderers[letter].GetPosition(i) - fluidVrRenderer.renderers[letter].GetPosition(i - 1);
        }

    }
    LineRenderer CreateNewBranchRenderer(Vector3 startPoint)
    {
        GameObject lineRendererParent = Instantiate(new GameObject(), startPoint, Quaternion.identity);
        lineRendererParent.tag = "Branch";
        lineRendererParent.AddComponent<LineRenderer>();
        LineRenderer currentLineRenderer = lineRendererParent.GetComponent<LineRenderer>();
        currentLineRenderer.positionCount = 0;
        currentLineRenderer.material = branchMaterial;
        currentLineRenderer.startWidth = branchThickness;
        currentLineRenderer.endWidth = branchThickness;
        currentLineRenderer.material.color = branchColor;

        return currentLineRenderer;
    }
    void DestroyPreviousSystem()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Branch"))
        {
            Destroy(go);
        }
        renderedBranches = new Dictionary<char, List<LineRenderer>>();
        sectionsRenderedCounter = new Dictionary<char, List<int>>();
        savedPositions = new List<Vector3>();
    }
}
*/