using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputController : MonoBehaviour
{

    public  NodeModel hoveredNode;
    NodeController nodeController;

    // Start is called before the first frame update
    void Start()
    {
        nodeController = GameObject.Find("NodeController").GetComponent<NodeController>() ;
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
}
