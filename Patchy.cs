using HarmonyLib;
using System.Reflection;
using QModManager.API;
using QModManager.API.ModLoading;

namespace HardcoreWarnings
{
    [QModCore]
    public static class Patchy
    {
        [QModPatch]
        public static void Patch()
        {
            var harmony = new Harmony("com.ri.subnautica.HardcoreWarnings");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

    }
}
