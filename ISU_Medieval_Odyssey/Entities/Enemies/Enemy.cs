// Author: Steven Ung, Joon Song
// File Name: Enemy.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 1/15/2018
// Modified Date: 1/15/2018
// Description: Class to hold Enemy object


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public abstract class Enemy : Entity
    {       

        /// <summary>
        /// Possible loot drops for this <see cref="Enemy"/>
        /// </summary>
        public List<Item> LootTable { get; }

        /// <summary>
        /// The amount of experience this <see cref="Enemy"/> drops/gives to <see cref="Player"/>
        /// </summary>
        public short Experience { get; }

        private Queue<Vector2Int> futureTiles;
        private Vector2Int nextTile;
        private float reevaluateTime;
        private float timeToReevaluate;

        public Enemy()
        {
            futureTiles = new Queue<Vector2Int>();
        }

        /// <summary>
        /// Update subprogram for <see cref="Enemy"/> objecy
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating time to reevaluate and reevaluating movement when approriate
            timeToReevaluate += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (timeToReevaluate >= reevaluateTime || futureTiles.Count == 0)
            {
                timeToReevaluate = 0;
                ReevaluateMovement();
            }

            // Calling subprogram to update movement
            UpdateMovement(gameTime);
        }

        protected virtual void ReevaluateMovement() { }

        /// <summary>
        /// Subprogram to update this <see cref="Enemy"/>'s movement
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        protected virtual void UpdateMovement(GameTime gameTime)
        {
            // Moving enemy in appropriate direction
            switch (Direction)
            {
                case Direction.Up:
                    break;
                case Direction.Right:
                    break;
                //case Direction

            }

        }

        /// <summary>
        /// Draw subprogram for <see cref="Enemy"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
