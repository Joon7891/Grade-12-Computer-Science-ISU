// Author: Joon Song
// File Name: Interaction.cs
// Projec Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold Interaction object

using Newtonsoft.Json;

namespace ISU_Medieval_Odyssey
{
    public sealed class Interaction
    {
        /// <summary>
        /// The procedure to execute when the player chooses to interact on a given tile
        /// </summary>
        /// <param name="player">The <see cref="Player"/> interacting with the tile</param>
        public delegate void OnInteract(Player player);

        // The required direction for this interaction to be invoked
        [JsonProperty]
        private readonly Direction requiredDirection;

        // The interaction to be invoked
        [JsonProperty]
        private readonly OnInteract onInteract;

        /// <summary>
        /// Constructor for <see cref="Interaction"/> object
        /// </summary>
        /// <param name="requiredDirection">The required <see cref="Direction"/> for this <see cref="Interaction"/> to be invoked</param>
        /// <param name="onInteract">The <see cref="OnInteract"/> to be invoked</param>
        public Interaction(Direction requiredDirection, OnInteract onInteract)
        {
            // Setting class attributes
            this.requiredDirection = requiredDirection;
            this.onInteract = onInteract;
        }

        /// <summary>
        /// Subprogram to invoke this <see cref="Interaction"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> invoking this <see cref="Interaction"/></param>
        public void Invoke(Player player)
        {
            // Invoking delegate if player is facing the right direction
            if (player.Direction == requiredDirection)
            {
                onInteract(player);
            }
        }
    }
}
