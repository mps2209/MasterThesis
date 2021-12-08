using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SketchBranchModel : MonoBehaviour
{
    public Dictionary<char, SketchedBranch> sketchedBranches = new Dictionary<char, SketchedBranch>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SketchedBranchSection AddBranchSection(SketchedBranchSection newBranchSection)
    {
        if (sketchedBranches.ContainsKey(newBranchSection.letter))
        {

            sketchedBranches[newBranchSection.letter].AddBranchSection(newBranchSection);

        }
        else
        {
            /*char newLetter = sketchedBranches.Keys.Last();
            newLetter++;
            if (newLetter == 'B')
            {
                newLetter++;
            }*/
            sketchedBranches[newBranchSection.letter] = new SketchedBranch(newBranchSection.letter, newBranchSection);

        }
       
        return sketchedBranches[newBranchSection.letter].sections.Last();
    }
}
[Serializable]
public class SketchedBranch
{
    public char letter;
    public List<SketchedBranchSection> sections;
    public SketchedBranch(char letter, SketchedBranchSection startingPoint)
    {
        this.letter = letter;
        
        sections.Add(startingPoint);
    }
    public void AddBranchSection(SketchedBranchSection sketchedBranchSection)
    {
        sections.Add(sketchedBranchSection);

    }
}
[Serializable]
public class SketchedBranchSection
{

    public int index;
    public char letter;
    public float distance;
    public float x;
    public float y;
    public float z;
    public float w;
    public SketchedBranchSection(int index, char letter, float distance, Quaternion rotation)
    {
        this.index = index;
        this.letter = letter;
        this.distance = distance;
        this.x = rotation.x;
        this.y = rotation.y;
        this.z = rotation.z;
        this.w = rotation.w;
    }
    public Quaternion GetRotation()
    {
        return new Quaternion(x, y, z, w);
    }

}