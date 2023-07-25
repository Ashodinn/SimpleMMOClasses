using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject classSelectionPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            classSelectionPanel.gameObject.SetActive(!classSelectionPanel.gameObject.activeSelf);
        }
    }
}
