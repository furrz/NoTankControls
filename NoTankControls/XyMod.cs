using System;
using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine;
using FrooxEngine.UIX;

namespace NoTankControls
{
    public class XyMod : ResoniteMod
    {
        public override string Name => "NoTankControls";
        public override string Author => "zyntaks";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/furrz/NoTankControls";

        private static bool _first_trigger = false;
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("U-xyla.XyMod");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(InteractionHandler))]
        [HarmonyPatch("BeforeInputUpdate")]
        class Patch
        {
            private static void Postfix(InteractionHandler __instance, ref InteractionHandlerInputs ____inputs)
            {
                ____inputs.Axis.RegisterBlocks = false;
            }
        }
    }
}