using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllElements : MonoBehaviour
{
    [HideInInspector]
    public List<Element> levelElements = new List<Element>();   //все элементы уровня

    public void GetElements()   //получить
    {
        Element[] els = GameObject.FindObjectsOfType<Element>();
        foreach (Element el in els)
            levelElements.Add(el);
    }

    public void InitializeElements()    //проинициализировать
    {
        foreach (Element el in levelElements)
            el.Initialization();
    }

    public void CreateUIElements()  //создать юай
    {
        foreach (Element el in levelElements)
            el.CreateUI();
    }

    public void CheckWin()  //пробежка и проверка победы
    {
        bool flag = true;
        foreach (Element el in levelElements)
            if (el.isActive)
                flag = false;

        if(flag)
        {
            GameManager.gm.panelWin.gameObject.SetActive(true);
        }
    }
}
