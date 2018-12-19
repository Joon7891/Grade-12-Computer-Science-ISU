// Author: Joon Song, Steven Ung
// File Name: Armour.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Armour object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Armour : Item
    {
        // Dictionary to map MovementTypes to the appropriate images
        protected Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Draw subprogram for Armour object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="playerRectangle">The corresponding player's rectangle</param>
        /// <param name="movementType">The movement type</param>
        /// <param name="direction">The current direction</param>
        /// <param name="frameNo">The current frame number</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, MovementType movementType, Direction direction, byte frameNo)
        {
            // Drawing armour
            spriteBatch.Draw(movementImages[movementType][(byte)direction, frameNo] , playerRectangle, Color.White);
        }
    }
}
