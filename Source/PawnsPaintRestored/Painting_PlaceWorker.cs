using Verse;

namespace PawnsPaintRestored;

public class Painting_PlaceWorker : PlaceWorker
{
    public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
        Thing thingToIgnore = null, Thing thing = null)

    {
        var c = loc;

        var support = c.GetEdifice(map);
        if (support == null)
        {
            return "PPR.onsupport".Translate();
        }

        if (support.def?.graphicData == null)
        {
            return "PPR.onsupport".Translate();
        }

        if ((support.def.graphicData.linkFlags & (LinkFlags.Rock | LinkFlags.Wall)) == 0)
        {
            return "PPR.onsupport".Translate();
        }

        c = loc + rot.Opposite.FacingCell;
        if (!c.Walkable(map))
        {
            return "PPR.walkable".Translate();
        }

        var currentBuildings = loc.GetThingList(map);
        foreach (var building in currentBuildings)
        {
            if (building?.def?.defName.StartsWith("Painting") == false)
            {
                continue;
            }

            return "PPR.existing".Translate();
        }

        return AcceptanceReport.WasAccepted;
    }
}