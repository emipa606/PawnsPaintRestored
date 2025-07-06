using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace PawnsPaintRestored;

[HarmonyPatch(typeof(BeautyUtility), nameof(BeautyUtility.CellBeauty))]
public static class BeautyUtility_CellBeauty
{
    public static void Postfix(IntVec3 c, Map map, ref HashSet<Thing> countedThings, ref float __result)
    {
        var possiblePaintings = map.listerThings.ThingsOfDef(ThingDefOf.PaintingOnWall);

        if (!possiblePaintings.Any())
        {
            return;
        }

        var outdoors = c.GetRoom(map)?.PsychologicallyOutdoors ?? true;
        foreach (var possiblePainting in possiblePaintings)
        {
            if (c != possiblePainting.Position + possiblePainting.Rotation.Opposite.FacingCell)
            {
                continue;
            }

            if (countedThings?.Contains(possiblePainting) == true)
            {
                continue;
            }

            __result += possiblePainting.GetBeauty(outdoors);
            countedThings ??= [];

            countedThings.Add(possiblePainting);
        }
    }
}