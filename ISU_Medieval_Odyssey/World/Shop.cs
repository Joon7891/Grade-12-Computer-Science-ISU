
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Shop : IBuilding
    {
        private Tile[,] groundTiles = new Tile[7, 7];
        private Sprite[] wallSprites = new Sprite[7];
        private Tile exitTile;
        private Sprite shelfSprite;

        // Shop roof sprites
        private Sprite[] roofDownSprites = new Sprite[7];
        private Sprite[] roofUpSprites = new Sprite[6];
        private Sprite[,] roofVerticalSprites = new Sprite[2, 9];
        private Sprite[] roofCornerSprites = new Sprite[4];

        /// <summary>
        /// Constructor for <see cref="Shop"/> object
        /// </summary>
        /// <param name="cornerPosition">The position of the top left corner</param>
        public Shop(Vector2Int cornerPosition)
        {
            // Loading and setting up various textures for inside the Shop
            for (byte i = 0; i < groundTiles.GetLength(0); ++i)
            {
                for (byte j = 0; j < groundTiles.GetLength(1); ++j)
                {
                    groundTiles[i, j] = new Tile((i + j) % 2 == 0 ? TileType.WoodFloorHorizontal : TileType.WoodFloorVertical,
                        new Vector2Int(cornerPosition.X + i + 1, cornerPosition.Y + j + 1));
                }
                wallSprites[i] = new Sprite(Main.Content.Load<Texture2D>("Images/Sprites/Buildings/Shop/shopWall"),
                    new Rectangle((cornerPosition.X + i + 1) * Tile.SPACING, (cornerPosition.Y + 1) * Tile.SPACING,
                    Tile.SPACING, Tile.SPACING));
            }
            exitTile = new Tile(TileType.WoodFloorVertical, new Vector2Int(cornerPosition.X + 4, cornerPosition.Y + 8));
            shelfSprite = new Sprite(Main.Content.Load<Texture2D>("Images/Sprites/Buildings/Shop/shopShelf"),
                new Rectangle((cornerPosition.X + 1) * Tile.SPACING, (cornerPosition.Y + 1) * Tile.SPACING + 5,
                Tile.SPACING * 7, Tile.SPACING));
   
            // Setting up the inside shop roof
            for (byte i = 0; i < roofDownSprites.Length; ++i)
            {
                roofDownSprites[i] = new Sprite(Main.Content.Load<Texture2D>("Images/Sprites/Buildings/Shop/indoorShopRoofDown"),
                    new Rectangle((cornerPosition.X + i + 1) * Tile.SPACING, (cornerPosition.Y) * Tile.SPACING,
                    Tile.SPACING, Tile.SPACING));
            }
            for (byte i = 0; i < roofUpSprites.Length; ++i)
            {
                roofUpSprites[i] = new Sprite(Main.Content.Load<Texture2D>("Images/Sprites/Buildings/Shop/indoorShopRoofUp"),
                    new Rectangle((cornerPosition.X + 1 + i + (i > 2 ? 1 : 0)) * Tile.SPACING, (cornerPosition.Y + 9) * Tile.SPACING,
                    Tile.SPACING, Tile.SPACING));
            }
            for (byte i = 0; i < roofVerticalSprites.GetLength(1); ++i)
            {
                roofVerticalSprites[0, i] = new Sprite(Main.Content.Load<Texture2D>("Images/Sprites/Buildings/Shop/indoorShopRoofRight"),
                    new Rectangle((cornerPosition.X + (i == 8 ? 3 : 0)) * Tile.SPACING, (cornerPosition.Y + i + 1) * Tile.SPACING,
                    Tile.SPACING, Tile.SPACING));
                roofVerticalSprites[1, i] = new Sprite(Main.Content.Load<Texture2D>("Images/Sprites/Buildings/Shop/indoorShopRoofLeft"),
                    new Rectangle((cornerPosition.X + 8 - (i == 8 ? 3 : 0)) * Tile.SPACING, (cornerPosition.Y + i + 1) * Tile.SPACING,
                    Tile.SPACING, Tile.SPACING));
            }
            for (byte i = 0; i < roofCornerSprites.Length; ++i)
            {
                roofCornerSprites[i] = new Sprite(Main.Content.Load<Texture2D>($"Images/Sprites/Buildings/Shop/indoorShopRoofCorner{i + 1}"),
                    new Rectangle((cornerPosition.X + (i > 1 ? 8 : 0)) * Tile.SPACING, (cornerPosition.Y + i % 2 == 1 ? 9 : 0) * Tile.SPACING,
                    Tile.SPACING, Tile.SPACING));
            }
        }

        public void DrawInside(SpriteBatch spriteBatch)
        {
            // Drawing inside components of Shop
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

            // Drawing roof
            for (byte i = 0; i < roofDownSprites.Length; ++i)
            {
                roofDownSprites[i].Draw(spriteBatch);
            }
            for (byte i = 0; i < roofUpSprites.Length; ++i)
            {
                roofUpSprites[i].Draw(spriteBatch);
            }
            foreach (Sprite roofVertical in roofVerticalSprites)
            {
                roofVertical.Draw(spriteBatch);
            }
            for (byte i = 0; i < roofCornerSprites.Length; ++i)
            {
                roofCornerSprites[i].Draw(spriteBatch);
            }
        }

        public void DrawOutside(SpriteBatch spriteBatch)
        {

        }
    }
}