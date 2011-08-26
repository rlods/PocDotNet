using com.mojang.escape.entities;

namespace com.mojang.escape.level.block
{
    public class WinBlock : Block
    {
        public override void addEntity(Entity entity) {
		    base.addEntity(entity);
            Player player = entity as Player;
            if (null != player)
            {
                player.win();
		    }
	    }
    }
}
