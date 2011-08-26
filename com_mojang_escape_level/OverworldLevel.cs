using com.mojang.escape.entities;

namespace com.mojang.escape.level
{
    public class OverworldLevel : Level
    {
        public OverworldLevel()
        {
            ceilTex = -1;
            floorCol = 0x508253;
            floorTex = 8 * 3;
            wallCol = 0xa0a0a0;
            name = "The Island";
        }

        public override void switchLevel(int id)
        {
            if (id == 1) game.switchLevel(LevelEnum.Start, 1);
            if (id == 2) game.switchLevel(LevelEnum.Crypt, 1);
            if (id == 3) game.switchLevel(LevelEnum.Temple, 1);
            if (id == 5) game.switchLevel(LevelEnum.Ice, 1);
        }

        public override void getLoot(int id)
        {
            base.getLoot(id);
            if (id == 1) game.getLoot(Item.cutters);
        }
    }
}
