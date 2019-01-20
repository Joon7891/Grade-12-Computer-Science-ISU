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

        public Dungeon(Vector2Int cornerTile)
        {
            DungeonGenerator generator = new DungeonGenerator();
            int[,] layout = generator.GenerateDungeon();

            // Setting up inside obstruction tiles
            for (int i = 0; i < INSIDE_WIDTH; i++)
            {
                for(int j = 0; j < INSIDE_HEIGHT; j++)
                {
                    if(layout[i,j] == -1)
                    {
                        insideObstructionLocs.Add(new Vector2Int(i, j));
                    }
                }
            }

            // put enter at top left, exit at bottom right
            for (int i = 0; i < INSIDE_HEIGHT; i++)
            {
                for (int j = 0; j < INSIDE_WIDTH; j++)
                {
                    if (layout[i, j] != -1)
                    {
                        enterLocation = (new Vector2Int(i, j));
                        break;
                    }
                }
            }

            for (int i = INSIDE_HEIGHT; i >= 0; i--)
            {
                for (int j = INSIDE_WIDTH; j >= 0; j--)
                {
                    if (layout[i, j] != -1)
                    {
                        exitLocation = (new Vector2Int(i, j));
                        break;
                    }
                }
            }

            CornerTile = cornerTile;
            SetTiles(cornerTile);
        }

        /// <summary>
        /// Setup world tiles
        /// </summary>
        /// <param name="cornerTile"></param>
        public void SetTiles(Vector2Int cornerTile)
        {
            for (int i = 0; i < insideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(cornerTile + insideObstructionLocs[i]).InsideObstructState = true;
            }

            for (int i = 0; i < outsideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(cornerTile + outsideObstructionLocs[i]).OutsideObstructState = true;
            }

            World.Instance.GetTileAt(cornerTile + exitLocation).OnInteractProcedure = new Interaction(Direction.Down, (player) =>
            {
                World.Instance.IsInside = false;
                World.Instance.CurrentBuilding = null;
            });

            World.Instance.GetTileAt(cornerTile + enterLocation).OnInteractProcedure = new Interaction(Direction.Up, (player) =>
            {
                World.Instance.IsInside = true;
                World.Instance.CurrentBuilding = this;
            });
        }


        /// <summary>
        /// Update subprogram for this <see cref="Shop"/>
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
            Console.WriteLine("in");
        }



    }
}
