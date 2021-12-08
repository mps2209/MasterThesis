/*using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidVrRenderer : MonoBehaviour
{
    public Material lineRendererMaterial;
    public float lineRendererWidth;
    public GameObject startIndicatorPrefab;
    public GameObject sectionIndicatorPrefab;

    public float granularity = 0.1f;
    GameObject startIndicator;
    LineRenderer currentLineRenderer;
    InputController inputController;
    public Dictionary<char, LineRenderer> renderers = new Dictionary<char, LineRenderer>();
    public int sectionRatio = 1;
    //stores the positions relating to the sections
    public Dictionary<char, List<int>> numSections = new Dictionary<char, List<int>>();
    GameController gameController;
    LSystemRenderer lSystemRenderer;
    NodeController nodeController;
    // Start is called before the first frame update
    void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lSystemRenderer = GameObject.Find("LSystemController").GetComponent<LSystemRenderer>();
        startIndicator = Instantiate(startIndicatorPrefab, Vector3.zero, Quaternion.identity);
        nodeController = GameObject.Find("NodeController").GetComponent<NodeController>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (gameController.gameState)
        {
            case GameState.TrunkNotStarted:
                if (startIndicator != null)
                {

                    IndicateStart();
                }
                if (inputController.rightTriggerDown)
                {
                    if (startIndicator != null)
                    {
                        lSystemRenderer.startingPoint = startIndicator.transform.position;
                        Destroy(startIndicator);
                    }
                    Debug.Log("TrunkNotStarted rightTriggerDown");

                    //TriggerPressed();
                }
                break;

            case GameState.TrunkDrawn:

                if (inputController.rightTriggerDown)
                {
                    Debug.Log("TrunkDrawn rightTriggerDown");
                    //TriggerPressed();
                }
                break;
            case GameState.BranchDrawn:
                break;
        }


    }
    void IndicateStart()
    {
        Vector3 startPosition = inputController.controllerPosition;
        startPosition.y = 0;
        startIndicator.transform.position = startPosition;
    }
    void InitLineRenderer(char letter, Vector3 start)
    {
        GameObject lineRendererParent = new GameObject();
        lineRendererParent.name = "Renderer" + letter;
        lineRendererParent.AddComponent<LineRenderer>();
        renderers[letter] = lineRendererParent.GetComponent<LineRenderer>();
        currentLineRenderer = renderers[letter];
        currentLineRenderer.positionCount = 1;
        currentLineRenderer.material = lineRendererMaterial;
        currentLineRenderer.startWidth = lineRendererWidth;
        currentLineRenderer.endWidth = lineRendererWidth;
        currentLineRenderer.SetPosition(0, start);


    }

    public void TriggerPressed()
    {
        if (nodeController.selectedNode != null)
        {
            Debug.Log("Selected Node: " + nodeController.selectedNode.letter + nodeController.selectedNode.index);
            nodeController.UpdateLetter();
            if (!renderers.ContainsKey(nodeController.GetLetter()))
            {
                Debug.Log("Inititating branch: " + nodeController.GetLetter());

                InitLineRenderer(nodeController.GetLetter(), nodeController.selectedNode.transform.position);
                currentLineRenderer.SetPosition(currentLineRenderer.positionCount++, inputController.controllerPosition);

            }

            nodeController.selectedNode = null;
        }
        if (!renderers.ContainsKey(nodeController.GetLetter()))
        {
            Debug.Log("Init LineRenderer");
            Debug.Log("Inititating branch: " + nodeController.GetLetter());

            InitLineRenderer(nodeController.GetLetter(), lSystemRenderer.startingPoint);
        }
        else
        {
            Debug.Log("Adding Point");

            currentLineRenderer.SetPosition(currentLineRenderer.positionCount++, inputController.controllerPosition);
        }
    }
   
    public void Simplify()
    {
        currentLineRenderer.Simplify(granularity);

    }
    public void RenderSections()
    {
        foreach (char key in numSections.Keys)
        {
            foreach (int index in numSections[key])
            {
                GameObject sectionIndicator = Instantiate(sectionIndicatorPrefab, renderers[key].GetPosition(index), Quaternion.identity);
                NodeModel nodeModel = sectionIndicator.GetComponent<NodeModel>();
                //nodeModel.letter = key;
                //nodeModel.index = index;
            }
        }
    }
    public void SpliceTrunk()
    {
        LineRenderer trunk = renderers['A'];
        List<int> sections = new List<int>();
        for (int i = 0; i < trunk.positionCount; i++)
        {
            if (i % sectionRatio == 0)
            {
                sections.Add(i);

            }
        }
        if (!sections.Contains(trunk.positionCount - 1))
        {
            sections.Add(trunk.positionCount - 1);
        }
        numSections['A'] = sections;
    }
    public void SpliceBranch(char letter)
    {
        LineRenderer branch = renderers[letter];
        List<int> sections = new List<int>();
        for (int i = 0; i < branch.positionCount; i++)
        {
            if (i % sectionRatio == 0)
            {
                sections.Add(i);

            }
        }
        if (!sections.Contains(branch.positionCount - 1))
        {
            sections.Add(branch.positionCount - 1);
        }
        numSections[letter] = sections;
    }
    public void TriggerReleased()
    {
        switch (gameController.gameState)
        {
            case GameState.TrunkNotStarted:
                Simplify();
                SpliceTrunk();
                RenderSections();
                gameController.gameState = GameState.TrunkDrawn;
                break;
            case GameState.TrunkDrawn:
                Simplify();
                SpliceBranch(nodeController.GetLetter());
                gameController.gameState = GameState.BranchDrawn;
                break;
            case GameState.BranchDrawn:
                break;
            default:
                break;
        }
    }
}
*/