using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour
{

    GameObject xrRig;
    // Start is called before the first frame update
    void Start()
    {
        xrRig = GameObject.Find("XR Rig");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScale(float growth)
    {
        xrRig.transform.localScale = xrRig.transform.localScale + new Vector3(growth, growth, growth);
    }

}
