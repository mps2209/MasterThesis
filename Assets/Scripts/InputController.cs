/*using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputController : MonoBehaviour
{

    public NodeModel hoveredNode;
    NodeController nodeController;
    public Vector3 controllerPosition;
    // Start is called before the first frame update
    private bool rightTriggerPressed = false;
    FluidVrRenderer fluidVrRenderer;
    LSystemController lSystemController;
    LSystemRenderer lSystemRenderer;

    public float growthRatio=0.2f;
    VRController vrController;
    public bool rightTriggerDown = false;
    void Start()
    {
        nodeController = GameObject.Find("NodeController").GetComponent<NodeController>();
        fluidVrRenderer = GameObject.Find("VrRenderer").GetComponent<FluidVrRenderer>();
        lSystemController = GameObject.Find("LSystemController").GetComponent<LSystemController>();
        vrController = GameObject.Find("GameController").GetComponent<VRController>();
        lSystemRenderer = GameObject.Find("LSystemController").GetComponent<LSystemRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectNode(CallbackContext callBackContext)
    {
        Debug.Log("trying to select node");
        if (hoveredNode != null)
        {

            nodeController.selectedNode = hoveredNode;
            //Debug.Log("selected Node: " + hoveredNode.index);

        }
    }
    public void AddSectionToBranch(CallbackContext callBackContext)
    {
        float triggerPressed = callBackContext.ReadValue<float>();
        if (triggerPressed == 1)
        {
            Debug.Log("Pressed trigger");
            rightTriggerDown = true;
        }

            //pressing
        
        if (triggerPressed == 0)
        {
            Debug.Log("Done Pressing");
            //donepressing
            rightTriggerDown = false;
            fluidVrRenderer.TriggerReleased();

        }


        float triggerPressed = callBackContext.ReadValue<float>();
        if (triggerPressed == 0)
        {
            if (nodeController.selectedNode != null)
            {

                nodeController.UpdateLetter();
                if (!vrRenderer.lineRenderers.ContainsKey(nodeController.GetLetter()))
                    {
                    vrRenderer.AddPoint(nodeController.GetLetter(), nodeController.selectedNode.transform.position);

                }
                lSystemController.UpdateRules(nodeController.GetLetter(),nodeController.selectedNode);
                nodeController.selectedNode = null;
            }
            //vrRenderer.AddPoint(nodeController.GetLetter(), controllerPosition);
            //nodeController.AddNode(nodeController.GetLetter(), vrRenderer.GetDelta());
            
        }
        else
        {
            rightTriggerPressed = true;
        }
    }
    public void UpdateControllerPosition(CallbackContext callBackContext)
    {

            controllerPosition = callBackContext.ReadValue<Vector3>();

        
    }

    public void HandleOcculusEvents(PlayerInput playerInput)
    {

    }
    public void StepForwardLSystem(CallbackContext callBackContext)
    {
        float buttonPressed = callBackContext.ReadValue<float>();
        if (buttonPressed == 0)
        {
            Debug.Log("StepForwardLSystem");
            Debug.Log(callBackContext);
            lSystemController.StepForward();
        }
    }
    public void StepBackLSystem(CallbackContext callBackContext)
    {
        float buttonPressed = callBackContext.ReadValue<float>();
        

        if (buttonPressed == 0)
        {
            Debug.Log("StepBackLSystem");
            Debug.Log(callBackContext);
            lSystemController.StepBack();

        }
    }


    public void GrowPlayer(CallbackContext callbackContext)
    {
        
        float buttonPressed = callbackContext.ReadValue<float>();
        if (buttonPressed == 0)
        {
            Debug.Log("GrowPlayer");
            Debug.Log(callbackContext);
            vrController.ChangeScale(growthRatio);
        }
    }
    public void ShrinkPlayer(CallbackContext callbackContext)
    {
        float buttonPressed = callbackContext.ReadValue<float>();
        if (buttonPressed == 0)
        {
            Debug.Log("ShrinkPlayer");
            Debug.Log(callbackContext);

            vrController.ChangeScale(-growthRatio);
        }
    }
}
*/