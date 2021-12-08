using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{

    public class SketchedNodeModel : MonoBehaviour
    {

        [SerializeField]
        char letter;
        [SerializeField]
        int index;
        Renderer renderer;
        public Color activeColor;
        public Color inactiveColor;
        SketchBranchView sketchBranchView;
        private void Start()
        {
            sketchBranchView= GameObject.Find("SketchingPlatform").GetComponent<SketchBranchView>();
            renderer = GetComponent<Renderer>();
            inactiveColor = renderer.material.GetColor("_Color");
            activeColor= renderer.material.GetColor("_Color")*.8f;
        }


        public void SetMaterial(Material material)
        {
            renderer = GetComponent<Renderer>();
            renderer.material = material;
            
        }
        public void SetColors(Color activeColor, Color inactiveColor)
        {
            this.activeColor = activeColor;
            this.inactiveColor = inactiveColor;
            renderer.material.SetColor("_Color", inactiveColor);

        }

        public void Selected(SelectEnterEventArgs selectEnterEventArgs)
        {
            Debug.Log("selected Node"+ letter + index);

            sketchBranchView.SetSelectedNode(this);
        }
        public void HoverEnter(HoverEnterEventArgs selectEnterEventArgs)
        {
            renderer.material.color = activeColor;


        }
        public void HoverExxit(HoverExitEventArgs selectEnterEventArgs)
        {

            renderer.material.color = inactiveColor;

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
            this.index=index;
        }
        public void Letter(char letter)
        {
            this.letter = letter;
        }
    }

}