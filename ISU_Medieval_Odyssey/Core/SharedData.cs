// Author: Joon Song, Steven Ung
// File Name: SharedData.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 01/01/2019
// Description: Static class to hold shared data

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        /// A vector representing the center of the screen
        /// </summary>
        public static Vector2 ScreenCenter { get; private set; }

        /// <summary>
        /// The hexadecimal representation of "infinity" 
        /// </summary>
        public const int INFINITY = 0x3f3f3f3f;

        /// <summary>
        /// Dictionary to map movement types to its number of frames
        /// </summary>
        public static Dictionary<MovementType, byte> MovementNumFrames { get; private set; }

        /// <summary>
        /// Random number generator
        /// </summary>
        public static Random RNG { get; private set; }

        /// <summary>
        /// A texture of a white image - used for drawing blank rectangles
        /// </summary>
        public static Texture2D WhiteImage { get; private set; }

        /// <summary>
        /// Static constructor to setup various SharedData components
        /// </summary>
        static SharedData()
        {
            // Constructing Random Number Generator
            RNG = new Random();

            // Constructing screen center vector
            ScreenCenter = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);

            // Setting up movement type num frames dictionary
            MovementNumFrames = new Dictionary<MovementType, byte>();
            MovementNumFrames.Add(MovementType.Walk, 9);
            MovementNumFrames.Add(MovementType.Slash, 6);
            MovementNumFrames.Add(MovementType.Shoot, 13);
            MovementNumFrames.Add(MovementType.Thrust, 8);

            // Importing white image
            WhiteImage = Main.Instance.Content.Load<Texture2D>("Images/Sprites/whiteImage");
        }
    }
}