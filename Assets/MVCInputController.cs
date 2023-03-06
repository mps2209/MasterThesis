using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;
public class MVCInputController : MonoBehaviour
{
    public bool activateTutorial;
    SketchBranchController sketchBranchController;
    public GameObject treeRenderer;
    // Start is called before the first frame update
    public GameObject sketchingPlatform;
    bool platFormIsGrabbed = false;
    public XRBaseInteractor activeController;
    SketchBranchView sketchBranchView;
    BranchRenderer branchRenderer;
    MVCLSystem lSystem;
    public bool rightSelectPressed;
    public bool leftSelectPressed;
    public TutorialController tutorialController;
    public GameObject tutorialGerman;
    public GameObject tutorialEnglish;


    void Start()
    {
        activeController = GameObject.Find("RightHand Controller").GetComponent<XRBaseInteractor>();
        sketchBranchController = GameObject.Find("SketchBranchController").GetComponent<SketchBranchController>();
        sketchBranchView = sketchingPlatform.GetComponent<SketchBranchView>();
        branchRenderer = treeRenderer.GetComponent<BranchRenderer>();
        lSystem = GameObject.Find("LSystem").GetComponent<MVCLSystem>();


    }

    // Update is called once per frame
    void Update()
    {
        if (activeController == null)
        {
            activeController = GameObject.Find("RightHand Controller").GetComponent<XRBaseInteractor>();

        }
    }
    public void toggleTutorial(bool value)
    {
        switch (LevelController.language)
        {
            case Language.English:
                tutorialEnglish.SetActive(value);
                tutorialController = tutorialEnglish.GetComponent<TutorialController>();

                break;
            case Language.Deutsch:
                tutorialGerman.SetActive(value);
                tutorialController = tutorialGerman.GetComponent<TutorialController>();

                break;
        }

    }
    public void RightTriggerPressed(CallbackContext callbackContext)
    {
        
        float triggerPressed = callbackContext.ReadValue<float>();
        if (triggerPressed == 0)
        {
            if (!platFormIsGrabbed && !CheckIfHoveringOrSelected())
            {

                //sketchBranchController.ConfirmBranchSection(sketchBranchView.GetCurrentDelta());
                sketchBranchView.UpdateSketchedBranches();
            }
        }
    }
    bool CheckIfHoveringOrSelected()
    {
        List<XRBaseInteractable> targets = new List<XRBaseInteractable>();
        activeController.GetHoverTargets(targets);
        return targets.Count > 0 || activeController.selectTarget!=null;
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
            if (activateTutorial)
            {
                if (tutorialController.tutorialState == TutorialPoint.AdvanceTree)
                {
                    tutorialController.AdvanceTutorial();
                }
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
            if (activateTutorial)
            {
                if (tutorialController.tutorialState == TutorialPoint.StepBackTree)
                {
                    tutorialController.AdvanceTutorial();
                }
            }

            lSystem.StepTo(-1);

            branchRenderer.RenderTree();
        }
    }
    public void ResetTree()
    {
        if (activateTutorial)
        {
            if (tutorialController.tutorialState == TutorialPoint.ResetTree)
            {
                tutorialController.AdvanceTutorial();
            }
        }


            lSystem.StepTo(-lSystem.step);

        
        sketchBranchView.InitSketchBranchView();
        branchRenderer.ResetTree();

    }


    public void StepTo(int stepTo)
    {
        lSystem.StepTo(stepTo);
        branchRenderer.RenderTree();
    }

}
