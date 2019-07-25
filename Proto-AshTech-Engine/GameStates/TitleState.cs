using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AshTechEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Proto_AshTech_Engine.GameStates
{
    class TitleState : GameState
    {
        Texture2D spriteTexture;
        public override void LoadContent(ContentManager content)
        {
            spriteTexture = content.Load<Texture2D>("spriteSheets/groundTiles");
        }

        public override void Update(GameTime gameTime)
        { 

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, new Vector2(100, 100), Color.White);
            spriteBatch.End();
        }

        public override void UnloadContent()
        {
            
        }
    }
}
