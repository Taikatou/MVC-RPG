using Microsoft.Xna.Framework;

namespace FrameWork.GameEngine.Objects
{
    class NPCObject: EntityObject
    {
        public NPCObject(): base()
        {

        }

        public override void Update(GameTime game_time)
        {
            // Velocity += new Vector2(1, 0);
            // Direction = Facing.Right;
            // RequestMovement = true;
        }
    }
}
