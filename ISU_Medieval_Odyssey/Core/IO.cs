// Author: Joon Song, Steven Ung
// Project Name: ISU_Medieval_Odyssey
// File Name: IO.cs
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Desription: Class to various subprograms to hold in various data


namespace ISU_Medieval_Odyssey
{
    public static class IO
    {
        ///// <summary>
        ///// Subprogram to load a chunk with a given x and y coordinate - uses Json Serialization
        ///// </summary>
        ///// <param name="x">The x coordinate of the chunk</param>
        ///// <param name="y">The y coordinate of the chunk</param>
        ///// <returns>The loaded chunk</returns>
        //public static Chunk LoadChunk(int x, int y)
        //{
        //    // Variables to hold the file name and loaded chunk
        //    string fileName = $"World/Chunk_x_{x}y_{y}.json";
        //    Chunk loadedChunk;

        //    // If the appropraite Json file exists, deserialize it
        //    if (File.Exists(fileName))
        //    {
        //        loadedChunk = JsonConvert.DeserializeObject<Chunk>(File.ReadAllText(fileName));
        //    }
        //    else
        //    {
        //        // Procedurely generating chunk and saving it in memory
        //        loadedChunk = Chunk.GenerateChunk(x, y);
        //        File.WriteAllText(fileName, JsonConvert.SerializeObject(loadedChunk));
        //    }

        //    // Returning loaded chunk
        //    return loadedChunk;
        //}
    }
}