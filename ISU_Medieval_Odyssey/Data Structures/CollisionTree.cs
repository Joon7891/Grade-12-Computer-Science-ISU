using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey.Data_Structures
{
    class CollisionTree
    {
        private const int MAX_DEPTH = 5;

        private int level;
        private Rectangle range;
        private List<Projectile> projectiles;
        private CollisionTree[] nodes;

        public CollisionTree(int level, Rectangle range)
        {
            this.level = level;
            this.range = range;
            nodes = new CollisionTree[4];
            projectiles = new List<Projectile>();
        }

        private void Fit(Projectile projectile)
        {
            double halfWidth = range.X + range.Width / 2.0;
            double halfHeight = range.Y + range.Height / 2.0;
                                                                                                                                                                                                                                                                                                                                    

        }

        private void Split()
        {
            int x = range.X;
            int y = range.Y;
            int halfWidth = range.Width / 2;
            int halfHeight = range.Height / 2;

            nodes[1] = new CollisionTree(level + 1, new Rectangle(x, y, halfWidth, halfHeight));
            nodes[0] = new CollisionTree(level + 1, new Rectangle(x + halfWidth, y, halfWidth, halfHeight));
            nodes[2] = new CollisionTree(level + 1, new Rectangle(x, y + halfHeight, halfWidth, halfHeight));
            nodes[3] = new CollisionTree(level + 1, new Rectangle(x + halfWidth, y + halfHeight, halfWidth, halfHeight));
        }

        public void Clear()
        {
            projectiles.Clear();

            for(int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }

        }

        public void Insert(Projectile newProjectile)
        {

        }

        public void Update()
        {
            // clear and readd, return collisions
        }

    }
}
