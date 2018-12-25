// Author: Joon Song
// File Name: Camera.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold Camera object

using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public class Camera
    {
        /// <summary>
        /// The orthographic size of the camera
        /// </summary>
        public float OrthographicSize { get; set; }

        /// <summary>
        /// The position of the camera
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The center of the camera
        /// </summary>
        public Vector2 Center => Position + SharedData.ScreenCenter;

        /// <summary>
        /// The origin of the camera - defualt center of screen
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// The matrix from which the camera should render with
        /// </summary>
        public Matrix ViewMatrix => Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                                    Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                                    Matrix.CreateScale(OrthographicSize, OrthographicSize, 1) *
                                    Matrix.CreateTranslation(new Vector3(Origin, 1));

        /// <summary>
        /// Constructor for Camera object
        /// </summary>
        public Camera()
        {
            // Resets the camera
            Reset();
        }

        /// <summary>
        /// Determines and returns the world coordiante point - adjusting for Camera movement
        /// </summary>
        /// <param name="screenPoint">The screen coordinate point</param>
        /// <returns>The world coordinate point</returns>
        public Vector2 ScreenPointToWorldPoint(Vector2 screenPoint) => Position + screenPoint;

        /// <summary>
        /// Resets the Camera
        /// </summary>
        public void Reset()
        {
            // Setting orthographic size and origin to default values
            OrthographicSize = 1;
            Origin = new Vector2(Main.Instance.GraphicsDevice.Viewport.Width / 2.0f,
                Main.Instance.GraphicsDevice.Viewport.Height / 2.0f);
        }
    }
}
