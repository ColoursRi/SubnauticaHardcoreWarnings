using HarmonyLib;
using System.Reflection;

namespace HardcoreWarnings
{
    public class Patchy
    {   
        public static void Patch()
        {
            var harmony = new Harmony("com.ri.subnautica.HardcoreWarnings");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

    }
}
