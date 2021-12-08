using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SketchBranchController : MonoBehaviour
{

    public SketchedBranchSection previousSection;
    SketchBranchModel sketchBranchModel;
    MVCInputController inputController;
    // Start is called before the first frame update
    void Start()
    {
        previousSection = new SketchedBranchSection(0,'A',0f,Quaternion.identity);
        inputController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MVCInputController>();
        sketchBranchModel = GetComponent<SketchBranchModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ConfirmBranchSection(Vector3 delta)
    {
        Quaternion rotation = Quaternion.LookRotation(delta, Vector3.up);
        sketchBranchModel.AddBranchSection(new SketchedBranchSection(previousSection.index + 1, previousSection.letter, delta.magnitude, rotation));
    }

}
