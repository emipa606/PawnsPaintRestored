using System.Reflection;
using HarmonyLib;
using Verse;

namespace PawnsPaintRestored;

[StaticConstructorOnStartup]
public static class PawnsPaintRestored
{
    static PawnsPaintRestored()
    {
        new Harmony("Mlie.PawnsPaintRestored").PatchAll(Assembly.GetExecutingAssembly());
    }
}