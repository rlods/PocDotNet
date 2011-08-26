using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;

namespace com.mojang.escape.level.block
{
    public class PitBlock : Block
    {
        private bool filled = false;

        public PitBlock()
        {
            floorTex = 1;
            blocksMotion = true;
        }

        public override void addEntity(Entity entity) {
		    base.addEntity(entity);
            if (!filled && entity is BoulderEntity)
            {
			    entity.remove();
			    filled = true;
			    blocksMotion = false;
			    addSprite(new Sprite(0, 0, 0, 8 + 2, BoulderEntity.COLOR));
			    Sound.thud.play();
		    }
	    }

        public override bool blocks(Entity entity) {
		    if (entity is BoulderEntity) return false;
		    return blocksMotion;
	    }
    }
}
