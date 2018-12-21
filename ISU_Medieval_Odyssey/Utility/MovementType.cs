// Author: Joon Song
// File Name: MovementType.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold MovementType enum

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey.Utility
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
