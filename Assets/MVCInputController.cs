using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;
public class MVCInputController : MonoBehaviour
{
    SketchBranchController sketchBranchController;
    public GameObject treeRenderer;
    // Start is called before the first frame update
    public GameObject sketchingPlatform;
    bool platFormIsGrabbed = false;
    XRRayInteractor rightController;
    GameObject leftController;
    SketchBranchView sketchBranchView;
    BranchRenderer branchRenderer;
    MVCLSystem lSystem;
    public bool rightSelectPressed;
    public bool leftSelectPressed;
    TutorialController tutorialController;

    void Start()
    {
        tutorialController = GameObject.Find("Tutorial").GetComponent<TutorialController>();
        sketchBranchController = GameObject.Find("SketchBranchController").GetComponent<SketchBranchController>();
        rightController = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
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
            if (!platFormIsGrabbed && !CheckIfHoverTargets())
            {
               
                //sketchBranchController.ConfirmBranchSection(sketchBranchView.GetCurrentDelta());
                sketchBranchView.UpdateSketchedBranches();
            }
        }
    }
    bool CheckIfHoverTargets()
    {
        List<XRBaseInteractable> targets = new List<XRBaseInteractable>();

        rightController.GetHoverTargets(targets);
        return targets.Count > 0;
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
            if (tutorialController.tutorialState == TutorialPoint.AdvanceTree)
            {
                tutorialController.AdvanceTutorial();
            }
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
            if (tutorialController.tutorialState == TutorialPoint.StepBackTree)
            {
                tutorialController.AdvanceTutorial();
            }
            lSystem.StepBack();

            branchRenderer.RenderTree();
        }
    }
    public void ResetTree()
    {
        if (tutorialController.tutorialState == TutorialPoint.ResetTree)
        {
            tutorialController.AdvanceTutorial();
        }
        while (lSystem.Step() > 1)
        {
            lSystem.StepBack();

        }
        sketchBranchView.InitSketchBranchView();
        branchRenderer.ResetTree();

    }
}
