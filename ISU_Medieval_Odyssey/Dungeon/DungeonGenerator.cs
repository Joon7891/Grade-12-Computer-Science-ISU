/* Author: Steven Ung
// File Name: DungeonGenerator.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/15/2019
// Modified Date: 01/15/2019
// Description: Room and passage-dungeon generation using the following steps:
1) Place x rooms with random size and position - with no overlaps
2) Fill remaining space with mazes
3) Every tiles between two unconnected region is marked
4) randomly chose marked tiles and make them open
5) remove dead ends by removing every tile that touches 3 walls.
*/

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    class DungeonGenerator
    {
        const int ROOM_ATTEMPTS = 500;
        const int SIZE_MODIFIER = 0;
        const int MAX_WIDTH = 501;
        const int MAX_HEIGHT = 501;
        
        /// <summary>
        /// Indicates the extra chance that the maze will continue in the same direction 
        /// </summary>
        const int DIRECTION_CHANCE = 0;

        /// <summary>
        /// Indicates the extra chance that the maze will have more than one connector for any two+ regions
        /// </summary>
        const int CONNECTION_CHANCE = 30;

        /// <summary>
        /// Direction vector2ints that can be added to another vector2int to move in that direction
        /// Using same numbering as the direction enum.
        /// </summary>
        private readonly Vector2Int[] MoveDirections =
        {
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0)
        };

        Random rng;
        CollisionTree collisionTree;
        // The rooms of the dungeon
        List<Rectangle> rooms;

        // What region each tile is in, -1 means inpassable
        int[,] region;

        // The region that is currently being generated 
        int currentRegion;

        public DungeonGenerator()
        {
            rng = new Random();
            collisionTree = new CollisionTree(new Rectangle(0, 0, MAX_WIDTH, MAX_HEIGHT));
            region = new int[MAX_WIDTH, MAX_HEIGHT];
            currentRegion = -1;

            for(int i = 0; i < MAX_WIDTH; i++)
            {
                for(int j = 0; j < MAX_HEIGHT; i++)
                {
                    region[i, j] = -1;
        
                }
            }
        }

        private void GenerateRooms()
        {
            for(int i = 0; i < ROOM_ATTEMPTS; i++)
            {
                // ensure room dimensions are odd
                int size = rng.Next(1, 3) * 2 + 1;
                int width = size;
                int height = size;

                // make the room less perfect
                if(rng.Next(0,2) == 0)
                {
                    width += 2 * rng.Next(0, 1 + size / 2);
                }
                else
                {
                    height += 2 * rng.Next(0, 1 + size / 2);
                }

                // align room with odd coordinate
                int x = rng.Next(0, MAX_HEIGHT) * 2 + 1;
                int y = rng.Next(0, MAX_WIDTH) * 2 + 1;

                Rectangle room = new Rectangle(x, y, width, height);

                foreach(Rectangle rectangle in rooms) // TODO: collision for rooms 
                {
                    throw new NotImplementedException();
                    //collisionTree.Update(rooms);

                    // if overlap, continue
                }

                currentRegion++;

                for(int j = room.X; j <= room.X + room.Width; j++)
                {
                    for(int k = room.Y; k <= room.Y + room.Height; k++)
                    {
                        region[i,j] = currentRegion;
                    }
                }
            }

        }

        private void ChangeRegion(Vector2Int cur, Vector2Int offset)
        {
            region[(offset + cur).X, (offset + cur).Y] = currentRegion;
        }

        private void ExpandMaze(Vector2Int current) // dfs style path finding to create maze
        {
            currentRegion++;

            region[current.X, current.Y] = currentRegion;

            int lastDirection = -1;
            Stack<Vector2Int> stack = new Stack<Vector2Int>();
            stack.Push(current);

            while(stack.Count > 0)
            {
                current = stack.Peek();
                List<int> openDirections = new List<int>(); 

                for(int i = 0; i < 4; i++)
                {
                    if(region[(MoveDirections[i] * 2 + current).X, (MoveDirections[i] * 2 + current).Y] == -1)
                    {
                        openDirections.Add(i);
                    }
                }

                if (openDirections.Count > 0)
                {
                    int nextDirection = -1;

                    if (openDirections.Contains(lastDirection))
                    {
                        if (rng.Next(0, 101) > DIRECTION_CHANCE)
                        {
                            nextDirection = lastDirection;
                        }
                    }
                    else
                    {
                        nextDirection = openDirections[rng.Next(0, openDirections.Count)];
                    }

                    // create path twice in the chosen direction, because rooms and initial paths can only be odd-numbered
                    ChangeRegion(current, MoveDirections[nextDirection]);
                    ChangeRegion(current, MoveDirections[nextDirection] * 2);

                    stack.Push(current + MoveDirections[nextDirection] * 2);
                    lastDirection = nextDirection;
                }
                else
                {
                    lastDirection = -1;
                    stack.Pop();
                }
            }

        }

        private void ConnectRegions()
        {
            // create disjoint set for spanning tree
            DisjointSet disjointSet = new DisjointSet(rooms.Count);

            // List of all tiles that are currently impassible and touch 2+ regions
            List<Vector2Int> connectors = new List<Vector2Int>();
            for(int i = 1; i < MAX_WIDTH-1; i++)
            {
                for(int j = 1; j < MAX_HEIGHT-1; j++)
                {
                    if (region[i, j] == -1)
                    {
                        List<int> regions = new List<int>();
                        Vector2Int current = new Vector2Int(i, j);
                        
                        for(int k = 0; k < 4; k++)
                        {
                            Vector2Int check = current + MoveDirections[k];
                            if(!regions.Contains(region[check.X, check.Y])){
                                regions.Add(region[check.X, check.Y]);
                            }
                        }

                        if(regions.Count >= 2)
                        {
                            connectors.Add(current);
                            // add disjoint set connections here //////////////////////////////////////////////////////
                        }
                    }
                }
            }

            int mainRoom = rng.Next(0, rooms.Count);
            
            //foreach()
            







        }

        private void CreateMazes()
        {
            for(int i = 1; i < MAX_WIDTH; i+=2)
            {
                for(int j = 1; j < MAX_HEIGHT; j+=2)
                {
                    if (region[i,j] == -1)  
                    {
                        ExpandMaze(new Vector2Int(i,j));
                    }
                }
            }
        }


    }
}
