using com.mojang.escape;
using com.mojang.escape.gui;

namespace com.mojang.escape.menu
{
    public class Menu
    {
        public virtual void render(Bitmap target)
        {
        }

        public virtual void tick(Game game, bool up, bool down, bool left, bool right, bool use)
        {
        }
    }
}
