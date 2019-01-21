// Author: Steven Ung
// File Name: Dungeon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/20/2019
// Modified Date: 01/20/2019
// Description: Class to hold an instance of dungeon
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    class Dungeon : IBuilding
    {
        /// <summary>
        /// Constants pertaining to the dungeon
        /// </summary>
        private const int TILE_SIZE = Tile.SPACING;
        private const int INSIDE_WIDTH = 50;
        private const int INSIDE_HEIGHT = 50;
        private const int OUTSIDE_WIDTH = 1;
        private const int OUTSIDE_HEIGHT = 1;
        private static Vector2Int tileSpacing;

        /// <summary>
        /// Chance of enemy spawning on any given tile
        /// </summary>
        private const int ENEMY_CHANCE = 5;

        /// <summary>
        /// Metadata for buildings
        /// </summary>
        public Rectangle Rectangle { get; set; }
        public Vector2Int CornerTile { get; }

        /// <summary>
        /// Properties of the dungeon
        /// </summary>
        private List<Vector2Int> insideObstructionLocs = new List<Vector2Int>();
        private List<Vector2Int> outsideObstructionLocs = new List<Vector2Int>();
        private Vector2Int enterLocation;
        private Vector2Int exitLocation;

        /// <summary>
        /// Graphics related fields
        /// </summary>
        static Texture2D tile;
        static Texture2D stairUp;
        static Texture2D stairDown;
        Sprite enterSprite;
        Sprite exitSprite;
        List<Sprite> tileSprites;

        static Dungeon()
        {
            tile = Main.Content.Load<Texture2D>("Images/Sprites/Tiles/tileStone");
            stairUp = Main.Content.Load<Texture2D>("Images/Sprites/Tiles/stair_up");
            stairDown = Main.Content.Load<Texture2D>("Images/Sprites/Tiles/stair_down");
            tileSpacing = new Vector2Int(TILE_SIZE, TILE_SIZE);
        }

        public Dungeon(Vector2Int cornerTile)
        {
            DungeonGenerator generator = new DungeonGenerator();
            int[,] layout = generator.GenerateDungeon();

            tileSprites = new List<Sprite>();

            // Setting up inside obstruction tiles, sprites and enemies
            for (int i = 0; i < INSIDE_WIDTH; i++)
            {
                for(int j = 0; j < INSIDE_HEIGHT; j++)
                {
                    Vector2Int currentTile = new Vector2Int(i, j) + cornerTile;
                    if (layout[i, j] == -1)
                    {
                        insideObstructionLocs.Add(currentTile);
                    }
                    else
                    {
                        if (SharedData.RNG.Next(0, 101) < ENEMY_CHANCE)
                        {
                         //   World.Instance.DungeonEnemies.Add(Enemy.RandomEnemy(currentTile + cornerTile, true));
                        }

                        currentTile *= tileSpacing;
                        tileSprites.Add(new Sprite(tile, new Rectangle(currentTile.X, currentTile.Y, TILE_SIZE, TILE_SIZE)));
                        tileSprites.Add(new Sprite(tile, new Rectangle(currentTile.X, currentTile.Y + TILE_SIZE/2, TILE_SIZE, TILE_SIZE)));
                    }
                }
            }

            // barriers to avoid access to the void
            for (int i = 0; i < INSIDE_WIDTH; i++)
            {
               insideObstructionLocs.Add (new Vector2Int(i, INSIDE_WIDTH) + cornerTile);
               insideObstructionLocs.Add (new Vector2Int(INSIDE_HEIGHT, i) + cornerTile);
            }

            // put enter at top left, exit at bottom right
            enterLocation = new Vector2Int(-1, -1);

            for (int i = 0; i < INSIDE_WIDTH; i++)
            {
                for (int j = 0; j < INSIDE_HEIGHT; j++)
                {
                    if (layout[i, j] != -1 && enterLocation.X == -1)
                    {
                        enterLocation = (new Vector2Int(i, j));
                    }
                    exitLocation = (new Vector2Int(i, j));
                }
            }

            enterLocation += cornerTile;
            exitLocation += cornerTile;

            enterSprite = new Sprite(stairDown, new Rectangle(enterLocation.X * TILE_SIZE, enterLocation.Y * TILE_SIZE
                                    , TILE_SIZE, TILE_SIZE));
            exitSprite = new Sprite(stairUp, new Rectangle(exitLocation.X * TILE_SIZE, exitLocation.Y * TILE_SIZE
                                    , TILE_SIZE, TILE_SIZE));

            CornerTile = cornerTile;
            SetTiles();
        }

        /// <summary>
        /// Setup world tiles
        /// </summary>
        public void SetTiles()
        {
            for (int i = 0; i < insideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(insideObstructionLocs[i]).InsideObstructState = true;
            }

            World.Instance.GetTileAt(exitLocation).OnInteractProcedure = new Interaction(Direction.Left, (player) =>
            {
                World.Instance.IsInside = false;
                World.Instance.CurrentBuilding = null;
            });

            World.Instance.GetTileAt(enterLocation).OnInteractProcedure = new Interaction(Direction.Right, (player) =>
            {
                World.Instance.IsInside = true;
                World.Instance.CurrentBuilding = this;
            });
        }

        /// <summary>
        /// Update subprogram for this <see cref="Dungeon"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            for (int i = World.Instance.DungeonEnemies.Count - 1; i >= 0; --i)
            {
                if (World.Instance.DungeonEnemies[i].IsStuck())
                {
                    World.Instance.DungeonEnemies.RemoveAt(i);
                }
            }

            for (int i = World.Instance.DungeonEnemies.Count - 1; i >= 0; --i)
            {
                if (World.Instance.DungeonEnemies[i].CurrentTile.X > exitLocation.X ||
                    World.Instance.DungeonEnemies[i].CurrentTile.Y > exitLocation.Y ||
                    enterLocation.X > World.Instance.DungeonEnemies[i].CurrentTile.X ||
                    enterLocation.Y > World.Instance.DungeonEnemies[i].CurrentTile.Y)
                {
                    World.Instance.DungeonEnemies.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Subprogram to draw the outside of the <see cref="Dungeon"/>
        /// </summary>
        /// <param name="spriteBatch"> Spritebatch to draw sprites </param>
        public void DrawOutside(SpriteBatch spriteBatch)
        {
            Console.WriteLine(enterLocation.X + " " + enterLocation.Y);

            enterSprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Draw the inside of the <see cref="Dungeon"/>
        /// </summary>
        /// <param name="spriteBatch"> Spritebatch to draw sprites </param>
        public void DrawInside(SpriteBatch spriteBatch)
        {
            Console.WriteLine(exitLocation.X + " " + exitLocation.Y);
            foreach (Sprite sprite in tileSprites)
            {
                sprite.Draw(spriteBatch);
            }

            exitSprite.Draw(spriteBatch);
        }
    }
}
