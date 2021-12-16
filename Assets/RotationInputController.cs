using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotationInputController : MonoBehaviour
{

    bool isSelected = false;
    XRRayInteractor grabber;
    public float distanceToCenter=0.3f;
    public Quaternion rotation;
    public float eulerY;
    BranchRenderer branchRenderer;
    // Start is called before the first frame update
    void Start()
    {
        branchRenderer = GameObject.Find("Tree").GetComponent<BranchRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {

            Plane raycastPlane = new Plane(Vector3.up, transform.parent.position);
            Ray ray = new Ray(grabber.transform.position, grabber.transform.forward);
            float enter = 0.0f;
            if (raycastPlane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);

                //Move your cube GameObject to the point where you clicked
                Vector3 delta = hitPoint- transform.parent.transform.position;
                transform.localPosition=delta.normalized * distanceToCenter+new Vector3(0,1.27f,0);
                


            }

        }
        rotation = Quaternion.LookRotation(transform.localPosition, Vector3.up);
        eulerY = rotation.eulerAngles.y;
    }

    public void SelectEntered(SelectEnterEventArgs args) 
    {
        isSelected = true;
        grabber = args.interactor.GetComponent<XRRayInteractor>();
    }
    public void SelectExit(SelectExitEventArgs args)
    {
        isSelected = false;
        branchRenderer.rotationAngle = eulerY;
        branchRenderer.UpdateBranchRotation();

    }
}
