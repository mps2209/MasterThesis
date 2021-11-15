using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    //Action<string> deviceLoaded = (x) => Console.WriteLine(x);
    public List<GameObject> oculus;
    public List<GameObject> desktop;

    void Start()
    {
        Debug.Log("Started Game Controller");
        if(XRSettings.loadedDeviceName=="oculus display")
        {
            toggleOculus(true);

        }
        else
        {
            toggleOculus(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void toggleOculus(bool on)
    {
        if (on)
        {
            Debug.Log("Turning oculus on");

        }
        else
        {
            Debug.Log("Turning oculus off");

        }

        foreach (GameObject oculusSpecific in oculus)
        {
            oculusSpecific.SetActive(on);
        }
        foreach (GameObject desktopSpecific in desktop)
        {
            desktopSpecific.SetActive(!on);
        }
    }
}
