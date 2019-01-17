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
using Newtonsoft.Json;

namespace ISU_Medieval_Odyssey
{
    public sealed class Tile
    {
        /// <summary>
        /// The spacing between adjacent <see cref="Tile"/>s, in pixels
        /// </summary>
        public const int SPACING = 72;

        // Various constants for drawing graphics in appropraite location
        private const byte HORIZONTAL_SIZE = 72;
        private const byte VERTICAL_SIZE = 144;
        private const byte HORIZONTAL_SPACING = HORIZONTAL_SIZE;
        private const byte VERTICAL_SPACING = VERTICAL_SIZE / 2;

        /// <summary>
        /// The <see cref="TileType"/> of the <see cref="Tile"/>
        /// </summary>
        [JsonProperty]
        public TileType Type { get; }

        /// <summary>
        /// The position of the tile in the world
        /// </summary>
        [JsonProperty]
        public Vector2Int WorldPosition { get; }

        /// <summary>
        /// Whether the <see cref="Tile"/> is obstructed from a <see cref="Entity"/> from the inside of a <see cref="IBuilding"/>
        /// </summary>
        public bool InsideObstructState { get; set; } = false;

        /// <summary>
        /// Whether the <see cref="Tile"/> is obstructed from a <see cref="Entity"/> from the outside of a <see cref="IBuilding"/>
        /// </summary>
        public bool OutsideObstructState { get; set; } = false;

        /// <summary>
        /// The <see cref="OnInteract"/> procedure to execute while on this <see cref="Tile"/>
        /// </summary>
        [JsonProperty]
        public OnInteract OnInteractProcedure { get; set; } = null;

        // Variables required for drawing tile
        private static Dictionary<TileType, Texture2D> tileImageDictionary = new Dictionary<TileType, Texture2D>();
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