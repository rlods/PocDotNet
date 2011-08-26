using com.mojang.escape.entities;
using com.mojang.escape.level;

namespace com.mojang.escape.level.block
{
    public class LockedDoorBlock : DoorBlock
    {
        public LockedDoorBlock()
        {
            tex = 5;
        }

        public override bool use(Level level, Item item)
        {
            return false;
        }

        public override void trigger(bool pressed)
        {
            open = pressed;
        }
    }
}
