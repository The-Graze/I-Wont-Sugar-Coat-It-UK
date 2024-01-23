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
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }
    }
    [HarmonyPatch(typeof(HUDOptions))]
    [HarmonyPatch("Start", MethodType.Normal)]
    internal class HudPatch
    {
        private static void Postfix(HUDOptions __instance)
        {
            if (__instance.GetComponent<DAAYYM>() == null)
            {
                __instance.gameObject.AddComponent<DAAYYM>();
            }
        }
    }
    public static class cAssets
    {
        static Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("IWontSugarCoatIt.Assets.f");
        static AssetBundle bundle = AssetBundle.LoadFromStream(str);
        static GameObject loader = bundle.LoadAsset<GameObject>("f");
        public static GameObject Image = loader.transform.GetChild(0).GetChild(0).gameObject;
        public static GameObject Aud = loader.transform.GetChild(1).gameObject;
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
                Parryflash = transform.Find("ParryFlash").gameObject;
                Image = Instantiate(cAssets.Image);
                Image.transform.SetParent(gameObject.transform);
                Image.transform.localPosition = Vector3.zero;
                Image.transform.localScale = new Vector3(4, 4, 4);
                Aud = Instantiate(cAssets.Aud);
                Aud.SetActive(true);
                Image.transform.SetParent(Parryflash.transform);
                Aud.transform.SetParent(gameObject.transform);
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
