// Author: Joon Song
// File Name: SettingsData.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/15/2019
// Modified Date: 01/15/2019
// Description: Class to hold wrapper object for various Settings

using Newtonsoft.Json;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public class SettingsData
    {
        /// <summary>
        /// The music volume setting
        /// </summary>
        public float MusicVolume { get; set; }

        /// <summary>
        /// The sound effect volume setting
        /// </summary>
        public float SoundEffectVolume { get; set; }

        /// <summary>
        /// The keybindings setting
        /// </summary>
        public Keys[] KeyBindings { get; set; }
        private readonly Keys[] defaultKeyBindings =
        {
            Keys.W,
            Keys.D,
            Keys.S,
            Keys.A,
            Keys.E,
            Keys.F,
            Keys.I,
            Keys.Escape,
            Keys.F12,
            Keys.D1,
            Keys.D2,
            Keys.D3,
            Keys.D4,
            Keys.D5,
            Keys.D6,
            Keys.D7,
            Keys.D8,
            Keys.D9
        };

        
        /// <summary>
        /// Constructor for wrapper <see cref="Settings"/> object
        /// </summary>
        /// <param name="musicVolume">The music volume setting</param>
        /// <param name="soundEffectVolume">The sound effect volume setting</param>
        /// <param name="keyBindings">The keybindings</param>
        public SettingsData(float musicVolume = 1.0f, float soundEffectVolume = 1.0f, Keys[] keyBindings = null)
        {
            // Setting wrapper object properties
            if (keyBindings == null)
            {
                keyBindings = defaultKeyBindings;
            }
            MusicVolume = musicVolume;
            SoundEffectVolume = soundEffectVolume;
            KeyBindings = keyBindings;
        }

        /// <summary>
        /// Subprogram to serialize <see cref="Settings"/> wrapper object
        /// </summary>
        /// <returns>The serialized <see cref="Settings"/> object</returns>
        public string Serialize() => JsonConvert.SerializeObject(this);

        /// <summary>
        /// Subprogram to deserialize and return a <see cref="Settings"/> wrapper object
        /// </summary>
        /// <param name="serializedData">The serialized data</param>
        /// <returns>The deserialized <see cref="Settings"/> wrapper object</returns>
        public static SettingsData Deserialize(string serializedData) => JsonConvert.DeserializeObject<SettingsData>(serializedData);
    }
}
