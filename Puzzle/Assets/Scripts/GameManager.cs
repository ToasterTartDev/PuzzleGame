using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; //сингл на себя
    public SaveLoadData slData; //ссылка на скрипт сохранений

    [Space]
    public List<GameObject> allLevelPrefab = new List<GameObject>();    //префабы уровней
    public static int selectLevel;  //выбранный уровень
    [HideInInspector]
    public AllElements allElements; //ввыска на объект уровня

    [Space] //юай
    public Canvas mainCanvas;   
    public RectTransform panelUITransform;
    public RectTransform panelGameTransform;
    public RectTransform panelWin;


    void Awake()
    {
        gm = this;

        if (selectLevel < allLevelPrefab.Count) //создаем объект уровня
            allElements = Instantiate(allLevelPrefab[selectLevel], panelGameTransform).GetComponent<AllElements>();

        allElements.GetElements();  //получаем его элементы
        allElements.InitializeElements();   //инициализируем их
        slData.LoadData();  //загружаем инфу
        allElements.CreateUIElements(); //создаем юай элементв для них
        allElements.CheckWin(); //проверяем победу, вдруг уровень уже пройден
    }

    public void CreateUIElement(GameObject gm, int id)  //создание UI элемента
    {
        ElementUI element = Instantiate(gm, panelUITransform).GetComponent<ElementUI>();
        element.id = id;
    }

    public void LoadMainScene()     //кнопка назад
    {
        slData.SaveData();
        SceneManager.LoadScene(0);
    }

    public void ResetGame()     //кнопка ресет
    {
        slData.ResetData(selectLevel);
        SceneManager.LoadScene(1);
    }
}
