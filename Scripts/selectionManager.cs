using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionManager : MonoBehaviour
{
    public GameObject simpleMMOClass;
    
    public void selectedMMOClass()
    {
        if (simpleMMOClass.activeInHierarchy == true)
            simpleMMOClass.SetActive(false);
        else
            simpleMMOClass.SetActive(true);
    }
}
