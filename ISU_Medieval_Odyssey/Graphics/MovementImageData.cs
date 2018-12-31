// Author: Joon Song
// File Name: 12/31/2018
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/31/2018
// Modified Date: 12/31/2018
// Description: FrameData struct - the movement type and frame no 

namespace ISU_Medieval_Odyssey
{
    public struct MovementImageData
    {
        /// <summary>
        /// The movement type of this image
        /// </summary>
        public MovementType MovementType { get; }

        /// <summary>
        /// The frame number of this image
        /// </summary>
        public int FrameNumber { get; }

        /// <summary>
        /// Constructor for <see cref="MovementImageData"/> struct
        /// </summary>
        /// <param name="movementType">The movement type of the image</param>
        /// <param name="frameNumber">The frame no of the image</param>
        public MovementImageData(MovementType movementType, int frameNumber)
        {
            // Assigning parameters to struct properties
            MovementType = movementType;
            FrameNumber = frameNumber;
        }
    }
}
