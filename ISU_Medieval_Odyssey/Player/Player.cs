// Author: Joon Song
// File Name: Player.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Player object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Player
    {
        // Graphics related data
        private static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private MovementType movementType = MovementType.Thrust;
        private Direction direction = Direction.Down;
        private Rectangle rectangle = new Rectangle(0, 0, 128, 128);
        private int currentFrame = 0;
        private int counter = 0;

        // Instances of each type of armour
        private Shoes shoes;
        private Helmet helmet;
        private Belt belt;
        
        /// <summary>
        /// Static constructor to setup various Player components
        /// </summary>
        static Player()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Player/";
            string entityTypeName = "player";
            movementImages = EntityHelper.LoadMovementImages(basePath, entityTypeName);
        }

        public Player()
        {
            shoes = new LeatherShoes();
            helmet = new LeatherHat();
        }

        public void Update(GameTime gameTime)
        {
            if (counter == 5)
            {
                currentFrame = (currentFrame + 1) % SharedData.MovementNumFrames[movementType];
                counter = 0;
            }
            ++counter;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing player and its corresponding armour
            spriteBatch.Draw(movementImages[movementType][(byte)direction, currentFrame], rectangle, Color.White);
            shoes?.Draw(spriteBatch, rectangle, movementType, direction, currentFrame);
            helmet.Draw(spriteBatch, rectangle, movementType, direction, currentFrame);
        }
    }
}
