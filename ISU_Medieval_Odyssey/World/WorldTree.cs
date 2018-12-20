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

namespace ISU_Medieval_Odyssey.World
{
    class WorldTree
    {
        const int RENDERDIST = 2; //TODO: MOVE TO SHARED CONSTANTS

        Dictionary<Vector2, HashSet<Vector2>> adjList;

        public void RenderWorld(Vector2 playerLoc)
        {
            Queue<Tuple<Vector2,int> > BFSQ = new Queue<Tuple<Vector2, int>>();
            while (BFSQ.Count > 1)
            {
                Vector2 cur = BFSQ.Peek().Item1;
                int dist = BFSQ.Peek().Item2;
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
            // TODO: create connections 
        }

        public void Update(Vector2 playerLoc) // *note: call only when player moves boundaries
        {
            for(int i = -RENDERDIST; i < RENDERDIST; i++)
            {
                for(int j = -RENDERDIST; i < RENDERDIST; i++)
                {
                    UpdateAdj(new Vector2(playerLoc.X + i, playerLoc.Y + j ));
                }
            }
        }

        public void GetBoundary()
        {

        }
    }
}
