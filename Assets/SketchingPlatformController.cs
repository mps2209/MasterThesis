using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SketchingPlatformController : MonoBehaviour
{

    GameObject player;
    Vector3 distanceToPlayer;
    GameObject grabber;
    Rigidbody rigidbody;
    MVCInputController inputController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 yCorrection = player.transform.position;
        yCorrection.y = transform.position.y;
        distanceToPlayer = yCorrection - transform.position;
        rigidbody = GetComponent<Rigidbody>();
        inputController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MVCInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputController.PlatFormGrabbed())
        {
            rigidbody.MovePosition(grabber.transform.position - distanceToPlayer);
        }
        else
        {
            Vector3 yCorrection = player.transform.position;
            yCorrection.y = transform.position.y;
            rigidbody.MovePosition(Vector3.Lerp(transform.position, yCorrection - distanceToPlayer, Time.deltaTime));

        }

    }

    public void SelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("SelectEntered");
        inputController.SetPlatFormGrabbed(true);
        grabber = args.interactor.gameObject;
        distanceToPlayer = grabber.transform.position - transform.position;
    }
    public void SelectExit(SelectExitEventArgs args)
    {
        Debug.Log("SelectExit");
        inputController.SetPlatFormGrabbed(false);
        grabber = null;
        Vector3 yCorrection = player.transform.position;
        yCorrection.y = transform.position.y;
        distanceToPlayer = yCorrection - transform.position;
    }

}
