using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightOnHover : MonoBehaviour
{
    // Start is called before the first frame update
    public Color activeColor;
    public Color inactiveColor;
    List<Renderer> renderer;
    void Start()
    {
        renderer = new List<Renderer>();
        renderer.AddRange(GetComponentsInChildren<Renderer>());
        SetInactive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("OnHoverEnter");
        SetActive();
    }
    public void OnHoverExit(HoverExitEventArgs args)
    {
        Debug.Log("OnHoverExit");

        SetInactive();

    }

    void SetActive() {
        foreach(Renderer r in renderer)
        {
           r.material.SetColor("_Color", activeColor);

        }
    }
    void SetInactive()
    {
        foreach (Renderer r in renderer)
        {
            r.material.SetColor("_Color", inactiveColor);

        }
    }
}
