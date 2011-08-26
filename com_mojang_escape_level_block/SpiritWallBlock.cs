using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;

namespace com.mojang.escape.level.block
{
    public class SpiritWallBlock : Block
    {
        public SpiritWallBlock()
        {
            floorTex = 7;
            ceilTex = 7;
            blocksMotion = true;
            for (int i = 0; i < 6; i++)
            {
                double x = (random.NextDouble() - 0.5);
                double y = (random.NextDouble() - 0.7) * 0.3;
                double z = (random.NextDouble() - 0.5);
                addSprite(new Sprite(x, y, z, 4 * 8 + 6 + random.Next(2), Art.GetColor(0x202020)));
            }
        }

        public override bool blocks(Entity entity) {
		    if (entity is Bullet) return false;
		    return base.blocks(entity);
	    }
    }
}
