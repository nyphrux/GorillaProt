using BepInEx;
using GorillaProt.Core;
using System;
using UnityEngine;

/*
 * This mod is NOT for cheating, its built for protection.
 * There are NO cheats or hacks in the mod.
 * 
 * Enjoy :3 -nyph
*/

namespace GorillaProt
{
    [System.ComponentModel.Description(Description)]
    [BepInPlugin(GUID, Name, Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "me.nyphrux.gorillaprot";
        public const string Name = "GorillaProt";
        public const string Description = "Protects your game/user from bad stuff.";
        public const string Version = "1.0.0";
        public const string Developer = "Silly Nyph";

        private void Awake()
        {
            UnityEngine.Debug.Log(@"Loading...
========+-+========-- GorillaProt --========+-+========
Protects you from:
        - Stump Kicks
        - Crashes   - Ban Crashes
Also:
        - Clears cache automatically

Developed by: " + Developer + @"
Version: " + Version + @"
=======================================================
");
            // Patch & enable protections
            AntiCrashPatches.enabled = true; // <- Anti crash
            AntiBanCrash.enabled = true; // <- Anti crash when you get banned
            GroupPatch.enabled = true; // <- Anti stump kick
        }


        private static float lastCacheClearedTime;

        private void Update()
        {
            if (Time.time > lastCacheClearedTime)
            {
                lastCacheClearedTime = Time.time + 60f;
                GC.Collect();
            }
        }
    }
}