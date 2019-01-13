// Author: Joon Song, Steven Ung
// Project Name: ISU_Medieval_Odyssey
// File Name: IO.cs
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Desription: Class to various subprograms to hold in various data

using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace ISU_Medieval_Odyssey
{
    public static class IO
    {
        // Streams and paths to write and read data
        private static StreamReader inFile;
        private static StreamWriter outFile;
        private const string FILE_PATH = "IO/";

        /// <summary>
        /// Subprogram to deserialize and load various <see cref="SettingsScreen"/> data
        /// </summary>
        /// <returns></returns>
        public static Tuple<float, float, KeyBinding[]> LoadSettingsData()
        {
            // The raw and serialized data
            Tuple<float, float, KeyBinding[]> loadedData;
            string serializedData;

            // Try-Catch block for file reading
            try
            {
                // Opening file
                inFile = File.OpenText(FILE_PATH + "SettingsData.json");

                // Loading and deserializing data
                serializedData = inFile.ReadLine();
                loadedData = JsonConvert.DeserializeObject<Tuple<float, float, KeyBinding[]>>(serializedData);

                // Closing file and returning data
                inFile.Close();
                return loadedData;
            }
            catch (FileNotFoundException)
            {
                // Informing user of error and closing file
                Console.WriteLine("Error: The file could not be found");
                inFile.Close();
            }
            catch (FormatException)
            {
                // Informing user of error and closing file
                Console.WriteLine("Error: Data to be uploaded was not formatted correctly");
                inFile.Close();
            }
            catch (EndOfStreamException)
            {
                // Informing user that file was read past its end and closing file
                Console.WriteLine("Error: Attempted to read past end of file");
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
        public static void UploadSettingsData(float musicVolume, float soundEffectVolume, KeyBinding[] keyBindings)
        {
            // The raw and serialized data
            Tuple<float, float, KeyBinding[]> uploadData;
            string serializedData;

            // Try-Catch block for file writing
            try
            {
                // Opening file
                outFile = File.CreateText(FILE_PATH + "SettingsData.json");

                // Creating data, converting, and uploading it
                uploadData = new Tuple<float, float, KeyBinding[]>(0.1f, 0.2f, new KeyBinding[10]);
                serializedData = JsonConvert.SerializeObject(uploadData);
                outFile.WriteLine(serializedData);

                // Closing file
                outFile.Close();
            }
            catch (FileNotFoundException)
            {
                // Informing user of error and closing file
                Console.WriteLine("Error: The file could not be found");
                outFile.Close();
            }
            catch (FormatException)
            {
                // Informing user of error and closing file
                Console.WriteLine("Error: Upload data was not formatted correctly");
                outFile.Close();
            }
        }
    }
}