using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.InputSystem.InputAction;

public class ControllerAnimation : MonoBehaviour
{

    public GameObject rightController;
    public GameObject leftController;
    Animator rightControllerAnimator;
    Animator leftControllerAnimator;

    // Start is called before the first frame update
    /*
     *     
    m_animator.SetFloat("Button 1", OVRInput.Get(OVRInput.Button.One, m_controller) ? 1.0f : 0.0f);
    m_animator.SetFloat("Button 2", OVRInput.Get(OVRInput.Button.Two, m_controller) ? 1.0f : 0.0f);
    m_animator.SetFloat("Button 3", OVRInput.Get(OVRInput.Button.Start, m_controller) ? 1.0f : 0.0f);

    m_animator.SetFloat("Joy X", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).x);
    m_animator.SetFloat("Joy Y", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).y);

    m_animator.SetFloat("Trigger", OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller));
    m_animator.SetFloat("Grip", OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller));
     * */
    void Start()
    {
        rightControllerAnimator = rightController.GetComponent<Animator>();
        leftControllerAnimator = leftController.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateRightTrigger(CallbackContext cb)
    {
        float buttonPress = cb.ReadValue<float>();

        rightControllerAnimator.SetFloat("Trigger", buttonPress);

        

    }
    public void AnimateLeftTrigger(CallbackContext cb)
    {
        float buttonPress = cb.ReadValue<float>();

        leftControllerAnimator.SetFloat("Trigger", buttonPress);

    }
    public void AnimateRightGrip(CallbackContext cb)
    {
        float buttonPress = cb.ReadValue<float>();

        rightControllerAnimator.SetFloat("Grip", buttonPress);

    }
    public void AnimateLeftGrip(CallbackContext cb)
    {
        float buttonPress = cb.ReadValue<float>();

        leftControllerAnimator.SetFloat("Grip", buttonPress);

    }
    public void AnimateLeftX(CallbackContext cb)
    {
        float buttonPress = cb.ReadValue<float>();

        leftControllerAnimator.SetFloat("Button 1", buttonPress);

    }
    public void AnimateLeftY(CallbackContext cb)
    {
        float buttonPress = cb.ReadValue<float>();

        leftControllerAnimator.SetFloat("Button 2", buttonPress);

    }
    public void AnimateRightA(CallbackContext cb)
    {
        float buttonPress = cb.ReadValue<float>();

        rightControllerAnimator.SetFloat("Button 1", buttonPress);

    }
    public void AnimateRightB(CallbackContext cb)
    {
        float buttonPress = cb.ReadValue<float>();

        rightControllerAnimator.SetFloat("Button 2", buttonPress);

    }
}
