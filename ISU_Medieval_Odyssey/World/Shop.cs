
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

            // Setting up borders
            //for (int i = 0; i < 9; ++i)
            //{
            //    for (int j = 0; j < 9; ++j)
            //    {
            //        World.Instance.GetTileAt(new Vector2Int(i, j) + cornerPosition).InsideObstructState = true;
            //    }
            //}
            //for (int i = 1; i < 8; ++i)
            //{
            //    for (int j = 2; j < 10; ++j)
            //    {
            //        World.Instance.GetTileAt(new Vector2Int(i, j) + cornerPosition).InsideObstructState = false;
            //        World.Instance.GetTileAt(new Vector2Int(i, j) + cornerPosition).OutsideObstructState = true;
            //    }
            //}
            //World.Instance.GetTileAt(new Vector2Int(4, 9) + cornerPosition).OnInteractProcedure = (player) =>
            //{
            //    if (player.Direction == Direction.Up)
            //    {
            //        World.Instance.IsInside = false;
            //        player.Y += Tile.SPACING;
            //    }
            //};
            //for (int i = 0; i < 9; ++i)
            //{
            //    World.Instance.GetTileAt(new Vector2Int(i, 9) + cornerPosition).InsideObstructState = true;
            //}
            //World.Instance.GetTileAt(new Vector2Int(4, 9) + cornerPosition).InsideObstructState = false;
            //World.Instance.GetTileAt(new Vector2Int(4, 10) + cornerPosition).OnInteractProcedure = (player) =>
            //{
            //    if (player.Direction == Direction.Down)
            //    {
            //        World.Instance.IsInside = true;
            //        player.Y -= Tile.SPACING;
            //    }
            //};
            //World.Instance.GetTileAt(new Vector2Int(4, 10) + cornerPosition).InsideObstructState = false;
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