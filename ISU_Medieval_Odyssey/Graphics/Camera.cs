using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey.Graphics
{
    public class Camera
    {
        public float OrthographicSize;
        public Vector2 Position { get; set; }
        public float Rotation;
        public Vector2 Origin { get; set; }

        internal Matrix ViewMatrix => Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                                      Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                                      Matrix.CreateRotationZ(Rotation) *
                                      Matrix.CreateScale(OrthographicSize, OrthographicSize, 1) *
                                      Matrix.CreateTranslation(new Vector3(Origin, 1));

        public Camera()
        {
            Reset();
        }

        public Vector2 ScreenToWorldPoint(Vector2 screenPoint) => Position + screenPoint;

        public void Reset()
        {
            OrthographicSize = 1;
            Origin = new Vector2(Main.Context.GraphicsDevice.Viewport.Width / 2.0f,
                Main.Context.GraphicsDevice.Viewport.Height / 2.0f);
        }
    }
}
