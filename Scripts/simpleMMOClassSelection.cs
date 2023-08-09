using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SkillManager;
//using HarmonyLib;
using JetBrains.Annotations;

namespace SimpleMMOClassSelection
{
    public class simpleMMOClassSelection : MonoBehaviour
    {

        public string simpleMMOClass;
        public static string simpleMMOClassSelected;
        private bool buttonToggled;

        public void SimpleMMOSelector()
        {
            buttonToggled = !buttonToggled;
    
            if (buttonToggled) simpleMMOClassSelected = simpleMMOClass;
            Debug.Log(simpleMMOClassSelected);
        }
    }
}