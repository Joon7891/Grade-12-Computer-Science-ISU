// Author: Joon Song
// File Name: OnInteract.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/16/2019
// Modified Date: 01/16/2019
// Description: Class to hold OnInteract delegate

namespace ISU_Medieval_Odyssey
{
    /// <summary>
    /// The procedure to execute when the player chooses to interact on a given tile
    /// </summary>
    /// <param name="direction">The <see cref="Player"/> interacting with the tile</param>
    public delegate void OnInteract(Player player);
}
