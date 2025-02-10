using HarmonyLib;

namespace O2AlertsInHardcore
{
	[HarmonyPatch(typeof(HintSwimToSurface))]
	[HarmonyPatch("ShouldShowWarning")]
	internal class HintSwimToSurface_ShouldShowWarning_Patch
	{
		[HarmonyPrefix]
		public static bool Prefix(HintSwimToSurface __instance)
		{
			Player main = Player.main;
			if (main == null)
			{
				return false;
			}
			if (__instance.numShown >= __instance.maxNumToShow)
			{
				return false;
			}
			float oxygenAvailable = main.GetOxygenAvailable();
			float depthOf = Ocean.GetDepthOf(main.gameObject);
			Vehicle vehicle = main.GetVehicle();
			return oxygenAvailable < __instance.oxygenThreshold && depthOf > 0f && main.IsSwimming() && vehicle == null;
		}

	}
}
