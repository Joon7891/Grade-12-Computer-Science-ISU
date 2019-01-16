// Author: Joon Song, Steven Ung
// File Name: Entity.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/22/2018
// Modified Date: 01/05/2019
// Description: Static class to hold shared entity data

using Microsoft.Xna.Framework;
using System;

namespace ISU_Medieval_Odyssey
{
    public abstract class Entity
    {
        /// <summary>
        /// The center of this <see cref="Entity"/>
        /// </summary>
        public Vector2Int Center { get; protected set; }

        /// <summary>
        /// A cartesian intergral vector representing this <see cref="Entity"/>'s current tile coordinates
        /// </summary>
        public Vector2Int CurrentTile { get; protected set; }

        /// <summary>
        /// A cartesian vector representing this <see cref="Entity"/>'s exact position
        /// </summary>
        protected Vector2 unroundedLocation;

        /// <summary>
        /// A cartesian intergral vector representing this <see cref="Entity"/>'s current chunk coordinates
        /// </summary>
        public Vector2Int CurrentChunk
        { 
            get
            {
                int x = (int)Math.Floor(CurrentTile.X / (float) Chunk.SIZE);
                int y = (int)Math.Floor(CurrentTile.Y / (float)Chunk.SIZE);
                return new Vector2Int(x, y);
            }
        }

        /// <summary>
        /// This <see cref="Entity"/>'s colission rectangle
        /// </summary>
        public Rectangle CollisionRectangle; // Fix...

        /// <summary>
        /// The health of the <see cref="Entity"/>
        /// </summary>
        public int Health
        {
            get => healthBar.CurrentValue;
            set => healthBar.CurrentValue = Math.Max(Math.Min(healthBar.MaxValue, value), 0);
        }
        protected NumberBar healthBar;

        /// <summary>
        /// An integer value that represents the speed at which the <see cref="Entity"/> can move, in tiles/second
        /// </summary>
        public int Speed { get; protected set; } = 3;

        /// <summary>
        /// The amount of gold (currency) that the <see cref="Entity"/> has
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// The current direction the <see cref="Entity"/> is facing
        /// </summary>
        public Direction Direction { get; protected set; }
    }
}
