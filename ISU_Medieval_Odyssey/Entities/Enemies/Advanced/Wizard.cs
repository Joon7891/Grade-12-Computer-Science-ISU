// Author: Joon Song
// File Name: Wizard.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold Wizard object

namespace ISU_Medieval_Odyssey
{
    public sealed class Wizard : RangedEnemy
    {
        private static new DirectionalSpriteSheet directionalSpriteSheet;
        private const int NUM_FRAMES = 3;
        private const int COUNTER_MAX = 5;
        private const int WIDTH = 50;
        private const int HEIGHT = 75;

        /// <summary>
        /// Static constructor for <see cref="Wizard"/> object
        /// </summary>
        static Wizard()
        {
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Wizard", "wizard", NUM_FRAMES);
        }

        public Wizard(Vector2Int tileCoordinate)
        {
            base.directionalSpriteSheet = directionalSpriteSheet;
        }
    }
}
