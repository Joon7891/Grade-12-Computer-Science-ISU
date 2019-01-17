// Author: Joon Song
// File Name: ArrayHelper.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/13/2019
// Modified Date: 01/13/2019
// Description: Class to hold various subprograms to help with array functionality

namespace ISU_Medieval_Odyssey
{
    public static class ArrayHelper
    {
        /// <summary>
        /// Subprogram to return the subarray of a given array
        /// </summary>
        /// <param name="array">The array to derive the subarray from</param>
        /// <param name="startIndex">The start index of the subarray</param>
        /// <param name="length">The this of the subarray</param>
        /// <returns>The requested subarray</returns>
        public static T[] SubArray<T>(this T[] array, int startIndex, int length)
        {
            // Constructing subarray with given length
            T[] subArray = new T[length];

            // Copying elements to subarray
            for (int i = 0; i < length; ++i)
            {
                subArray[i] = array[startIndex + i];
            }

            // Returning subarray
            return subArray;
        }
    }
}
