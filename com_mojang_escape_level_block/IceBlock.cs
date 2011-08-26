using com.mojang.escape;
using com.mojang.escape.entities;

namespace com.mojang.escape.level.block
{
    public class IceBlock : Block
    {
        public IceBlock()
        {
            blocksMotion = false;
            floorTex = 16;
        }

        public override void tick()
        {
            base.tick();
            floorCol = Art.GetColor(0x8080ff);
        }

        public override double getWalkSpeed(Player player)
        {
            if (player.getSelectedItem() == Item.skates) return 0.05;
            return 1.4;
        }

        public override double getFriction(Player player)
        {
            if (player.getSelectedItem() == Item.skates) return 0.98;
            return 1;
        }

        public override bool blocks(Entity entity) {
		    if (entity is Player) return false;
		    if (entity is Bullet) return false;
		    if (entity is EyeBossEntity) return false;
		    if (entity is EyeEntity) return false;
		    return true;
	    }
    }
}
