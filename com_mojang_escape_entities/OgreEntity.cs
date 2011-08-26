using com.mojang.escape;
using System;

namespace com.mojang.escape.entities
{
    public class OgreEntity : EnemyEntity
    {
	    private int shootDelay;

	    public OgreEntity(double x, double z)
            :
            base(x, z, 4 * 8 + 2, Art.GetColor(0x82A821))
        {
		    this.x = x;
		    this.z = z;
		    health = 6;
		    r = 0.4;
		    spinSpeed = 0.05;
	    }

	    protected override void hurt(double xd, double zd)
        {
		    base.hurt(xd, zd);
		    shootDelay = 50;
	    }

        public override void tick()
        {
            base.tick();
		    if (shootDelay > 0) shootDelay--;
		    else if (random.Next(40) == 0) {
			    shootDelay = 40;
			    level.addEntity(new Bullet(this, x, z, Math.Atan2(level.player.x - x, level.player.z - z), 0.3, 1, defaultColor));
		    }
	    }
    }
}
