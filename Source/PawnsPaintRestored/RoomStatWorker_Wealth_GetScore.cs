using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace PawnsPaintRestored;

[HarmonyPatch(typeof(RoomStatWorker_Wealth), "GetScore")]
public static class RoomStatWorker_Wealth_GetScore
{
    public static void Postfix(Room room, ref float __result)
    {
        var possiblePaintnings = room.Map.listerThings.ThingsOfDef(ThingDefOf.PaintingOnWall);

        if (!possiblePaintnings.Any())
        {
            return;
        }

        foreach (var possiblePainting in possiblePaintnings)
        {
            if (!room.Cells.Contains(possiblePainting.Position + possiblePainting.Rotation.Opposite.FacingCell))
            {
                continue;
            }

            __result += possiblePainting.MarketValue;
        }
    }
}