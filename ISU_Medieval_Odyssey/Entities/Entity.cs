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
    public abstract class Entity : ICollidable
    {
        public int X
        {
            get => rectangle.X;
            set => rectangle.X = value;
        }

        public int Y
        {
            get => rectangle.Y;
            set => rectangle.Y = value;
        }

        /// <summary>
        /// The center of this <see cref="Entity"/>
        /// </summary>
        public Vector2Int Center => center;
        protected Vector2Int center = Vector2Int.Zero;

        /// <summary>
        /// A cartesian intergral vector representing this <see cref="Entity"/>'s current tile coordinates
        /// </summary>
        public Vector2Int CurrentTile { get; protected set; }

        /// <summary>
        /// A cartesian intergral vector representing this <see cref="Entity"/>'s current chunk coordinates
        /// </summary>
        public Vector2Int CurrentChunk => World.TileToChunkCoordinate(CurrentTile);

        /// <summary>
        /// This <see cref="Entity"/>'s colission rectangle
        /// </summary>
        public Rectangle CollisionRectangle => collisionRectangle;
        protected Rectangle collisionRectangle;

        /// <summary>
        /// The health of the <see cref="Entity"/>
        /// </summary>
        public int Health
        {
            get => healthBar.CurrentValue;
            set => healthBar.CurrentValue = (short) Math.Max(Math.Min(healthBar.MaxValue, value), 0);
        }
        protected NumberBar healthBar;

        /// <summary>
        /// An integer value that represents the speed at which the <see cref="Entity"/> can move, in tiles/second
        /// </summary>
        public int Speed { get; protected set; }

        /// <summary>
        /// The amount of gold (currency) that the <see cref="Entity"/> has
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// The current direction the <see cref="Entity"/> is facing
        /// </summary>
        public Direction Direction { get; protected set; }

        // The entity's rounded and unrounded location/rectangle
        protected Vector2 unroundedLocation;
        protected Rectangle rectangle;
    }
}
