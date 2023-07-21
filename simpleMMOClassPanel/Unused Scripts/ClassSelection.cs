using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelection : MonoBehaviour
{
    public GameObject[] simpleMMOClass;
    public int selectedClass = 0;
    public void NextClass()
    {
        simpleMMOClass[selectedClass].SetActive(false);
        selectedClass = (selectedClass + 1) % simpleMMOClass.Length;
        simpleMMOClass[selectedClass].SetActive(true);
    }

    public void PreviousClass()
    {
        simpleMMOClass[--selectedClass].SetActive(false);
        selectedClass--;
        if (selectedClass < 0)
        {
            selectedClass += simpleMMOClass.Length;
        }
        simpleMMOClass[selectedClass].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedClass", selectedClass);
    }
}
