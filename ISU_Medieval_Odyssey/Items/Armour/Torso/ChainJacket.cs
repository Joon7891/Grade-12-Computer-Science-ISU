// Author: Joon Song
// File Name: ChainJacket.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold ChainJacket object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ChainJacket : Torso
    {
        // ChainJacket specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 4;
        private const int MAX_DEFENSE = 7;
        private const int MIN_DURABILITY = 20;
        private const int MAX_DURABILITY = 35;

        /// <summary>
        /// Static constructor to setup various <see cref="ChainJacket"/> components
        /// </summary>
        static ChainJacket()
        {
            // Loading in various ChainJacket images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Torso/ChainJacket/", "chainJacket");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/chainJacketIcon");
        }
        
        /// <summary>
        /// Constructor for <see cref="ChainJacket"/> object
        /// </summary>
        public ChainJacket() : base(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY, movementImages, iconImage) { }
    }
}
