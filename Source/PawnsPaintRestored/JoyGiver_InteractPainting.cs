using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace PawnsPaintRestored;

public class JoyGiver_InteractPainting : JoyGiver_InteractBuilding
{
    private static readonly List<Thing> tmpCandidates = new List<Thing>();

    protected override Job TryGivePlayJob(Pawn pawn, Thing t)
    {
        var interactionSpot = t.Position + IntVec3.South.RotatedBy(t.Rotation);
        if (interactionSpot.Standable(t.Map) && !t.IsForbidden(pawn) && !interactionSpot.IsForbidden(pawn) &&
            !pawn.Map.pawnDestinationReservationManager.IsReserved(interactionSpot) &&
            pawn.CanReserveSittableOrSpot(interactionSpot))
        {
            return JobMaker.MakeJob(def.jobDef, t, interactionSpot);
        }

        return null;
    }

    public override Job TryGiveJob(Pawn pawn)
    {
        var thing = FindBestPainting(pawn);
        return thing != null ? TryGivePlayJob(pawn, thing) : null;
    }

    private Thing FindBestPainting(Pawn pawn)
    {
        tmpCandidates.Clear();
        GetSearchSet(pawn, tmpCandidates);
        if (tmpCandidates.Count == 0)
        {
            return null;
        }

        float Selector(Thing thing)
        {
            return pawn.Position.DistanceTo(thing.Position + IntVec3.South.RotatedBy(thing.Rotation));
        }

        tmpCandidates.SortBy(Selector);

        foreach (var tmpCandidate in tmpCandidates)
        {
            var postitionToCheck = tmpCandidate.Position + IntVec3.South.RotatedBy(tmpCandidate.Rotation);

            if (pawn.CanReach(new LocalTargetInfo(postitionToCheck), PathEndMode.OnCell, Danger.None))
            {
                return tmpCandidate;
            }
        }

        return null;
    }
}