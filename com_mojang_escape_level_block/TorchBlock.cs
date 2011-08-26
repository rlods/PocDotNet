using com.mojang.escape;
using com.mojang.escape.gui;
using com.mojang.escape.level;
using System;

namespace com.mojang.escape.level.block
{
    public class TorchBlock : Block
    {
        private Sprite torchSprite;

        public TorchBlock()
        {
            torchSprite = new Sprite(0, 0, 0, 3, Art.GetColor(0xffff00));
            sprites.Add(torchSprite);
        }

        public override void decorate(Level level, int x, int y)
        {
            Random random = new Random((x + y * 1000) * 341871231);
            double r = 0.4;
            for (int i = 0; i < 1000; i++)
            {
                int face = random.Next(4);
                if (face == 0 && level.getBlock(x - 1, y).solidRender)
                {
                    torchSprite.x -= r;
                    break;
                }
                if (face == 1 && level.getBlock(x, y - 1).solidRender)
                {
                    torchSprite.z -= r;
                    break;
                }
                if (face == 2 && level.getBlock(x + 1, y).solidRender)
                {
                    torchSprite.x += r;
                    break;
                }
                if (face == 3 && level.getBlock(x, y + 1).solidRender)
                {
                    torchSprite.z += r;
                    break;
                }
            }
        }

        public override void tick()
        {
            base.tick();
            if (random.Next(4) == 0) torchSprite.tex = 3 + random.Next(2);
        }
    }
}
