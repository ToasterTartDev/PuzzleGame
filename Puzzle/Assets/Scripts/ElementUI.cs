using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int id;
    public bool isActive = true;
    private RectTransform rectTransform;
    private Animator animator;
    [SerializeField]
    private CanvasGroup canvasGroup;


    //Логика drag and drop
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(isActive)
            rectTransform.anchoredPosition += eventData.delta / GameManager.gm.mainCanvas.scaleFactor;
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (isActive)
        {
            rectTransform.gameObject.transform.parent = GameManager.gm.panelGameTransform;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.5f;
        }
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (isActive)
        {
            rectTransform.gameObject.transform.parent = GameManager.gm.panelUITransform;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }
    }


    void Start()    //инициализация компонентов
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    public void CheckElement(bool flag) //при отпускании попали ли на нужный элемент
    {
        if(flag)
        {
            animator.SetInteger("Status", 1);
            isActive = false;
        }
        else
            rectTransform.gameObject.transform.parent = GameManager.gm.panelUITransform;
    }
}
