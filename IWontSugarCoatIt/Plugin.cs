using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace IWontSugarCoatIt
{
    [HarmonyPatch]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static GameObject Image;
        public static GameObject Aud;
        GameObject loader;
        List<HUDOptions> hud = new List<HUDOptions>();
        private static Harmony _harmony;

        public void Start()
        {
            _harmony = new Harmony(PluginInfo.GUID);
            _harmony.PatchAll();
        }


        void Awake()
        {
            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("IWontSugarCoatIt.Assets.f");
            AssetBundle bundle = AssetBundle.LoadFromStream(str);
            loader = bundle.LoadAsset<GameObject>("f");
            Image = loader.transform.GetChild(0).GetChild(0).gameObject;
            Aud = loader.transform.GetChild(1).gameObject;
        }


        [HarmonyPatch(typeof(HUDOptions), "Start")]
        [HarmonyPrefix]
        public static void HUDOptions_Start(HUDOptions __instance)
        {
            if (!__instance.gameObject.GetComponent<DAAYYM>()) __instance.gameObject.AddComponent<DAAYYM>();
        }
    }
    public class DAAYYM : MonoBehaviour
    {
        GameObject Parryflash;
        GameObject Image;
        GameObject Aud;
        void Awake()
        {
            if (Parryflash == null)
            {
                Parryflash = gameObject.transform.GetChild(8).gameObject;
                Image = Instantiate(Plugin.Image, Parryflash.transform);
                Image.transform.localScale = new Vector3(4, 4, 4);
                Aud = Instantiate(Plugin.Aud, gameObject.transform);
                Aud.SetActive(true);
            }
        }
        void Update()
        {
            if (Parryflash.activeSelf)
            {
                Aud.GetComponent<AudioSource>().Play();
            }
        }
    }
}
