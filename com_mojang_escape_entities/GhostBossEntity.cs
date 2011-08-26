using com.mojang.escape;
using System;

namespace com.mojang.escape.entities
{
    public class GhostBossEntity : EnemyEntity
    {
	    private double rotatePos = 0;
	    private int shootDelay = 0;

	    public GhostBossEntity(double x, double z)
        :
        base(x, z, 4 * 8 + 6, Art.GetColor(0xffff00))
        {
		    this.x = x;
		    this.z = z;
		    health = 10;
		    flying = true;
	    }

        public override void tick()
        {
		    animTime++;
		    sprite.tex = defaultTex + animTime / 10 % 2;

		    double xd = (level.player.x + Math.Sin(rotatePos) * 2) - x;
		    double zd = (level.player.z + Math.Cos(rotatePos) * 2) - z;
		    double dd = xd * xd + zd * zd;

		    if (dd < 1) {
			    rotatePos += 0.04;
		    } else {
			    rotatePos = level.player.rot;
		    }
		    if (dd < 4 * 4) {
			    dd = Math.Sqrt(dd);

			    xd /= dd;
			    zd /= dd;

			    xa += xd * 0.006;
			    za += zd * 0.006;
    			
			    if (shootDelay > 0) shootDelay--;
			    else if (random.Next(10) == 0) {
				    shootDelay = 10;
				    level.addEntity(new Bullet(this, x, z, Math.Atan2(level.player.x - x, level.player.z - z), 0.20, 1, defaultColor));
			    }

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
