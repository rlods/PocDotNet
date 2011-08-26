using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;
using com.mojang.escape.level;

namespace com.mojang.escape.level.block
{
    public class VanishBlock : SolidBlock
    {
        private bool gone = false;

        public VanishBlock()
        {
            tex = 1;
        }

        public override bool use(Level level, Item item)
        {
            if (gone) return false;

            gone = true;
            blocksMotion = false;
            solidRender = false;
            Sound.crumble.play();

            for (int i = 0; i < 32; i++)
            {
                RubbleSprite sprite = new RubbleSprite();
                sprite.col = col;
                addSprite(sprite);
            }

            return true;
        }
    }
}
