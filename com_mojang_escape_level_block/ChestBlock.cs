using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;
using com.mojang.escape.level;

namespace com.mojang.escape.level.block
{
    public class ChestBlock : Block
    {
        private bool open = false;
        private Sprite chestSprite;

        public ChestBlock()
        {
            tex = 1;
            blocksMotion = true;

            chestSprite = new Sprite(0, 0, 0, 8 * 2 + 0, Art.GetColor(0xffff00));
            addSprite(chestSprite);
        }

        public override bool use(Level level, Item item)
        {
            if (open) return false;

            chestSprite.tex++;
            open = true;

            level.getLoot(id);
            Sound.treasure.play();

            return true;
        }
    }
}
