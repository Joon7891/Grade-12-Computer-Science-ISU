using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    class Projectile // TODO: MAKE POSITIONING WORK BY TILE, NOT COORD, UNLOADING
    {
        readonly double maxDist;

        // positions of the projectile in tiles
        protected Vector2 positionInitial;
        protected Vector2 position;
        protected Rectangle hitBox;

        // in degrees 
        protected double angleFired;

        protected double velocityInitial;

        protected double acceleration;

        protected double timePassed;

        public Projectile(double angleFired, double velocityInitial, double acceleration, Vector2 positionInitial)
        {
            this.angleFired = angleFired;
            this.velocityInitial = velocityInitial;
            this.acceleration = acceleration;
            this.positionInitial = positionInitial;
            this.position = positionInitial;
            this.timePassed = 0;
        }

        public void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.Milliseconds;
            timePassed += deltaTime;
            position.X = Convert.ToInt32(positionInitial.X + velocityInitial * Math.Cos(angleFired) * timePassed);
            position.Y = Convert.ToInt32(positionInitial.Y + velocityInitial * Math.Sin(angleFired) * timePassed);

            Vector2Int coordPos = GameScreen.Instance.World.GetCoord(position);
            hitBox = new Rectangle(coordPos.X, coordPos.Y, 10, 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PLACEHOLDER, hitBox, Color.White);
        }

        static Texture2D PLACEHOLDER;
        public static void Load(ContentManager content)
        {
            PLACEHOLDER = content.Load<Texture2D>("Images/Sprites/aaa");
        }
    }
}
