using RimWorld;
using Verse;

namespace PawnsPaintRestored;

[DefOf]
public static class ThingDefOf
{
    public static ThingDef PaintingOnWall;

    static ThingDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
    }
}