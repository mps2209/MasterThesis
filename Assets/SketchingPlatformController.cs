using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SketchingPlatformController : MonoBehaviour
{

    GameObject player;
    Vector3 distanceToPlayer;
    GameObject grabber;
    XRRayInteractor otherGrabber;
    Rigidbody rigidbody;
    MVCInputController inputController;
    SketchingPlatformInteractable platformInteractable;
    bool grabStarted = false;
    bool startRotate=false;
    public GameObject initialGrabPoint;
    //public GameObject secondGrabPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 yCorrection = player.transform.position;
        yCorrection.y = transform.position.y;
        distanceToPlayer = yCorrection - transform.position;
        rigidbody = GetComponent<Rigidbody>();
        inputController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MVCInputController>();
        platformInteractable = GetComponent<SketchingPlatformInteractable>();
        //GameObject plane= Instantiate(new GameObject(PrimitiveType.Plane), transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("SelectEntered");
        Debug.Log(args.interactor);
        inputController.SetPlatFormGrabbed(true);
        grabber = args.interactor.gameObject;
        distanceToPlayer = grabber.transform.position - transform.position;
        grabStarted = true;
        initialGrabPoint.GetComponent<Renderer>().enabled = true;

    }

    public void UpdateMainHandGrab()
    {

        /*RaycastHit hit;
        XRRayInteractor rayInteractor = grabber.GetComponent<XRRayInteractor>();
        if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                initialGrabPoint.transform.position = hit.point;

            }
        }*/



        Ray ray =  new Ray(grabber.transform.position, grabber.transform.forward);
        // create a plane at 0,0,0 whose normal points to +Y:
        Plane hPlane = new Plane(Vector3.up, transform.position);
        // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
        float distance = 0;
        // if the ray hits the plane...
        if (hPlane.Raycast(ray, out distance))
        {
            // get the hit point:
            initialGrabPoint.transform.position = ray.GetPoint(distance);
            initialGrabPoint.GetComponent<Renderer>().enabled = true;

        }
    }
    public void SelectExit(SelectExitEventArgs args)
    {
        Debug.Log("SelectExit");
        inputController.SetPlatFormGrabbed(false);
        grabber = null;
        Vector3 yCorrection = player.transform.position;
        yCorrection.y = transform.position.y;
        distanceToPlayer = yCorrection - transform.position;
        grabStarted = false;
        initialGrabPoint.GetComponent<Renderer>().enabled = false;
    }

    void FixedUpdate()
    {
        //transform.LookAt(initialGrabPoint.transform.position);
        if (inputController.PlatFormGrabbed())
        {
            rigidbody.MovePosition(grabber.transform.position - distanceToPlayer);
            rigidbody.MoveRotation(Quaternion.LookRotation(transform.position - initialGrabPoint.transform.position, Vector3.up));

        }
        else
        {
            Vector3 yCorrection = player.transform.position;
            yCorrection.y = transform.position.y;
            rigidbody.MovePosition(Vector3.Lerp(transform.position, yCorrection - distanceToPlayer, Time.deltaTime));

        }
        if (grabStarted)
        {
            UpdateMainHandGrab();

        }

    }
}
