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

        private void Split()
        {

        }

        public void Clear()
        {

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
