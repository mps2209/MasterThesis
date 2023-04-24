using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkMaterial : MonoBehaviour
{
    Material blinkingMaterial;
    Color emmisionColor;
    public bool blink = false;
    public float blinkSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        blinkingMaterial = GetComponent<Renderer>().material;
        emmisionColor = blinkingMaterial.GetColor("_EmissionColor");
        blinkingMaterial.SetColor("_EmissionColor", emmisionColor * 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (blink)
        {
            Blink();
        }
    }
    public void DisableBlink()
    {
        blink = false;
        blinkingMaterial.SetColor("_EmissionColor", emmisionColor * 0f);

    }


    void Blink()
    {
        float intensity = Mathf.Lerp(0, 1, (Mathf.Sin(Time.time * blinkSpeed) + 1) / 2);
        //Debug.Log("setting intensity to " + intensity);
        blinkingMaterial.SetColor("_EmissionColor", emmisionColor * intensity);
    }
}
