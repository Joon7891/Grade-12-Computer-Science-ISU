// Author: Joon Song, Steven Ung
// Project Name: ISU_Medieval_Odyssey
// File Name: IO.cs
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Desription: Class to various subprograms to hold in various data

using System;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Input;
using System.Reflection;

namespace ISU_Medieval_Odyssey
{
    public static class IO
    {
        // Streams and paths to write and read data
        private static StreamReader inFile;
        private static StreamWriter outFile;
        private const string BASE_DIRECTORY = "IO";
        private const string SETTINGS_PATH = BASE_DIRECTORY + "/SettingsData.json";

        /// <summary>
        /// Subprogram to deserialize and load various <see cref="SettingsData"/> data
        /// </summary>
        /// <returns>The current <see cref="SettingsData"/> data</returns>
        public static SettingsData LoadSettingsData()
        {
            // Raw and serialized data
            string serializedSettings;
            SettingsData settings;
            
            // Try-Catch block
            try
            {
                // Opening file
                inFile = File.OpenText(SETTINGS_PATH);

                // Loading and deserializing data
                serializedSettings = inFile.ReadLine();
                settings = SettingsData.Deserialize(serializedSettings);

                // Closing file and returning settings
                inFile.Close();
                return settings;
            }
            catch (Exception exception)
            {
                // Informing user of exception and closing file
                Console.WriteLine(exception.Message);
                inFile.Close();
            }

            // Otherwise, returning null
            return null;
        }

        /// <summary>
        /// Subprogram to serialized and upload various <see cref="SettingsScreen"/> data
        /// </summary>
        /// <param name="musicVolume">The music volume</param>
        /// <param name="soundEffectVolume">The sound effects volume</param>
        /// <param name="keyBindings">Array of keybindings</param>
        public static void SaveSettingsData(float musicVolume, float soundEffectVolume, Keys[] keyBindings)
        {
            // Raw and serialized data
            SettingsData settings = new SettingsData(musicVolume, soundEffectVolume, keyBindings);
            string serializedData;

            // Try-Catch block for file writing
            try
            {
                // Creating file
                outFile = File.CreateText(SETTINGS_PATH);

                // Serializing data and writing it to file
                serializedData = settings.Serialize();
                outFile.WriteLine(serializedData);
            }
            catch (Exception exception)
            {
                // Informing user of exception
                Console.WriteLine(exception.Message);
            }

            // Closing file
            outFile.Close();
        }
    }
}