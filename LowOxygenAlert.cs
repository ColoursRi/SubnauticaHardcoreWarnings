using HarmonyLib;
using UnityEngine;



namespace HardcoreWarnings
{
    [HarmonyPatch(typeof(LowOxygenAlert))]
    [HarmonyPatch("Update")]

    internal class LowOxygenAlert_Update_Patch
	{
		[HarmonyPrefix]
        public static bool Prefix(LowOxygenAlert __instance, ref Utils.ScalarMonitor ___secondsMonitor, Player ___player, ref float ___lastOxygenCapacity)
        {
			___secondsMonitor.Update(___player.GetOxygenAvailable());
			float oxygenCapacity = ___player.GetOxygenCapacity();
			if (Utils.NearlyEqual(oxygenCapacity, ___lastOxygenCapacity, 1.401298E-45f) || oxygenCapacity < ___lastOxygenCapacity)
			{
				for (int i = __instance.alertList.Count - 1; i >= 0; i--)
				{
					LowOxygenAlert.Alert alert = __instance.alertList[i];
					if (oxygenCapacity >= alert.minO2Capacity && ___secondsMonitor.JustDroppedBelow((float)alert.oxygenTriggerSeconds) && Ocean.GetDepthOf(Utils.GetLocalPlayer()) > alert.minDepth && (___player.IsSwimming() || (___player.GetMode() == Player.Mode.LockedPiloting && !___player.GetVehicle().IsPowered()) || (___player.IsInSub() && !___player.CanBreathe())))
					{
						Subtitles.Add(alert.notification.text);
						alert.soundSFX.Play();
						break;
					}
				}
			}
			___lastOxygenCapacity = oxygenCapacity;
			return false;
		}

    }
}
