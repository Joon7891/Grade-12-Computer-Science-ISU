using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    class Dungeon : IBuilding
    {
        private const int TILE_SIZE = Tile.SPACING;
        private Vector2Int TILE_SPACING = new Vector2Int(TILE_SIZE,TILE_SIZE);
        private const int INSIDE_WIDTH = 50;
        private const int INSIDE_HEIGHT = 50;
        private const int OUTSIDE_WIDTH = 1;
        private const int OUTSIDE_HEIGHT = 1;

        public Rectangle Rectangle { get; set; }
        public Vector2Int CornerTile { get; }

        private List<Vector2Int> insideObstructionLocs = new List<Vector2Int>();
        private List<Vector2Int> outsideObstructionLocs = new List<Vector2Int>();
        private Vector2Int enterLocation;
        private Vector2Int exitLocation;

        static Texture2D tile;
        List<Sprite> tileSprites;

        static Dungeon()
        {
            tile = Main.Content.Load<Texture2D>("Images/Sprites/Tiles/tileStone");
        }

        public Dungeon(Vector2Int cornerTile)
        {
            DungeonGenerator generator = new DungeonGenerator();
            int[,] layout = generator.GenerateDungeon();

            tileSprites = new List<Sprite>();

            // Setting up inside obstruction tiles and sprites
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
                        currentTile *= TILE_SPACING;
                        tileSprites.Add(new Sprite(tile, new Rectangle(currentTile.X, currentTile.Y, TILE_SIZE, TILE_SIZE)));
                        tileSprites.Add(new Sprite(tile, new Rectangle(currentTile.X, currentTile.Y + TILE_SIZE/2, TILE_SIZE, TILE_SIZE)));
                    }
                }
            }

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

            //World.Instance.GetTileAt(CornerTile + enterLocation).OutsideObstructState = true;

            World.Instance.GetTileAt(exitLocation).OnInteractProcedure = new Interaction(Direction.Down, (player) =>
            {
                World.Instance.IsInside = false;
                World.Instance.CurrentBuilding = null;
            });

            World.Instance.GetTileAt(enterLocation).OnInteractProcedure = new Interaction(Direction.Up, (player) =>
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
            Console.WriteLine("up");
        }

        /// <summary>
        /// Subprogram to draw the outside of the <see cref="Dungeon"/>
        /// </summary>
        /// <param name="spriteBatch"> Spritebatch to draw sprites </param>
        public void DrawOutside(SpriteBatch spriteBatch)
        {
            Console.WriteLine(enterLocation.X + " " + enterLocation.Y);
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
        }
    }
}
