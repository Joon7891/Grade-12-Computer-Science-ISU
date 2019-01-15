// Author: Joon Song
// File Name: SharedData.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 01/01/2019
// Description: Static class to hold shared data

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        /// A <see cref="Vector2"/> representing the center of the screen
        /// </summary>
        public static Vector2 ScreenCenter { get; } = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);

        /// <summary>
        /// Random number generator
        /// </summary>
        public static Random RNG { get; } = new Random();

        /// <summary>
        /// Array of information fonts 0-index is smallest, 3-index is largest
        /// </summary>
        public static SpriteFont[] InformationFonts { get; private set; }

        /// <summary>
        /// A texture of a white image - used for drawing blank rectangles
        /// </summary>
        public static Texture2D WhiteImage { get; private set; }

        /// <summary>
        /// Static constructor to setup various <see cref="SharedData"/> components
        /// </summary>
        static SharedData()
        {
            // Importing images and fonts
            WhiteImage = Main.Content.Load<Texture2D>("Images/Sprites/whiteImage");
            InformationFonts = new SpriteFont[4];
            for (byte i = 0; i < InformationFonts.Length; ++i)
            {
                InformationFonts[i] = Main.Content.Load<SpriteFont>($"Fonts/InformationFont{i}");
            }
        }
    }
}