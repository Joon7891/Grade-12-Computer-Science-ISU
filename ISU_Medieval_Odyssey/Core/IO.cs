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
        private const string CHUNK_PATH = BASE_DIRECTORY + "/Chunk";

        /// <summary>
        /// Subprogram to deserialize and load various <see cref="SettingsData"/> data
        /// </summary>
        /// <returns>The current <see cref="SettingsData"/> data</returns>
        public static SettingsData LoadSettingsData()
        {
            // Raw and serialized data
            SettingsData settings = null;
            string serializedSettings;
            
            // Try-Catch block
            try
            {
                // Opening file and loading data
                inFile = File.OpenText(SETTINGS_PATH);
                serializedSettings = inFile.ReadLine();
                settings = SettingsData.Deserialize(serializedSettings);
            }
            catch (Exception exception)
            {
                // Informing user of exception and closing file
                Console.WriteLine(exception.Message);
                inFile.Close();
            }

            // Closing file and returning settings
            inFile.Close();
            return settings;
        }

        /// <summary>
        /// Subprogram to serialize and save various <see cref="SettingsScreen"/> data
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

        public static bool ChunkExists(Vector2Int chunkCoordinate) => File.Exists($"{CHUNK_PATH}/Chunk_{chunkCoordinate.X}_{chunkCoordinate.Y}.json");

        /// <summary>
        /// Subprogram to deserialized and load a <see cref="Chunk"/>
        /// </summary>
        /// <param name="chunkCoordinate"></param>
        /// <returns>The loaded chunk</returns>
        public static Chunk LoadChunk(Vector2Int chunkCoordinate)
        {
            // Raw and serialized chunk
            Chunk chunk = null;
            string serializedChunk;

            // Try-Catch block for file reading
            try
            {
                // Opening file and loading chunk
                inFile = File.OpenText($"{CHUNK_PATH}/Chunk_{chunkCoordinate.X}_{chunkCoordinate.Y}.json");
                serializedChunk = inFile.ReadLine();
                chunk = Chunk.Deserialize(serializedChunk);
            }
            catch (Exception exception)
            {
                // Informing user of exception
                Console.WriteLine(exception.Message);
            }


            return chunk;
        }

        /// <summary>
        /// Subprogram to serialized and save a <see cref="Chunk"/>'s data
        /// </summary>
        /// <param name="chunk">The chunk to be serialized</param>
        public static void SaveChunk(Chunk chunk)
        {
            // String to hold serialized data and file path
            string serializedChunk;
            string filePath = $"{CHUNK_PATH}/Chunk_{chunk.Position.X}_{chunk.Position.Y}.json";

            // Try-Catch block for file writing
            try
            {
                // Creating file
                outFile = File.CreateText(filePath);

                // Serializing data and writing it to file
                serializedData = chunk.Serialize();
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