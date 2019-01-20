// Author: Joon Song, Steven Ung
// File Name: Entity.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/22/2018
// Modified Date: 01/05/2019
// Description: Static class to hold shared entity data

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ISU_Medieval_Odyssey
{
    public abstract class Entity : ICollidable
    {
        /// <summary>
        /// The x-coordinate of this <see cref="Entity"/>
        /// </summary>
        public int X
        {
            get => rectangle.X;
            set => unroundedLocation.X = value;
        }

        /// <summary>
        /// The y-coordinate of this <see cref="Entity"/>
        /// </summary>
        public int Y
        {
            get => rectangle.Y;
            set => unroundedLocation.Y = value;
        }

        /// <summary>
        /// The center of this <see cref="Entity"/>
        /// </summary>
        public Vector2Int Center => center;
        protected Vector2Int center = Vector2Int.Zero;
        protected Vector2Int groundCoordinate = Vector2Int.Zero;

        /// <summary>
        /// A cartesian intergral vector representing this <see cref="Entity"/>'s current tile coordinates
        /// </summary>
        public Vector2Int CurrentTile => World.PixelToTileCoordinate(groundCoordinate);

        /// <summary>
        /// A cartesian intergral vector representing this <see cref="Entity"/>'s current chunk coordinates
        /// </summary>
        public Vector2Int CurrentChunk => World.TileToChunkCoordinate(CurrentTile);

        /// <summary>
        /// This <see cref="Entity"/>'s colission rectangle
        /// </summary>
        public Rectangle HitBox => hitBox;
        protected Rectangle hitBox;

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
        /// Whether this <see cref="Entity"/> is alive
        /// </summary>
        public bool Alive => Health > 0;

        /// <summary>
        /// An integer value that represents the speed at which the <see cref="Entity"/> can move, in tiles/second
        /// </summary>
        public float Speed { get; protected set; }

        /// <summary>
        /// The amount of gold (currency) that the <see cref="Entity"/> has
        /// </summary>
        public virtual int Gold { get; set; }

        /// <summary>
        /// The current direction the <see cref="Entity"/> is facing
        /// </summary>
        public Direction Direction { get; protected set; }

        // The entity's rounded and unrounded location/rectangle
        protected Vector2 unroundedLocation;
        protected Rectangle rectangle;

        // Objects/variables related to the entity's mini-icon
        protected Circle miniIcon;
        protected const int MINI_ICON_RADIUS = 50;

        /// <summary>
        /// Subprogram to draw the mini-version of this <see cref="Entity"/> for the minimap
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void DrawMini(SpriteBatch spriteBatch)
        {
            // Drawing mini version of entity
            miniIcon.Draw(spriteBatch);
        }
    }
}
