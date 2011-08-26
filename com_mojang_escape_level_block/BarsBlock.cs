using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;
using com.mojang.escape.level;
using com.mojang.escape.level.block;

namespace com.mojang.escape.level.block
{
    public class BarsBlock : Block
    {
        private Sprite sprite;
        private bool open = false;

        public BarsBlock()
        {
            sprite = new Sprite(0, 0, 0, 0, 0x202020);
            addSprite(sprite);
            blocksMotion = true;
        }

        public override bool use(Level level, Item item)
        {
            if (open) return false;

            if (item == Item.cutters)
            {
                Sound.cut.play();
                sprite.tex = 1;
                open = true;
            }

            return true;
        }

        public override bool blocks(Entity entity) {
		    if (open && entity is Player) return false;
		    if (open && entity is Bullet) return false;
		    return blocksMotion;
	    }
    }
}
