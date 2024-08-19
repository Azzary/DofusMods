using BepInEx;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

namespace DofusMods
{
    [BepInPlugin(GUID, PluginName, PluginVersion)]
    public class Plugin : BasePlugin
    {
        internal const string GUID = "com.yourname.DofusMobsPlugin";
        internal const string PluginName = "Dofus Mods Plugin";
        internal const string PluginVersion = "1.0";
        public GameObject BepInExUtility;

        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<SimpleMenu>();
            ClassInjector.RegisterTypeInIl2Cpp<SpeedBoostComponent>();
            ClassInjector.RegisterTypeInIl2Cpp<TacticalManager>();

            BepInExUtility = GameObject.Find("BepInExUtility");
            if (BepInExUtility == null)
            {
                BepInExUtility = new GameObject("BepInExUtility");
                GameObject.DontDestroyOnLoad(BepInExUtility);
                BepInExUtility.hideFlags = HideFlags.HideAndDontSave;
            }

            BepInExUtility.AddComponent<SimpleMenu>();
            BepInExUtility.AddComponent<SpeedBoostComponent>();
            BepInExUtility.AddComponent<TacticalManager>();

            Log.LogInfo($"Plugin {PluginName} is loaded!");
        }
    }
}