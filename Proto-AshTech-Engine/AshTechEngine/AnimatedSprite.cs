using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshTechEngine
{
    public class AnimatedSprite
    {
        private TextureAtlas textureAtlas;
        private SortedList<string, int[]> animations;

        private Vector2 position;

        private string current_AnimationName;
        private int[] current_AnimationFrames;

        public AnimatedSprite(TextureAtlas textureAtlas)
        {            
            this.textureAtlas = textureAtlas;
            position = new Vector2(0, 0);
        }


    }
}
