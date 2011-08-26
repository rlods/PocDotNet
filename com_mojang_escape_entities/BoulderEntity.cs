using com.mojang.escape;
using com.mojang.escape.gui;
using System;

namespace com.mojang.escape.entities
{
    public class BoulderEntity : Entity
    {
	    public static readonly int COLOR = Art.GetColor(0xAFA293);
	    private Sprite sprite;
	    private double rollDist = 0;

	    public BoulderEntity(int x, int z) {
		    this.x = x;
		    this.z = z;
            this.sprite = new Sprite(0, 0, 0, 16, COLOR);
            this.sprites.Add(sprite);
	    }

        public override void tick()
        {
		    rollDist += Math.Sqrt(xa * xa + za * za);
		    sprite.tex = 8 + ((int) (rollDist * 4) & 1);
		    double xao = xa;
		    double zao = za;
		    move();
		    if (xa == 0 && xao != 0) xa = -xao * 0.3;
		    if (za == 0 && zao != 0) za = -zao * 0.3;
		    xa *= 0.98;
		    za *= 0.98;
		    if (xa * xa + za * za < 0.0001) {
			    xa = za = 0;
		    }
	    }

	    public override bool use(Entity source, Item item) {
		    if (item != Item.powerGlove) return false;
		    Sound.roll.play();

		    xa += Math.Sin(source.rot) * 0.1;
		    za += Math.Cos(source.rot) * 0.1;
		    return true;
	    }
    }
}
