using com.mojang.escape;
using System;

namespace com.mojang.escape.entities
{
    public class GhostEntity : EnemyEntity
    {
	    private double rotatePos = 0;

	    public GhostEntity(double x, double z)
        :
        base(x, z, 4 * 8 + 6, Art.GetColor(0xffffff))
        {
		    this.x = x;
		    this.z = z;
		    health = 4;
		    r = 0.3;

		    flying = true;
	    }

        public override void tick()
        {
		    animTime++;
		    sprite.tex = defaultTex + animTime / 10 % 2;

		    double xd = (level.player.x + Math.Sin(rotatePos)) - x;
		    double zd = (level.player.z + Math.Cos(rotatePos)) - z;
		    double dd = xd * xd + zd * zd;

		    if (dd < 4 * 4) {
			    if (dd < 1) {
				    rotatePos += 0.04;
			    } else {
				    rotatePos = level.player.rot;
				    xa += (random.NextDouble() - 0.5) * 0.02;
				    za += (random.NextDouble() - 0.5) * 0.02;
			    }
    			
			    dd = Math.Sqrt(dd);

			    xd /= dd;
			    zd /= dd;

			    xa += xd * 0.004;
			    za += zd * 0.004;
		    }

		    move();

		    xa *= 0.9;
		    za *= 0.9;
	    }

        protected override void hurt(double xd, double zd)
        {
	    }

        protected override void move()
        {
		    x += xa;
		    z += za;
	    }
    }
}
