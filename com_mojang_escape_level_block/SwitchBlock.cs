using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.level;

namespace com.mojang.escape.level.block
{
    public class SwitchBlock : SolidBlock
    {
        private bool pressed = false;

        public SwitchBlock()
        {
            tex = 2;
        }

        public override bool use(Level level, Item item)
        {
            pressed = !pressed;
            if (pressed) tex = 3;
            else tex = 2;

            level.trigger(id, pressed);
            if (pressed)
                Sound.click1.play();
            else
                Sound.click2.play();

            return true;
        }
    }
}
