using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshTechEngine
{
    public class TextureAtlas
    {
        private int width; //width of each region on the atlas
        private int height; ///height of each region on the atlas
        Texture2D spriteSheet;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">the width of each individual image on the atlas</param>
        /// <param name="height">the height of each individual image on the atlas</param>
        /// <param name="spriteSheet">Texture 2d of the spriteSheet Atlas</param>
        public TextureAtlas(int width, int height, Texture2D spriteSheet)
        {
            this.width = width;
            this.height = height;
        }

        private Rectangle GetRectangleForID(int ID)
        {
            int rectangleX = ID % (spriteSheet.Width / width);
            int rectangleY = ID / (spriteSheet.Width / width);

            return new Rectangle(rectangleX * width, rectangleY * height, width, height);
        }

        public void Draw(SpriteBatch spriteBatch, int atlasID, Vector2 position, Vector2 origin, Color color, float rotation = 0f, float depth = 0f, SpriteEffects effects = SpriteEffects.None)
        {
            Rectangle rect = new Rectangle((int)position.X, (int)position.Y, width, height);
            spriteBatch.Draw(spriteSheet, rect, GetRectangleForID(atlasID), color, rotation, origin, effects, depth);
        }

    }
}
