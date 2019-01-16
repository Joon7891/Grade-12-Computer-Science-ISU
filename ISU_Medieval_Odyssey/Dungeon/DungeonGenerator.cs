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
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    class DungeonGenerator
    {
        const int ROOM_ATTEMPTS = 500;
        const int SIZE_MODIFIER = 0;
        const int MAX_WIDTH = 0;
        const int MAX_HEIGHT = 0;

        // The rooms of the dungeon
        List<Rectangle> rooms;

        // What region each tile is in, -1 means inpassable
        int[,] region;

        // The region that is currently being generated 
        int currentRegion;

        public DungeonGenerator()
        {
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
    }
}
