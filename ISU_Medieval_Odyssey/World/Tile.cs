// Author: Joon Song, Steven Ung
// File Name: Tile.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold Tile object

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Tile
    {
        // Various constants required to draw tile at appropraite location
        private const int PIXEL_SIZE = 15;
        private const int VERTICAL_SPACING = 4;
        private const int HORIZONTAL_SPACING = 6;

        /// <summary>
        /// The type of the <see cref="Tile"/>
        /// </summary>
        public TileType Type { get; set;  }

        /// <summary>
        /// The position of the tile in the world
        /// </summary>
        public Vector2Int WorldPosition { get; }
        
        // Variables required for drawing tile at appropraite location
        public static Dictionary<TileType, Texture2D> tileImageDictionary = new Dictionary<TileType, Texture2D>();
        private Rectangle rectangle;

        /// <summary>
        /// Static constructor to load various <see cref="Tile"/> components
        /// </summary>
        static Tile()
        {
            // Loading images for each tile type
            foreach (TileType tileType in Enum.GetValues(typeof(TileType)))
            {
                tileImageDictionary.Add(tileType, Main.Instance.Content.Load<Texture2D>($"Images/Sprites/Tiles/tile{tileType.ToString()}"));
            }
        }

        public Tile(TileType type, Vector2Int worldPosition)
        {
            Type = type;
            WorldPosition = worldPosition;
            rectangle = new Rectangle(HORIZONTAL_SPACING * (worldPosition.X - worldPosition.Y), 
                VERTICAL_SPACING * (worldPosition.X + worldPosition.Y), PIXEL_SIZE, PIXEL_SIZE);
        }

        /// <summary>
        /// Subprogram to draw <see cref="Tile"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing tile
            spriteBatch.Draw(tileImageDictionary[Type], rectangle, Color.White);
        }
    }
}
