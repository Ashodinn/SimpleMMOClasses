using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SkillManager;
using ServerSync;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;
using YamlDotNet.Core.Tokens;
using Object = UnityEngine.Object;


namespace SimpleMMOClasses
{
    /* Please note, by default this template uses the class and not the DLL load for Skill Manager.
     Follow the instructions in the README.md file and the ILRepack.targets file to switch over to using the DLL if you wish. */
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class SimpleMMOClasses : BaseUnityPlugin
    {
        internal const string ModName = "SimpleMMOClasses";
        internal const string ModVersion = "1.0.0";
        internal const string Author = "GGrNoob";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;
        internal static string ConnectionError = "";

        public static GameObject SimpleMMOClassesPanel;
        
        public static readonly ManualLogSource SimpleMMOClassesLogger =
            BepInEx.Logging.Logger.CreateLogSource(ModName);

        private static ConfigEntry<KeyCode> _panelToggleKey;

        private static readonly ConfigSync ConfigSync = new(ModGUID)
        { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };

        public enum Toggle
        {
            On = 1,
            Off = 0
        }



        public void Awake()
        {
            // Uncomment the line below to use the LocalizationManager for localizing your mod.
            //Localizer.Load(); // Use this to initialize the LocalizationManager (for more information on LocalizationManager, see the LocalizationManager documentation https://github.com/blaxxun-boop/LocalizationManager#example-project).
            _serverConfigLocked = config("1 - General", "Lock Configuration", Toggle.On,
            "If on, the configuration is locked and can be changed by server admins only.");
            _ = ConfigSync.AddLockingConfigEntry(_serverConfigLocked);
            
                Skill
                    warrior = new("Warrior", "warrior-icon.png");
                warrior.Description.English("Reduces damage taken by 0.2% per level.");
                warrior.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
                warrior.Description.German(
                    "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
                warrior.Configurable = true;
            
                Skill
                    paladin = new("Paladin", "paladin-icon.png");
                paladin.Description.English("Reduces damage taken by 0.2% per level.");
                paladin.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
                paladin.Description.German(
                    "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
                paladin.Configurable = true;
                
                Skill
                    rogue = new("Rogue", "rogue-icon.png");
                rogue.Description.English("Reduces damage taken by 0.2% per level.");
                rogue.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
                rogue.Description.German(
                    "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
                rogue.Configurable = true;
            
                Skill
                    hunter = new("Hunter", "hunter-icon.png");
                hunter.Description.English("Reduces damage taken by 0.2% per level.");
                hunter.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
                hunter.Description.German(
                    "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
                hunter.Configurable = true;
           
                Skill
                    priest = new("Priest", "priest-icon.png");
                priest.Description.English("Reduces damage taken by 0.2% per level.");
                priest.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
                priest.Description.German(
                    "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
                priest.Configurable = true;
            
                Skill
                    druid = new("Druid", "druid-icon.png");
                druid.Description.English("Reduces damage taken by 0.2% per level.");
                druid.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
                druid.Description.German(
                    "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
                druid.Configurable = true;
            
                Skill
                    mage = new("Mage", "mage-icon.png");
                mage.Description.English("Reduces damage taken by 0.2% per level.");
                mage.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
                mage.Description.German(
                    "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
                mage.Configurable = true;
            
                Skill
                    warlock = new("Warlock", "warlock-icon.png");
                warlock.Description.English("Reduces damage taken by 0.2% per level.");
                warlock.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
                warlock.Description.German(
                    "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
                warlock.Configurable = true;
                 
            

            LoadAssets();
            
            _panelToggleKey = config<KeyCode>("1 - General", "Panel Toggle Key", KeyCode.Tab,
                "Key used to toggle the panel on and off.");

            Assembly assembly = Assembly.GetExecutingAssembly();
            Harmony harmony = new(ModGUID);
            harmony.PatchAll(assembly);
            SetupWatcher();
            
            
        }
        
        private static AssetBundle GetAssetBundleFromResources(string filename)
        {
            var execAssembly = Assembly.GetExecutingAssembly();
            var resourceName = execAssembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(filename));

            using (var stream = execAssembly.GetManifestResourceStream(resourceName))
            {
                return AssetBundle.LoadFromStream(stream);
            }
        }


        public static void LoadAssets()
        {
            var assetBundle = GetAssetBundleFromResources("simplemmoclassespanel");
            globalprefabVariable = assetBundle.LoadAsset<GameObject>("SimpleMMOClassesPanel");
            assetBundle?.Unload(false);

            if (globalprefabVariable != null)
            {

                SimpleMMOClassesPanel = Object.Instantiate(globalprefabVariable);
                UnityEngine.Object.DontDestroyOnLoad(SimpleMMOClassesPanel);
            }
        }
   
        
        // Blocking Skills by Class system
        [HarmonyPatch(typeof(Skills.Skill), nameof(Skills.Skill.Raise))]
        private static class SkillCapping
        {
            private static bool Prefix(Skills.Skill __instance)
            {
                if (Player.m_localPlayer.m_customData.ContainsValue("Warrior")) //Warrior
                {
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "ElementalMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "BloodMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Sneak"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Crossbows"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Bows"))
                    {
                        return __instance.m_level < 20f;
                    }
                }
                
                else if (Player.m_localPlayer.m_customData.ContainsValue("Paladin")) //Paladin
                {
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "ElementalMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "BloodMagic"))
                    {
                        return __instance.m_level < 50f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Unarmed"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Sneak"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Bows"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Crossbows"))
                    {
                        return __instance.m_level < 20f;
                    }
                } 
                
                else if (Player.m_localPlayer.m_customData.ContainsValue("Rogue")) //Rogue
                {
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Blocking"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Polearms"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "BloodMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "ElementalMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Bows"))
                    {
                        return __instance.m_level < 50f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Crossbows"))
                    {
                        return __instance.m_level < 50f;
                    }
                }
                
                else if (Player.m_localPlayer.m_customData.ContainsValue("Hunter"))
                {
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Blocking"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Polearms"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "BloodMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "ElementalMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Clubs"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Swords"))
                    {
                        return __instance.m_level < 30f;
                    }
                }
                
                else if (Player.m_localPlayer.m_customData.ContainsValue("Priest")) //Priest
                {
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Unarmed"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Axes"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Blocking"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Bows"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Swords"))
                    {
                        return __instance.m_level < 30f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "ElementalMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                }
                
                else if (Player.m_localPlayer.m_customData.ContainsValue("Druid")) //Druid
                {
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Knives"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Blocking"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Swords"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "BloodMagic"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Polearms"))
                    {
                        return __instance.m_level < 20f;
                    }
                }
                
                else if (Player.m_localPlayer.m_customData.ContainsValue("Mage")) //Mage
                {
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Unarmed"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Axes"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Blocking"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Bows"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Crossbows"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Swords"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "BloodMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                }
                
                else if (Player.m_localPlayer.m_customData.ContainsValue("Warlock")) //Warlock
                {
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Axes"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Blocking"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Bows"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Crossbows"))
                    {
                        return __instance.m_level < 20f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "Swords"))
                    {
                        return __instance.m_level < 0f;
                    }
                    if (string.Equals(__instance.m_info.m_skill.ToString(), "ElementalMagic"))
                    {
                        return __instance.m_level < 0f;
                    }
                }


                return true;
            }
        }
        
        //LEVELING SYSTEM
        [HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
        static class WarriorLeveling
        {
            [UsedImplicitly]
            private static void Prefix(Player __instance, Skills.SkillType skill, float level)
            {

                if (Player.m_localPlayer.m_customData.ContainsValue("Warrior"))
                {
                    if (skill is Skills.SkillType.Unarmed || skill is Skills.SkillType.Clubs || skill is Skills.SkillType.Blocking || skill is Skills.SkillType.Swords)
                    {
                        __instance.RaiseSkill(Skill.fromName("Warrior"), 0.25f);
                    }

                }
            }
        }
        
        [HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
        static class PaladinLeveling
        {
            [UsedImplicitly]
            private static void Prefix(Player __instance, Skills.SkillType skill, float level)
            {

                if (Player.m_localPlayer.m_customData.ContainsValue("Paladin"))
                {
                    if (skill is Skills.SkillType.Unarmed || skill is Skills.SkillType.Clubs || skill is Skills.SkillType.Blocking || skill is Skills.SkillType.Swords)
                    {
                        __instance.RaiseSkill(Skill.fromName("Paladin"), 0.25f);
                    }
                }
            }
        }
        
        [HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
        static class RogueLeveling
        {
            [UsedImplicitly]
            private static void Prefix(Player __instance, Skills.SkillType skill, float level)
            {
                if (Player.m_localPlayer.m_customData.ContainsValue("Rogue"))
                {
                    if (skill is Skills.SkillType.Unarmed || skill is Skills.SkillType.Knives || skill is Skills.SkillType.Bows || skill is Skills.SkillType.Spears)
                    {
                        __instance.RaiseSkill(Skill.fromName("Hunter"), 0.25f);
                    }
                }
                
            }
        }
        
        [HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
        static class HunterLeveling
        {
            [UsedImplicitly]
            private static void Prefix(Player __instance, Skills.SkillType skill, float level)
            {
                if (Player.m_localPlayer.m_customData.ContainsValue("Hunter"))
                {
                    if (skill is Skills.SkillType.Bows || skill is Skills.SkillType.Knives || skill is Skills.SkillType.Spears || skill is Skills.SkillType.Swords)
                    {
                        __instance.RaiseSkill(Skill.fromName("Hunter"), 0.25f);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
        static class PriestLeveling
        {
            [UsedImplicitly]
            private static void Prefix(Player __instance, Skills.SkillType skill, float level)
            {
                if (Player.m_localPlayer.m_customData.ContainsValue("Priest"))
                {
                    if (skill is Skills.SkillType.BloodMagic || skill is Skills.SkillType.Clubs || skill is Skills.SkillType.Spears || skill is Skills.SkillType.Polearms)
                        
                    {
                        __instance.RaiseSkill(Skill.fromName("Priest"), 0.25f);
                    }
                }
                
            }
        }
        
        [HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
        static class DruidLeveling
        {
            [UsedImplicitly]
            private static void Prefix(Player __instance, Skills.SkillType skill, float level)
            {
                if (Player.m_localPlayer.m_customData.ContainsValue("Druid"))
                {
                    if (skill is Skills.SkillType.ElementalMagic || skill is Skills.SkillType.Spears || skill is Skills.SkillType.Bows || skill is Skills.SkillType.Polearms)
                    {
                        __instance.RaiseSkill(Skill.fromName("Druid"), 0.25f);
                    }
                }
                
            }
        }

        
        [HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
        static class MageLeveling
        {
            [UsedImplicitly]
            private static void Prefix(Player __instance, Skills.SkillType skill, float level)
            {
                if (Player.m_localPlayer.m_customData.ContainsValue("Mage"))
                {
                    if (skill is Skills.SkillType.ElementalMagic || skill is Skills.SkillType.Clubs || skill is Skills.SkillType.Spears || skill is Skills.SkillType.Polearms)
                    {
                        __instance.RaiseSkill(Skill.fromName("Mage"), 0.25f);
                    }
                }
            }
        }
        
        [HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
        static class WarlockLeveling
        {
            [UsedImplicitly]
            private static void Prefix(Player __instance, Skills.SkillType skill, float level)
            {
                if (Player.m_localPlayer.m_customData.ContainsValue("Warlock"))
                {
                    if (skill is Skills.SkillType.ElementalMagic || skill is Skills.SkillType.Spears || skill is Skills.SkillType.Bows || skill is Skills.SkillType.Polearms)
                    {
                        __instance.RaiseSkill(Skill.fromName("Warlock"), 0.25f);
                    }
                }
            }
        }

   
     
        

        private void OnDestroy()
        {
            Config.Save();
        }

        private void SetupWatcher()
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, ConfigFileName);
            watcher.Changed += ReadConfigValues;
            watcher.Created += ReadConfigValues;
            watcher.Renamed += ReadConfigValues;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadConfigValues(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(ConfigFileFullPath)) return;
            try
            {
                SimpleMMOClassesLogger.LogDebug("ReadConfigValues called");
                Config.Reload();
            }
            catch
            {
                SimpleMMOClassesLogger.LogError($"There was an issue loading your {ConfigFileName}");
                SimpleMMOClassesLogger.LogError("Please check your config entries for spelling and format!");
            }
        }

       #region ConfigOptions

        private static ConfigEntry<Toggle> _serverConfigLocked = null!;
        public static GameObject globalprefabVariable;


        private ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description,
            bool synchronizedSetting = true)
        {
            ConfigDescription extendedDescription =
                new(
                    description.Description +
                    (synchronizedSetting ? " [Synced with Server]" : " [Not Synced with Server]"),
                    description.AcceptableValues, description.Tags);
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, extendedDescription);
            //var configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = ConfigSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, string description,
            bool synchronizedSetting = true)
        {
            return config(group, name, value, new ConfigDescription(description), synchronizedSetting);
        }

        private class ConfigurationManagerAttributes
        {
            public bool? Browsable = false;
        }

        #endregion
    }
}