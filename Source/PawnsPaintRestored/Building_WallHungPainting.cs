using RimWorld;
using Verse;

namespace PawnsPaintRestored;

public class Building_WallHungPainting : Building_Art
{
    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        if (respawningAfterLoad)
        {
            return;
        }

        (Position + Rotation.Opposite.FacingCell).GetRoom(Map).Notify_ContainedThingSpawnedOrDespawned(this);
    }

    public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
    {
        (Position + Rotation.Opposite.FacingCell).GetRoom(Map).Notify_ContainedThingSpawnedOrDespawned(this);
        base.DeSpawn(mode);
    }
}