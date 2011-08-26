using com.mojang.escape;
using com.mojang.escape.gui;

namespace com.mojang.escape.menu
{
    public class TitleMenu : Menu
    {
        private string[] options = { "New game", "Instructions", "About" };
        private int selected = 0;
        private bool firstTick = true;

        public override void render(Bitmap target)
        {
            target.fill(0, 0, 160, 120, 0);
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
            target.draw("Copyright (C) 2011 Mojang", 1 + 4, 120 - 9, Art.GetColor(0x303030));
        }

        public override void tick(Game game, bool up, bool down, bool left, bool right, bool use)
        {
            if (firstTick)
            {
                firstTick = false;
                Sound.altar.play();
            }
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
                    game.setMenu(null);
                    game.newGame();
                }
                if (selected == 1)
                {
                    game.setMenu(new InstructionsMenu());
                }
                if (selected == 2)
                {
                    game.setMenu(new AboutMenu());
                }
            }
        }
    }
}
