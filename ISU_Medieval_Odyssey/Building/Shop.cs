
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Shop : IBuilding
    {
        private Tile[,] groundTiles = new Tile[7, 7];
        private Tile exitTile; // 9 x 2, 7 x 2
        private Sprite[] wallSprites = new Sprite[7];
        private Sprite[,] indoorRoofSpritesVertical = new Sprite[2, 9];
        private Sprite[,] indoorRoofSpritesHorizontal = new Sprite[2, 7];
        private Sprite shelfSprite;

        public Shop(Vector2Int chunkPosition)
        {
            for (int i = 0; i < groundTiles.GetLength(0); ++i)
            {
                for (int j = 0; j < groundTiles.GetLength(1); ++j)
                {
                    groundTiles[i, j] = new Tile(
                        (i + j) % 2 == 0 ? TileType.WoodFloorHorizontal : TileType.WoodFloorVertical, 
                        new Vector2Int(chunkPosition.X * Chunk.SIZE + i + 4, chunkPosition.Y * Chunk.SIZE + j + 4));
                }
                wallSprites[i] = new Sprite(Main.Instance.Content.Load<Texture2D>("Images/Sprites/Buildings/shopWall"), 
                    new Rectangle((chunkPosition.X * Chunk.SIZE + i + 4) * Tile.HORIZONTAL_SIZE, (chunkPosition.Y * Chunk.SIZE + 2) * Tile.HORIZONTAL_SIZE,
                    Tile.HORIZONTAL_SIZE, Tile.HORIZONTAL_SIZE));
            }
            exitTile = new Tile(TileType.WoodFloorVertical, new Vector2Int(Chunk.SIZE * chunkPosition.X + 7, chunkPosition.Y * Chunk.SIZE + 11));
            shelfSprite = new Sprite(Main.Instance.Content.Load<Texture2D>("Images/Sprites/Buildings/shopShelf"),
                new Rectangle((chunkPosition.X * Chunk.SIZE + 4) * Tile.HORIZONTAL_SIZE, (chunkPosition.Y * Chunk.SIZE + 2) * Tile.HORIZONTAL_SIZE + 5,
                Tile.HORIZONTAL_SIZE * 7, Tile.HORIZONTAL_SIZE));

            // Roof
            for (int i = 0; i < indoorRoofSpritesHorizontal.GetLength(0); ++i)
            {
                for (int j = 0; j < indoorRoofSpritesHorizontal.GetLength(1); ++j)
                {
                    indoorRoofSpritesHorizontal[i, j] = new Sprite(Main.Instance.Content.Load<Texture2D>("Images/Sprites/Buildings/shopIndoorRoof"),
                        new Rectangle((chunkPosition.X * Chunk.SIZE + j + 4) * Tile.HORIZONTAL_SIZE, (chunkPosition.Y * Chunk.SIZE + 7 * i + 1) * Tile.HORIZONTAL_SIZE,
                    Tile.HORIZONTAL_SIZE, Tile.HORIZONTAL_SIZE));
                }
                for (int j = 0; j < indoorRoofSpritesVertical.GetLength(1); ++j)
                {
                    indoorRoofSpritesVertical[i, j] = new Sprite(Main.Instance.Content.Load<Texture2D>("Images/Sprites/Buildings/shopIndoorRoof"),
                        new Rectangle((chunkPosition.X * Chunk.SIZE + 3 + 8 * i) * Tile.HORIZONTAL_SIZE, (chunkPosition.Y * Chunk.SIZE + 1 + j) * Tile.HORIZONTAL_SIZE,
                        Tile.HORIZONTAL_SIZE, Tile.HORIZONTAL_SIZE));
                }
            }
        }

        public void DrawInside(SpriteBatch spriteBatch)
        {
            for (byte i = 0; i < groundTiles.GetLength(0); ++i)
            {
                for (byte j = 0; j < groundTiles.GetLength(1); ++j)
                {
                    groundTiles[i, j].Draw(spriteBatch);
                }

                wallSprites[i].Draw(spriteBatch);
            }
            exitTile.Draw(spriteBatch);
            shelfSprite.Draw(spriteBatch);

            for (int i = 0; i < indoorRoofSpritesHorizontal.GetLength(0); ++i)
            {
                for (int j = 0; j < indoorRoofSpritesHorizontal.GetLength(1); ++j)
                {
                    if (i != 1 || j != 3)
                    {
                        indoorRoofSpritesHorizontal[i, j].Draw(spriteBatch);
                    }
                }
                for (int j = 0; j < indoorRoofSpritesVertical.GetLength(1); ++j)
                {
                    indoorRoofSpritesVertical[i, j].Draw(spriteBatch);
                }
            }
        }

        public void DrawOutside(SpriteBatch spriteBatch)
        {

        }
    }
}