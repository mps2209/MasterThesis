using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystemController : MonoBehaviour
{
    LSystem lSystem;
    // Start is called before the first frame update
    void Start()
    {
        lSystem = GetComponent<LSystem>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddStep(string letter)
    {
        lSystem.AddStep(letter);
    }
    public void StepForward()
    {
        lSystem.StepForward();
    }
    public void StepBack()
    {
        lSystem.StepBack();
    }
}
