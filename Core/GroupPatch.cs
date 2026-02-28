using HarmonyLib;

/*
 * This mod is NOT for cheating, its built for protection.
 * There are NO cheats or hacks in the mod.
 * 
 * Enjoy :3 -nyph
*/

namespace GorillaProt.Core
{
    [HarmonyPatch(typeof(RoomSystem), nameof(RoomSystem.SearchForNearby))]
    public class GroupPatch
    {
        public static bool enabled;
        public static bool Prefix() =>
            !enabled;
    }
}
