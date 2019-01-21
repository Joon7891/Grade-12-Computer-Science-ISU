// Author: Steven Ung
// File Name: Safehouse.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/20/2019
// Modified Date: 01/20/2019
// Description: Class to hold an instance of a safehouse
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    class Safehouse : IBuilding
    {
        private const int PIXEL_WIDTH = Tile.SPACING * OUTSIDE_WIDTH;
        private const int PIXEL_HEIGHT = Tile.SPACING * OUTSIDE_HEIGHT;
        private const int INSIDE_WIDTH = 7;
        private const int INSIDE_HEIGHT = 4;
        private const int OUTSIDE_WIDTH = 9;
        private const int OUTSIDE_HEIGHT = 6;

        public Rectangle Rectangle { get; set; }
        public Vector2Int CornerTile { get; }
        Sprite insideSprite;
        Sprite outsideSprite;

        static private List<Vector2Int> insideObstructionLocs = new List<Vector2Int>();
        static private List<Vector2Int> outsideObstructionLocs = new List<Vector2Int>();
        static private Vector2Int enterLocation;
        static private Vector2Int exitLocation;
        static private Texture2D insideImage;
        static private Texture2D outsideImage;

        static Safehouse()
        {
            exitLocation = new Vector2Int(4, 5);
            enterLocation = new Vector2Int(4, 6);

            // loading textures
            insideImage = Main.Content.Load<Texture2D>("Images/Sprites/Buildings/homebaseimage");
            outsideImage = Main.Content.Load<Texture2D>("Images/Sprites/Buildings/homeOutsideImage");

            // Setting up inside obstruction tiles
            for (int i = 0; i <= INSIDE_HEIGHT; ++i)
            {
                insideObstructionLocs.Add(new Vector2Int(0, i));
                insideObstructionLocs.Add(new Vector2Int(INSIDE_WIDTH + 1, i));
            }

            for (int i = 1; i <= INSIDE_WIDTH; ++i)
            {
                insideObstructionLocs.Add(new Vector2Int(i, 5));
                insideObstructionLocs.Add(new Vector2Int(i, 1));
            }

            insideObstructionLocs.Remove(exitLocation);
            insideObstructionLocs.Add(exitLocation + new Vector2Int(0, 1));

            // Setting up outside obstruction tiles
            for (int i = 0; i < INSIDE_WIDTH; ++i)
            {
                outsideObstructionLocs.Add(new Vector2Int(1 + i, 1));
                outsideObstructionLocs.Add(new Vector2Int(1 + i, 5));
            }
            for (int i = 1; i < OUTSIDE_HEIGHT - 1; ++i)
            {
                outsideObstructionLocs.Add(new Vector2Int(1, 1 + i));
                outsideObstructionLocs.Add(new Vector2Int(INSIDE_WIDTH, 1 + i));
            }
    }

        /// <summary>
        /// Constructor for <see cref="Shop"/> object
        /// </summary>
        /// <param name="cornerTile">The position of the tile <see cref="Tile"/> in the top left corner</param>
        public Safehouse(Vector2Int cornerTile)
        {
            Rectangle = new Rectangle(cornerTile.X * Tile.SPACING, cornerTile.Y * Tile.SPACING, PIXEL_WIDTH, PIXEL_HEIGHT);
            insideSprite = new Sprite(insideImage, Rectangle);
            outsideSprite = new Sprite(outsideImage, Rectangle);

            // Setting up appropriate obstructions certain locations
            CornerTile = cornerTile;
            SetTiles();
        }


        public void SetTiles()
        {
            for (int i = 0; i < insideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(CornerTile + insideObstructionLocs[i]).InsideObstructState = true;
            }

            for (int i = 0; i < outsideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(CornerTile + outsideObstructionLocs[i]).OutsideObstructState = true;
            }

            World.Instance.GetTileAt(CornerTile + exitLocation).OnInteractProcedure = new Interaction(Direction.Down, (player) =>
            {
                World.Instance.IsInside = false;
                World.Instance.CurrentBuilding = null;
                player.Y += Tile.SPACING;
            });
            World.Instance.GetTileAt(CornerTile + enterLocation).OnInteractProcedure = new Interaction(Direction.Up, (player) =>
            {
                World.Instance.IsInside = true;
                World.Instance.CurrentBuilding = this;
                player.Y -= Tile.SPACING;
            });
        }

        /// <summary>
        /// Update subprogram for this <see cref="Safehouse"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            if (KeyboardHelper.IsKeyDown(SettingsScreen.Instance.Interact))
            {
                GameScreen.Instance.Player.Health = int.MaxValue;
            }
        }

        /// <summary>
        /// Subprogram to draw the outside of the <see cref="Safehouse"/>
        /// </summary>
        /// <param name="spriteBatch"> Spritebatch to draw sprites </param>
        public void DrawOutside(SpriteBatch spriteBatch)
        {
            outsideSprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Draw the inside of the <see cref="Safehouse"/>
        /// </summary>
        /// <param name="spriteBatch"> Spritebatch to draw sprites </param>
        public void DrawInside(SpriteBatch spriteBatch)
        {
            insideSprite.Draw(spriteBatch);
        }
    }
}
