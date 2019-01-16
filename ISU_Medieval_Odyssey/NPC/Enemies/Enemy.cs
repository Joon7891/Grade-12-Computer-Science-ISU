// Author: Steven Ung
// File Name: Player.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 1/15/2018
// Modified Date: 1/15/2018
// Description: Class to hold Enemy object
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

        /// <summary>
        /// Represents whether or not the enemy is aggroed on the player
        /// </summary>
        public bool Aggro { get; protected set; }
        
        /// <summary>
        /// Moves the enemy by an amount in a direction. Kept seperate for external movments(i.e. knockback)
        /// </summary>
        /// <param name="direction"> The direction to move in </param>
        /// <param name="amount"> The amount of coordinate values to move </param>
        public void Move(Direction direction, int amount)
        {
            switch (direction)
            {
                case Direction.Up:
                    {
                        unroundedLocation.Y -= amount;
                        break;
                    }
                case Direction.Down:
                    {
                        unroundedLocation.Y += amount;
                        break;
                    }
                case Direction.Left:
                    {
                        unroundedLocation.X -= amount;
                        break;
                    }
                case Direction.Right:
                    {
                        unroundedLocation.X += amount;
                        break;
                    }
            }
        }
    }
}
