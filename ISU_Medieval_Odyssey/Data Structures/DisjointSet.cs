// Author: Steven Ung
// File Name: DisjointSet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 1/15/2018
// Modified Date: 1/20/2018
// Description: Implementation of the disjoint set data structure using union by rank.
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    class DisjointSet
    {
        private List<int> parent;
        private List<int> rank;

        public DisjointSet(int size)
        {
            parent = new List<int>();
            rank = new List<int>();

            for(int i = 0; i <= size; i++)
            {
                parent.Add(i);
                rank.Add(0);
            }
        }

        /// <summary>
        /// Find the parent of the item
        /// </summary>
        /// <param name="v"> the item to find </param>
        /// <returns> v's parent </returns>
        public int Find(int v)
        {
            if (v == parent[v])
            {
                return v;
            }
            return parent[v] = Find(parent[v]);
        }

        /// <summary>
        /// Unions two sets together
        /// </summary>
        /// <param name="a"> an item in set 1 </param>
        /// <param name="b"> an item in set 2 </param>
        public void Union(int a, int b)
        {
            a = Find(a);
            b = Find(b);
            if (a != b)
            {
                if(rank[b] > rank[a])
                {
                    int temp = b;
                    b = a;
                    a = temp;
                }
                parent[b] = a;
                if(rank[a] == rank[b])
                {
                    rank[a]++;
                }
            }
        }
    }
}
