// Author: Joon Song, Steven Ung
// File Name: SharedData.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 01/01/2019
// Description: Static class to hold shared data

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public static class SharedData
    {
        /// <summary>
        /// The width of the screen
        /// </summary>
        public const int SCREEN_WIDTH = 1000;

        /// <summary>
        /// The height of the screen
        /// </summary>
        public const int SCREEN_HEIGHT = 800;

        /// <summary>
        /// The number of frames in Walk animation
        /// </summary>
        public const byte NUM_WALK_FRAMES = 9;

        /// <summary>
        /// The number of frames in Slash animation 
        /// </summary>
        public const byte NUM_SLASH_FRAMES = 6;

        /// <summary>
        /// The number of frames in Shoot animation
        /// </summary>
        public const byte NUM_SHOOT_FRAMES = 13;

        /// <summary>
        /// The number of frames in Thrust animation
        /// </summary>
        public const byte NUM_THRUST_FRAMES = 8;

        /// <summary>
        /// Random number generator
        /// </summary>
        public static Random RNG { get; private set; }

        /// <summary>
        /// Static constructor to setup various SharedData components
        /// </summary>
        static SharedData()
        {
            // Constructing Random Number Generator
            RNG = new Random();
        }
    }
}