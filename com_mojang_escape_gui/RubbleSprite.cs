using System;

namespace com.mojang.escape.gui
{
    public class RubbleSprite : Sprite
    {
        protected static readonly Random random = new Random();
        private double xa, ya, za;

        public RubbleSprite()
             :
            base(random.NextDouble() - 0.5, random.NextDouble() * 0.8, random.NextDouble() - 0.5, 2, 0x555555)
        {
            xa = random.NextDouble() - 0.5;
            ya = random.NextDouble();
            za = random.NextDouble() - 0.5;
        }

        public override void tick()
        {
            x += xa * 0.03;
            y += ya * 0.03;
            z += za * 0.03;
            ya -= 0.1;
            if (y < 0)
            {
                y = 0;
                xa *= 0.8;
                za *= 0.8;
                if (random.NextDouble() < 0.04)
                    removed = true;
            }
        }
    }
}
