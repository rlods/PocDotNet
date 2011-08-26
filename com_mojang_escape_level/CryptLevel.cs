using com.mojang.escape.entities;

namespace com.mojang.escape.level
{
    public class CryptLevel : Level
    {
        public CryptLevel()
        {
            floorCol = 0x404040;
            ceilCol = 0x404040;
            wallCol = 0x404040;
            name = "The Crypt";
        }

        public override void switchLevel(int id)
        {
            if (id == 1) game.switchLevel(LevelEnum.Overworld, 2);
        }

        public override void getLoot(int id)
        {
            base.getLoot(id);
            if (id == 1) game.getLoot(Item.flippers);
        }
    }
}
