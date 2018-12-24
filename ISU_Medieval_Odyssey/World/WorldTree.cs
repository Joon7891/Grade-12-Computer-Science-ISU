// Author: Steven Ung
// File Name: WorldTree.cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to optmize world loading

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    class WorldTree
    {
        /// <summary>
        /// How many chunks away from the player to render
        /// </summary>
        public const int RENDERDIST = 20; //TODO: move to settings?

        Dictionary<Vector2Int, HashSet<Vector2Int>> adjList = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

        public HashSet<Vector2Int> GetLoadedChunks(Vector2Int playerLoc)
        {
            HashSet<Vector2Int> chunksToLoad = new HashSet<Vector2Int>();

            Queue<Vector2Int> posQueue = new Queue<Vector2Int>();
            Queue<int> distQueue = new Queue<int>();
            posQueue.Enqueue(playerLoc);
            distQueue.Enqueue(1);

            while (posQueue.Count > 0)
            {
                Vector2Int cur = posQueue.Dequeue();
                int dist = distQueue.Dequeue();

                if (dist <= RENDERDIST)
                {
                    chunksToLoad.Add(cur);
                    foreach (Vector2Int next in adjList[cur])
                    {
                        if (!chunksToLoad.Contains(next) && !posQueue.Contains(next))
                        {
                            posQueue.Enqueue(next);
                            distQueue.Enqueue(dist + 1);
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
            return chunksToLoad;
        }

        private void UpdateAdj(Vector2Int playerLoc)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    Vector2Int nextChunk = new Vector2Int(playerLoc.X + i, playerLoc.Y + j);

                    if (!adjList.ContainsKey(playerLoc))
                    {
                        adjList[playerLoc] = new HashSet<Vector2Int>();
                    }

                    if (!adjList[playerLoc].Contains(nextChunk))
                    {
                        adjList[playerLoc].Add(nextChunk);
                    }
                }
            }
        }

        public void Update(Vector2Int playerLoc) // *note: call only when player moves boundaries
        {
            for(int i = -RENDERDIST; i < RENDERDIST; i++)
            {
                for(int j = -RENDERDIST; j < RENDERDIST; j++)
                {
                    UpdateAdj(new Vector2Int(playerLoc.X + i, playerLoc.Y + j));
                }
            }
        }

        public void GetBoundary()
        {

        }
    }
}
