using com.mojang.escape.entities;

namespace com.mojang.escape.level
{
    public class TempleLevel : Level
    {
        private int triggerMask = 0;

        public TempleLevel()
        {
            floorCol = 0x8A6496;
            ceilCol = 0x8A6496;
            wallCol = 0xCFADDB;
            name = "The Temple";
        }

        public override void switchLevel(int id)
        {
            if (id == 1) game.switchLevel(LevelEnum.Overworld, 3);
        }

        public override void getLoot(int id)
        {
            base.getLoot(id);
            if (id == 1) game.getLoot(Item.skates);
        }

        public override void trigger(int id, bool pressed)
        {
            triggerMask |= 1 << id;
            if (!pressed) triggerMask ^= 1 << id;

            if (triggerMask == 14)
            {
                base.trigger(1, true);
            }
            else
            {
                base.trigger(1, false);
            }
        }
    }
}
