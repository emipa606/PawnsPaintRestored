using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace PawnsPaintRestored;

[HarmonyPatch(typeof(BeautyUtility), "CellBeauty")]
public static class BeautyUtility_CellBeauty
{
    public static void Postfix(IntVec3 c, Map map, ref List<Thing> countedThings, ref float __result)
    {
        var possiblePaintnings = map.listerThings.ThingsOfDef(ThingDefOf.PaintingOnWall);

        if (!possiblePaintnings.Any())
        {
            return;
        }

        foreach (var possiblePainting in possiblePaintnings)
        {
            if (c != possiblePainting.Position + possiblePainting.Rotation.Opposite.FacingCell)
            {
                continue;
            }

            if (countedThings.Contains(possiblePainting))
            {
                continue;
            }

            __result += possiblePainting.GetStatValue(StatDefOf.Beauty);
            countedThings.Add(possiblePainting);
        }
    }
}