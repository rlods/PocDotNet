using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.level;

namespace com.mojang.escape.level.block
{
    public class DoorBlock : SolidBlock
    {
        public bool open = false;
        public double openness = 0;

        public DoorBlock()
        {
            tex = 4;
            solidRender = false;
        }

        public override bool use(Level level, Item item)
        {
            open = !open;
            if (open)
                Sound.click1.play();
            else
                Sound.click2.play();
            return true;
        }

        public override void tick()
        {
            base.tick();

            if (open) openness += 0.2;
            else openness -= 0.2;
            if (openness < 0) openness = 0;
            if (openness > 1) openness = 1;

            double openLimit = 7 / 8.0;
            if (openness < openLimit && !open && !blocksMotion)
            {
                if (level.containsBlockingEntity(x - 0.5, y - 0.5, x + 0.5, y + 0.5))
                {
                    openness = openLimit;
                    return;
                }
            }

            blocksMotion = openness < openLimit;
        }

        public override bool blocks(Entity entity) {
		    double openLimit = 7 / 8.0;
            if (openness >= openLimit && entity is Player) return blocksMotion;
		    if (openness >= openLimit && entity is Bullet) return blocksMotion;
		    if (openness >= openLimit && entity is OgreEntity) return blocksMotion;
		    return true;
	    }
    }
}
