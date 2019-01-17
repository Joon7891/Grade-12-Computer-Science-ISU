// Author: Joon Song, Steven Ung
// File Name: Chunk.cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/17/2018
// Modified Date: 12/29/2018
// Description: Class to hold Chunk object - used to optimize graphics rendering

using System;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Graphics;
using static ISU_Medieval_Odyssey.Tile;

namespace ISU_Medieval_Odyssey
{
    public sealed class Chunk
    {
        /// <summary>
        /// The horizontal/vertical size of the chunk - a chunk will contain 16 x 16 tiles
        /// </summary>
        public const int SIZE = 16;

        // The tiles in this chunk
        [JsonProperty]
        private Tile[,] tiles;

        /// <summary>
        /// The tile at the specificed coordinate
        /// </summary>
        /// <param name="x">The x-coordinate of the tile, relative to this <see cref="Chunk"/></param>
        /// <param name="y">The y-coordinate of the tile, relative to this <see cref="Chunk"/></param>
        /// <returns>The <see cref="Tile"/> at the specified coordinate</returns>
        public Tile this[int x, int y] => tiles[x, y];

        /// <summary>
        /// The position of this <see cref="Chunk"/> in chunk-space
        /// </summary>
        [JsonProperty]
        public Vector2Int Position { get; private set; }

        /// <summary>
        /// The position of this <see cref="Chunk"/> in world/tile-space
        /// </summary>
        [JsonProperty]
        public Vector2Int WorldPosition => Position * SIZE;

        // The terrain generator for this Chunk
        [JsonProperty]
        private TerrainGenerator terrainGenerator;

        /// <summary>
        /// Constructor for <see cref="Chunk"/> object
        /// </summary>
        /// <param name="position">The position of the chunk</param>
        /// <param name="terrainGenerator">The terrain generator</param>
        public Chunk(Vector2Int position, TerrainGenerator terrainGenerator)
        {
            // Assigning position and generating chunks
            Position = position;
            tiles = new Tile[SIZE, SIZE];
            this.terrainGenerator = terrainGenerator;
            tiles = terrainGenerator.GenerateChunkTiles(position); 
        }

        /// <summary>
        /// Draw subprogram for <see cref="Chunk"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing all 32 x 32 tiles in the chunk
            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public void SetTileOnInteract(OnInteract onInteract)
        {

        }

        public static Vector2Int TileToChunkCoordinate(Vector2Int tileCoordinate)
        {
            Vector2Int chunkCoordinate = Vector2Int.Zero;
            chunkCoordinate.X = (int)Math.Floor(tileCoordinate.X / (float)SIZE);
            chunkCoordinate.Y = (int)Math.Floor(tileCoordinate.Y / (float)SIZE);
            return chunkCoordinate;
        }

        /// <summary>
        /// Subprogarm to serialized this <see cref="Chunk"/>
        /// </summary>
        /// <returns>The serialized version of this <see cref="Chunk"/></returns>
        public string Serialize() => JsonConvert.SerializeObject(this);

        /// <summary>
        /// Subprogram to deserialized a <see cref="Chunk"/>
        /// </summary>
        /// <param name="serializedData">The serialized data</param>
        /// <returns>The deserialized <see cref="Chunk"/></returns>
        public static Chunk Deserialize(string serializedData) => JsonConvert.DeserializeObject<Chunk>(serializedData);
    }
}
