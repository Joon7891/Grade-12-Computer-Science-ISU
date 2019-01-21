// Author: Joon Song
// File Name: Attribute.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/20/2019
// Modified Date: 01/20/2019
// Description: Class to hold Attribute object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace ISU_Medieval_Odyssey
{
    public sealed class Attribute
    {
        /// <summary>
        /// The level of this <see cref="Attribute"/>
        /// </summary>
        [JsonProperty] 
        public int Level { get; set; } = 0;

        /// <summary>
        /// The <see cref="Rectangle"/> of this <see cref="Attribute"/>
        /// </summary>
        [JsonIgnore]
        public Rectangle Rectangle { get; }

        /// <summary>
        /// An additional method to invoke off the update button
        /// </summary>
        public delegate void UpgradeUpdate();

        // Various variables required for the function of the Attribute
        private Button upgradeButton;
        private readonly string name;
        private Vector2[] textLocations = new Vector2[3];

        /// <summary>
        /// Consturctor for <see cref="Attribute"/> object
        /// </summary>
        /// <param name="rectangle">The rectangle to draw the <see cref="Attribute"/> button in</param>
        /// <param name="name">The name of this attribute</param>
        /// <param name="upgradeUpdate">The update to invoke off the update</param>
        public Attribute(Rectangle rectangle, string name, UpgradeUpdate upgradeUpdate = null)
        {
            // Setting up attribute upgrade button
            Rectangle = rectangle;
            upgradeButton = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/upgradeButton"), rectangle, () =>
            {
                Player.Instance.AttributePoints -= LevelUpRequirement();
                ++Level;
                if (upgradeUpdate != null)
                {
                    upgradeUpdate();
                }
            });
            this.name = name;
            textLocations[0].Y = rectangle.Y + 5;
            textLocations[0].X = rectangle.X - 78;
            textLocations[1].Y = rectangle.Y + rectangle.Height;
            textLocations[1].X = rectangle.X - 78;
            textLocations[2].Y = rectangle.Bottom + 5;
            textLocations[2].X = (rectangle.Width - SharedData.InformationFonts[3].MeasureString($"2 Points").X) / 2 + rectangle.X;
        }

        /// <summary>
        /// Update subprogram for this <see cref="Attribute"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating attribute update button
            upgradeButton.Active = Player.Instance.AttributePoints >= LevelUpRequirement();
            upgradeButton.Update(gameTime);
        }

        /// <summary>
        /// Draw subprogram for this <see cref="Attribute"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing attribute update button and corresponding text
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"{name}", textLocations[0], Color.Indigo);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Level {Level}", textLocations[1], Color.Indigo);
            spriteBatch.DrawString(SharedData.InformationFonts[3], $"{LevelUpRequirement()} Points", textLocations[2], Color.White);
            upgradeButton.Draw(spriteBatch);
        }

        /// <summary>
        /// Subprogram to determine the level up requirement for this <see cref="Attribute"/>
        /// </summary>
        /// <returns>Toe level up requirement</returns>
        private int LevelUpRequirement() => 2 * (Level + 1);
    }
}
