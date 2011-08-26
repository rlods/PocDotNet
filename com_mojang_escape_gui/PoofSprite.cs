namespace com.mojang.escape.gui
{
    public class PoofSprite : Sprite
    {
        int life = 20;

        public PoofSprite(double x, double y, double z)
            :
            base(x, y, z, 5, 0x222222)
        {
        }

        public override void tick()
        {
            if (life-- <= 0) removed = true;

        }
    }
}
