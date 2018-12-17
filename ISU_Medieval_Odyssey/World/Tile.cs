// Author: Joon Song, Steven Ung
// File Name: Tile.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold Tile object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Tile
    {
        // Dictionary to map a TileType to a given Tile image
        private Dictionary<TileType, Texture2D> tileImageDictionary = new Dictionary<TileType, Texture2D>();

        /// <summary>
        /// Stastic constructor for Tile object
        /// </summary>
        static Tile()
        {
            // To Do: Load images
        }

        /// <summary>
        /// Subprogram to draw Tile object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Add draw logic here...
        }
    }
}
