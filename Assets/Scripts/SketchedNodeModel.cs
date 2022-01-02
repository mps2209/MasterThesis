using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{

    public class SketchedNodeModel : MonoBehaviour
    {
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
        MVCInputController inputController;
        bool selected = false;
        GameObject grabber;
        Coroutine currentCoroutine;
        private void Start()
        {
            sketchBranchView = GameObject.Find("SketchingPlatform").GetComponent<SketchBranchView>();
            renderer = GetComponent<Renderer>();
            renderer.material.color = inactiveColor;

        }

        private void Update()
        {
            if (selected)
            {
                transform.position = grabber.transform.position;
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
            grabber = selectEnterEventArgs.interactor.gameObject;
            sketchBranchView.SetSelectedNode(this);
            currentCoroutine = StartCoroutine(SetSelected(true));
        }

        public void LeftSelect(SelectEnterEventArgs selectEnterEventArgs)
        {
            DestroyNode();
        }
        IEnumerator SetSelected(bool selected)
        {
            yield return new WaitForSeconds(selectDelay);
            this.selected = selected;


        }

        public void SelectExit(SelectExitEventArgs selectEnterEventArgs)
        {
            StopCoroutine(currentCoroutine);
            selected = false;
        }
        public void RightHoverEnter(HoverEnterEventArgs selectEnterEventArgs)
        {
            renderer.material.SetColor("_Color", rightColor);


        }
        public void RightHoverExit(HoverExitEventArgs selectEnterEventArgs)
        {
            renderer.material.SetColor("_Color", inactiveColor);

        }
        public void LeftHoverEnter(HoverEnterEventArgs selectEnterEventArgs)
        {
            renderer.material.SetColor("_Color", leftColor);


        }
        public void LeftHoverExit(HoverExitEventArgs selectEnterEventArgs)
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
        public void LeftActivate(ActivateEventArgs args)
        {
            Debug.Log("Activate");

            DestroyNode();
        }
    }

}