using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;

namespace Game.Common
{
    class InputManager : BaseManager
    {
        private InputListenerManager input_manager;
        public InputManager()
        {
            input_manager = Loader.LoadInputManager();
        }
        public override void Update(GameTime game_time)
        {
            input_manager.Update(game_time);
        }
    }
}
