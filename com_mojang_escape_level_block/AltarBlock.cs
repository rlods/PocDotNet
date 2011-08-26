using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;

namespace com.mojang.escape.level.block
{
    public class AltarBlock : Block
    {
        private bool filled = false;
        private Sprite sprite;

        public AltarBlock()
        {
            blocksMotion = true;
            sprite = new Sprite(0, 0, 0, 16 + 4, Art.GetColor(0xE2FFE4));
            addSprite(sprite);
        }

        public override void addEntity(Entity entity) {
		    base.addEntity(entity);
		    if (!filled && (entity is GhostEntity || entity is GhostBossEntity)) {
			    entity.remove();
			    filled = true;
			    blocksMotion = false;
			    sprite.removed = true;

			    for (int i = 0; i < 8; i++) {
				    RubbleSprite s = new RubbleSprite();
				    s.col = this.sprite.col;
				    addSprite(s);
			    }

                if (entity is GhostBossEntity)
                {
				    level.addEntity(new KeyEntity(x, y));
				    Sound.bosskill.play();
			    }
                else
                {
				    Sound.altar.play();
			    }
		    }
	    }

        public override bool blocks(Entity entity)
        {
            return blocksMotion;
        }
    }
}
