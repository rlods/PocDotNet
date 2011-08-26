using com.mojang.escape.entities;

namespace com.mojang.escape.level
{
    public class IceLevel : Level
    {
        public IceLevel()
        {
            floorCol = 0xB8DBE0;
            ceilCol = 0xB8DBE0;
            wallCol = 0x6BE8FF;
            name = "The Frost Cave";
        }

        public override void switchLevel(int id)
        {
            if (id == 1) game.switchLevel(LevelEnum.Overworld, 5);
        }

        public override void getLoot(int id)
        {
            base.getLoot(id);
            if (id == 1) game.getLoot(Item.skates);
        }
    }
}
