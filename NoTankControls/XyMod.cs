using FrooxEngine;
using HarmonyLib;
using ResoniteModLoader;
using Renderite.Shared;

namespace NoTankControls
{
	public class NoTankControls : ResoniteMod
	{
		public override string Name => "NoTankControls";
		public override string Author => "zyntaks / Nytra";
		public override string Version => "1.1.1";
		public override string Link => "https://github.com/Nytra/NoTankControls";

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
			Harmony harmony = new Harmony("owo.Nytra.NoTankControls");
			harmony.PatchAll();
		}

		static InteractionHandler userSpaceHandlerLeft = null;
		static InteractionHandler userSpaceHandlerRight = null;

		static bool ShouldBlock(InteractionHandler interactionHandler)
		{
			IAxisActionReceiver axisActionReceiver = interactionHandler?.Laser.CurrentTouchable as IAxisActionReceiver;
			if (axisActionReceiver != null)
			{
				if (((interactionHandler.ActiveTool != null && interactionHandler.ActiveTool.UsesSecondary) ||
					interactionHandler.InputInterface.GetControllerNode(interactionHandler.Side).GetType() == typeof(IndexController)) && !interactionHandler.InputInterface.ScreenActive)
				{
					return true;
				}
			}
			return false;
		}

		[HarmonyPatch(typeof(InteractionHandler))]
		[HarmonyPatch("BeforeInputUpdate")]
		class NoTankControlsPatch
		{
			private static void Postfix(InteractionHandler __instance)
			{
				if (Config.GetValue(MOD_ENABLED))
				{
					if (Config.GetValue(INSPECTOR_SCROLL_COMPATIBILITY))
					{
						if (__instance.World == Engine.Current.WorldManager.FocusedWorld)
						{
							if (ShouldBlock(__instance)
								|| (__instance.Side == Chirality.Left && ShouldBlock(userSpaceHandlerLeft))
								|| (__instance.Side == Chirality.Right && ShouldBlock(userSpaceHandlerRight)))
							{
								__instance.Inputs.Axis.RegisterBlocks = true;
								return;
							}
						}
						else if (__instance.World == Userspace.UserspaceWorld)
						{
							if (__instance.Side == Chirality.Left)
							{
								if (userSpaceHandlerLeft.FilterWorldElement() == null)
								{
									userSpaceHandlerLeft = __instance;
								}
							}
							else
							{
								if (userSpaceHandlerRight.FilterWorldElement() == null)
								{
									userSpaceHandlerRight = __instance;
								}
							}
						}
					}
					__instance.Inputs.Axis.RegisterBlocks = false;
				}
			}
		}
	}
}