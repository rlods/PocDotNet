using com.mojang.escape;
using com.mojang.escape.entities;

namespace com.mojang.escape.level.block
{
    public class WaterBlock : Block
    {
        int steps = 0;

        public WaterBlock()
        {
            blocksMotion = true;
        }

        public override void tick()
        {
            base.tick();
            steps--;
            if (steps <= 0)
            {
                floorTex = 8 + random.Next(3);
                floorCol = Art.GetColor(0x0000ff);
                steps = 16;
            }
        }

        public override bool blocks(Entity entity) {
            Player player = entity as Player;
            if (null != player)
            {
			    if (player.getSelectedItem() == Item.flippers) return false;
		    }
		    if (entity is Bullet) return false;
		    return blocksMotion;
	    }

        public override double getFloorHeight(Entity e)
        {
            return -0.5;
        }

        public override double getWalkSpeed(Player player)
        {
            return 0.4;
        }
    }
}
