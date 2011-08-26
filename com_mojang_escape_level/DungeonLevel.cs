using com.mojang.escape;
using com.mojang.escape.entities;

namespace com.mojang.escape.level
{
    public class DungeonLevel : Level
    {
        public DungeonLevel()
        {
            wallCol = 0xC64954;
            floorCol = 0x8E4A51;
            ceilCol = 0x8E4A51;
            name = "The Dungeons";
        }

        public override void init(Game game, int w, int h, int[] pixels)
        {
            base.init(game, w, h, pixels);
            base.trigger(6, true);
            base.trigger(7, true);
        }

        public override void switchLevel(int id)
        {
            if (id == 1) game.switchLevel(LevelEnum.Start, 2);
        }

        public override void getLoot(int id)
        {
            base.getLoot(id);
            if (id == 1) game.getLoot(Item.powerGlove);
        }

        public override void trigger(int id, bool pressed)
        {
            base.trigger(id, pressed);
            if (id == 5) base.trigger(6, !pressed);
            if (id == 4) base.trigger(7, !pressed);
        }
    }
}
