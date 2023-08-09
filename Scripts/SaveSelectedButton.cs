using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;





public class SaveSelectedButton : MonoBehaviour
{
   [SerializeField] private Toggle toggle1, toggle2, toggle3, toggle4, toggle5, toggle6, toggle7, toggle8;
   private string _savedSimpleClass = "Classless";
   private const string _simpleMMOClass = "SimpleMMOClass";
   //private static GameObject SimpleMMOClassesPanel;

   void Awake()
   {
      
      if (!Player.m_localPlayer.m_customData.ContainsKey(_simpleMMOClass))
         Player.m_localPlayer.m_customData.Add(_simpleMMOClass, _savedSimpleClass);
      
      //debug
      if (!Player.m_localPlayer.m_customData.ContainsKey(_simpleMMOClass))
      {
         Debug.Log("SimpleMMOClass Entry is not found");
      }
      else
      {
         Debug.Log("SimpleMMOClass Entry Found!");
      }
      
      if (!Player.m_localPlayer.m_customData.ContainsValue(_savedSimpleClass))
      {
         Debug.Log("Selected Class Entry is not found");
      }
      else
      {
         Debug.Log("Selected Class Entry Found!");
      }
      //end debug
      
      
      if (Player.m_localPlayer.m_customData.ContainsValue("Warrior"))
      {
         toggle1.interactable = true;
         toggle1.isOn = true;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Warrior");
         
      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Paladin"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = true;
         toggle2.isOn = true;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Paladin");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Rogue"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = true;
         toggle3.isOn = true;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Rogue");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Hunter"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = true;
         toggle4.isOn = true;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Hunter");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Priest"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = true;
         toggle5.isOn = true;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Priest");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Druid"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = true;
         toggle6.isOn = true;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Druid");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Mage"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = true;
         toggle7.isOn = true;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Mage");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Warlock"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = true;
         toggle8.isOn = true;
         Debug.Log("You are a Warlock");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Classless"))
      {
         toggle1.interactable = true;
         toggle1.isOn = false;
         toggle2.interactable = true;
         toggle2.isOn = false;
         toggle3.interactable = true;
         toggle3.isOn = false;
         toggle4.interactable = true;
         toggle4.isOn = false;
         toggle5.interactable = true;
         toggle5.isOn = false;
         toggle6.interactable = true;
         toggle6.isOn = false;
         toggle7.interactable = true;
         toggle7.isOn = false;
         toggle8.interactable = true;
         toggle8.isOn = false;
         Debug.Log("You have no class yet");
      }
     
   }

//[HarmonyPatch(typeof(Player), nameof(Player.Load))]
   /*void PlayerLoad()
   {
      if (Player.m_localPlayer.m_customData.ContainsValue("Warrior"))
      {
         toggle1.interactable = true;
         toggle1.isOn = true;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Warrior");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Paladin"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = true;
         toggle2.isOn = true;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Paladin");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Rogue"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = true;
         toggle3.isOn = true;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Rogue");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Hunter"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = true;
         toggle4.isOn = true;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Hunter");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Priest"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = true;
         toggle5.isOn = true;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Priest");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Druid"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = true;
         toggle6.isOn = true;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Druid");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Mage"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = true;
         toggle7.isOn = true;
         toggle8.interactable = false;
         toggle8.isOn = false;
         Debug.Log("You are a Mage");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Warlock"))
      {
         toggle1.interactable = false;
         toggle1.isOn = false;
         toggle2.interactable = false;
         toggle2.isOn = false;
         toggle3.interactable = false;
         toggle3.isOn = false;
         toggle4.interactable = false;
         toggle4.isOn = false;
         toggle5.interactable = false;
         toggle5.isOn = false;
         toggle6.interactable = false;
         toggle6.isOn = false;
         toggle7.interactable = false;
         toggle7.isOn = false;
         toggle8.interactable = true;
         toggle8.isOn = true;
         Debug.Log("You are a Warlock");

      }
      else if (Player.m_localPlayer.m_customData.ContainsValue("Classless"))
      {
         toggle1.interactable = true;
         toggle1.isOn = false;
         toggle2.interactable = true;
         toggle2.isOn = false;
         toggle3.interactable = true;
         toggle3.isOn = false;
         toggle4.interactable = true;
         toggle4.isOn = false;
         toggle5.interactable = true;
         toggle5.isOn = false;
         toggle6.interactable = true;
         toggle6.isOn = false;
         toggle7.interactable = true;
         toggle7.isOn = false;
         toggle8.interactable = true;
         toggle8.isOn = false;
         Debug.Log("You have no class yet");

      }
   }*/
   
   
   public void Toggle1Selected()
   {
      Player.m_localPlayer.m_customData.Remove(_savedSimpleClass);
      Player.m_localPlayer.m_customData.Add(_savedSimpleClass, "Warrior"); 
   }
   public void Toggle2Selected()
   {
      Player.m_localPlayer.m_customData.Remove(_savedSimpleClass);
      Player.m_localPlayer.m_customData.Add(_savedSimpleClass, "Paladin"); 
   }
   public void Toggle3Selected()
   {
      Player.m_localPlayer.m_customData.Remove(_savedSimpleClass);
      Player.m_localPlayer.m_customData.Add(_savedSimpleClass, "Rogue"); 
   }
   public void Toggle4Selected()
   {
      Player.m_localPlayer.m_customData.Remove(_savedSimpleClass);
      Player.m_localPlayer.m_customData.Add(_savedSimpleClass, "Hunter"); 
   }
   public void Toggle5Selected()
   {
      Player.m_localPlayer.m_customData.Remove(_savedSimpleClass);
      Player.m_localPlayer.m_customData.Add(_savedSimpleClass, "Priest"); 
   }
   public void Toggle6Selected()
   {
      Player.m_localPlayer.m_customData.Remove(_savedSimpleClass);
      Player.m_localPlayer.m_customData.Add(_savedSimpleClass, "Druid"); 
   }
   public void Toggle7Selected()
   {
      Player.m_localPlayer.m_customData.Remove(_savedSimpleClass);
      Player.m_localPlayer.m_customData.Add(_savedSimpleClass, "Mage"); 
   }
   public void Toggle8Selected()
   {
      Player.m_localPlayer.m_customData.Remove(_savedSimpleClass);
      Player.m_localPlayer.m_customData.Add(_savedSimpleClass, "Warlock"); 
      
   }
   
  
}
