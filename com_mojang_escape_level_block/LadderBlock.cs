using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;

namespace com.mojang.escape.level.block
{
    public class LadderBlock : Block
    {
	    private const int LADDER_COLOR = 0xDB8E53;
	    public bool wait;

	    public LadderBlock(bool down) {
		    if (down) {
			    floorTex = 1;
			    addSprite(new Sprite(0, 0, 0, 8 + 3, Art.GetColor(LADDER_COLOR)));
		    }
            else
            {
			    ceilTex = 1;
			    addSprite(new Sprite(0, 0, 0, 8 + 4, Art.GetColor(LADDER_COLOR)));
		    }
	    }

	    public override void removeEntity(Entity entity) {
		    base.removeEntity(entity);
		    if (entity is Player) {
			    wait = false;
		    }
	    }

	    public override void addEntity(Entity entity) {
		    base.addEntity(entity);

		    if (!wait && entity is Player) {
			    level.switchLevel(id);
			    Sound.ladder.play();
		    }
	    }
    }
}
