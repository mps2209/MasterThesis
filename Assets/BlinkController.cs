using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BlinkController : MonoBehaviour
{
    BlinkMaterial[] blinkingMaterials;
    
    // Start is called before the first frame update
    void Start()
    {
        blinkingMaterials = GetComponentsInChildren<BlinkMaterial>();


        Debug.Log("Found " + blinkingMaterials.Length + " BlinkMaterials");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartBlink()
    {
        //Debug.Log("Start Blink");
        blinkingMaterials.All(blinkMat =>
        {
            blinkMat.blink = true;
            return blinkMat;
        });
    }
    public void StopBlink()
    {
        //Debug.Log("Stop Blink");

        blinkingMaterials.All(blinkMat =>
        {
            blinkMat.DisableBlink();
            return blinkMat;
        });
    }
}
