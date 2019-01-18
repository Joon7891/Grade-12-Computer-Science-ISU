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
        /// Possible loot drops for this enemy
        /// </summary>
        public List<Item> LootTable { get; set; }

        protected DirectionalSpriteSheet directionalSpriteSheet;
        protected int numFrames;
        private int currentFrame = 0;
        protected int counterMax;
        private int animationCounter = 0;


        private float attackSpeed = 1.5f;
        private float timeToAttack = 0;
        private int attackAmount = 10;

        /// <summary>
        /// Subprogram to setup various <see cref="Enemy"/> components - used in derived constructors
        /// </summary>
        /// <param name="tileCoordinate">The initial tile coordinate of this <see cref="Enemy"/></param>
        protected void Setup(Vector2Int tileCoordinate)
        {
            rectangle.X = (int)((tileCoordinate.X + 0.5) * Tile.SPACING - rectangle.Width);
            rectangle.Y = (int)((tileCoordinate.Y + 0.5) * Tile.SPACING - rectangle.Height);
            center.X = rectangle.X + rectangle.Width / 2;
            center.Y = rectangle.Y + rectangle.Height / 2;            
        }

        /// <summary>
        /// Update subprogram for <see cref="Enemy"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public virtual void Update(GameTime gameTime, Player player)
        {
            // If enemy is within a tile to the player, attack, otherwise move towards player
            if ((Center - Player.Instance.Center).Length() <= Tile.SPACING)
            {
                Attack(gameTime, player);
            }
            else
            {
                UpdateMovement(gameTime, player);
            }
        }

        /// <summary>
        /// Draw subprogram for <see cref="Enemy"/> object
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Drawing enemy at in appropriate direction and frame
            directionalSpriteSheet.Draw(spriteBatch, Direction, currentFrame, rectangle);
        }

        /// <summary>
        /// Subprogram to update the <see cref="Enemy"/>'s movement
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="player">The player the <see cref="Enemy"/> is suppose to target</param>
        protected virtual void UpdateMovement(GameTime gameTime, Player player)
        {

        }

        /// <summary>
        /// Subprogram for this <see cref="Enemy"/> to attack a <see cref="Plane"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="player">The <see cref="Player"/> to attack</param>
        protected virtual void Attack(GameTime gameTime, Player player)
        {
            // Updating the time left for the enemy to attack
            timeToAttack += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            
            // Inflicting damage at the enemy's attack speed
            if (timeToAttack > attackSpeed)
            {
                player.Health -= attackAmount;
                timeToAttack = 0;
            }
        }
    }
}
