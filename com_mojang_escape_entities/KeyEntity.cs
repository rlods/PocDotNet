using com.mojang.escape;
using com.mojang.escape.gui;

namespace com.mojang.escape.entities
{
    public class KeyEntity : Entity
    {
        public static readonly int COLOR = Art.GetColor(0x00ffff);
	    private Sprite sprite;
	    private double y, ya;

	    public KeyEntity(double x, double z) {
		    this.x = x;
		    this.z = z;
		    y = 0.5;
		    ya = 0.025;
		    sprite = new Sprite(0, 0, 0, 16 + 3, COLOR);
		    sprites.Add(sprite);
	    }

        public override void tick()
        {
		    move();
		    y += ya;
		    if (y < 0) y = 0;
		    ya -= 0.005;
		    sprite.y = y;
	    }

	    protected override void collide(Entity entity) {
            Player player = entity as Player;
            if (null != player)
            {
			    Sound.key.play();
                player.keys++;
			    remove();
		    }
	    }
    }
}
