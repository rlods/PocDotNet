using com.mojang.escape;
using com.mojang.escape.entities;

namespace com.mojang.escape.level.block
{
    public class PressurePlateBlock : Block
    {
        public bool pressed = false;

        public PressurePlateBlock()
        {
            floorTex = 2;
        }

        public override void tick()
        {
            base.tick();
            double r = 0.2;
            bool steppedOn = level.containsBlockingNonFlyingEntity(x - r, y - r, x + r, y + r);
            if (steppedOn != pressed)
            {
                pressed = steppedOn;
                if (pressed) floorTex = 3;
                else floorTex = 2;

                level.trigger(id, pressed);
                if (pressed)
                    Sound.click1.play();
                else
                    Sound.click2.play();
            }
        }

        public override double getFloorHeight(Entity e)
        {
            if (pressed) return -0.02;
            else return 0.02;
        }
    }
}
