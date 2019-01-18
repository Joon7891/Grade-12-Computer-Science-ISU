// Author: Joon Song
// File Name: Shop.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/01/2019
// Modified Date: 01/17/2019
// Description: Class to hold Shop object

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Shop : IBuilding
    {
        /// <summary>
        /// The graphical <see cref="Rectangle"/> for this <see cref="Shop"/>
        /// </summary>
        public Rectangle Rectangle => insideShopSprite.Rectangle;

        private static Texture2D insideShopImage;
        private static List<Vector2Int> insideObstructionLocs = new List<Vector2Int>();
        private static List<Vector2Int> outsideObstructionLocs = new List<Vector2Int>();

        // Various constants representing dimensions of the Shop
        private const int OUTSIDE_WIDTH = 9;
        private const int INSIDE_WIDTH = 7;
        private const int INSIDE_HEIGHT = 6;
        private const int OUTSIDE_HEIGHT = 10;

        private const int PIXEL_WIDTH = Tile.SPACING * OUTSIDE_WIDTH;
        private const int PIXEL_HEIGHT = Tile.SPACING * OUTSIDE_HEIGHT;

        private Sprite insideShopSprite;
        private Sprite outsideShopSprite;

        /// <summary>
        /// Static constructor for <see cref="Shop"/> object
        /// </summary>
        static Shop()
        {
            // Importing shop images
            insideShopImage = Main.Content.Load<Texture2D>("Images/Sprites/Buildings/shopInsideImage"); 

            // Setting up obstruction tiles
            for (int i = 0; i < INSIDE_WIDTH; ++i)
            {
                insideObstructionLocs.Add(new Vector2Int(1 + i, 2));
            }
            for (int i = 0; i < INSIDE_HEIGHT; ++i)
            {
                insideObstructionLocs.Add(new Vector2Int(0, 3 + i));
                insideObstructionLocs.Add(new Vector2Int(INSIDE_WIDTH + 1, 3 + i));
            }
        }

        /// <summary>
        /// Constructor for <see cref="Shop"/> object
        /// </summary>
        /// <param name="cornerTile">The position of the tile <see cref="Tile"/> in the top left corner</param>
        public Shop(Vector2Int cornerTile)
        {
            insideShopSprite = new Sprite(insideShopImage, new Rectangle(cornerTile.X * Tile.SPACING, cornerTile.Y * Tile.SPACING, PIXEL_WIDTH, PIXEL_HEIGHT));

            // Setting up appropriate
            for (int i = 0; i < insideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(cornerTile + insideObstructionLocs[i]).InsideObstructState = true;
            }
            for (int i = 0; i < outsideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(cornerTile + outsideObstructionLocs[i]).OutsideObstructState = true;
            }
        }

        /// <summary>
        /// Subprogram to draw the inside of the <see cref="Shop"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        public void DrawInside(SpriteBatch spriteBatch)
        {
            // Drawing the inside of the shop
            insideShopSprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Subprogram to draw the outside of the <see cref="Shop"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        public void DrawOutside(SpriteBatch spriteBatch)
        {
            // Drawing the outside of the shop
            outsideShopSprite.Draw(spriteBatch);
        }
    }
}