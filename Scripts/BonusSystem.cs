using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace SimpleMMOClasses;

public partial class ClassBonuses
{
    ///Base HP BONSUES by CLASS
    
    //Works but used a different method, keeping this as backup for consideration.
    /*[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
    public static class WarriorAddHpBonus
    {
        public static void Postfix(ref float hp)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Warrior"))
            {
                Debug.Log("Warrior Health Bonus Set!");
                hp += +50;
            }
        }
    }*/
    
      
    [HarmonyPatch(typeof(Player),  "SetMaxHealth")]
    public static class WarriorHealthBonus
    {
        
        private static void Prefix(Player __instance, ref float health)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Warrior"))
            {
                health += 50;
            }
            
        }
    }
    
    [HarmonyPatch(typeof(Player),  "SetMaxHealth")]
    public static class PaladinHealthBonus
    {
        
        private static void Prefix(Player __instance, ref float health)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Paladin"))
            {
                health += 25;
            }
            
        }
    }
    
    [HarmonyPatch(typeof(Player),  "SetMaxHealth")]
    public static class RogueHealthBonus
    {
        
        private static void Prefix(Player __instance, ref float health)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Rogue"))
            {
                health += 25;
            }
            
        }
    }
    
    [HarmonyPatch(typeof(Player),  "SetMaxHealth")]
    public static class HunterHealthBonus
    {
        
        private static void Prefix(Player __instance, ref float health)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Hunter"))
            {
                health += 25;
            }
            
        }
    }    
    
    [HarmonyPatch(typeof(Player),  "SetMaxHealth")]
    public static class DruidHealthBonus
    {
        
        private static void Prefix(Player __instance, ref float health)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Druid"))
            {
                health += 25;
            }
            
        }
    }
    
    ///Base Stamina BONSUES by CLASS
    
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxStamina))]
    public static class WarriorStaminaBonus
    {
        private static void Prefix(Player __instance, ref float stamina)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Warrior"))
            {
                stamina += 25;
            }
        }
    }
    
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxStamina))]
    public static class RogueStaminaBonus
    {
        private static void Prefix(Player __instance, ref float stamina)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Rogue"))
            {
                stamina += 50;
            }
        }
    }
        
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxStamina))]
    public static class HunterStaminaBonus
    {
        private static void Prefix(Player __instance, ref float stamina)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Hunter"))
            {
                stamina += 25;
            }
        }
    }
    
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxStamina))]
    public static class DruidStaminaBonus
    {
        private static void Prefix(Player __instance, ref float stamina)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Druid"))
            {
                stamina += 25;
            }
        }
    }
    
    ///Base Eitr BONSUES by CLASS 
    
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxEitr))]
    public static class PaladinEitrBonus
    {
        private static void Prefix(Player __instance, ref float eitr)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Paladin"))
            {
                eitr += 25;
            }
        }
    }
    
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxEitr))]
    public static class PriestEitrBonus
    {
        private static void Prefix(Player __instance, ref float eitr)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Priest"))
            {
                eitr += 75;
            }
        }
    }
    
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxEitr))]
    public static class DruidEitrBonus
    {
        private static void Prefix(Player __instance, ref float eitr)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Druid"))
            {
                eitr += 25;
            }
        }
    }
    
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxEitr))]
    public static class MageEitrBonus
    {
        private static void Prefix(Player __instance, ref float eitr)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Mage"))
            {
                eitr += 50;
            }
        }
    }
    
        
    [HarmonyPatch(typeof(Player),  nameof(Player.SetMaxEitr))]
    public static class WarlockEitrBonus
    {
        private static void Prefix(Player __instance, ref float eitr)
        {
            if (Player.m_localPlayer.m_customData.ContainsValue("Warlock"))
            {
                eitr += 50;
            }
        }
    }
    
    

}
