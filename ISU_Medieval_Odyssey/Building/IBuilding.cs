// Author: Joon Song
// File Name: IBuilding.cs
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Interface to hold IBuilding

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public interface IBuilding
    {
        void DrawOutside(SpriteBatch spriteBatch);

        void DrawInside(SpriteBatch spriteBatch);
    }
}
