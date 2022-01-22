using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveLoadData : MonoBehaviour
{

    [Serializable]
    public class ElementData    //класс элементов
    {
        [SerializeField]
        public bool isActive;
        [SerializeField]
        public int id;
    }

    [Serializable]
    public class LevelData  //класс уровней (ид уровня и элемента на нем)
    {
        [SerializeField]
        public int idLevel = 0;
        [SerializeField]
        public List<ElementData> levelElements = new List<ElementData>();   //список элементов уровня
    }

    [Serializable]
    public class Data   //общий класс для сериализации
    {
        [SerializeField]
        public List<LevelData> allLevel = new List<LevelData>();    //список всех уровней
    }
    [HideInInspector]
    public Data dataClass;

    public void SaveData()  //сохранение
    {
        bool flag = false;
        foreach (LevelData ld in dataClass.allLevel)    
        {
            if (ld.idLevel == GameManager.selectLevel)  //если проходим этот уровенть не первый раз, то перезаписываем данные
            {
                ld.levelElements = new List<ElementData>();
                foreach (Element el in GameManager.gm.allElements.levelElements)
                {
                    ElementData newElement = new ElementData();
                    newElement.id = el.id;
                    newElement.isActive = el.isActive;
                    ld.levelElements.Add(newElement);
                    flag = true;
                }
            }
        }

        if (!flag)  //если играем первый раз, то нужно записать данные
        {
            LevelData ld = new LevelData();
            ld.idLevel = GameManager.selectLevel;
            ld.levelElements = new List<ElementData>();
            foreach (Element el in GameManager.gm.allElements.levelElements)
            {
                ElementData newElement = new ElementData();
                newElement.id = el.id;
                newElement.isActive = el.isActive;
                ld.levelElements.Add(newElement);
            }
            dataClass.allLevel.Add(ld);
        }

        string s = JsonUtility.ToJson(dataClass);
        PlayerPrefs.SetString("Data", s);
        //Debug.Log("SaveDate:    " + s);
    }
    public void LoadData()  //загрузка
    {
        if (PlayerPrefs.GetString("Data") != "")
        {
            dataClass = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("Data"));
            //Debug.Log("LoadData:    " + PlayerPrefs.GetString("Data"));
        }
        else
        {
            dataClass = new Data();
            //Debug.Log("Create a new Data");
        }

        foreach (LevelData ld in dataClass.allLevel)
        {
            if (ld.idLevel == GameManager.selectLevel)
            {
                foreach (ElementData ed in ld.levelElements)
                {
                    foreach (Element el in GameManager.gm.allElements.levelElements)
                    {
                        if ((el.id == ed.id) && (!ed.isActive))
                        {
                            el.isActive = false;
                            el.SetActive(el.isActive);
                        }
                    }
                }
            }
        }
    }
    public void ResetData(int idLevel)  //ресет уровня
    {
        SaveData();
        LevelData lData = new LevelData();
        if (dataClass != null)
        {
            foreach (LevelData ld in dataClass.allLevel)
            {
                if (ld.idLevel == idLevel)
                {
                    lData = ld;
                }
            }
        }
        dataClass.allLevel.Remove(lData);
        string s = JsonUtility.ToJson(dataClass);
        PlayerPrefs.SetString("Data", s);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PlayerPrefs.DeleteAll();
    }
}
