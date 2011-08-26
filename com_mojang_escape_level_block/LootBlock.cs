using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;

namespace com.mojang.escape.level.block
{
    public class LootBlock : Block
    {
        private bool taken = false;
        private Sprite sprite;

        public LootBlock()
        {
            sprite = new Sprite(0, 0, 0, 16 + 2, Art.GetColor(0xffff80));
            addSprite(sprite);
            blocksMotion = true;
        }

        public override void addEntity(Entity entity) {
		    base.addEntity(entity);
            Player player = entity as Player;
            if (!taken && null != player)
            {
			    sprite.removed = true;
			    taken = true;
			    blocksMotion = false;
                player.loot++;
			    Sound.pickup.play();
    			
		    }
	    }

        public override bool blocks(Entity entity) {
		    if (entity is Player) return false;
		    return blocksMotion;
	    }
    }
}
