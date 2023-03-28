using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ULTRAKIT;

namespace IWontSugarCoatIt
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public GameObject Image;
        public GameObject Aud;
        GameObject loader;
        List<HUDOptions> hud = new List<HUDOptions>();
        void Awake()
        {
            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("IWontSugarCoatIt.Assets.f");
            AssetBundle bundle = AssetBundle.LoadFromStream(str);
            loader = bundle.LoadAsset<GameObject>("f");
            Image = loader.transform.GetChild(0).GetChild(0).gameObject;
            Aud = loader.transform.GetChild(1).gameObject;
        }
        void Update()
        {
            hud = GameObject.FindObjectsOfType<HUDOptions>().ToList();
            foreach (HUDOptions h in hud) 
            {
                if (h.gameObject.GetComponent<DAAYYM>() == null)
                {
                    h.gameObject.AddComponent<DAAYYM>();
                }
            }
        }
    }
    public class DAAYYM : MonoBehaviour
    {
        Plugin p = GameObject.FindObjectOfType<Plugin>();
        GameObject Parryflash;
        GameObject Image;
        GameObject Aud;
        void Awake()
        {
            if (Parryflash == null)
            {
                Parryflash = gameObject.transform.GetChild(8).gameObject;
                Image = Instantiate(p.Image);
                Image.transform.SetParent(gameObject.transform);
                Image.transform.localPosition = Vector3.zero;
                Aud = Instantiate(p.Aud);
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
