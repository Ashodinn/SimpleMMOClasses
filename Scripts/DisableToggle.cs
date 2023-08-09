using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableToggle : MonoBehaviour
{
    public Toggle newToggle1;
    public Toggle newToggle2;
    public Toggle newToggle3;
    public Toggle newToggle4;
    public Toggle newToggle5;
    public Toggle newToggle6;
    public Toggle newToggle7;
    public Toggle newToggle8;
    
    
    public void newMethod()
    {
        newToggle1.interactable = false;
        newToggle2.interactable = false;
        newToggle3.interactable = false;
        newToggle4.interactable = false;
        newToggle5.interactable = false;
        newToggle6.interactable = false;
        newToggle7.interactable = false;
        newToggle8.interactable = false;
    }

}
