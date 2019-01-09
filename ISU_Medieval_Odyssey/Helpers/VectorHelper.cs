// Author: Joon Song
// File Name: VectorHelper.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold a variety of methods to help the functionality of various vectors 

using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public static class VectorHelper
    {
        /// <summary>
        /// Extension method to convert a <see cref="Point"/> into a <see cref="Vector2Int"/>
        /// </summary>
        /// <param name="originalVector">The original vector, in <see cref="Point"/> form</param>
        /// <returns>The <see cref="Vector2Int"/> representation of the parameter <see cref="Point"/></returns>
        public static Vector2Int ToVector2Int(this Point originalVector) => new Vector2Int(originalVector.X, originalVector.Y);
    }
}
