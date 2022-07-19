using RimWorld;
using UnityEngine;
using Verse;

namespace PawnsPaintRestored;

/// <summary>
///     This is a stupid workaround as the original mod had the paintings interaction-cell on the wrong side.
///     So since South is actually North and MinifiedThings always uses the south texture if the thing uses Graphic_Multi
///     this custom Minified thing-def is needed to force the painting being shown when minified instead of the blank
///     south-texture.
/// </summary>
public class MinifiedWallPainting : MinifiedThing
{
    private Graphic crateFrontGraphic;

    private Graphic CrateFrontGraphic
    {
        get
        {
            if (crateFrontGraphic == null)
            {
                crateFrontGraphic = LoadCrateFrontGraphic();
            }

            return crateFrontGraphic;
        }
    }

    public override void DrawAt(Vector3 drawLoc, bool flip = false)
    {
        CrateFrontGraphic.DrawFromDef(drawLoc + (Altitudes.AltIncVect * 0.1f), Rot4.North, null);
        Graphic.Draw(drawLoc, Rot4.North, this);
    }

    public override void Print(SectionLayer layer)
    {
        var drawPos = DrawPos;
        var matSingle = CrateFrontGraphic.MatSingle;
        Graphic.TryGetTextureAtlasReplacementInfo(matSingle, TextureAtlasGroup.Item, false, false, out matSingle,
            out var uvs, out _);
        Printer_Plane.PrintPlane(layer, drawPos + (Altitudes.AltIncVect * 0.1f), CrateFrontGraphic.drawSize, matSingle,
            0f, false, uvs);
        var rot = Rot4.North;

        var mat = Graphic.MatAt(rot, this);
        Graphic.TryGetTextureAtlasReplacementInfo(mat, InnerThing.def.category.ToAtlasGroup(), false, false, out mat,
            out uvs, out _);
        Printer_Plane.PrintPlane(layer, drawPos, Graphic.drawSize, mat, 0f, false, uvs);
    }
}