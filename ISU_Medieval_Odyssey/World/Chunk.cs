// Author: Joon Song, Steven Ung
// File Name: Chunk.cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/17/2018
// Modified Date: 12/29/2018
// Description: Class to hold Chunk object - used to optimize graphics rendering

using Newtonsoft.Json;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Chunk
    {
        /// <summary>
        /// The horizontal/vertical size of the chunk - a chunk will contain 16 x 16 tiles
        /// </summary>
        public const byte SIZE = 16;

        /// <summary>
        /// The <see cref="Tile"/>s in this <see cref="Chunk"/>
        /// </summary>
        public Tile[,] Tiles { get; private set; }

        /// <summary>
        /// The position of this <see cref="Chunk"/> in chunk-space
        /// </summary>
        public Vector2Int Position { get; private set; }

        /// <summary>
        /// The position of this <see cref="Chunk"/> in world/tile-space
        /// </summary>
        public Vector2Int WorldPosition => Position * SIZE;

        /// <summary>
        /// Constructor for <see cref="Chunk"/> object
        /// </summary>
        /// <param name="position">The position of the chunk</param>
        /// <param name="terrainGenerator">The terrain generator</param>
        public Chunk(Vector2Int position, TerrainGenerator terrainGenerator)
        {
            // Assigning position and generating chunks
            Position = position;
            Tiles = new Tile[SIZE, SIZE];

            if (Tiles[0 ,0] == null)
            {
                Tiles = terrainGenerator.GenerateChunkTiles(position);
            }
        }

        /// <summary>
        /// Draw subprogram for <see cref="Chunk"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing all 32 x 32 tiles in the chunk
            foreach (Tile tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }
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
