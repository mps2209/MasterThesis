using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{

    public class SketchedNodeModel : MonoBehaviour
    {
        public GameObject branchOffTools;
        public Color leftColor;
        public Color inactiveColor;
        public Color rightColor;
        public float selectDelay = .5f;
        [SerializeField]
        char letter;
        [SerializeField]
        int index;
        Renderer renderer;
        SketchBranchView sketchBranchView;
        bool selected = false;
        GameObject grabber;
        Coroutine currentCoroutine;
        bool isBranchOffNode = false;
        MVCLSystem lSystem;
        MVCInputController inputController;
        Transform initialParent;
        private void Start()
        {
            sketchBranchView = GameObject.Find("SketchingPlatform").GetComponent<SketchBranchView>();
            renderer = GetComponent<Renderer>();
            renderer.material.color = inactiveColor;
            initialParent = transform.parent;
            inputController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MVCInputController>();
        }

        private void Update()
        {
            if (selected)
            {
                sketchBranchView.UpdateNodePosition(this);
      

            }
        }
        public void SetMaterial(Material material)
        {
            renderer = GetComponent<Renderer>();
            renderer.material = material;

        }


        public void Selected(SelectEnterEventArgs selectEnterEventArgs)
        {
            sketchBranchView.SetSelectedNode(this);
            inputController.activeController = selectEnterEventArgs.interactor;
             currentCoroutine = StartCoroutine(SetSelected( selectEnterEventArgs.interactor.gameObject));
        }

        public void Activate(ActivateEventArgs args)
        {
            Debug.Log("Activated");
            DestroyNode();
        }
        IEnumerator SetSelected(GameObject grabber)
        {
            yield return new WaitForSeconds(selectDelay);
            this.selected = true;
            transform.parent = grabber.transform;
            transform.localPosition = Vector3.zero;

        }

        public void SelectExit(SelectExitEventArgs selectEnterEventArgs)
        {


            StopCoroutine(currentCoroutine);
            transform.parent = initialParent;

            selected = false;
        }
        public void HoverEnter(HoverEnterEventArgs selectEnterEventArgs)
        {
            renderer.material.SetColor("_Color", rightColor);


        }
        public void HoverExit(HoverExitEventArgs selectEnterEventArgs)
        {
            renderer.material.SetColor("_Color", inactiveColor);

        }
        public int Index()
        {
            return index;
        }
        public char Letter()
        {
            return letter;
        }
        public void Index(int index)
        {
            this.index = index;
        }
        public void Letter(char letter)
        {
            this.letter = letter;
        }

        public void DestroyNode()
        {
            sketchBranchView.DestroyNode(this);
        }


        public void SetBranchOffNode(bool branchOff)
        {
            /*
            this.isBranchOffNode = branchOff;
            branchOffTools.SetActive(branchOff);
            lSystem = GameObject.Find("LSystem").GetComponent<MVCLSystem>();*/
        }

        public void IncreaseBranchOffRate(SelectEnterEventArgs selectEnterEventArgs)
        {
            lSystem.IncreaseBranchOffRate();
        }
        public void DecreaseBranchOffRate(SelectEnterEventArgs selectEnterEventArgs)
        {
            lSystem.DecreaseBranchOffRate();
        }
        public void IncreaseBranchOffRate(ActivateEventArgs activateEventArgs)
        {
            lSystem.IncreaseBranchOffRate();

        }
        public void DecreaseBranchOffRate(ActivateEventArgs activateEventArgs)
        {
            lSystem.DecreaseBranchOffRate();

        }
    }

}