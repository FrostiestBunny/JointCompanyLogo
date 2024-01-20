using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using JointCompanyLogo.Utils;
using UnityEngine;
using System.Reflection;

namespace JointCompanyLogo
{
    public static class PluginInformation
    {
        public const string PLUGIN_NAME = "JointCompanyLogo";
        public const string PLUGIN_VERSION = "1.0.0";
        public const string PLUGIN_GUID = "me.frost.bunny.jointcompanylogo";
    }

    [BepInPlugin(PluginInformation.PLUGIN_GUID, PluginInformation.PLUGIN_NAME, PluginInformation.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Texture2D mainLogo;
        public static ManualLogSource StaticLogger;

        private Harmony harmony;

        private void Awake()
        {
            StaticLogger = Logger;

            // Plugin startup logic
            harmony = new Harmony(PluginInformation.PLUGIN_GUID);
            harmony.PatchAll();

            AssetBundle bundle = BundleUtilities.LoadBundleFromInternalAssembly("jointcompany.assets", Assembly.GetExecutingAssembly());
            foreach (var name in bundle.GetAllAssetNames())
            {
                Logger.LogInfo($"Loading asset: {name}");
            }

            mainLogo = bundle.LoadPersistentAsset<Texture2D>("assets/jointlogo.png");
            bundle.Unload(false);

            Logger.LogInfo($"Resolution {mainLogo.width} x {mainLogo.height}");

            Logger.LogInfo($"Plugin {PluginInformation.PLUGIN_NAME} is loaded!");
        }

    }
}
