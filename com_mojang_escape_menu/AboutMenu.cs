using com.mojang.escape;
using com.mojang.escape.gui;

namespace com.mojang.escape.menu
{
    public class AboutMenu : Menu
    {
        private int tickDelay = 30;

        public override void render(Bitmap target)
        {
            target.fill(0, 0, 160, 120, 0);

            target.draw("About", 60, 8, Art.GetColor(0xffffff));

            string[] lines = {
				"Prelude of the Chambered",
				"by Markus Persson.",
				"Made Aug 2011 for the",
				"21'st Ludum Dare compo.",
				"",
				"This game is freeware,",
				"and was made from scratch",
				"in just 48 hours.",
				".NET port by Romain Lods"
            };

            for (int i = 0; i < lines.Length; i++)
            {
                target.draw(lines[i], 4, 28 + i * 8, Art.GetColor(0xa0a0a0));
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
