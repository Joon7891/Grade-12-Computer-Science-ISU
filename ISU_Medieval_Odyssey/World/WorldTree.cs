// Author: Steven Ung
// File Name: WorldTree.cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to optmize world loading
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    class WorldTree
    {
        /// <summary>
        /// How many chunks away from the player to render
        /// </summary>
        public const int RENDERDIST = 2; //TODO: move to settings?

        Dictionary<Vector2, HashSet<Vector2>> adjList;

        public void LoadWorld(Vector2 playerLoc)
        {
            Queue<Tuple<Vector2, int>> queue = new Queue<Tuple<Vector2, int>>();
            while (queue.Count > 1)
            {
                Vector2 cur = queue.Peek().Item1;
                int dist = queue.Peek().Item2;

                if (dist <= RENDERDIST)
                {
                    //draw and load chunk
                }
                else
                {
                    continue;
                }
            }
        }

        private void UpdateAdj(Vector2 playerLoc)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    Vector2 nextChunk = new Vector2(playerLoc.X + i, playerLoc.Y + j);
                    if (!adjList[playerLoc].Contains(nextChunk))
                    {
                        adjList[playerLoc].Add(nextChunk);
                    }
                }
            }
        }

        public void Update(Vector2 playerLoc) // *note: call only when player moves boundaries
        {
            for(int i = -RENDERDIST; i < RENDERDIST; i++)
            {
                for(int j = -RENDERDIST; j < RENDERDIST; j++)
                {
                    UpdateAdj(new Vector2(playerLoc.X + i, playerLoc.Y + j));
                }
            }
        }

        public void GetBoundary()
        {

        }
    }
}
