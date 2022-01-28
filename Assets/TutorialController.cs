using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public TutorialPoint tutorialState = TutorialPoint.GrabPlatform;
    public List<GameObject> tutorialSteps;
    public Dictionary<TutorialPoint, GameObject> tutorialSetups;
    BlinkController sketchingPlatformBlinkController;
    // Start is called before the first frame update
    void Start()
    {
        sketchingPlatformBlinkController = GameObject.Find("SketchingPlatform").GetComponent<BlinkController>();
        tutorialSetups = new Dictionary<TutorialPoint, GameObject>();
        int i = 0;
        foreach(GameObject go in tutorialSteps)
        {
            if (go != null)
            {
                tutorialSetups[(TutorialPoint)i] = go;
                i++;
            }

        }
        SetupTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTutorial()
    {
        tutorialState= TutorialPoint.GrabPlatform;
    }
    public void AdvanceTutorial()
    {
        int state = (int)tutorialState;
        state++;
        tutorialState = (TutorialPoint)state;
        ResetTutorialSpecifics();
        SetupTutorial();
    }
    void ResetTutorialSpecifics()
    {
        sketchingPlatformBlinkController.StopBlink();

    }
    void SetupTutorial()
    {
        TutorialPoint[] keys = new TutorialPoint[tutorialSetups.Keys.Count];
        tutorialSetups.Keys.CopyTo(keys,0);
        foreach (TutorialPoint key in keys)
        {
            if ((int)key > (int)tutorialState)
            {
                tutorialSetups[key].SetActive(false);

            }
        }
        tutorialSetups[tutorialState].SetActive(true);
        //if special setup is required
        switch (tutorialState)
        {
            case TutorialPoint.GrabPlatform:
                sketchingPlatformBlinkController.StartBlink();
                break;
            case TutorialPoint.AddTrunk:
  

                break;
            case TutorialPoint.MoveSection:
                break;
            case TutorialPoint.AddBranch:
                break;
            case TutorialPoint.DeleteSection:
                break;
            case TutorialPoint.AdvanceTree:
                break;
            case TutorialPoint.StepBackTree:
                break;
            case TutorialPoint.ResetTree:
                break;
            case TutorialPoint.DrawTree:
                break;
        }
    }
}
public enum TutorialPoint
{
    GrabPlatform,AddTrunk, MoveSection, AddBranch, DeleteSection, AdvanceTree, StepBackTree,ResetTree, DrawTree
}
