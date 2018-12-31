using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LongSpear : ThrustWeapon
    {
        private new static Texture2D[,] directionalImages;

        static LongSpear()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Weapon/Thrust/LongSpear/";
            string weaponTypeName = "longSpear";
            directionalImages = EntityHelper.LoadDirectionalImages(basePath, weaponTypeName, SharedData.MovementNumFrames[MovementType.Thrust]);
        }

        public LongSpear()
        {
            base.directionalImages = directionalImages;
        }
    }
}
