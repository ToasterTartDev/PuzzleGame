using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Element : MonoBehaviour, IDropHandler
{
    public int id;
    public bool isActive = true;
    public GameObject refElementUI;
    private RectTransform rectTransform;
    private Animator animator;
    private Image image;

    void IDropHandler.OnDrop(PointerEventData eventData)    //отпускание
    {
        if (isActive)   //проверка на активность
        {
            if ((eventData.pointerDrag != null) && (eventData.pointerDrag.GetComponent<ElementUI>().id == id))  //если id совпадает
            {
                isActive = false;
                SetActive(isActive);
                eventData.pointerDrag.GetComponent<ElementUI>().CheckElement(true);
                animator.SetInteger("Status", 2);
                GameManager.gm.allElements.CheckWin();
            }
            else
                eventData.pointerDrag.GetComponent<ElementUI>().CheckElement(false);
        }
    }

    public void SetActive(bool flag)    //цвет элемента
    {
        if(flag)
            image.color = new Color(1f, 1f, 1f, 0.3f);
        else
            image.color = new Color(1f, 1f, 1f, 1f);
    }

    public void Initialization()    //инициализация компонентов
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public void CreateUI()  //создание элемента в UI
    {
        SetActive(isActive);
        if (isActive)
            GameManager.gm.CreateUIElement(refElementUI, id);
    }
}
