// Author: Joon Song
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
        /// <summary>
        /// The horizontal size of the <see cref="Tile"/>, in pixels
        /// </summary>
        public const byte HORIZONTAL_SIZE = 72;

        /// <summary>
        /// The vertical size of the <see cref="Tile"/>, in pixels
        /// </summary>
        public const byte VERTICAL_SIZE = 144;

        /// <summary>
        /// The horizontal spacing between adjacent <see cref="Tile"/>s
        /// </summary>
        public const byte HORIZONTAL_SPACING = HORIZONTAL_SIZE;

        /// <summary>
        /// The vertical spacing between adjacent <see cref="Tile"/>s
        /// </summary>
        public const byte VERTICAL_SPACING = VERTICAL_SIZE / 2;

        /// <summary>
        /// The <see cref="TileType"/> of the <see cref="Tile"/>
        /// </summary>
        public TileType Type { get; }

        /// <summary>
        /// The position of the tile in the world
        /// </summary>
        public Vector2Int WorldPosition { get; }
        
        // Variables required for drawing tile
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
                tileImageDictionary.Add(tileType, Main.Content.Load<Texture2D>($"Images/Sprites/Tiles/tile{tileType.ToString()}"));
            }
        }

        /// <summary>
        /// Constructor for <see cref="Tile"/> objec
        /// </summary>
        /// <param name="type">The type of the tile</param>
        /// <param name="worldPosition">The world position of the tile</param>
        public Tile(TileType type, Vector2Int worldPosition)
        {
            // Assigning tile properties
            Type = type;
            WorldPosition = worldPosition;
            rectangle = new Rectangle(HORIZONTAL_SPACING * worldPosition.X, VERTICAL_SPACING * worldPosition.Y, HORIZONTAL_SIZE, VERTICAL_SIZE);
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