using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    class Projectile
    {
        readonly double maxDist;

        protected Vector2Int positionInitial;

        protected Vector2Int position;

        // in degrees 
        protected double angleFired;

        protected double velocityInitial;

        protected double acceleration;

        protected double timePassed;

        public Projectile(double angleFired, double velocityInitial, double acceleration, Vector2Int positionInitial)
        {
            this.angleFired = angleFired;
            this.velocityInitial = velocityInitial;
            this.acceleration = acceleration;
            this.positionInitial = positionInitial;
            this.position = positionInitial;
            this.timePassed = 0;
        }

        public void Update(double deltaTime)
        {
            timePassed += deltaTime;
            position.X = Convert.ToInt32(velocityInitial * Math.Cos(angleFired) * timePassed);
            position.Y = Convert.ToInt32(velocityInitial * Math.Sin(angleFired) * timePassed);
        }

        static Texture2D PLACEHOLDER;
        public static void Load(ContentManager content)
        {
            content.Load<Texture2D>("Images/Sprites/aaa");
        }


    }
}
