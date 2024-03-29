﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshTechEngine
{
    public abstract class GameState
    {
        //the game state manager handeling this state
        protected GameStateManager gameStateManager;

        public GameState()
        {
        }

        public virtual void Initialize(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}