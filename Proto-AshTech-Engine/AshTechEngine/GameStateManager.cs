using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshTechEngine
{
    public class GameStateManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SortedDictionary<string, GameState> gameStates = new SortedDictionary<string, GameState>();
        private GameState currentGameState;

        public GameStateManager(Game game): base(game) {
            
        }

        public override void Initialize()
        {
            base.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void AddState(GameState gameState, string stateNameKey)
        {
            gameState.Initialize(this);
            gameState.LoadContent(Game.Content);

            if(currentGameState == null)
            {
                currentGameState = gameState;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //update input
            Input.Update(gameTime);

            //update the current scene if there is one.
            if(currentGameState != null)
            {
                //update the current game state
                currentGameState.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if(currentGameState != null)
            {
                //draw the current game state
                currentGameState.Draw(spriteBatch);
            }
        }
    }


}
