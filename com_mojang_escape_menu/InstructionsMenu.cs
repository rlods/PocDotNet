using com.mojang.escape;
using com.mojang.escape.gui;

namespace com.mojang.escape.menu
{
    public class InstructionsMenu : Menu
    {
        private int tickDelay = 30;

        public override void render(Bitmap target)
        {
            target.fill(0, 0, 160, 120, 0);

            target.draw("Instructions", 40, 8, Art.GetColor(0xffffff));

            string[] lines = {
				"Use W,A,S,D to move, and",
				"the arrow keys to turn.",
				"",
				"The 1-8 keys select",
				"items from the inventory",
				"",
				"Space uses items",
		};

            for (int i = 0; i < lines.Length; i++)
            {
                target.draw(lines[i], 4, 32 + i * 8, Art.GetColor(0xa0a0a0));
            }

            if (tickDelay == 0) target.draw("-> Continue", 40, target.height - 16, Art.GetColor(0xffff80));
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
