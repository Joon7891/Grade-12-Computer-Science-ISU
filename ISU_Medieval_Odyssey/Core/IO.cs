// Author: Joon Song
// Project Name: ISU_Medieval_Odyssey
// File Name: IO.cs
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Desription: Class to various subprograms to hold in various data

using System;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public static class IO
    {
        // Streams and paths to write and read data
        private static StreamReader inFile;
        private static StreamWriter outFile;
        private const string BASE_DIRECTORY = "IO";
        private const string CHUNK_PATH = BASE_DIRECTORY + "/Chunks";
        private const string SETTINGS_PATH = BASE_DIRECTORY + "/SettingsData.json";

        /// <summary>
        /// Whether a certain <see cref="Chunk"/> exists in file
        /// </summary>
        /// <param name="chunkCoordinate">The coordainte of the <see cref="Chunk"/></param>
        /// <returns>Whether the <see cref="Chunk"/> exists in file</returns>
        public static bool ChunkExists(Vector2Int chunkCoordinate) => File.Exists($"{CHUNK_PATH}/Chunk_{chunkCoordinate.X}_{chunkCoordinate.Y}.json");

        /// <summary>
        /// Subprogram to deserialized and load a <see cref="Chunk"/>
        /// </summary>
        /// <param name="chunkCoordinate">The chunk coordinate of the <see cref="Chunk"/> to load</param>
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
                // Catching and informing user of exception
                Console.WriteLine(exception.Message);
            }

            // Closing file and returing chunk
            inFile.Close();
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

            // Try-Catch block for file writing
            try
            {
                // Creating file
                outFile = File.CreateText($"{CHUNK_PATH}/Chunk_{chunk.Position.X}_{chunk.Position.Y}.json");

                // Serializing data and writing it to file
                serializedChunk = chunk.Serialize();
                outFile.WriteLine(serializedChunk);
            }
            catch (Exception exception)
            {
                // Catching and informing user of exception
                Console.WriteLine(exception.Message);
            }

            // Closing file
            outFile.Close();
        }

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
                // Catching and informing user of exception
                Console.WriteLine(exception.Message);
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
                // Creating file and serializing data to file
                outFile = File.CreateText(SETTINGS_PATH);
                serializedData = settings.Serialize();
                outFile.WriteLine(serializedData);
            }
            catch (Exception exception)
            {
                // Catching and informing user of exception
                Console.WriteLine(exception.Message);
            }

            // Closing file
            outFile.Close();
        }
    }
}