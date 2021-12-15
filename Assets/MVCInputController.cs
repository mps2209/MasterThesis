using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MVCInputController : MonoBehaviour
{
    SketchBranchController sketchBranchController;
    public GameObject treeRenderer;
    // Start is called before the first frame update
    public GameObject sketchingPlatform;
    bool platFormIsGrabbed = false;
    GameObject rightController;
    GameObject leftController;
    SketchBranchView sketchBranchView;
    BranchRenderer branchRenderer;
    MVCLSystem lSystem;

    void Start()
    {
        sketchBranchController = GameObject.Find("SketchBranchController").GetComponent<SketchBranchController>();
        rightController = GameObject.Find("RightHand Controller");
        leftController = GameObject.Find("LeftHand Controller");
        sketchBranchView = sketchingPlatform.GetComponent<SketchBranchView>();
        branchRenderer = treeRenderer.GetComponent<BranchRenderer>();
        lSystem = GameObject.Find("LSystem").GetComponent<MVCLSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RightTriggerPressed(CallbackContext callbackContext)
    {
        float triggerPressed = callbackContext.ReadValue<float>();
        if (triggerPressed == 0)
        {
            Debug.Log("TriggerPressed");
            if (!platFormIsGrabbed)
            {
                //sketchBranchController.ConfirmBranchSection(sketchBranchView.GetCurrentDelta());
                sketchBranchView.UpdateSketchedBranches();
            }
        }
    }
    public void SetPlatFormGrabbed(bool isGrabbed)
    {
        platFormIsGrabbed = isGrabbed;
    }
    public bool PlatFormGrabbed()
    {
        return platFormIsGrabbed;
    }

    public void StepForward(CallbackContext callbackContext)
    {
        float buttonPressed = callbackContext.ReadValue<float>();
        if (buttonPressed == 0)
        {
            lSystem.StepForward();

            branchRenderer.RenderTree();
        }
    }
    public void StepBackward(CallbackContext callbackContext)
    {
        float buttonPressed = callbackContext.ReadValue<float>();
        if (buttonPressed == 0)
        {
            Debug.Log("Stepping Backward");

            lSystem.StepBack();

            branchRenderer.RenderTree();
        }
    }
}
