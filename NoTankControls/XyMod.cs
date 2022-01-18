using System;
using HarmonyLib;
using NeosModLoader;
using FrooxEngine;
using FrooxEngine.UIX;

namespace NoTankControls
{
    public class XyMod : NeosMod
    {
        public override string Name => "NoTankControls";
        public override string Author => "xyla";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/furrz/NoTankControls";

        private static bool _first_trigger = false;

        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("U-xyla.XyMod");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(CommonTool))]
        [HarmonyPatch("BeforeInputUpdate")]
        class Patch
        {
            private static void Postfix(CommonTool __instance, ref CommonToolInputs ____inputs)
            {
                ____inputs.Axis.RegisterBlocks = false;
            }
        }
    }
}