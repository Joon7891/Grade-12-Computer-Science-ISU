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
        
        /// <summary>
        /// Constructor for wrapper <see cref="Settings"/> object
        /// </summary>
        /// <param name="musicVolume">The music volume setting</param>
        /// <param name="soundEffectVolume">The sound effect volume setting</param>
        /// <param name="keyBindings">The keybindings</param>
        public SettingsData(float musicVolume, float soundEffectVolume, Keys[] keyBindings)
        {
            // Setting wrapper object properties
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
