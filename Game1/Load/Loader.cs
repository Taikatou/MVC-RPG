using FrameWork.GameEngine;
using FrameWork.GameEngine.Controllers;
using FrameWork.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.InputListeners;
using System;
using System.Diagnostics;

namespace FrameWork.Load
{
    public class Loader
    {

        public enum InputType { None, KeyBoard, GamePad };
        private static InputType input_type = InputType.None;

        public static Vector2 StringToVector2(string vector_string)
        {
            string[] split_vector_string = vector_string.Split(',');
            int x = Int32.Parse(split_vector_string[0]), y = Int32.Parse(split_vector_string[1]);
            return new Vector2(x, y);
        }

        public static InputListenerManager LoadInputManager()
        {
            InputListenerManager input_manager = new InputListenerManager();
            var mouse_listener = input_manager.AddListener(new MouseListenerSettings());
            mouse_listener.MouseClicked += (sender, args) =>
            {
                    Debug.WriteLine("Click here: " + args.Position);
                    Game.Instance.hud?.Click(args.Position);
            };
            var keyboard_listener = input_manager.AddListener(new KeyboardListenerSettings());
            keyboard_listener.KeyPressed += (sender, args) =>
            {
                KeyAction(args.Key, true);
            };
            keyboard_listener.KeyReleased += (sender, args) =>
            {
                KeyAction(args.Key, false);
            };
            var game_pad_listener = input_manager.AddListener(new GamePadListenerSettings(PlayerIndex.One));
            game_pad_listener.ButtonDown += (sender, args) =>
            {
                GamePadButton(args.Button, true);
            };
            game_pad_listener.ButtonUp += (sender, args) =>
            {
                GamePadButton(args.Button, false);
            };

            return input_manager;
        }

        public static void KeyAction(Keys key, bool down)
        {
            CheckInputSystemChange(InputType.KeyBoard);
            switch (key)
            {
                case Keys.Left:
                    ControllerSingleton.Instance.Left = down;
                    break;
                case Keys.Right:
                    ControllerSingleton.Instance.Right = down;
                    break;
                case Keys.Up:
                    ControllerSingleton.Instance.Up = down;
                    break;
                case Keys.Down:
                    ControllerSingleton.Instance.Down = down;
                    break;
                case Keys.A:
                    ControllerSingleton.Instance.A = down;
                    break;
                case Keys.B:
                    ControllerSingleton.Instance.B = down;
                    break;
            }
            SceneManager.CurrentWorld.ControllerUpdate(ControllerSingleton.Instance);
            Debug.WriteLine(string.Format("{0}: {1}", key.ToString(), down));
        }

        public static void CheckInputSystemChange(InputType new_input)
        {
            if (input_type != new_input)
            {
                input_type = new_input;
                Debug.WriteLine("New input system:" + new_input);
            }
        }

        public static void GamePadButton(Buttons button, bool down)
        {
            CheckInputSystemChange(InputType.GamePad);
            switch (button)
            {
                case Buttons.DPadLeft:
                    ControllerSingleton.Instance.Left = down;
                    break;
                case Buttons.DPadRight:
                    ControllerSingleton.Instance.Right = down;
                    break;
                case Buttons.DPadUp:
                    ControllerSingleton.Instance.Up = down;
                    break;
                case Buttons.DPadDown:
                    ControllerSingleton.Instance.Down = down;
                    break;
                case Buttons.A:
                    ControllerSingleton.Instance.A = down;
                    break;
                case Buttons.B:
                    ControllerSingleton.Instance.B = down;
                    break;
            }
            int id = ControllerSingleton.Instance.Id;
            SceneManager.CurrentWorld.ControllerUpdate(ControllerSingleton.Instance);
            Debug.WriteLine(string.Format("{0}: {1}", button.ToString(), down));
        }
    }
}
