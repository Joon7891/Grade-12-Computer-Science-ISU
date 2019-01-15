// Author: Steven Ung
// File Name: CollisionTree.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 1/15/2018
// Modified Date: 1/15//2018
// Description: Tree that holds projectiles and effciently checks for collisions with rectangles
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
        private const int MAX_PROJECTILES = 5;

        private int depth;
        private Rectangle range;
        private List<Projectile> projectiles;
        private CollisionTree[] nodes;

        public CollisionTree(int level, Rectangle range)
        {
            this.depth = level;
            this.range = range;
            nodes = new CollisionTree[4];
            projectiles = new List<Projectile>();
        }

        private int Fit(Rectangle hitBox)
        {
            double halfWidth = range.X + range.Width / 2.0;
            double halfHeight = range.Y + range.Height / 2.0;

            bool fitLeft = halfWidth > hitBox.X + hitBox.Width;
            bool fitRight = halfWidth < hitBox.X;
            bool fitUp = halfHeight > hitBox.Y + hitBox.Height;
            bool fitDown = halfHeight < hitBox.Y;

            if(fitLeft && fitUp)
            {
                return 0;
            }
            if(fitLeft && fitDown)
            {
                return 3;
            }
            if (fitRight && fitUp)
            {
                return 1;
            }
            if (fitRight && fitDown)
            {
                return 2;
            }
            return -1;
        }

        private void Split() 
        {
            int x = range.X;
            int y = range.Y;
            int halfWidth = range.Width / 2;
            int halfHeight = range.Height / 2;

            nodes[0] = new CollisionTree(depth + 1, new Rectangle(x, y, halfWidth, halfHeight));
            nodes[1] = new CollisionTree(depth + 1, new Rectangle(x + halfWidth, y, halfWidth, halfHeight));
            nodes[2] = new CollisionTree(depth + 1, new Rectangle(x, y + halfHeight, halfWidth, halfHeight));
            nodes[3] = new CollisionTree(depth + 1, new Rectangle(x + halfWidth, y + halfHeight, halfWidth, halfHeight));
        }

        private void Clear()
        {
            projectiles.Clear();

            foreach(CollisionTree node in nodes)
            {
                node.Clear();
                nodes = null;
            }
        }

        private void Insert(Projectile newProjectile)
        {
            if(nodes[0] != null)
            {
                int i = Fit(newProjectile.GetRectangle());

                if (i == -1)
                {
                    projectiles.Add(newProjectile);
                }
                else
                {
                    nodes[i].Insert(newProjectile);
                    return;
                }
            }

            if(MAX_PROJECTILES < projectiles.Count && MAX_DEPTH > depth)
            {
                if (nodes[0] == null)
                {
                    Split();
                }
                foreach(Projectile projectile in projectiles)
                {
                    if (MAX_PROJECTILES > projectiles.Count) {
                        return;
                    }
                    int i = Fit(projectile.GetRectangle());
                    
                    if(i != -1)
                    {
                        nodes[i].Insert(projectile);
                        projectiles.Remove(projectile);
                    }
                }
                    
            }

        }

        public void Update(List<Projectile> projectiles)
        {
            Clear();

            foreach(Projectile projectile in projectiles)
            {
                Insert(projectile);
            }
        }

        private bool CheckCollision(Rectangle x, Rectangle y)
        {

        }

        private List<Projectile> ReturnCollisions(Rectangle hitBox, List<Projectile> collisions)
        {
            int i = Fit(hitBox);

            if(nodes[0] != null && i != 0)
            {
                nodes[i].ReturnCollisions(hitBox, collisions);
            }

            foreach(Projectile projectile in projectiles)
            {
                collisions.Add(projectile);
            }

            return collisions;
        }

        public List<Projectile> ReturnCollisions(Rectangle hitBox)
        {
            List<Projectile> collisions = ReturnCollisions(hitBox, new List<Projectile>());

            for(int i = collisions.Count; i >= 0; i--)
            {
                if(!CheckCollision(hitBox, collisions[i].GetRectangle()))
                {
                    collisions.Remove(collisions[i]);
                }
            }

            return collisions;
        }

    }
}
