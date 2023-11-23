using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Sprite ImageEnter;
    [SerializeField] public Sprite ImageExite;

    private void Start()
    {
        GetComponent<Image>().sprite = ImageExite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //GetComponent<Image>().sprite = ImageEnter;
        if (eventData.pointerDrag != null)
        {
            //GetComponent<Image>().sprite = ImageEnter;
            Debug.Log("OnDrop");
            GameObject droppedObject = eventData.pointerDrag;
            DragDrop dragDrop = droppedObject.GetComponent<DragDrop>();
            dragDrop.parentAfterDrag = transform;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = ImageExite;
    }
}
