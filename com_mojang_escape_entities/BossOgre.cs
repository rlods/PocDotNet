using com.mojang.escape;
using System;

namespace com.mojang.escape.entities
{
    public class BossOgre : EnemyEntity
    {
	    private int shootDelay;
	    private int shootPhase;

	    public BossOgre(double x, double z)
            :
            base(x, z, 4 * 8 + 2, Art.GetColor(0xffff00))
        {
		    this.x = x;
		    this.z = z;
		    health = 10;
		    r = 0.4;
		    spinSpeed = 0.05;
	    }

        protected override void die()
        {
		    Sound.bosskill.play();
		    level.addEntity(new KeyEntity(x, z));
	    }

        public override void tick()
        {
		    base.tick();
		    if (shootDelay > 0) shootDelay--;
		    else {
			    shootDelay = 5;
			    int salva = 10;

			    for (int i = 0; i < 4; i++) {
				    double rot = Math.PI / 2 * (i + shootPhase / salva % 2 * 0.5);
				    level.addEntity(new Bullet(this, x, z, rot, 0.4, 1, defaultColor));
			    }

			    shootPhase++;
			    if (shootPhase % salva == 0) shootDelay = 40;
		    }

	    }
    }
}
