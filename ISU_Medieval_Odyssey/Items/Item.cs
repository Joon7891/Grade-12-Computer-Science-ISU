// Author: Joon Song, Steven Ung
// File Name: Item.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Item object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace ISU_Medieval_Odyssey
{
    public class Item
    {
        /// <summary>
        /// The value of the item - the price at which it will be purchased at
        /// </summary>
        [JsonProperty]
        public virtual int Value { get; protected set; }

        /// <summary>
        /// Whether the item is valid or not - does it still exist
        /// </summary>
        [JsonProperty]
        public bool Valid { get; protected set; } = true;

        /// <summary>
        /// Whether the item is the <see cref="Player"/>'s item
        /// </summary>
        [JsonProperty]
        public bool IsPlayerItem { get; set; } = true;

        // The image the item's icon
        protected Texture2D iconImage;

        // Variables for drawing item tooltip 
        protected string itemName;
        protected static Vector2 cornerBuffer = new Vector2(5, 5);
        protected static Vector2 verticalBuffer = new Vector2(0, 25);
        protected static Texture2D informationBackImage;

        /// <summary>
        /// Static constructor for <see cref="Item"/>
        /// </summary>
        static Item()
        {
            // Setting up item information graphics
            informationBackImage = Main.Content.Load<Texture2D>("Images/Sprites/informationBoxImage");
        }

        /// <summary>
        /// Subprogram to use this <see cref="Item"/>
        /// </summary>
        /// <param name="player">The player using this particular item</param>
        public virtual void Use(Player player) { }

        /// <summary>
        /// Subprogram to draw the <see cref="Item"/>'s icon
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="rectangle">The rectangle to draw the item's icon in</param>
        public virtual void DrawIcon(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            // Drawing icon
            spriteBatch.Draw(iconImage, rectangle, Color.White);
        }

        /// <summary>
        /// Subprogram to draw information about this <see cref="Item"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        /// <param name="iconRectangle">The icon's rectangle</param>
        public virtual void DrawInformation(SpriteBatch spriteBatch, Rectangle iconRectangle)
        {
            // Adjusting rectangle and drawing backing
            iconRectangle.X -= 2 * iconRectangle.Width / 3;
            iconRectangle.Y -= 5 * iconRectangle.Height / 2;
            iconRectangle.Width *= 3;
            iconRectangle.Height = 2 * iconRectangle.Height;
            spriteBatch.Draw(informationBackImage, iconRectangle, Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[3], itemName, iconRectangle.Location.ToVector2() + cornerBuffer, Color.Black);
            spriteBatch.DrawString(SharedData.InformationFonts[3], $"${Value}", iconRectangle.Location.ToVector2() + cornerBuffer + verticalBuffer, Color.Black);
        }

        /// <summary>
        /// Subprogram to generate and return a random <see cref="Item"/>
        /// </summary>
        /// <returns>The random <see cref="Item"/></returns>
        public static Item RandomItem()
        {
            // Randomly picking an item type
            int randomItemType = SharedData.RNG.Next(3);

            // Returning new instance of item
            switch (randomItemType)
            {
                // Type-0 -> returning a potion 
                case 0:
                    return Potion.RandomPotion();

                // Type-1 -> returning a weapon
                case 1:
                    return Weapon.RandomWeapon();

                // Otherwise, returning an armour
                default:
                    return Armour.RandomArmour();
            }
        }
    }
}
