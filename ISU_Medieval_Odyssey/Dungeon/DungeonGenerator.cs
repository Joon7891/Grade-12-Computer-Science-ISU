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

        CollisionTree collisionTree;
        // The rooms of the dungeon
        List<Rectangle> rooms;

        // What region each tile is in, -1 means inpassable
        int[,] region;

        // The region that is currently being generated 
        int currentRegion;

        public DungeonGenerator()
        {
            collisionTree = new CollisionTree(0, new Rectangle(0, 0, MAX_WIDTH, MAX_HEIGHT));
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
            Random rng = new Random();
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

        private void ExpandMaze(Vector2Int current) // dfs style path finding to create maze
        {
            currentRegion++;
            Stack<Vector2Int> stack = new Stack<Vector2Int>();
            stack.Push(current);

            while(stack.Count > 0)
            {
                //foreach(Direction direction in Direction)
                //{

                //}
                current = stack.Pop();

                foreach(Vector2Int direction in MoveDirections)
                {
                    throw new NotImplementedException();
                    //if(current + direction * 3//is in bounds, and not connection regions)
                }
            }

        }

        private void CreateMazes()
        {
            for(int i = 1; i < MAX_WIDTH; i++)
            {
                for(int j = 1; j < MAX_HEIGHT; i++)
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
