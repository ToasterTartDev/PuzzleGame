using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    private int selectLevel = 0; //выбранный уровень

    public void SelectedLevel(int id)   //клики по кнопкам уровня
    {
        GameManager.selectLevel = id;
        SceneManager.LoadScene(1);
    }
}
