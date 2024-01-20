using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace JointCompanyLogo.Patches
{
    [HarmonyPatch(typeof(MenuManager), "Awake")]
    public static class LogoPatch
    {
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(MenuManager __instance)
        {
            try
            {
                // Main Menu
                GameObject parent = __instance.transform.parent.gameObject;
                GameObject menuContainer = parent.transform.Find("MenuContainer").gameObject;
                if (menuContainer.scene.name != "MainMenu") return;
                GameObject logo = menuContainer.transform.Find("MainButtons").Find("HeaderImage").gameObject;

                Image image = logo.GetComponent<Image>();
                image.sprite = Sprite.Create(Plugin.mainLogo, new Rect(0, 0, Plugin.mainLogo.width, Plugin.mainLogo.height), new Vector2(0.5f, 0.5f));

                // Loading screen
                GameObject loadingScreenImage = menuContainer.transform.Find("LoadingScreen").Find("Image").gameObject;
                Image loadingImage = loadingScreenImage.GetComponent<Image>();
                loadingImage.sprite = Sprite.Create(Plugin.mainLogo, new Rect(0, 0, Plugin.mainLogo.width, Plugin.mainLogo.height), new Vector2(0.5f, 0.5f));
            }
            catch (Exception e)
            {
                Plugin.StaticLogger.LogError(e.Message);
            }
        }

        private static void Traverse(GameObject obj)
        {
            Plugin.StaticLogger.LogInfo(obj.name);
            foreach (Transform child in obj.transform)
            {
                Traverse(child.gameObject);
            }
            Plugin.StaticLogger.LogInfo("-----------");
        }
    }
}
