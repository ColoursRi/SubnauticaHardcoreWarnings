using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace O2AlertsInHardcore
{
    public static class Patcher
    {
        public static IEnumerable<string> TargetDLLs { get; } = new[] {"Assembly-CSharp.dll"};
        public static void Patch()
        {
            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}