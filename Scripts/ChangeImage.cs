using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{

    //public Sprite newButtonimage;
    public Button newButton;
    public Image buttonImage;
    public Sprite[] toggledSprites;
    private bool buttonToggled;

    public void ToggleButton()
    {
        buttonToggled = !buttonToggled;

        if (buttonToggled) buttonImage.sprite = toggledSprites[1];
        else buttonImage.sprite = toggledSprites[0];
    }
    
    public void newMethod()
    {
        newButton.interactable = false;
    }



}
