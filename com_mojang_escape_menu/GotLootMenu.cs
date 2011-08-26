using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;

namespace com.mojang.escape.menu
{
    public class GotLootMenu : Menu
    {
        private int tickDelay = 30;
        private Item item;

        public GotLootMenu(Item item)
        {
            this.item = item;
        }

        public override void render(Bitmap target)
        {
            string str = "You found the " + item.name + "!";
            target.scaleDraw(Art.items, 3, target.width / 2 - 8 * 3, 2, item.icon * 16, 0, 16, 16, Art.GetColor(item.color));
            target.draw(str, (target.width - str.Length * 6) / 2 + 2, 60 - 10, Art.GetColor(0xffff80));

            str = item.description;
            target.draw(str, (target.width - str.Length * 6) / 2 + 2, 60, Art.GetColor(0xa0a0a0));

            if (tickDelay == 0) target.draw("-> Continue", 40, target.height - 40, Art.GetColor(0xffff80));
        }

        public override void tick(Game game, bool up, bool down, bool left, bool right, bool use)
        {
            if (tickDelay > 0) tickDelay--;
            else if (use)
            {
                game.setMenu(null);
            }
        }
    }
}
