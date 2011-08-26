using com.mojang.escape;
using com.mojang.escape.gui;

namespace com.mojang.escape.menu
{
    public class PauseMenu : Menu
    {
        private string[] options = { "Abort game", "Continue" };
        private int selected = 1;

        public override void render(Bitmap target)
        {
            target.draw(Art.logo, 0, 8, 0, 0, 160, 36, Art.GetColor(0xffffff));
            for (int i = 0; i < options.Length; i++)
            {
                string msg = options[i];
                int col = 0x909090;
                if (selected == i)
                {
                    msg = "-> " + msg;
                    col = 0xffff80;
                }
                target.draw(msg, 40, 60 + i * 10, Art.GetColor(col));
            }
        }

        public override void tick(Game game, bool up, bool down, bool left, bool right, bool use)
        {
            if (up || down) Sound.click2.play();
            if (up) selected--;
            if (down) selected++;
            if (selected < 0) selected = 0;
            if (selected >= options.Length) selected = options.Length - 1;
            if (use)
            {
                Sound.click1.play();
                if (selected == 0)
                {
                    game.setMenu(new TitleMenu());
                }
                if (selected == 1)
                {
                    game.setMenu(null);
                }
            }
        }
    }
}
