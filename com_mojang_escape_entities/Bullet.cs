using com.mojang.escape;
using com.mojang.escape.gui;
using System;

namespace com.mojang.escape.entities
{
    public class Bullet : Entity
    {
        public Entity owner;

	    public Bullet(Entity owner, double x, double z, double rot, double pow, int sprite, int col) {
		    this.r = 0.01;
		    this.owner = owner;

		    xa = Math.Sin(rot) * 0.2 * pow;
		    za = Math.Cos(rot) * 0.2 * pow;
		    this.x = x - za / 2;
		    this.z = z + xa / 2;

            this.sprites.Add(new Sprite(0, 0, 0, 8 * 3 + sprite, Art.GetColor(col)));

		    flying = true;
	    }

        public override void tick()
        {
		    double xao = xa;
		    double zao = za;
		    move();

		    if ((xa == 0 && za == 0) || xa != xao || za != zao) {
			    remove();
		    }
	    }

	    public override bool blocks(Entity entity, double x2, double z2, double r2) {
		    if (entity is Bullet) {
			    return false;
		    }
		    if (entity == owner) return false;
    		
		    return base.blocks(entity, x2, z2, r2);
	    }

        protected override void collide(Entity entity)
        {
	    }
    }
}
