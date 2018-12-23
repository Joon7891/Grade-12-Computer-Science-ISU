using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public class CameraMovement
    {
        private const float Speed = 100;
        private Vector2 targetPosition = Vector2.Zero;

        public void Update(GameTime gameTime)
        {
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            HandleInput(deltaTime);

            if (targetPosition != Vector2.Zero)
            {
                Main.Instance.Camera.Position += new Vector2((float)Math.Round(targetPosition.X), (float)Math.Round(targetPosition.Y));
            }

            targetPosition = Vector2.Zero;
        }

        private void HandleInput(float deltaTime)
        {
            if (Main.Instance.NewKeyboard.IsKeyDown(Keys.A))
            {
                targetPosition += new Vector2(-Speed * deltaTime, 0);
            }

            if (Main.Instance.NewKeyboard.IsKeyDown(Keys.D))
            {
                targetPosition += new Vector2(Speed * deltaTime, 0);
            }

            if (Main.Instance.NewKeyboard.IsKeyDown(Keys.W))
            {
                targetPosition += new Vector2(0, -Speed * deltaTime);
            }

            if (Main.Instance.NewKeyboard.IsKeyDown(Keys.S))
            {
                targetPosition += new Vector2(0, Speed * deltaTime);
            }
        }
    }
}
