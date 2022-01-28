using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CogwheelRotationController : MonoBehaviour
{
    XRRayInteractor grabber;
    Quaternion originalSelectedRotation;
    Quaternion originalRotation;
    Quaternion rotationDifference;
    float eulerYpreviousPosition;
    public float rotationGranularity = 1;
    public float speed = 1f;
    MVCInputController inputController;
    int stepDiff = 0;
    float originaleulerY;
    float angleDiff = 0;
    // Start is called before the first frame update
    void Start()
    {
        inputController = GameObject.Find("GameController").GetComponent<MVCInputController>();
        eulerYpreviousPosition = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabber!=null)
        {

            Plane raycastPlane = new Plane(Vector3.up, transform.position);
            Ray ray = new Ray(grabber.transform.position, grabber.transform.forward);
            float enter = 0.0f;
            if (raycastPlane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 delta = hitPoint - transform.position;


                Quaternion newRotation = Quaternion.LookRotation(delta);
                Quaternion difference = newRotation * Quaternion.Inverse(originalSelectedRotation);
                rotationDifference = newRotation;
                Quaternion newFineRotation = originalRotation * difference;
                
                transform.rotation = Quaternion.Slerp(transform.rotation,newFineRotation,Time.deltaTime*speed);
                angleDiff = transform.rotation.eulerAngles.y - originaleulerY;
                if(Mathf.Abs(angleDiff)>= rotationGranularity)
                {
                    Debug.Log(angleDiff + "Passed granularity. Adding step.");
                    if (angleDiff < 0)
                    {
                        stepDiff++;
                        inputController.StepTo(stepDiff);
                        stepDiff = 0;

                    }
                    else
                    {
                        stepDiff--;
                        inputController.StepTo(stepDiff);
                        stepDiff = 0;
                    }
                    originaleulerY = transform.rotation.eulerAngles.y;
                }
            }

        }
    }

    public void GrabStart(SelectEnterEventArgs args)
    {
        originalRotation = transform.rotation;
        originaleulerY = transform.rotation.eulerAngles.y;
        Debug.Log("Started Grab");
        grabber = args.interactor.GetComponent<XRRayInteractor>();
        Plane raycastPlane = new Plane(Vector3.up, transform.position);
        Ray ray = new Ray(grabber.transform.position, grabber.transform.forward);
        float enter = 0.0f;
        if (raycastPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            //Move your cube GameObject to the point where you clicked
            Vector3 delta = hitPoint - transform.position;
            originalSelectedRotation = Quaternion.LookRotation(delta);

        }
    }
    public void GrabEnd(SelectExitEventArgs args)
    {
        //round to whole number
        transform.rotation = Quaternion.Euler(0, (Mathf.Floor(transform.rotation.eulerAngles.y / rotationGranularity) * rotationGranularity), 0);
        Quaternion difference = transform.rotation * Quaternion.Inverse(originalSelectedRotation);

        grabber = null;
        Debug.Log("EndedGrab: Royated To: " + transform.rotation.eulerAngles.y);
        Debug.Log("difference in angles: "+ (transform.rotation.eulerAngles.y - eulerYpreviousPosition));
        Debug.Log("difference in steps: " + stepDiff);
        stepDiff = 0;
    }
}
