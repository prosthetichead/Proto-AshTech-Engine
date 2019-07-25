using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace AshTechEngine
{
    public enum GamePadButtons
    {
        // Center
        Home,
        Start,
        Back,
        // D-Pad
        Up,
        Left,
        Down,
        Right,
        // Face buttons
        FaceButtonUp,
        FaceButtonLeft,
        FaceButtonDown,
        FaceButtonRight,
        // Shoulder buttons
        LeftShoulder,
        RightShoulder,
        // Triggers
        LeftTrigger,
        RightTrigger,
        // Left Stick
        LeftStick,
        LeftStickUp,
        LeftStickLeft,
        LeftStickDown,
        LeftStickRight,
        // Right Stick
        RightStick,
        RightStickUp,
        RightStickLeft,
        RightStickDown,
        RightStickRight
    }

    /// <summary>
    /// A combination of gamepad and keyboard keys mapped to a particular action.
    /// </summary>
    [DataContract]  
    public class ActionMap
    {
        [DataMember]
        public List<GamePadButtons> gamePadButtons = new List<GamePadButtons>();
        [DataMember]
        public List<Keys> keyboardKeys = new List<Keys>();
        
        /// <summary>
        /// Add a map of any combo of gamepad buttons
        /// if more then 1 button provided then all buttons must be pressed for the action
        /// </summary>
        /// <param name="gamePadButtonMap"></param>
        public void AddGamePadMap(GamePadButtons gamePadButtonMap)
        {
            gamePadButtons.Add(gamePadButtonMap);
        }

        public void AddKeyboardKeyMap(Keys keyboardKeyMap)
        {
            keyboardKeys.Add(keyboardKeyMap);
        }

        //TODO: add methods to replace maps
    }

    public static class Input
    {
        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;
        private static GamePadState currentGamePadState;
        private static GamePadState previousGamePadState;
        private static SortedDictionary<string,ActionMap> actionMaps;
        /// The value of an analog control that reads as a "pressed button"
        const float analogLimit = 0.5f;


        static Input()
        {
            ResetActionMaps();
        }

        /// <summary>
        /// Reset the action mapping to the defualts
        /// </summary>
        private static void ResetActionMaps()
        {
            //Try Load config
            var jsonRead = FileSystem.ReadTextLocalStorage("Input.json").Result;
            actionMaps = JsonConvert.DeserializeObject<SortedDictionary<string, ActionMap>>(jsonRead);
            if (actionMaps == null)
            {
                actionMaps = new SortedDictionary<string, ActionMap>();

                //actionMap
                //move up
                var actionMap = new ActionMap();
                actionMap.AddKeyboardKeyMap(Keys.W);
                actionMap.AddKeyboardKeyMap(Keys.Up);
                actionMap.AddGamePadMap(GamePadButtons.Up);
                actionMaps.Add("MoveUp", actionMap);

                var json = JsonConvert.SerializeObject(actionMaps);
                FileSystem.WriteTextLocalStorage("Input.json", json).Wait();
            }



        }

        /// <summary>
        /// Check if an action has been pressed.
        /// </summary>
        public static bool IsActionPressed(string action)
        {
            ActionMap actionMap; 
            if(actionMaps.TryGetValue(action, out actionMap)){
                return IsActionMapPressed(actionMap);
            }
            return false;
        }
        private static bool IsActionMapPressed(ActionMap actionMap)
        {
            for (int i = 0; i < actionMap.keyboardKeys.Count; i++)
            {
                if (IsKeyPressed(actionMap.keyboardKeys[i]))
                    return true;
            }

            //Is a Gamepad pugged in?
            if (currentGamePadState.IsConnected)
            {
                for (int i = 0; i < actionMap.gamePadButtons.Count; i++)
                {
                    if (IsGamePadButtonPressed(actionMap.gamePadButtons[i]))
                        return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Check if an action was just performed in the most recent update.
        /// </summary>
        public static bool IsActionTriggered(string action)
        {
            ActionMap actionMap;
            if (actionMaps.TryGetValue(action, out actionMap)) {
                return IsActionMapTriggered(actionMap);
            }
            return false;
        }
        private static bool IsActionMapTriggered(ActionMap actionMap)
        {
            for (int i = 0; i < actionMap.keyboardKeys.Count; i++)
            {
                if (IsKeyTriggered(actionMap.keyboardKeys[i]))
                {
                    return true;
                }
            }
            if (currentGamePadState.IsConnected)
            {
                for (int i = 0; i < actionMap.gamePadButtons.Count; i++)
                {
                    if (IsGamePadButtonTriggered(actionMap.gamePadButtons[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        public static bool IsKeyTriggered(Keys key)
        {
            if ((currentKeyboardState.IsKeyDown(key)) && (!previousKeyboardState.IsKeyDown(key)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check if a keys are pressed.
        /// </summary>
        public static bool IsKeyPressed(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key))
                return true;
            else
                return false;

        }


        //GAMEPAD REGION
        #region GamePad Region

        public static bool IsGamePadButtonPressed(GamePadButtons gamePadKey)
        {
            switch (gamePadKey)
            {
                case GamePadButtons.Up:
                    if ((currentGamePadState.DPad.Up == ButtonState.Pressed))
                        return true;
                    else
                        return false;
                case GamePadButtons.Down:
                    if ((currentGamePadState.DPad.Down == ButtonState.Pressed))
                        return true;
                    else
                        return false;
                case GamePadButtons.Left:
                    if ((currentGamePadState.DPad.Left == ButtonState.Pressed))
                        return true;
                    else
                        return false;
                case GamePadButtons.Right:
                    if ((currentGamePadState.DPad.Right == ButtonState.Pressed))
                        return true;
                    else
                        return false;
            }
            return false;
        }
        public static bool IsGamePadButtonTriggered(GamePadButtons gamePadKey)
        {
            switch (gamePadKey)
            {
                case GamePadButtons.Up:
                    if ((currentGamePadState.DPad.Up == ButtonState.Pressed) && (previousGamePadState.DPad.Up == ButtonState.Released))
                        return true;
                    else
                        return false;
                case GamePadButtons.Down:
                    if ((currentGamePadState.DPad.Down == ButtonState.Pressed) && (previousGamePadState.DPad.Down == ButtonState.Released))
                        return true;
                    else
                        return false;
                case GamePadButtons.Left:
                    if ((currentGamePadState.DPad.Left == ButtonState.Pressed) && (previousGamePadState.DPad.Left == ButtonState.Released))
                        return true;
                    else
                        return false;
                case GamePadButtons.Right:
                    if ((currentGamePadState.DPad.Right == ButtonState.Pressed) && (previousGamePadState.DPad.Right == ButtonState.Released))
                        return true;
                    else
                        return false;

            }
            return false;
        }

        #endregion

        ///<summary>
        ///Updates the keyboard and gamepad control states.
        ///Dont run this Game State Manager runs it for you like magic
        ///</summary>
        public static void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            previousGamePadState = currentGamePadState;

            //update the keyboard state
            currentKeyboardState = Keyboard.GetState();
            //update the gamepad state
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

    }
}
