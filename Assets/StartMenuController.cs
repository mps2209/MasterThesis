using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class StartMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text treeNrText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartApplication()
    {
        Debug.Log("StartApplication");
        SceneManager.LoadScene("MVCMain");        

    }
    public void IncreaseTreeChoice()
    {
        Debug.Log("IncreaseTreeChoice");
        LevelController.IncreaseChosenTree();
        treeNrText.text = LevelController.exampleTreeNr.ToString();

    }
    public void DecreaseTreeChoice()
    {
        Debug.Log("DecreaseTreeChoice");
        LevelController.DecreaseChosenTree();
        treeNrText.text = LevelController.exampleTreeNr.ToString();

    }
    public void ToggleImmersion(bool value)
    {
        Debug.Log("ToggleImmersion");
        LevelController.loadForest = value;
    }
    public void ToggleTutorial(bool value)
    {
        Debug.Log("ToggleImmersion");
        LevelController.skipTutorial = value;
    }
}
