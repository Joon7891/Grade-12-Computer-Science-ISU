// Author: Joon Song, Steven Ung
// File Name: Quadrant.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/17/2019
// Modified Date: 01/17/2019
// Description: Class to hold Quadrant enum

namespace ISU_Medieval_Odyssey
{
    /// <summary>
    /// Quadrant enum to hold various quadrant types
    /// </summary>
    public enum Quadrant : sbyte
    {
        None = -1,
        TopRight = 0,
        TopLeft = 1,
        BottomLeft = 2,
        BottomRight = 3
    }
}
