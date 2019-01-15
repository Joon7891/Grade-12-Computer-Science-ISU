// Author: Joon Song, Steven Ung
// File Name: Entity.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/22/2018
// Modified Date: 01/05/2019
// Description: Static class to hold shared entity data
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public abstract class Entity
    {
        /// <summary>
        /// The center of the entity
        /// </summary>
        public Vector2Int Center { get; protected set; }

        /// <summary>
        /// A cartesian intergral vector representing the player's current tile coordinates
        /// </summary>
        public Vector2Int CurrentTile { get; protected set; }

        /// <summary>
        /// A cartesian intergral vector representing the player's current chunk coordinates
        /// </summary>
        public Vector2Int CurrentChunk { get; protected set; }

        public Rectangle colisionRectangle;

        /// <summary>
        /// The name of the <see cref="Entity"/>
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The current level of the <see cref="Entity"/>
        /// </summary>
        public byte Level { get; protected set; }

        /// <summary>
        /// The amount of experience that the <see cref="Entity"/> has
        /// </summary>
        public int Experience
        {
            get => experienceBar.CurrentValue;
            set => experienceBar.CurrentValue = Math.Min(experienceBar.MaxValue, value);         //THIS ISN'T RIGHT, should carry onto next level
        }

        protected NumberBar experienceBar;

        /// <summary>
        /// The health of the <see cref="Entity"/>
        /// </summary>
        public int Health
        {
            get => healthBar.CurrentValue;
            set => healthBar.CurrentValue = Math.Min(healthBar.MaxValue, value);
        }

        public int MaxHealth => healthBar.MaxValue;

        /// <summary>
        /// An integer value that represents the speed at which the <see cref="Entity"/> can move, in tiles/second
        /// </summary>
        public int Speed { get; protected set; } = 3;

        protected NumberBar healthBar;

        /// <summary>
        /// The amount of gold (currency) that the <see cref="Entity"/> has
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// The current direction of the player
        /// </summary>
        public Direction Direction { get; protected set; }
    }
}
