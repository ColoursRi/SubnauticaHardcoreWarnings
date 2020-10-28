using HarmonyLib;
using UnityEngine;



namespace HardcoreWarnings
{
	[HarmonyPatch(typeof(HintSwimToSurface))]
	[HarmonyPatch("ShouldShowWarning")]

	internal class HintSwimToSurface_ShouldShowWarning_Patch
	{
		[HarmonyPrefix]
		public static bool Prefix(HintSwimToSurface __instance, ref bool __result, int ___numShown)
		{
			Player main = Player.main;
			if (main == null)
			{
				__result = false;
				return false;
			}
			if (___numShown >= __instance.maxNumToShow)
			{
				__result = false;
				return false;
			}
			Ocean main2 = Ocean.main;
			if (main2 == null)
			{
				__result = false;
				return false;
			}
			float oxygenAvailable = main.GetOxygenAvailable();
			float depthOf = main2.GetDepthOf(main.gameObject);
			Vehicle vehicle = main.GetVehicle();
			__result = (oxygenAvailable < __instance.oxygenThreshold && depthOf > 0f && main.IsSwimming()) || (vehicle != null && !vehicle.IsPowered());
			return false;
		}

	}
}
