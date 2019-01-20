using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    class DisjointSet
    {
        private List<int> parent;
        private List<int> rank;

        public DisjointSet(int size)
        {
            parent = new List<int>(size+1);
            rank = new List<int>(size+1);

            for(int i = 0; i <= size; i++)
            {
                parent.Add(i);
                rank.Add(0);
            }
        }


        public int Find(int v)
        {
            if (v == parent[v])
            {
                return v;
            }
            return parent[v] = Find(parent[v]);
        }

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
