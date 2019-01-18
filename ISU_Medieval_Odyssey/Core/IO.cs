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
        /// Whether a <see cref="World"/> already exists in file
        /// </summary>
        /// <returns>Whether a <see cref="World"/> exists in file</returns>
        public static bool WorldExists() => File.Exists($"{CHUNK_PATH}/Chunk_0_0.json");

        /// <summary>
        /// Subprogram to delete the previously saved <see cref="World"/>
        /// </summary>
        public static void DeleteWorld()
        {
            // The file directory and an array of the files
            DirectoryInfo folderDirectory;
            FileInfo[] files;

            // Try-Catch block for file reading
            try
            {
                // Going into folder directory and getting directories inside
                folderDirectory = new DirectoryInfo(CHUNK_PATH);
                files = folderDirectory.GetFiles();

                // Deleting files
                for (int i = 0; i < files.Length; ++i)
                {
                    files[i].Delete();
                }
            }
            catch (Exception exception)
            {
                // Catching and informing user of exception
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Subprogram to check of a certain <see cref="Chunk"/> exists in file
        /// </summary>
        /// <param name="chunkX">The x-coordinate of the chunk</param>
        /// <param name="chunkY">The y-coordinate of the chunk</param>
        /// <returns>Whether the <see cref="Chunk"/> exists in file</returns>
        public static bool ChunkExists(int chunkX, int chunkY) => File.Exists($"{CHUNK_PATH}/Chunk_{chunkX}_{chunkY}.json");

        /// <summary>
        /// Subprogram to load a <see cref="Chunk"/>
        /// </summary>
        /// <param name="chunkX">The x-coordinate of the <see cref="Chunk"/></param>
        /// <param name="chunkY">The y-coordinate of the <see cref="Chunk"/></param>
        /// <returns>The loaded <see cref="Chunk"/></returns>
        public static Chunk LoadChunk(int chunkX, int chunkY)
        {
            // Raw and serialized chunk
            Chunk chunk = null;
            string serializedChunk;

            // Try-Catch block for file reading
            try
            {
                // Opening file and loading chunk
                inFile = File.OpenText($"{CHUNK_PATH}/Chunk_{chunkX}_{chunkY}.json");
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