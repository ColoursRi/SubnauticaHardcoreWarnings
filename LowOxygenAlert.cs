using HarmonyLib;

namespace O2AlertsInHardcore
{
    [HarmonyPatch(typeof(LowOxygenAlert))]
    [HarmonyPatch("Update")]
    internal class LowOxygenAlert_Update_Patch
	{
		[HarmonyPrefix]
        public static bool Prefix(LowOxygenAlert __instance)
        {
	        __instance.secondsMonitor.Update(__instance.player.GetOxygenAvailable());
	        float oxygenCapacity = __instance.player.GetOxygenCapacity();
	        if (Utils.NearlyEqual(oxygenCapacity, __instance.lastOxygenCapacity, 1E-45f) || oxygenCapacity < __instance.lastOxygenCapacity)
	        {
		        for (int i = __instance.alertList.Count - 1; i >= 0; i--)
		        {
			        LowOxygenAlert.Alert alert = __instance.alertList[i];
			        if (oxygenCapacity >= alert.minO2Capacity && __instance.secondsMonitor.JustDroppedBelow((float)alert.oxygenTriggerSeconds) && Ocean.GetDepthOf(Utils.GetLocalPlayer()) > alert.minDepth && (__instance.player.IsSwimming() || (__instance.player.GetMode() == Player.Mode.LockedPiloting && !__instance.player.IsInsidePoweredVehicle()) || (__instance.player.IsInSub() && !__instance.player.CanBreathe())))
			        {
				        Subtitles.Add(alert.notification.text, null);
				        alert.soundSFX.Play();
				        break;
			        }
		        }
	        }
	        __instance.lastOxygenCapacity = oxygenCapacity;
	        return true;
        }

    }
}
