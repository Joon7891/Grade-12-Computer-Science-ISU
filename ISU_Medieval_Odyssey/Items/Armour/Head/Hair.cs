// Author: Joon Song
// File Name: Hair.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold Hair object - default for no helmet

namespace ISU_Medieval_Odyssey
{
    public sealed class Hair : Head
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static MovementSpriteSheet movementSpriteSheet;

        /// <summary>
        /// Static constructor to setup various <see cref="Hair"/> components
        /// </summary>
        static Hair()
        {
            // Loading Hair graphics
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Head/Hair/", "hair");
        }

        /// <summary>
        /// Constructor for <see cref="Hair"/> object
        /// </summary>
        public Hair()
        {
            // Setting up Hair
            base.movementSpriteSheet = movementSpriteSheet;
        }
    }
}
