using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StumpController : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public Vector3 axeHover;
    [SerializeField]
    GameObject axe;
    Renderer stumpRenderer;
    Renderer axeRenderer;
    [SerializeField]
    Material stumpEmmisionMaterial;
    [SerializeField]
    Material stumpNormalMaterial;
    [SerializeField]
    Material stumpTipEmmisionMaterial;
    [SerializeField]
    Material stumpTipNormalMaterial;
    [SerializeField]
    Material axeEmmisionMaterial;
    [SerializeField]
    Material axeNormalMaterial;
    MVCInputController inputController;

    Vector3 originalAxePosition;
    bool isHover = false;
    float animationTimePosition = 0;
    // Start is called before the first frame update
    void Start()
    {
        axeRenderer = axe.GetComponent<Renderer>();
        stumpRenderer = GetComponent<Renderer>();
        Material[] matArray = stumpRenderer.materials;
        matArray[0] = stumpNormalMaterial;
        matArray[1] = stumpTipNormalMaterial;
        stumpRenderer.materials = matArray;
        axeRenderer.material = axeNormalMaterial;
        inputController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MVCInputController>();
        originalAxePosition = axe.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (isHover)
        {
            animationTimePosition += Time.deltaTime;
            axe.transform.position = Vector3.Lerp(axe.transform.position, originalAxePosition + axeHover, moveCurve.Evaluate(animationTimePosition));

        }
        else
        {
            animationTimePosition += Time.deltaTime;
            axe.transform.position = Vector3.Lerp(axe.transform.position, originalAxePosition, moveCurve.Evaluate(animationTimePosition));

        }

    }


    public void SelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Stump SelectEntered");
        inputController.ResetTree();
    }
    public void HoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("Stump HoverEnter");

        Material[] matArray = stumpRenderer.materials;
        matArray[0] = stumpEmmisionMaterial;
        matArray[1] = stumpTipEmmisionMaterial;
        stumpRenderer.materials = matArray;
        axeRenderer.material = axeEmmisionMaterial;
        isHover = true;
        animationTimePosition = 0;
    }
    public void HoverExit(HoverExitEventArgs args)
    {
        Debug.Log("Stump HoverExit");

        Material[] matArray = stumpRenderer.materials;
        matArray[0] = stumpNormalMaterial;
        matArray[1] = stumpTipNormalMaterial;
        stumpRenderer.materials = matArray;
        axeRenderer.material = axeNormalMaterial;
        isHover = false;
        animationTimePosition = 0;
    }
    public void Activate(ActivateEventArgs args)
    {
        Debug.Log("Stump Activate");
        inputController.ResetTree();

    }
}
