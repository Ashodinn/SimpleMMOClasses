using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SkillManager;
using ServerSync;
using UnityEngine;
using System.Linq;
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

            SimpleMMOClassesPanel = Object.Instantiate(globalprefabVariable);
            Object.DontDestroyOnLoad(SimpleMMOClassesPanel);

            Skill
                    warrior = new("warrior", "warrior-icon.png");
            warrior.Description.English("Reduces damage taken by 0.2% per level.");
            warrior.Name.German("Hartnäckigkeit"); // Use this to localize values for the name
            warrior.Description.German(
                "Reduziert erlittenen Schaden um 0,2% pro Stufe."); // You can do the same for the description
            warrior.Configurable = true;

            _panelToggleKey = config<KeyCode>("1 - General", "Panel Toggle Key", KeyCode.O,
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

        void Update()
        {
            if (Input.GetKeyDown(_panelToggleKey.Value)) // Optionally check for Player.TakeInput here so you don't accidentally open the UI
            {
                // Assuming the 'globalprefabVariable' is the panel you want to toggle
                if (globalprefabVariable != null)
                {
                    // Toggle active state of the panel
                    globalprefabVariable.SetActive(!globalprefabVariable.activeSelf);
                }
            }
        }


        #region ConfigOptions

        private static ConfigEntry<Toggle> _serverConfigLocked = null!;
        public static GameObject? globalprefabVariable;


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