using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ActivateSelectionPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ActivateSelectionPanel.gameObject.SetActive(!ActivateSelectionPanel.gameObject.activeSelf);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateSelectionPanel.gameObject.SetActive(false);
        }
    }
}