using com.mojang.escape.level.block;

namespace com.mojang.escape.level
{
    public class StartLevel : Level
    {
        public StartLevel()
        {
            name = "The Prison";
        }

        public override void switchLevel(int id)
        {
            if (id == 1) game.switchLevel(LevelEnum.Overworld, 1);
            if (id == 2) game.switchLevel(LevelEnum.Dungeon, 1);
        }

        public override void getLoot(int id)
        {
            base.getLoot(id);
        }
    }
}
