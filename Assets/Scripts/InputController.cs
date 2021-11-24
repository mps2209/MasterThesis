using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputController : MonoBehaviour
{

    public NodeModel hoveredNode;
    NodeController nodeController;
    Vector3 controllerPosition;
    // Start is called before the first frame update
    private bool rightTriggerPressed = false;
    VrRenderer vrRenderer;
    void Start()
    {
        nodeController = GameObject.Find("NodeController").GetComponent<NodeController>();
        vrRenderer = GameObject.Find("VrRenderer").GetComponent<VrRenderer>();
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
            Debug.Log("selected Node: " + hoveredNode.index);

        }
    }
    public void AddSectionToBranch(CallbackContext callBackContext)
    {
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
                nodeController.selectedNode = null;
            }
            vrRenderer.AddPoint(nodeController.GetLetter(), controllerPosition);
            nodeController.AddNode(nodeController.GetLetter(), vrRenderer.GetDelta());
        }
        else
        {
            rightTriggerPressed = true;
        }
    }
    public void UpdateControllerPosition(CallbackContext callBackContext)
    {
        if (rightTriggerPressed)
        {
            this.controllerPosition = callBackContext.ReadValue<Vector3>();

        }
    }

    public void HandleOcculusEvents(PlayerInput playerInput)
    {

    }
}
