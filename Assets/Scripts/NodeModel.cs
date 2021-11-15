using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Assets.Scripts
{

    public class NodeModel : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
    {


        public string letter;
        public int index;
        InputController inputController;
        Renderer renderer;
        public Color activeColor;
        public Color inactiveColor;
       
        private void Start()
        {
            inputController = GameObject.Find("GameController").GetComponent<InputController>();
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
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            inputController.hoveredNode = this;
            renderer.material.SetColor("_Color", activeColor);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            inputController.hoveredNode = null;
            renderer.material.SetColor("_Color", inactiveColor);
        }
    }

}