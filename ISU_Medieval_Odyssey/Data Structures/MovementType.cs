// Author: Joon Song
// File Name: MovementType.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold MovementType enum

namespace ISU_Medieval_Odyssey
{
    /// <summary>
    /// MovementType enum to hold various movement types
    /// </summary>
    public enum MovementType : byte
    {
        Walk = 0,
        Slash = 1,
        Shoot = 2,
        Thrust = 3
    }
}
