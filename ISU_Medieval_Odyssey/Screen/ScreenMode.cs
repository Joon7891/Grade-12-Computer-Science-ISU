// Author: Joon Song
// File Name: ScreenMode.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2019
// Description: Class to hold ScreenMode enum

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    /// <summary>
    /// ScreenMode enum to hold various Screen Modes
    /// </summary>
    public enum ScreenMode : byte
    {
        MainMenu = 0,
        Game = 1,
        Settings = 2
    };
}
