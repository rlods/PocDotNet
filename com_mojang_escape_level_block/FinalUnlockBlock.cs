using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.level;

namespace com.mojang.escape.level.block
{
    public class FinalUnlockBlock : SolidBlock
    {
        private bool pressed = false;

        public FinalUnlockBlock()
        {
            tex = 8 + 3;
        }

        public override bool use(Level level, Item item)
        {
            if (pressed) return false;
            if (level.player.keys < 4) return false;

            Sound.click1.play();
            pressed = true;
            level.trigger(id, true);

            return true;
        }
    }
}
