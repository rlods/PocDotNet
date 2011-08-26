using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.gui;

namespace com.mojang.escape.menu
{
    public class WinMenu : Menu
    {
        private int tickDelay = 30;

        private Player player;

        public WinMenu(Player player)
        {
            this.player = player;
        }

        public override void render(Bitmap target)
        {
            target.draw(Art.logo, 0, 10, 0, 65, 160, 23, Art.GetColor(0xffffff));

            int seconds = player.time / 60;
            int minutes = seconds / 60;
            seconds %= 60;
            string timeString = minutes + ":";
            if (seconds < 10) timeString += "0";
            timeString += seconds;
            target.draw("Trinkets: " + player.loot + "/12", 40, 45 + 10 * 0, Art.GetColor(0x909090));
            target.draw("Time: " + timeString, 40, 45 + 10 * 1, Art.GetColor(0x909090));

            if (tickDelay == 0) target.draw("-> Continue", 40, target.height - 40, Art.GetColor(0xffff80));
        }

        public override void tick(Game game, bool up, bool down, bool left, bool right, bool use)
        {
            if (tickDelay > 0) tickDelay--;
            else if (use)
            {
                Sound.click1.play();
                game.setMenu(new TitleMenu());
            }
        }
    }
}
