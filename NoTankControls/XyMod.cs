using FrooxEngine;
using HarmonyLib;
using ResoniteModLoader;

namespace NoTankControls
{
	public class XyMod : ResoniteMod
	{
		public override string Name => "NoTankControls";
		public override string Author => "zyntaks";
		public override string Version => "1.0.0";
		public override string Link => "https://github.com/furrz/NoTankControls";

		public static ModConfiguration Config;

		[AutoRegisterConfigKey]
		private static ModConfigurationKey<bool> MOD_ENABLED = new ModConfigurationKey<bool>("MOD_ENABLED", "Mod Enabled:", () => true);
		[AutoRegisterConfigKey]
		private static ModConfigurationKey<bool> INSPECTOR_SCROLL_COMPATIBILITY = new ModConfigurationKey<bool>("INSPECTOR_SCROLL_COMPATIBILITY", "Use inspector scroll compatibility:", () => false);

		public override void OnEngineInit()
		{
			Config = GetConfiguration();
			SetupMod();
		}

		static void SetupMod()
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
				if (Config.GetValue(MOD_ENABLED))
				{
					if (Config.GetValue(INSPECTOR_SCROLL_COMPATIBILITY))
					{
						IAxisActionReceiver axisActionReceiver = __instance.Laser.CurrentTouchable as IAxisActionReceiver;
						if (axisActionReceiver != null)
						{
							if (((__instance.ActiveTool != null && __instance.ActiveTool.UsesSecondary) ||
								__instance.InputInterface.GetControllerNode(__instance.Side).GetType() == typeof(IndexController)) && !__instance.InputInterface.ScreenActive)
							{
								____inputs.Axis.RegisterBlocks = true;
								return;
							}
						}
					}
					____inputs.Axis.RegisterBlocks = false;
				}
			}
		}
	}
}