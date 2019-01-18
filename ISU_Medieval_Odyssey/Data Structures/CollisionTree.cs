// Author: Steven Ung, Joon Song
// File Name: CollisionTree.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 1/15/2018
// Modified Date: 1/15//2018
// Description: Tree that holds projectiles and effciently checks for collisions with rectangles

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace ISU_Medieval_Odyssey
{
    public sealed class CollisionTree
    {
        /// <summary>
        /// The <see cref="Rectangle"/> representing the range of this <see cref="CollisionTree"/>
        /// </summary>
        public Rectangle Range { get; set; }

        // Various constants to help with collision tree logic
        private const int MAX_DEPTH = 5;
        private const int MAX_COLLIDABLES = 5;

        // Various variables regarding the tree's position in the tree and what its searching for
        private int depth;
        private CollisionTree[] subtrees;

        /// <summary>
        /// Constructor for <see cref="CollisionTree"/> object
        /// </summary>
        /// <param name="depth">The current depth of this <see cref="CollisionTree"/> with respect to the root</param>
        /// <param name="range">The <see cref="Rectangle"/></param>
        public CollisionTree(int depth, Rectangle range)
        {
            // Setting up class attribites
            Range = range;
            this.depth = depth;
            subtrees = new CollisionTree[4];
        }

        /// <summary>
        /// Subprogarm to clear/reset this <see cref="CollisionTree"/>
        /// </summary>
        private void Clear()
        {
            // Resetting children nodes
            for (int i = 0; i < subtrees.Length; i++)
            {
                subtrees[i]?.Clear();
                subtrees[i] = null;
            }
        }

        /// <summary>
        /// Subprogram to determine the amount of colissions between a hitbox and a list of collidable objects
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="ICollidable"/></typeparam>
        /// <param name="hitBox">A <see cref="Rectangle"/> representing the refernce hitbox</param>
        /// <param name="collidableObjects">A list of objects the hitbox could collide with</param>
        /// <returns>The </returns>
        public List<T> GetCollisions<T>(Rectangle hitBox, List<T> collidableObjects) where T : ICollidable
        {            
            // A list representing the colissions between a hitbox and the colliable objects
            List<T> collisions = new List<T>();
            Quadrant subTreeQuadrant;

            // Base-case - there are no objects to check for collision
            if (collidableObjects.Count == 0)
            {
                return collidableObjects;
            }

            // Determining subtree quadrant
            subTreeQuadrant = GetQuadrant(hitBox);

            // If quadrant is valid and there are too many collidables
            if (subTreeQuadrant != Quadrant.None && collidableObjects.Count > MAX_COLLIDABLES)
            {
                collisions = GetSubtreeContainments(subTreeQuadrant, hitBox, collidableObjects);
                return subtrees[(int)(subTreeQuadrant)].GetCollisions(hitBox, collisions);
            }
            else
            {
                // Adding collidable to collided objects if it collides with hitbox
                for (int i = 0; i < collidableObjects.Count; ++i)
                {
                    if (collidableObjects[i].CollisionRectangle.Intersects(hitBox))
                    {
                        collisions.Add(collidableObjects[i]);
                    }
                }

                // Returning the collisions and clearing subtrees
                Clear();
                return collisions;
            }
        }

        private List<T> GetSubtreeContainments<T>(Quadrant subTreeQuadrant, Rectangle hitBox, List<T> collidableObjects) where T : ICollidable
        {
            // List of the collidable objects that are contained in a givne quadrant
            List<T> subTreeContainments = new List<T>();
            
            // Switch-Case to add appropriate 
            switch (subTreeQuadrant)
            {
                case Quadrant.TopRight:

                    // Adding collidables in top right quadrant
                    for (int i = 0; i < collidableObjects.Count; ++i)
                    {
                        if (subtrees[0].Range.Left <= collidableObjects[i].CollisionRectangle.Right && subtrees[0].Range.Bottom >= collidableObjects[i].CollisionRectangle.Top)
                        {
                            subTreeContainments.Add(collidableObjects[i]);
                        }
                    }

                    break;

                case Quadrant.TopLeft:

                    // Adding collidables in top left quadrant
                    for (int i = 0; i < collidableObjects.Count; ++i)
                    {
                        if (subtrees[1].Range.Right >= collidableObjects[i].CollisionRectangle.Left && subtrees[1].Range.Bottom >= collidableObjects[i].CollisionRectangle.Top)
                        {
                            subTreeContainments.Add(collidableObjects[i]);
                        }
                    }

                    break;

                case Quadrant.BottomLeft:

                    // Adding collidables in bottom left quadrant
                    for (int i = 0; i < collidableObjects.Count; ++i)
                    {
                        if (subtrees[2].Range.Top <= collidableObjects[i].CollisionRectangle.Bottom && subtrees[2].Range.Right >= collidableObjects[i].CollisionRectangle.Left)
                        {
                            subTreeContainments.Add(collidableObjects[i]);
                        }
                    }
                    break;

                case Quadrant.BottomRight:

                    // Adding collidables in bottom right quadrant
                    for (int i = 0; i < collidableObjects.Count; ++i)
                    {
                        if (subtrees[3].Range.Left <= collidableObjects[i].CollisionRectangle.Right && subtrees[3].Range.Top <= collidableObjects[i].CollisionRectangle.Bottom)
                        {
                            subTreeContainments.Add(collidableObjects[i]);
                        }
                    }

                    break;
            }

            return subTreeContainments;
        }

        /// <summary>
        /// Subprogram to obtain the <see cref="Quadrant"/> that the hitbox is contained in
        /// </summary>
        /// <param name="hitBox">The hitbox of the collision tree</param>
        /// <returns>The <see cref="Quadrant"/> that the hotbox is in</returns>
        private Quadrant GetQuadrant(Rectangle hitBox)
        {
            // Building subtrees
            BuildSubtrees();

            // Returning quadrant in which rectangle is contained
            for (byte i = 0; i < subtrees.Length; ++i)
            {
                if (subtrees[i].Range.Contains(hitBox))
                {
                    return (Quadrant)i;
                }
            }

            // Otherwise return none
            return Quadrant.None;
        }

        /// <summary>
        /// Subprogram to setup this <see cref="CollisionTree"/>'s subtrees
        /// </summary>
        private void BuildSubtrees()
        {
            // Various rectangle/related variables
            int x = Range.X;
            int y = Range.Y;
            int halfWidth = Range.Width / 2;
            int halfHeight = Range.Height / 2;

            // Setting up subtrees
            subtrees[0] = new CollisionTree(depth + 1, new Rectangle(x + halfWidth, y, halfWidth, halfHeight));
            subtrees[1] = new CollisionTree(depth + 1, new Rectangle(x, y, halfWidth, halfHeight));
            subtrees[2] = new CollisionTree(depth + 1, new Rectangle(x, y + halfHeight, halfWidth, halfHeight));
            subtrees[3] = new CollisionTree(depth + 1, new Rectangle(x + halfWidth, y + halfHeight, halfWidth, halfHeight));
        }        
    }                                                                
}
