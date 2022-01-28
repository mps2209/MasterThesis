
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject forestEnvironment;
    public GameObject sterileEnvironment;
    public GameObject user;
    public List<GameObject> exampleTrees;
    public GameObject stump;
    public MVCInputController inputController;
    public GameObject backToMenuButton;
    public GameObject confirmToMenuButton;

    void Start()
    {

        Debug.Log("Immersion: " + LevelController.loadForest);
        Debug.Log("Example Tree: " + LevelController.exampleTreeNr);
        inputController = GetComponent<MVCInputController>();
        inputController.toggleTutorial(!LevelController.skipTutorial);
        if (!LevelController.loadForest)
        {
            forestEnvironment.SetActive(false);
            sterileEnvironment.SetActive(true);
            Vector3 userOffset = user.transform.position;
            userOffset.y = 0;
            user.transform.position = userOffset;
            stump.GetComponent<StumpController>().InitStump();
        }
        exampleTrees[LevelController.exampleTreeNr - 1].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BackToMainMenu()
    {
        confirmToMenuButton.SetActive(!confirmToMenuButton.activeSelf);
        backToMenuButton.SetActive(!backToMenuButton.activeSelf);
    }
    public void ConfirmBacktoMainMenu()
    {
        SceneManager.LoadScene("StartMenu");       
    }

}
