using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public abstract class SmartEnemy : Enemy
    {

        protected override void UpdateMovement(GameTime gameTime, Player player)
        {
            base.UpdateMovement(gameTime, player);
        }

        protected override Queue<Vector2Int> PathFinding()
        {
            throw new NotImplementedException();
        }

        private Queue<Vector2Int> BuildPath(Vector2Int[,] previousTiles, Vector2Int targetTile)
        {
            Queue<Vector2Int> path;

            throw new NotImplementedException();
        }

    }
}
