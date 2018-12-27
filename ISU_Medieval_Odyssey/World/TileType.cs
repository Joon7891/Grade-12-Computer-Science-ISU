// Author: Joon Song
// File Name: TileType.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/20/2018
// Description: Class to hold TileType enum

namespace ISU_Medieval_Odyssey
{
    /// <summary>
    /// TileType enum to hold various tile types
    /// </summary>
    public enum TileType : byte
    {
        Empty,
        DeepWater,
        Water,
        WetSand,
        Sand,
        Dirt,
        DryGrass,
        Grass,
        ForestGrass,
        Stone,
        Snow,
        IcySnow,
        Ice,
        WoodFloorHorizontal,
        WoodFloorVertical
    }
}
